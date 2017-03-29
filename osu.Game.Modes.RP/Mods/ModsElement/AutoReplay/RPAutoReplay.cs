﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using System.Diagnostics;
using osu.Framework.Graphics.Transforms;
using osu.Framework.MathUtils;
using osu.Game.Beatmaps;
using osu.Game.Modes.RP.Objects;
using osu.Game.Modes.RP.Objects.Drawables;
using OpenTK;

namespace osu.Game.Modes.RP.Mods.ModsElement.AutoReplay
{
    public class RpAutoReplay : LegacyReplay
    {
        private static readonly Vector2 spinner_centre = new Vector2(256, 192);

        private const float spin_radius = 50;

        private readonly Beatmap<BaseRpObject> beatmap;

        private static readonly IComparer<LegacyReplayFrame> replayFrameComparer = new LegacyReplayFrameComparer();

        public RpAutoReplay(Beatmap<BaseRpObject> beatmap)
        {
            this.beatmap = beatmap;

            createAutoReplay();
        }

        private static Vector2 circlePosition(double t, double radius) => new Vector2((float)(Math.Cos(t) * radius), (float)(Math.Sin(t) * radius));

        private int findInsertionIndex(LegacyReplayFrame frame)
        {
            var index = Frames.BinarySearch(frame, replayFrameComparer);

            if (index < 0)
                index = ~index;
            else
                while (index < Frames.Count && frame.Time == Frames[index].Time)
                    ++index;

            return index;
        }

        private void addFrameToReplay(LegacyReplayFrame frame) => Frames.Insert(findInsertionIndex(frame), frame);

        private double applyModsToTime(double v) => v;
        private double applyModsToRate(double v) => v;

        private void createAutoReplay()
        {
            var buttonIndex = 0;

            var delayedMovements = false; // ModManager.CheckActive(Mods.Relax2);
            var preferredEasing = delayedMovements ? EasingTypes.InOutCubic : EasingTypes.Out;

            addFrameToReplay(new LegacyReplayFrame(-100000, 256, 500, LegacyButtonState.None));
            addFrameToReplay(new LegacyReplayFrame(beatmap.HitObjects[0].StartTime - 1500, 256, 500, LegacyButtonState.None));
            addFrameToReplay(new LegacyReplayFrame(beatmap.HitObjects[0].StartTime - 1000, 256, 192, LegacyButtonState.None));

            // We are using ApplyModsToRate and not ApplyModsToTime to counteract the speed up / slow down from HalfTime / DoubleTime so that we remain at a constant framerate of 60 fps.
            var frameDelay = (float)applyModsToRate(1000.0 / 60.0);

            // Already superhuman, but still somewhat realistic
            var reactionTime = (int)applyModsToRate(100);


            for (var i = 0; i < beatmap.HitObjects.Count; i++)
                if (beatmap.HitObjects[i] is BaseHitObject)
                {
                    var h = beatmap.HitObjects[i] as BaseHitObject;

                    //if (h.EndTime < InputManager.ReplayStartTime)
                    //{
                    //    h.IsHit = true;
                    //    continue;
                    //}

                    var endDelay = h is RpLongTailObject ? 1 : 0;


                    if (delayedMovements && i > 0)
                    {
                        var last = beatmap.HitObjects[i - 1];

                        //Make the cursor stay at a hitObject as long as possible (mainly for autopilot).
                        if (h.StartTime - h.HitWindowFor(RPScoreResult.Sad) > last.EndTime + h.HitWindowFor(RPScoreResult.Safe) + 50)
                        {
                            if (!(last is RpLongTailObject) && h.StartTime - last.EndTime < 1000)
                                addFrameToReplay(new LegacyReplayFrame(last.EndTime + h.HitWindowFor(RPScoreResult.Safe), last.EndPosition.X, last.EndPosition.Y, LegacyButtonState.None));
                            if (!(h is RpLongTailObject)) addFrameToReplay(new LegacyReplayFrame(h.StartTime - h.HitWindowFor(RPScoreResult.Sad), h.Position.X, h.Position.Y, LegacyButtonState.None));
                        }
                        else if (h.StartTime - h.HitWindowFor(RPScoreResult.Safe) > last.EndTime + h.HitWindowFor(RPScoreResult.Safe) + 50)
                        {
                            if (!(last is RpLongTailObject) && h.StartTime - last.EndTime < 1000)
                                addFrameToReplay(new LegacyReplayFrame(last.EndTime + h.HitWindowFor(RPScoreResult.Safe), last.EndPosition.X, last.EndPosition.Y, LegacyButtonState.None));
                            if (!(h is RpLongTailObject)) addFrameToReplay(new LegacyReplayFrame(h.StartTime - h.HitWindowFor(RPScoreResult.Safe), h.Position.X, h.Position.Y, LegacyButtonState.None));
                        }
                        else if (h.StartTime - h.HitWindowFor(RPScoreResult.Fine) > last.EndTime + h.HitWindowFor(RPScoreResult.Fine) + 50)
                        {
                            if (!(last is RpLongTailObject) && h.StartTime - last.EndTime < 1000)
                                addFrameToReplay(new LegacyReplayFrame(last.EndTime + h.HitWindowFor(RPScoreResult.Fine), last.EndPosition.X, last.EndPosition.Y, LegacyButtonState.None));
                            if (!(h is RpLongTailObject)) addFrameToReplay(new LegacyReplayFrame(h.StartTime - h.HitWindowFor(RPScoreResult.Fine), h.Position.X, h.Position.Y, LegacyButtonState.None));
                        }
                    }


                    var targetPosition = h.Position;
                    var easing = preferredEasing;
                    float spinnerDirection = -1;

                    if (h is RpLongTailObject)
                    {
                        targetPosition.X = Frames[Frames.Count - 1].MouseX;
                        targetPosition.Y = Frames[Frames.Count - 1].MouseY;

                        var difference = spinner_centre - targetPosition;

                        var differenceLength = difference.Length;
                        var newLength = (float)Math.Sqrt(differenceLength * differenceLength - spin_radius * spin_radius);

                        if (differenceLength > spin_radius)
                        {
                            var angle = (float)Math.Asin(spin_radius / differenceLength);

                            if (angle > 0)
                                spinnerDirection = -1;
                            else
                                spinnerDirection = 1;

                            difference.X = difference.X * (float)Math.Cos(angle) - difference.Y * (float)Math.Sin(angle);
                            difference.Y = difference.X * (float)Math.Sin(angle) + difference.Y * (float)Math.Cos(angle);

                            difference.Normalize();
                            difference *= newLength;

                            targetPosition += difference;

                            easing = EasingTypes.In;
                        }
                        else if (difference.Length > 0)
                        {
                            targetPosition = spinner_centre - difference * (spin_radius / difference.Length);
                        }
                        else
                        {
                            targetPosition = spinner_centre + new Vector2(0, -spin_radius);
                        }
                    }


                    // Do some nice easing for cursor movements
                    if (Frames.Count > 0)
                    {
                        var lastFrame = Frames[Frames.Count - 1];

                        // Wait until Auto could "see and react" to the next note.
                        //double waitTime = h.StartTime - Math.Max(0.0, DrawableRHitObject.TIME_PREEMPT - reactionTime);
                        var waitTime = h.StartTime - Math.Max(0.0, 0);
                        if (waitTime > lastFrame.Time)
                        {
                            lastFrame = new LegacyReplayFrame(waitTime, lastFrame.MouseX, lastFrame.MouseY, lastFrame.ButtonState);
                            addFrameToReplay(lastFrame);
                        }

                        var lastPosition = new Vector2(lastFrame.MouseX, lastFrame.MouseY);

                        var timeDifference = applyModsToTime(h.StartTime - lastFrame.Time);

                        // Only "snap" to hitcircles if they are far enough apart. As the time between hitcircles gets shorter the snapping threshold goes up.
                        if (timeDifference > 0 && // Sanity checks

                            //((lastPosition - targetPosition).Length > h.Radius * (1.5 + 100.0 / timeDifference) || // Either the distance is big enough
                            ((lastPosition - targetPosition).Length > 10 * (1.5 + 100.0 / timeDifference) || // Either the distance is big enough
                             timeDifference >= 266)) // ... or the beats are slow enough to tap anyway.
                        {
                            // Perform eased movement
                            for (var time = lastFrame.Time + frameDelay; time < h.StartTime; time += frameDelay)
                            {
                                var currentPosition = Interpolation.ValueAt(time, lastPosition, targetPosition, lastFrame.Time, h.StartTime, easing);
                                addFrameToReplay(new LegacyReplayFrame((int)time, currentPosition.X, currentPosition.Y, lastFrame.ButtonState));
                            }

                            buttonIndex = 0;
                        }
                        else
                        {
                            buttonIndex++;
                        }
                    }

                    var button = buttonIndex % 2 == 0 ? LegacyButtonState.Left1 : LegacyButtonState.Right1;

                    var newFrame = new LegacyReplayFrame(h.StartTime, targetPosition.X, targetPosition.Y, button);
                    var endFrame = new LegacyReplayFrame(h.EndTime + endDelay, h.EndPosition.X, h.EndPosition.Y, LegacyButtonState.None);

                    // Decrement because we want the previous frame, not the next one
                    var index = findInsertionIndex(newFrame) - 1;

                    // Do we have a previous frame? No need to check for < replay.Count since we decremented!
                    if (index >= 0)
                    {
                        var previousFrame = Frames[index];
                        var previousButton = previousFrame.ButtonState;

                        // If a button is already held, then we simply alternate
                        if (previousButton != LegacyButtonState.None)
                        {
                            Debug.Assert(previousButton != (LegacyButtonState.Left1 | LegacyButtonState.Right1));

                            // Force alternation if we have the same button. Otherwise we can just keep the naturally to us assigned button.
                            if (previousButton == button)
                            {
                                button = (LegacyButtonState.Left1 | LegacyButtonState.Right1) & ~button;
                                newFrame.SetButtonStates(button);
                            }

                            // We always follow the most recent slider / spinner, so remove any other frames that occur while it exists.
                            var endIndex = findInsertionIndex(endFrame);

                            if (index < Frames.Count - 1)
                                Frames.RemoveRange(index + 1, Math.Max(0, endIndex - (index + 1)));

                            // After alternating we need to keep holding the other button in the future rather than the previous one.
                            for (var j = index + 1; j < Frames.Count; ++j)
                                if (j < Frames.Count - 1 || Frames[j].ButtonState == previousButton)
                                    Frames[j].SetButtonStates(button);
                        }
                    }

                    addFrameToReplay(newFrame);

                    // We add intermediate frames for spinning / following a slider here.
                    if (h is RpLongTailObject)
                    {
                        var difference = targetPosition - spinner_centre;

                        var radius = difference.Length;
                        var angle = radius == 0 ? 0 : (float)Math.Atan2(difference.Y, difference.X);

                        double t;

                        for (var j = h.StartTime + frameDelay; j < h.EndTime; j += frameDelay)
                        {
                            t = applyModsToTime(j - h.StartTime) * spinnerDirection;

                            var pos = spinner_centre + circlePosition(t / 20 + angle, spin_radius);
                            addFrameToReplay(new LegacyReplayFrame((int)j, pos.X, pos.Y, button));
                        }

                        t = applyModsToTime(h.EndTime - h.StartTime) * spinnerDirection;
                        var endPosition = spinner_centre + circlePosition(t / 20 + angle, spin_radius);

                        addFrameToReplay(new LegacyReplayFrame(h.EndTime, endPosition.X, endPosition.Y, button));

                        endFrame.MouseX = endPosition.X;
                        endFrame.MouseY = endPosition.Y;
                    }
                    else if (h is RpLongTailObject)
                    {
                        var s = h as RpLongTailObject;

                        for (double j = frameDelay; j < s.Duration; j += frameDelay)
                        {
                            var pos = s.Curve.PositionAt(j / s.Duration);
                            addFrameToReplay(new LegacyReplayFrame(h.StartTime + j, pos.X, pos.Y, button));
                        }

                        addFrameToReplay(new LegacyReplayFrame(h.EndTime, s.EndPosition.X, s.EndPosition.Y, button));
                    }

                    // We only want to let go of our button if we are at the end of the current replay. Otherwise something is still going on after us so we need to keep the button pressed!
                    if (Frames[Frames.Count - 1].Time <= endFrame.Time)
                        addFrameToReplay(endFrame);
                }

            //Player.currentScore.Replay = InputManager.ReplayScore.Replay;
            //Player.currentScore.PlayerName = "osu!";
        }

        internal class LegacyReplayFrameComparer : IComparer<LegacyReplayFrame>
        {
            public int Compare(LegacyReplayFrame f1, LegacyReplayFrame f2)
            {
                return f1.Time.CompareTo(f2.Time);
            }
        }
    }
}
