﻿using System.Collections.Generic;
using osu.Game.Modes.RP.Objects.Drawables.Calculator.Height;
using osu.Game.Modes.RP.Objects.Drawables.Pieces;
using osu.Game.Modes.RP.SkinManager;
using OpenTK;

namespace osu.Game.Modes.RP.Objects.Drawables.Component.Container
{
    /// <summary>
    ///     裝判定點用
    /// </summary>
    internal class ContainerBeatLineComponent : BaseContainerComponent, IChangeableContainerComponent
    {
        /// <summary>
        ///     中間的節拍
        /// </summary>
        private readonly List<ImagePicec> _containerBeatDecisionLineComponent = new List<ImagePicec>();

        /// <summary>
        ///     計算物件的相關高度和Height位置
        /// </summary>
        private ContainerLayoutHeightCalculator _heightCalculator = new ContainerLayoutHeightCalculator();

        public ContainerBeatLineComponent(ObjectContainer hitObject)
            : base(hitObject)
        {
        }

        /// <summary>
        ///     修改物件高度
        /// </summary>
        /// <param name="newHeight"></param>
        public void ChangeHeight(float newHeight)
        {
        }

        /// <summary>
        ///     初始化顯示
        /// </summary>
        protected override void InitialObject(int layerCount = 0)
        {
            InitialBeat();
        }


        protected override void InitialChild()
        {
            Children = _containerBeatDecisionLineComponent;
        }

        /// <summary>
        ///     初始化節拍點
        /// </summary>
        private void InitialBeat()
        {
            for (var i = 0; i < 6; i++)
            {
                //物件
                var line = new ImagePicec(RpTexturePathManager.GetBeatLineTexture());
                //
                line.Scale = new Vector2(0.6f);
                //設定位置
                line.Position = CalculatePosition(i * GetDeltaBeatTime());
                //加入
                _containerBeatDecisionLineComponent.Add(line);
            }
        }
    }
}
