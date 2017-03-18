﻿using System;
using osu.Game.Modes.RP.Objects.Drawables.Component.Common;

namespace osu.Game.Modes.RP.Objects.Drawables.Component.HitObject.MovePiece
{
    /// <summary>
    /// 結束的物件後面帶的特效
    /// </summary>
    class BaseMovePicec : BaseComponent,ISliderProgress
    {
        /// <summary>
        /// 打擊物件
        /// </summary>
        protected BaseHitObject BaseHitObject;

        public BaseMovePicec(BaseHitObject baseHitObject)
        {
            BaseHitObject = baseHitObject;
        }

        /// <summary>
        /// 初始化顯示
        /// </summary>
        public override void Initial()
        {

        }

        /// <summary>
        /// 開始特效
        /// </summary>
        public override void FadeIn(double time = 0)
        {

        }

        /// <summary>
        /// 結束
        /// </summary>
        public override void FadeOut(double time = 0)
        {

        }

        public void UpdateProgress(double startProgress = 0, double endProgress = 1)
        {
            throw new NotImplementedException();
        }
    }
}
