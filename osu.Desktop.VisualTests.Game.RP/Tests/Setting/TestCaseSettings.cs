﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Testing;
using osu.Game.Overlays;

namespace osu.Desktop.VisualTests.Tests
{
    /// <summary>
    /// Just show the left bar setting view
    /// </summary>
    internal class TestCaseSettings : CategoryTestCase
    {
        public override string Description => @"Tests the settings overlay";

        public override string Category => TestCaseCategory.Setting.ToString();

        public override string TestName => @"Settings";

        private SettingsOverlay settings;

        public override void Reset()
        {
            base.Reset();

            Children = new[] { settings = new SettingsOverlay() };
            settings.ToggleVisibility();
        }
    }
}
