// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

namespace osu.Desktop.VisualTests.Ruleset.RP.Tests.HomePage
{
    internal class TestCaseUserPanel : CategoryTestCase
    {
        public override string Description => @"Panels for displaying a user's status";

        public override string Category => TestCaseCategory.HomePage.ToString();

        public override string TestName => @"User Panel";


        public override void Reset()
        {
            base.Reset();

            UserPanel flyte;
            UserPanel peppy;
            Add(new FillFlowContainer
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                AutoSizeAxes = Axes.Both,
                Spacing = new Vector2(10f),
                Children = new[]
                {
                    flyte = new UserPanel(new User
                    {
                        Username = @"flyte",
                        Id = 3103765,
                        Country = new Country { FlagName = @"JP" },
                        CoverUrl = @"https://osu.ppy.sh/images/headers/profile-covers/c6.jpg"
                    }) { Width = 300 },
                    peppy = new UserPanel(new User
                    {
                        Username = @"peppy",
                        Id = 2,
                        Country = new Country { FlagName = @"AU" },
                        CoverUrl = @"https://osu.ppy.sh/images/headers/profile-covers/c3.jpg"
                    }) { Width = 300 },
                },
            });

            flyte.Status.Value = new UserStatusOnline();
            peppy.Status.Value = new UserStatusSoloGame();

            AddStep(@"spectating", () => { flyte.Status.Value = new UserStatusSpectating(); });
            AddStep(@"multiplaying", () => { flyte.Status.Value = new UserStatusMultiplayerGame(); });
            AddStep(@"modding", () => { flyte.Status.Value = new UserStatusModding(); });
            AddStep(@"offline", () => { flyte.Status.Value = new UserStatusOffline(); });
            AddStep(@"null status", () => { flyte.Status.Value = null; });
        }
    }
}
