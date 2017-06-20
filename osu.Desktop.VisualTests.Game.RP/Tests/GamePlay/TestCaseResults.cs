﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;

namespace osu.Desktop.VisualTests.Ruleset.RP.Tests.GamePlay
{
    /// <summary>
    /// show the gameMode's result
    /// </summary>
    internal class TestCaseResults : CategoryTestCase
    {
        private BeatmapDatabase db;

        public override string Description => @"Results after playing.";

        public override string Category => TestCaseCategory.GamePlay.ToString();

        public override string TestName => @"Show GamePlay Result";


        [BackgroundDependencyLoader]
        private void load(BeatmapDatabase db)
        {
            this.db = db;
        }

        private WorkingBeatmap beatmap;

        public override void Reset()
        {
            base.Reset();

            if (beatmap == null)
            {
                var beatmapInfo = db.Query<BeatmapInfo>().FirstOrDefault(b => b.RulesetID == 0);
                if (beatmapInfo != null)
                    beatmap = db.GetWorkingBeatmap(beatmapInfo);
            }

            base.Reset();

            Add(new Results(new Score
            {
                TotalScore = 2845370,
                Accuracy = 0.98,
                MaxCombo = 123,
                Rank = ScoreRank.A,
                Date = DateTimeOffset.Now,
                Statistics = new Dictionary<string, dynamic>()
                {
                    { "300", 50 },
                    { "100", 20 },
                    { "50", 50 },
                    { "x", 1 }
                },
                User = new User
                {
                    Username = "peppy",
                }
            })
            {
                Beatmap = beatmap
            });
        }
    }
}
