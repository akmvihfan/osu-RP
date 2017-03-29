﻿using System.Collections.Generic;
using osu.Game.Modes.RP.Beatmaps.OsuBeatmap.Parameter;
using osu.Game.Modes.RP.Beatmaps.OtherBeatmap.HitObjectGegenerator.ObjectMovingPath.EndPosition;
using osu.Game.Modes.RP.Beatmaps.OtherBeatmap.HitObjectGegenerator.ObjectMovingPath.PathPosition;
using osu.Game.Modes.RP.Beatmaps.OtherBeatmap.HitObjectGegenerator.ObjectMovingPath.StartPosition;
using osu.Game.Modes.RP.Objects.type;

namespace osu.Game.Modes.RP.Beatmaps.OtherBeatmap.HitObjectGegenerator.ObjectMovingPath
{
    internal class ObjectPositionGenerator
    {
        private readonly StartPositionGenerator _startPositionGenerator = new StartPositionGenerator();

        private readonly EndPositionGenerator _endPositionGenerator = new EndPositionGenerator();

        private readonly RpPathGenerator _rpPathGenerator = new RpPathGenerator();

        /// <summary>
        ///     轉換座標
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal List<HitObjectConvertParameter> Convert(List<HitObjectConvertParameter> input)
        {
            foreach (var single in input)
                ProcessSingleItem(single);
            return input;
        }

        /// <summary>
        ///     轉換單一物件
        /// </summary>
        /// <param name="input"></param>
        private void ProcessSingleItem(HitObjectConvertParameter input)
        {
            //all auto
            if (input.ListConvertedParameter[0].CurveGenerate == RpBaseObjectType.CurveGenerate.Auto)
                _startPositionGenerator.Process(input);

            //auto create stop position
            if (input.ListConvertedParameter[0].CurveGenerate >= RpBaseObjectType.CurveGenerate.Manual_Start_End_Position)
                _endPositionGenerator.Process(input);

            //create path
            if (input.ListConvertedParameter[0].CurveGenerate >= RpBaseObjectType.CurveGenerate.Manual_StartPosition)
                _rpPathGenerator.Process(input);
        }
    }
}
