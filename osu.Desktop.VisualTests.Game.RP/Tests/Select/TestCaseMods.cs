﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

namespace osu.Desktop.VisualTests.Ruleset.RP.Tests.Select
{
    /// <summary>
    /// switch to different mods for each gameMode
    /// </summary>
    internal class TestCaseMods : CategoryTestCase
    {
        public override string Description => @"Mod select overlay and in-game display";

        public override string Category => TestCaseCategory.Select.ToString();

        public override string TestName => @"Mods";

        private ModSelectOverlay modSelect;
        private ModDisplay modDisplay;

        private RulesetDatabase rulesets;


        [BackgroundDependencyLoader]
        private void load(RulesetDatabase rulesets)
        {
            this.rulesets = rulesets;
        }

        public override void Reset()
        {
            base.Reset();

            Add(modSelect = new ModSelectOverlay
            {
                RelativeSizeAxes = Axes.X,
                Origin = Anchor.BottomCentre,
                Anchor = Anchor.BottomCentre,
            });

            Add(modDisplay = new ModDisplay
            {
                Anchor = Anchor.TopRight,
                Origin = Anchor.TopRight,
                AutoSizeAxes = Axes.Both,
                Position = new Vector2(0, 25),
            });

            modDisplay.Current.BindTo(modSelect.SelectedMods);

            AddStep("Toggle", modSelect.ToggleVisibility);

            foreach (var ruleset in rulesets.AllRulesets)
                AddStep(ruleset.CreateInstance().Description, () => modSelect.Ruleset.Value = ruleset);
        }
    }
}
