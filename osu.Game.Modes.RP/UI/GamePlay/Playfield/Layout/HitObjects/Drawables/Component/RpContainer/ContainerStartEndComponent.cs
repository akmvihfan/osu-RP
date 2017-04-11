using System.Collections.Generic;
using osu.Game.Modes.RP.Objects;
using osu.Game.Modes.RP.UI.GamePlay.Playfield.Layout.CommonDwawablePiece;
using OpenTK;

namespace osu.Game.Modes.RP.UI.GamePlay.Playfield.Layout.HitObjects.Drawables.Component.Container
{
    /// <summary>
    /// </summary>
    internal class ContainerStartEndComponent : BaseContainerComponent, IChangeableContainerComponent
    {
        /// <summary>
        ///     開始點
        /// </summary>
        private RectanglePiece _containerStartDecisionLineComponent;

        /// <summary>
        ///     結束
        /// </summary>
        private RectanglePiece _containerEndDecisionLineComponent;

        /// <summary>
        /// </summary>
        /// <param name="hitObject"></param>
        public ContainerStartEndComponent(RpContainer hitObject)
            : base(hitObject)
        {
        }

        /// <summary>
        ///     修改物件高度
        /// </summary>
        /// <param name="newHeight"></param>
        public void ChangeHeight(float newHeight)
        {
            _containerStartDecisionLineComponent.Height = newHeight;
            _containerEndDecisionLineComponent.Height = newHeight;
        }

        /// <summary>
        /// </summary>
        /// <param name="layerCount"></param>
        protected override void InitialObject(int layerCount = 1)
        {
            //開始點
            _containerStartDecisionLineComponent = new RectanglePiece(100, 100)
            {
                Scale = new Vector2(0.002f, 0.2f * layerCount),
                Position = CalculatePosition(0)
            };
            //結束物件
            _containerEndDecisionLineComponent = new RectanglePiece(100, 100)
            {
                Scale = new Vector2(0.002f, 0.2f * layerCount),
                Position = CalculatePosition(HitObject.EndTime - HitObject.StartTime)
            };
        }

        protected override void InitialChild()
        {
            var listContainer = new List<Framework.Graphics.Containers.Container>();
            listContainer.Add(_containerStartDecisionLineComponent);
            listContainer.Add(_containerEndDecisionLineComponent);
            Children = listContainer;
        }
    }
}
