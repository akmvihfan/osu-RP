﻿using System;
using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Modes.RP.Beatmaps.OtherBeatmap.Parameter;
using osu.Game.Modes.RP.Beatmaps.OtherBeatmap.Slicing.DiffStarCalculator;
using osu.Game.Modes.RP.Beatmaps.OtherBeatmap.Slicing.Parameter;
using osu.Game.Modes.RP.Beatmaps.OtherBeatmap.Slicing.TimeSliceCalculator;
using osu.Game.Modes.RP.Beatmaps.OtherBeatmap.Slicing.VolocityCalculator;

namespace osu.Game.Modes.RP.Beatmaps.OtherBeatmap.Slicing
{
    public class SliceProcessor
    {
        private double _diffStar = 0;

        private Beatmap _originalBeatmap;

        ContainerVolocityCalculator _containerVolocityCalculator=new ContainerVolocityCalculator();

        /// <summary>
        /// Difficulty Calculator
        /// </summary>
        private OriginalBeatmapDifficultyCalculator _originalBeatmapDifficultyCalculator = new OriginalBeatmapDifficultyCalculator();

        /// <summary>
        /// 回傳時間範圍內的index
        /// </summary>
        TimeSlicingCalculator _timeSlicingCalculator=new TimeSlicingCalculator();

        /// <summary>
        /// 回傳切割結果
        /// </summary>
        /// <returns>The list comvert parameter.</returns>
        public List<ComvertParameter> GetListComvertParameter(Beatmap originalBeatmap)
        {
            List<ComvertParameter> list=new List<ComvertParameter>();
            _originalBeatmap = originalBeatmap;
            //Get The Diff of beatmap;
            _diffStar = _originalBeatmapDifficultyCalculator.CalculateBeatmapDifficulty(_originalBeatmap);

            _timeSlicingCalculator.SetBeatmap(_originalBeatmap);

            int nowSlidingIndex = 0;

            //collect ComvertParameter and return list<ComvertParameter>
            while (nowSlidingIndex < originalBeatmap.HitObjects.Count)
            {
                _containerVolocityCalculator.UpdateCalculateVolocity();
                //set the slicing time
                _timeSlicingCalculator.SetSliceTime(_containerVolocityCalculator.GetTime());
                //calculate by BPM to devide by TimeSliceCalculator
                int endindex = _timeSlicingCalculator.SlicingFrom(nowSlidingIndex);
                //follow the tiem to slice the beatmap and get the startTime and endTime
                float volicity = _containerVolocityCalculator.GetVolocity();
                list.Add(SlicingSingle(nowSlidingIndex, endindex, volicity));
                nowSlidingIndex = endindex + 1;
            }

            return list;
        }

        /// <summary>
        /// use startTime and Endtime to grab the object and return ComvertParameter
        /// And calculate number of Container and Layout
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        ComvertParameter SlicingSingle(int startIndex, int endIndex,float volicity)
        {
            ComvertParameter single = new ComvertParameter();

            //Get Physical Refrence Object
            single.ListRefrenceObject = _originalBeatmap.HitObjects.GetRange(startIndex, endIndex- startIndex);

            //難度
            single.Difficulty = _diffStar;

            //Generate Parameter
            single.SliceConvertParameter = GetSliceConvertParameterResult(single, volicity);
            return single;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public SliceConvertParameter GetSliceConvertParameterResult(ComvertParameter result, float volicity)
        {
            //decide the number of container and layout
            SliceConvertParameter sliceConvertParameter=new SliceConvertParameter();
            
            //startTime
            sliceConvertParameter.StartTime = result.ListRefrenceObject[0].StartTime;

            //EndTime
            int endIndex = result.ListRefrenceObject.Count - 1;
            double lastHitObjectDuration = 0;
            sliceConvertParameter.EndTime = result.ListRefrenceObject[endIndex].StartTime + lastHitObjectDuration;

            //BPM
            sliceConvertParameter.BPM = _originalBeatmap.TimingInfo.BPMAt(sliceConvertParameter.StartTime);
            sliceConvertParameter.Volocity = volicity;

            return  sliceConvertParameter;
        }

    }
}
