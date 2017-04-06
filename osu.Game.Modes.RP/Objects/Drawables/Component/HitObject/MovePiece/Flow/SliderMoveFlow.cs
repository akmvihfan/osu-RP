﻿using osu.Framework.Graphics;
using osu.Game.Modes.RP.Objects.Drawables.Component.CommonInterface;
using osu.Game.Modes.RP.Objects.Drawables.Component.HitObject.Common;
using osu.Game.Modes.RP.Objects.Drawables.Component.HitObject.Common.ShapePiece;
using OpenTK;

namespace osu.Game.Modes.RP.Objects.Drawables.Component.HitObject.MovePiece.Flow
{
    internal class SliderMoveFlow : BaseMoveFlow, IComponentSliderProgress
    {
        /// <summary>
        ///     物件身體部分
        /// </summary>
        private readonly SliderBody _rpLongBody;

        /// <summary>
        ///     開頭和結尾物件
        /// </summary>
        private readonly HitObjectAnyShapePiece hitObjectAnyShapePieceFirstObjectAny;

        /// <summary>
        /// </summary>
        private readonly HitObjectAnyShapePiece hitObjectAnyShapePieceSecondObjectAny;


        public SliderMoveFlow(BaseHitObject baseHitObject): base(baseHitObject)
        {
            Children = new Drawable[]
            {
                //Slider身體
                _rpLongBody = new SliderBody(BaseHitObject)
                {
                    Position = new Vector2(0, 0),
                    PathWidth = BaseHitObject.Scale * 15
                },
                //結尾物件
                hitObjectAnyShapePieceSecondObjectAny = new HitObjectAnyShapePiece(BaseHitObject) //true
                {
                    Position = new Vector2(0, 0),
                    //Scale = new Vector2(_hitObject.Scale),
                    IsFirst = false
                },
                //開頭物件
                hitObjectAnyShapePieceFirstObjectAny = new HitObjectAnyShapePiece(BaseHitObject) //false
                {
                    Position = new Vector2(0, 0),
                    //Scale = new Vector2(_hitObject.Scale),
                    IsFirst = true
                }
            };
        }

        /// <summary>
        ///     初始化顯示
        /// </summary>
        public override void Initial()
        {
            _rpLongBody.Alpha = 1;
        }

        /// <summary>
        ///     開始特效
        /// </summary>
        public override void FadeIn(double time = 0)
        {
        }

        /// <summary>
        ///     結束
        /// </summary>
        public override void FadeOut(double time = 0)
        {
            _rpLongBody.FadeOut();
            hitObjectAnyShapePieceFirstObjectAny.FadeOut();
            hitObjectAnyShapePieceSecondObjectAny.FadeOut();
        }

        /// <summary>
        /// </summary>
        /// <param name="startProgress"></param>
        /// <param name="endProgress"></param>
        public void UpdateProgress(double startProgress = 0, double endProgress = 1)
        {
            _rpLongBody.UpdateProgress(startProgress, endProgress);
            hitObjectAnyShapePieceFirstObjectAny.Position = HitObject.Curve.PositionAt(startProgress) - HitObject.Position;
            hitObjectAnyShapePieceSecondObjectAny.Position = HitObject.Curve.PositionAt(endProgress) - HitObject.Position;
        }
    }
}
