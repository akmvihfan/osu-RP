﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE
using System;
using System.Collections.Generic;
using osu.Game.Modes.RP.Beatmaps.OtherBeatmap.HitObjectGegenerator.Parameter;
using osu.Game.Modes.RP.Beatmaps.OtherBeatmap.Parameter;
using osu.Game.Modes.RP.Objects;
using osu.Game.Modes.RP.Objects.type;

namespace osu.Game.Modes.RP.Beatmaps.OtherBeatmap.HitObjectGegenerator.Type
{

    public class TypeCalculator
    {
        //上一組形狀
        private List<RpBaseHitObjectType.Shape> listLastAssignShageShapes = new List<RpBaseHitObjectType.Shape>();


        public void ProcessType(ComvertParameter single)
        {
            int tupleCount = single.HitObjectConvertParameter.ListSingleHitObjectConvertParameter.Count;
            for (int i = 0; i < tupleCount; i++)
            {
                if (single.HitObjectConvertParameter.ListSingleHitObjectConvertParameter[i].isCombo)
                {
                    AssignComboTupleShapes(single.HitObjectConvertParameter.ListSingleHitObjectConvertParameter[i]);//Combo
                }
                else
                {
                    AssignNormalTupleShapes(single.HitObjectConvertParameter.ListSingleHitObjectConvertParameter[i]);//Normal
                }
            }
        }

        //指定一般
        public void AssignNormalTupleShapes(SingleHitObjectConvertParameter singleTuple)
        {
            for (int i = 0; i < singleTuple.ListBaseHitObject.Count; i++)
            {
                singleTuple.ListBaseHitObject[i].Shape = CalRandomShape(singleTuple);
            }
        }

        //指定Combo
        public void AssignComboTupleShapes(SingleHitObjectConvertParameter singleTuple)
        {
            for (int i = 0; i < singleTuple.ListBaseHitObject.Count; i++)
            {
                singleTuple.ListBaseHitObject[i].Shape = RpBaseHitObjectType.Shape.Right;
            }
        }

        /// <summary>
        /// 計算隨機形狀
        /// </summary>
        /// <returns></returns>
        RpBaseHitObjectType.Shape CalRandomShape(SingleHitObjectConvertParameter singleTuple)
        {
            int randNum = CalRandNumber(singleTuple);
            switch (randNum % 4)
            {
                case 0:
                    return RpBaseHitObjectType.Shape.Up;
                case 1:
                    return RpBaseHitObjectType.Shape.Down;
                case 2:
                    return RpBaseHitObjectType.Shape.Left;
                case 3:
                    return RpBaseHitObjectType.Shape.Right;
            }
            return RpBaseHitObjectType.Shape.Down;
        }

        /// <summary>
        /// 產生隨機參數
        /// </summary>
        /// <returns></returns>
        int CalRandNumber(SingleHitObjectConvertParameter singleTuple)
        {
            return singleTuple.MultiNumber+(int)singleTuple.StartTime+singleTuple.ListBaseHitObject.Count;
        }
    }
}