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
    public class KaraokeObject : HitObject, IHasPosition
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
    
    }
}
