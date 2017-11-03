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

        private const int oneLayerYPosition = 5;

        private const int twoLayerYPosition = 35;

        private const int objectHeight = 30;

        private Container panelContainer;

        public KaraokePanelOverlay(Playfield playField = null)
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
                                    Text = "00:00",
                                },

                                //switch to first sentence
                                new OsuButton()
                                {
                                     Position=new Vector2(-50,5),
                                     Width=30,
                                     Height=30,
                                     Text="P",
                                     Action=()=>
                                     {
                                         this.ToggleVisibility();
                                     }
                                },

                                //switch to previous sentence
                                new OsuButton()
                                {
                                     Position=new Vector2(-40,5),
                                     Width=30,
                                     Height=30,
                                     Text="P",
                                     //Action=
                                },

                                //switch to next sentence
                                new OsuButton()
                                {
                                     Position=new Vector2(-40,5),
                                     Width=30,
                                     Height=30,
                                     Text="P",
                                     //Action=
                                },

                                //"play" introduce text
                                new KaraokeIntroduceText
                                {
                                    Text = "00:00",
                                },

                                //Play and pause
                                new OsuButton()
                                {
                                     Position=new Vector2(-40,5),
                                     Width=30,
                                     Height=30,
                                     Text="P",
                                     //Action=
                                },

                                //now time
                                new OsuSpriteText
                                {
                                    Text = "00:00",
                                    //Font = @"Venera",
                                    UseFullGlyphHeight = false,
                                    Anchor = Anchor.TopLeft,
                                    Origin = Anchor.TopLeft,
                                    TextSize = 10,
                                    Alpha = 1,
                                    //ShadowColour = _textColor,
                                    //BorderColour = _textColor,
                                },

                                //time slider
                                new KaraokeTimerSliderBar()
                                {
                                    Position=new Vector2(50,10),
                                    Width=300,
                                },

                                //end time
                                new OsuSpriteText
                                {
                                    Text = "00:00",
                                    //Font = @"Venera",
                                    UseFullGlyphHeight = false,
                                    Anchor = Anchor.TopLeft,
                                    Origin = Anchor.TopLeft,
                                    TextSize = 10,
                                    Alpha = 1,
                                    //ShadowColour = _textColor,
                                    //BorderColour = _textColor,
                                },

                                 //"speed" introduce
                                 new KaraokeIntroduceText
                                 {
                                    Text = "00:00",
                                 },

                                //speed
                                new WithUpAndDownButtonSlider()
                                {
                                    Position=new Vector2(50,10),
                                    Width=300,
                                },

                                 //"tone" introduce
                                 new KaraokeIntroduceText
                                 {
                                    Text = "Tone",
                                 },

                                //Tone
                                new WithUpAndDownButtonSlider()
                                {
                                    Position=new Vector2(50,10),
                                    Width=300,
                                },

                                 //"offset" introduce
                                 new KaraokeIntroduceText
                                 {
                                    Text = "Offset",
                                 },

                                //offset
                                new WithUpAndDownButtonSlider()
                                {
                                    Position=new Vector2(50,10),
                                    Width=300,
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
