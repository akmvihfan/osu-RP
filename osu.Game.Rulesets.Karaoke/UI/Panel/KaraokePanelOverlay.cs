﻿using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Configuration;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Game.Graphics;
using osu.Game.Graphics.Backgrounds;
using osu.Game.Graphics.Sprites;
using osu.Game.Graphics.UserInterface;
using osu.Game.Overlays;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Karaoke.UI.Panel.Pieces;
using osu.Game.Rulesets.UI;
using osu.Game.Screens.Play.ReplaySettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Rulesets.Karaoke.UI.Extension;
using osu.Game.Rulesets.Karaoke.UI.Interface;

namespace osu.Game.Rulesets.Karaoke.UI
{
    /// <summary>
    /// to show the Karaoke panel on Playfield 
    /// </summary>
    public class KaraokePanelOverlay : WaveOverlayContainer
    {
        private const int button_duration = 700;
        private const int ranked_multiplier_duration = 700;
        private const float content_width = 0.8f;

        private const int oneLayerYPosition = 30;
        private const int twoLayerYPosition = 75;
        private const int objectHeight = 30;
        private const int startXPositin = -60;


        private Container panelContainer;

        public KaraokePanelOverlay(IAmKaraokeField playField = null)
        {
            FirstWaveColour = OsuColour.FromHex(@"19b0e2").Opacity(50);
            SecondWaveColour = OsuColour.FromHex(@"2280a2").Opacity(50);
            ThirdWaveColour = OsuColour.FromHex(@"005774").Opacity(50);
            FourthWaveColour = OsuColour.FromHex(@"003a4e").Opacity(50);
            //FourthWaveColour = new Color4(0, 0, 0, 0);

            Height = 110;
            Content.RelativeSizeAxes = Axes.X;
            Content.AutoSizeAxes = Axes.Y;
            Children = new Drawable[]
            {
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Masking = true,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Color4.Black.Opacity(0.4f)
                        },
                        //new Triangles
                        //{
                        //    TriangleScale = 5,
                        //    RelativeSizeAxes = Axes.X,
                        //    Height = Height, //set the height from the start to ensure correct triangle density.
                        //    ColourLight = new Color4(53, 66, 82, 150),
                        //    ColourDark = new Color4(41, 54, 70, 150),
                        //},
                    },
                },
                new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Direction = FillDirection.Vertical,
                    Spacing = new Vector2(0f, 10f),
                    Children = new Drawable[]
                    {
                        // Body
                        panelContainer = new Container
                        {
                            Origin = Anchor.TopCentre,
                            Anchor = Anchor.TopCentre,
                            RelativeSizeAxes = Axes.X,
                            Width = content_width,
                            Height=110,
                            Children=new Drawable[]
                            {
                                //"sentence" introduce text
                                new KaraokeIntroduceText
                                {
                                    Position=new Vector2(startXPositin - 35,oneLayerYPosition),
                                    Text = "Sentence",
                                    TooltipText="Choose the sentence you want to sing."
                                },

                                //switch to first sentence
                                new KaraokeButton()
                                {
                                     Position=new Vector2(startXPositin + 40,oneLayerYPosition),
                                     Origin = Anchor.CentreLeft,
                                     Width =objectHeight,
                                     Height=objectHeight,
                                     Text="1",
                                     TooltipText="Move to first sentence",
                                     Action=()=>
                                     {
                                         playField?.NavigationToFirst();
                                     }
                                },

                                //switch to previous sentence
                                new KaraokeButton()
                                {
                                     Position=new Vector2(startXPositin + 80,oneLayerYPosition),
                                     Origin = Anchor.CentreLeft,
                                     Width=objectHeight,
                                     Height=objectHeight,
                                     Text="<-",
                                     TooltipText="Move to previous sentence",
                                     Action=()=>
                                     {
                                         playField?.NavigationToPrevious();
                                     }
                                },

                                //switch to next sentence
                                new KaraokeButton()
                                {
                                     Position=new Vector2(startXPositin + 120, oneLayerYPosition),
                                     Origin = Anchor.CentreLeft,
                                     Width=objectHeight,
                                     Height=objectHeight,
                                     Text="->",
                                     TooltipText="Move to next sentence",
                                     Action=()=>
                                     {
                                         playField?.NavigationToNext();
                                     }
                                },

                                //"play" introduce text
                                new KaraokeIntroduceText
                                {
                                    Position=new Vector2(startXPositin + 160, oneLayerYPosition),
                                    Text = "Play",
                                    TooltipText="Pause,play the song and adjust time"
                                },

                                //Play and pause
                                new KaraokeButton()
                                {
                                     Position=new Vector2(startXPositin + 200, oneLayerYPosition),
                                     Origin = Anchor.CentreLeft,
                                     Width=objectHeight,
                                     Height=objectHeight,
                                     Text="P",
                                     TooltipText="Play",
                                     Action=()=>
                                     {
                                         //TODO : 
                                         playField?.Play();
                                     }
                                },

                                //now time
                                new OsuSpriteText
                                {
                                    Position=new Vector2(startXPositin + 240, oneLayerYPosition),
                                    Text = "00:00",
                                    UseFullGlyphHeight = false,
                                    Origin = Anchor.CentreLeft,
                                    Anchor = Anchor.TopLeft,
                                    TextSize = 10,
                                    Alpha = 1,
                                    //ShadowColour = _textColor,
                                    //BorderColour = _textColor,
                                },

                                //time slider
                                new KaraokeTimerSliderBar()
                                {
                                    Position=new Vector2(startXPositin + 280, oneLayerYPosition),
                                    Origin = Anchor.CentreLeft,
                                    Width=300,
                                    OnValueChanged = (eaa,newValue)=>
                                    {
                                        playField?.NavigateToTime(newValue);
                                    },
                                },

                                //end time
                                new OsuSpriteText
                                {
                                    Position=new Vector2(startXPositin + 600, oneLayerYPosition),
                                    Text = "03:20",
                                    UseFullGlyphHeight = false,
                                    Anchor = Anchor.TopLeft,
                                    Origin = Anchor.CentreLeft,
                                    TextSize = 10,
                                    Alpha = 1,
                                    //ShadowColour = _textColor,
                                    //BorderColour = _textColor,
                                },

                                 //"speed" introduce
                                 new KaraokeIntroduceText
                                 {
                                     Position=new Vector2(startXPositin - 30, twoLayerYPosition),
                                     Text = "Speed",
                                     TooltipText="Adjust song speed."
                                 },

                                //speed
                                new WithUpAndDownButtonSlider()
                                {
                                    Position=new Vector2(startXPositin + 20, twoLayerYPosition),
                                    Origin = Anchor.CentreLeft,
                                    Width=200,
                                     OnValueChanged = (eaa,newValue)=>
                                     {
                                         playField?.AdjustSpeed(newValue);
                                     },
                                },

                                 //"tone" introduce
                                 new KaraokeIntroduceText
                                 {
                                     Position=new Vector2(startXPositin + 235, twoLayerYPosition),
                                     Text = "Tone",
                                     TooltipText="Adjust song tone."
                                 },

                                //Tone
                                new WithUpAndDownButtonSlider()
                                {
                                    Position=new Vector2(startXPositin + 280, twoLayerYPosition),
                                    Origin = Anchor.CentreLeft,
                                    Width=200,
                                     OnValueChanged = (eaa,newValue)=>
                                     {
                                         playField?.AdjustTone(newValue);
                                     },
                                },

                                 //"offset" introduce
                                 new KaraokeIntroduceText
                                 {
                                     Position=new Vector2(startXPositin + 495, twoLayerYPosition),
                                     Text = "Offset",
                                     TooltipText="Adjust lyrics appear offset."
                                 },

                                //offset
                                new WithUpAndDownButtonSlider()
                                {
                                    Position=new Vector2(startXPositin + 550, twoLayerYPosition),
                                    Origin = Anchor.CentreLeft,
                                    Width=200,
                                     OnValueChanged = (eaa,newValue)=>
                                     {
                                         playField?.AdjustlyricsOffset(newValue);
                                     },
                                },
                            },
                        },
                    },
                },
            };
        }

        public static int ObjectHeight => ObjectHeight1;

        public static int ObjectHeight1 => objectHeight;
    }
}
