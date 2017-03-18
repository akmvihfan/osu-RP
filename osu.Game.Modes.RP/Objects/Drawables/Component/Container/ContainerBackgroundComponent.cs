﻿using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Transforms;
using osu.Game.Modes.RP.Objects.Drawables.Pieces;
using OpenTK;

namespace osu.Game.Modes.RP.Objects.Drawables.Component.Container
{
    /// <summary>
    /// Container背景
    /// </summary>
    class ContainerBackgroundComponent : BaseContainerComponent
    {
        /// <summary>
        /// 背景
        /// </summary>
        private RectanglePiece _rpRectangleComponent;


        /// <summary>
        /// 
        /// </summary>
        public ContainerBackgroundComponent(ObjectContainer hitObject) : base(hitObject)
        {
           
        }

        /// <summary>
        /// 初始化顯示
        /// </summary>
        protected override void InitialObject(int layerCount = 0)
        {
            _rpRectangleComponent = new RectanglePiece(2000,60)
            {
                Scale = new Vector2(1,0),//new OpenTK.Vector2(1, 0.13f * layerCount),
                Alpha = 0.5f,
                Position = new Vector2(0, 0.13f / 2 * layerCount),
                Colour = HitObject.Colour,
            };
        }

        protected override void InitialChild()
        {
            List<Drawable> listDrawable = new List<Drawable>();
            listDrawable.Add(_rpRectangleComponent);
            Children = listDrawable;
        }

        /// <summary>
        /// 開始特效
        /// </summary>
        public override void FadeIn(double time = 0)
        {
            //RP FadeIn 動畫特效
            int layerCount = 1;
            _rpRectangleComponent.ScaleTo(new OpenTK.Vector2(1, 1), time, EasingTypes.InOutElastic);
        }

        /// <summary>
        /// 結束
        /// </summary>
        public override void FadeOut(double time = 0)
        {
            //RP FadeIn 動畫特效
            int layerCount = 1;
            _rpRectangleComponent.ScaleTo(new OpenTK.Vector2(1, 0), time, EasingTypes.OutElastic);
        }
    }
}
