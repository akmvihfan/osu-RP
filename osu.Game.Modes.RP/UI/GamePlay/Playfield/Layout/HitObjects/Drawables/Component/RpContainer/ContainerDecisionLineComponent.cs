using System.Collections.Generic;
using osu.Game.Modes.RP.Objects;
using osu.Game.Modes.RP.SkinManager;
using osu.Game.Modes.RP.UI.GamePlay.Playfield.Layout.CommonDwawablePiece;
using OpenTK;

namespace osu.Game.Modes.RP.UI.GamePlay.Playfield.Layout.HitObjects.Drawables.Component.Container
{
    /// <summary>
    /// </summary>
    internal class ContainerDecisionLineComponent : BaseContainerComponent, IChangeableContainerComponent,IComponentHasStartTime,IComponentHasEndTime
    {
        /// <summary>
        ///     �����
        /// </summary>
        private ImagePicec _containerDecisionLineComponent;

        /// <summary>
        /// </summary>
        public ContainerDecisionLineComponent(RpContainer hitObject)
            : base(hitObject)
        {
        }

        /// <summary>
        ///     �X�V����
        /// </summary>
        /// <param name="time"></param>
        public void UpdateTime(double time)
        {
            //�v�Z�w�j�ʒu 
            _containerDecisionLineComponent.Position = CalculatePosition(time - HitObject.StartTime);
        }

        /// <summary>
        ///     �C���������x
        /// </summary>
        /// <param name="newHeight"></param>
        public void ChangeHeight(float newHeight)
        {
        }


        protected override void InitialObject(int layerCount = 1)
        {
            //�w�W
            _containerDecisionLineComponent = new ImagePicec(RpTexturePathManager.GetDecisionLineTexture())
            {
                Position = new Vector2(0, 0),
                Scale = new Vector2(1f, 1f * layerCount)
            };
        }


        protected override void InitialChild()
        {
            var listDrawable = new List<Framework.Graphics.Containers.Container>();
            listDrawable.Add(_containerDecisionLineComponent);
            Children = listDrawable;
        }

        
        public void SetStartTime(double startTime)
        {
            throw new System.NotImplementedException();
        }

        public void SetEndTime(double time)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        void UpdateMovingTransform()
        {

        }
    }
}
