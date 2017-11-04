﻿using osu.Framework.Audio;
using osu.Framework.Timing;
using osu.Game.Rulesets.Karaoke.UI.Interface;
using osu.Game.Rulesets.Objects.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.UI.Extension
{
    public static class KaraokeFieldExtension
    {
        public static void NavigationToFirst(this IAmKaraokeField karaokeField)
        {

        }

        public static void NavigationToPrevious(this IAmKaraokeField karaokeField)
        {

        }

        public static void NavigationToNext(this IAmKaraokeField karaokeField)
        {

        }

        public static void Play(this IAmKaraokeField karaokeField)
        {
            //karaokeField.WorkingBeatmap.Track.Start();
        }

        public static void Pause(this IAmKaraokeField karaokeField)
        {
            //Play and pause are the same
            //karaokeField.WorkingBeatmap.Track.Stop();
        }

        public static void NavigateToTime(this IAmKaraokeField karaokeField, double value)
        {
            karaokeField.WorkingBeatmap.Track.Seek(value);
        }

        public static void AdjustSpeed(this IAmKaraokeField karaokeField, double value)
        {
            //TODO : 修正因為Slider 拉動太快造成歌曲重來
            //refrence : IAdjustableClock.cs
            //karaokeField.WorkingBeatmap.Track.Reset();
            karaokeField.WorkingBeatmap.Track.Rate = value;
        }

        public static void AdjustTone(this IAmKaraokeField karaokeField, double value)
        {
            if (karaokeField.WorkingBeatmap.Track is IHasPitchAdjust pitchAdjustTrack)
            {
                //karaokeField.WorkingBeatmap.Track.Reset();
                pitchAdjustTrack.PitchAdjust = value;
            }

        }

        public static void AdjustlyricsOffset(this IAmKaraokeField karaokeField, double value)
        {

        }

        /// <summary>
        /// first Object's time
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <returns></returns>
        public static double FirstObjectTime(this IAmKaraokeField karaokeField)
        {
            //RulesetContainer.Objects;
            //Refrenca : SongProgress.cs
            return karaokeField.WorkingBeatmap.Beatmap.HitObjects.First().StartTime;
        }

        /// <summary>
        /// Last object's time
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <returns></returns>
        public static double LastObjectTime(this IAmKaraokeField karaokeField)
        {
            var hitObjects = karaokeField.WorkingBeatmap.Beatmap.HitObjects;
            return ((hitObjects.Last() as IHasEndTime)?.EndTime ?? hitObjects.Last().StartTime) + 1;
        }

        public static double TotalTime(this IAmKaraokeField karaokeField)
        {
            return LastObjectTime(karaokeField) - FirstObjectTime(karaokeField);
        }

        /// <summary>
        /// use to get the current time
        /// </summary>
        /// <returns></returns>
        public static double GetBeatmapFrameClock(this IAmKaraokeField karaokeField)
        {
            return karaokeField.WorkingBeatmap.Track.CurrentTime;
        }
    }
}
