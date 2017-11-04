﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Types;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    /// <summary>
    /// base karaoke object
    /// contain single sentence , a main text and several additional text
    /// </summary>
    public class KaraokeObject : HitObject, IHasPosition ,IHasEndTime
    {
        /// <summary>
        /// position
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// X position
        /// </summary>
        public float X => Position.X;

        /// <summary>
        /// Y position
        /// </summary>
        public float Y => Position.Y;

        /// <summary>
        /// width
        /// </summary>
        public float Width { get; set; } = 1024;

        /// <summary>
        /// height
        /// </summary>
        public float Height { get; set; } = 100;

        /// <summary>
        /// Main text
        /// </summary>
        public TextObject MainText { get; set; } =new TextObject()
        {
            FontSize = 70,//default Main text Size is 70
            Position = new Vector2(0,30),//default position
        };

        /// <summary>
        /// List little aid text,like japanese's text
        /// </summary>
        public List<TextObject> ListSubTextObject { get; set; } = new List<TextObject>();

        /// <summary>
        /// record list time where position goes
        /// </summary>
        public List<ProgressPoint> ListProgressPoint { get; set; }=new List<ProgressPoint>();

        /// <summary>
        /// the index of singer 
        /// Default is singler1;
        /// Each singer has different Text color
        /// </summary>
        public int SingerIndex { get; set; } = 0;

        /// <summary>
        /// all the translate for a single language
        /// </summary>
        /// <value>The list trans late.</value>
        public List<KaraokeTranslateString> ListTranslate { get; set; } = new List<KaraokeTranslateString>();

        /// <summary>
        /// The time at which the HitObject ends.
        /// </summary>
        public double EndTime { get; set;}

        /// <summary>
        /// The duration of the HitObject.
        /// </summary>
        public double Duration => EndTime - StartTime;

    }

    /// <summary>
    /// Karaoke object extension.
    /// </summary>
    public static class KaraokeObjectExtension
    {
        /// <summary>
        /// will filter if has same languate
        /// </summary>
        /// <param name="karaokeObject">Karaoke object.</param>
        public static bool AddNewTranslate(this KaraokeObject karaokeObject)
        {
            return false;
        }

        /// <summary>
        /// will check if this progress point is valid
        /// </summary>
        /// <returns><c>true</c>, if progress point was added, <c>false</c> otherwise.</returns>
        /// <param name="karaokeObject">Karaoke object.</param>
        public static bool AddProgressPoint(this KaraokeObject karaokeObject)
        {
            return false;
        }
    }
}
