﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

namespace osu.Desktop.VisualTests.Ruleset.RP.Tests.GamePlay
{
    internal class TestCaseSkipButton : CategoryTestCase
    {
        public override string Description => @"Skip skip skippediskip";

        public override string Category => TestCaseCategory.GamePlay.ToString();

        public override string TestName => @"Skip Button";


        public override void Reset()
        {
            base.Reset();
            Add(new SkipButton(Clock.CurrentTime + 5000));
        }
    }
}
