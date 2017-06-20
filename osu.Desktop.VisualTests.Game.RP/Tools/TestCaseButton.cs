﻿using osu.Desktop.VisualTests.Ruleset.RP.Tests;

namespace osu.Desktop.VisualTests.Ruleset.RP.Tools
{
    internal class TestCaseButton : ClickableContainer
    {
        private readonly Box box;
        private readonly Container text;

        public readonly CategoryTestCase TestCase;

        public bool Current
        {
            set
            {
                const float transition_duration = 100;

                if (value)
                {
                    box.FadeColour(new Color4(220, 220, 220, 255), transition_duration);
                    text.FadeColour(Color4.Black, transition_duration);
                }
                else
                {
                    box.FadeColour(new Color4(140, 140, 140, 255), transition_duration);
                    text.FadeColour(Color4.White, transition_duration);
                }
            }
        }

        public TestCaseButton(CategoryTestCase test)
        {
            Masking = true;

            TestCase = test;

            CornerRadius = 5;
            RelativeSizeAxes = Axes.X;
            Size = new Vector2(1, 60);

            Add(new Drawable[]
            {
                box = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = new Color4(140, 140, 140, 255),
                    Alpha = 0.7f
                },
                text = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding
                    {
                        Left = 4,
                        Right = 4,
                        Bottom = 2,
                    },
                    Children = new[]
                    {
                        new SpriteText
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Text = TestCase.Name,
                        },
                        new SpriteText
                        {
                            Anchor = Anchor.BottomLeft,
                            Origin = Anchor.BottomLeft,
                            Text = TestCase.Description,
                            TextSize = 15,
                            AutoSizeAxes = Axes.Y,
                            RelativeSizeAxes = Axes.X,
                        }
                    }
                }
            });
        }

        protected override bool OnHover(InputState state)
        {
            box.FadeTo(1, 150);
            return true;
        }

        protected override void OnHoverLost(InputState state)
        {
            box.FadeTo(0.7f, 150);
            base.OnHoverLost(state);
        }
    }
}