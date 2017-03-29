﻿using System.Collections.Generic;
using osu.Game.Modes.Objects.Drawables;
using osu.Game.Modes.RP.Objects.Drawables.Component.MultiLine;
using osu.Game.Modes.RP.ScoreProcessor;

namespace osu.Game.Modes.RP.Objects.Drawables
{
    /// <summary>
    ///     用來乘載一次觸發多個物件
    /// </summary>
    internal class DrawableMultiBaseHitObject : DrawableBaseHitObject
    {
        /// <summary>
        ///     存放所有顯示物件
        /// </summary>
        protected List<DrawableBaseHitObject> _listBaseHitObject = new List<DrawableBaseHitObject>();

        /// <summary>
        ///     用來繪製多重物件
        /// </summary>
        protected MultiLine _multiLine;

        /// <summary>
        ///     建構
        /// </summary>
        /// <param name="hitObject"></param>
        public DrawableMultiBaseHitObject(BaseHitObject hitObject)
            : base(hitObject)
        {
        }

        /// <summary>
        ///     把顯示物件增加進來
        /// </summary>
        /// <param name="hitObject"></param>
        public void AddObject(DrawableBaseHitObject drawableBaseHitObject)
        {
            _listBaseHitObject.Add(drawableBaseHitObject);
        }

        /// <summary>
        ///     更新
        /// </summary>
        protected override void Update()
        {
            DrawLine();
        }

        /// <summary>
        ///     把連接用的線畫上去
        /// </summary>
        protected void DrawLine()
        {
        }

        /// <summary>
        ///     RP判斷
        /// </summary>
        /// <returns></returns>
        protected override RpJudgement CreateJudgement() => new RpJudgement();

        /// <summary>
        /// </summary>
        /// <param name="state"></param>
        protected override void UpdateState(ArmedState state)
        {
        }
    }
}
