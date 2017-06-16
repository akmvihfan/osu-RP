﻿using osu.Framework.Configuration;
using osu.Framework.Platform;

namespace osu.Desktop.VisualTests.Ruleset.RP
{
    public class TestBrowserConfig : ConfigManager<TestBrowserSetting>
    {
        protected override string Filename => @"visualtests.cfg";

        public TestBrowserConfig(Storage storage) : base(storage)
        {
        }
    }

    public enum TestBrowserSetting
    {
        LastTest
    }
}
