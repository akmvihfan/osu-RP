﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using osu.Framework.Graphics;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.RP.Judgements;
using osu.Game.Rulesets.RP.Objects.Drawables.Play.Interface;
using osu.Game.Rulesets.RP.Objects.Drawables.Template.ContainerComponent;
using osu.Game.Rulesets.RP.Objects.Drawables.Template.ContainerComponent.ContainerLine;
using osu.Game.Rulesets.RP.Objects.Drawables.Template.RpContainer;
using osu.Game.Rulesets.RP.Objects.Drawables.Template.RpContainer.Component;
using osu.Game.Rulesets.RP.SkinManager;
using OpenTK;

namespace osu.Game.Rulesets.RP.Objects.Drawables.Play
{
    public class DrawableRpContainerLine : DrawableBaseContainableObject<IHasTemplate>
    {
        /// <summary>
        /// </summary>
        public new RpContainerLine HitObject
        {
            get { return (RpContainerLine)base.HitObject; }
        }

        private bool _startFadeont;

        /// <summary>
        /// </summary>
        /// <param name="hitObject"></param>
        public DrawableRpContainerLine(BaseRpObject hitObject)
            : base(hitObject)
        {
        }

        //建構樣板物件
        protected override void ConstructObject()
        {
             ContainerBackground _rpRectanglePiece;

             ContainerBackground _linePiece;

            //背景物件
            _linePiece = new ContainerBackground(HitObject.ParentObject)
            {
                Origin = Anchor.CentreRight,
                Scale = new Vector2(1.0f, 0f),
                Colour = RpTextureColorManager.GetCoopJudgementLineColor(HitObject.Coop)
            };

            //背景物件
            _rpRectanglePiece = new ContainerBackground(HitObject.ParentObject)
            {
                Scale = new Vector2(1.0f, 0f),
                Alpha = 0.5f,
                Colour = RpTextureColorManager.GetCoopLayoutColor(HitObject.Coop)
            };

            //背景
            Components.Add(_rpRectanglePiece);
            Components.Add(_linePiece);

            //放置子物件
            Components.Add(new ContainerHitObjectContainComponent(HitObject.ParentObject));
        }

        /// <summary>
        ///     這裡估計會一直更新
        /// </summary>
        protected override void UpdatePreemptState()
        {
            base.UpdatePreemptState();
        }

        /// <summary>
        ///     持續一直更新物件
        /// </summary>
        protected override void Update()
        {
            base.Update();

            //如果時間趁E��就執衁E
            if (HitObject.EndTime < Time.Current && !_startFadeont)
            {
                _startFadeont = true;
                this.FadeOut(FadeOutTime);
                Template.FadeOut(FadeOutTime);
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        protected override RpJudgement CreateJudgement()
        {
            return new RpJudgement();
        }

        /// <summary>
        ///     從這邊去更新狀慁E
        /// </summary>
        /// <param name="userTriggered"></param>
        protected override void CheckJudgement(bool userTriggered)
        {
        }

        /// <summary>
        ///     更新
        /// </summary>
        /// <param name="state"></param>
        protected override void UpdateCurrentState(ArmedState state)
        {
            this.Delay(HitObject.Duration + PreemptTime + FadeOutTime);

            if (state == ArmedState.Hit)
            {
            }
        }
    }
}
