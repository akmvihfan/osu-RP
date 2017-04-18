﻿namespace osu.Game.Rulesets.RP.Beatmaps.OtherBeatmap.Slicing.Parameter
{
    /// <summary>
    ///     Slice convert parameter.
    /// </summary>
    public class SliceConvertParameter
    {
        /// <summary>
        ///     the time container shoule start
        /// </summary>
        public double StartTime;

        /// <summary>
        ///     the time container should stop
        /// </summary>
        public double EndTime;

        /// <summary>
        /// </summary>
        public float Volocity;

        /// <summary>
        ///     BPM
        /// </summary>
        internal double BPM = 180;
    }
}
