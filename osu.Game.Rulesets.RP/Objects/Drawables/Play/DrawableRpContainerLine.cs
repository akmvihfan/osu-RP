﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using osu.Framework.Graphics;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.RP.Judgements;
using osu.Game.Rulesets.RP.Objects.Drawables.Component;
using osu.Game.Rulesets.RP.Objects.Drawables.Extension;
using osu.Game.Rulesets.RP.Objects.Drawables.Interface;

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
            //背景
            Components.Add(new ContainerLineBackground());
            Components.Add(new ContainerLineBeatLine());
            Components.Add(new ContainerLineContainArea());
            Components.Add(new ContainerLineDecisionLine());
        }

        /// <summary>
        ///     更新初始狀慁E
        /// </summary>
        protected override void UpdateInitialState()
        {
            base.UpdateInitialState();
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
                this.FadeOutComponents(FadeOutTime);
            }
        }


        /// <summary>
        ///     更新
        /// </summary>
        /// <param name="state"></param>
        //protected override void UpdateCurrentState(ArmedState state)
        //{
        //    this.Delay(HitObject.Duration + PreemptTime + FadeOutTime);

        //    if (state == ArmedState.Hit)
        //    {
        //    }
        //}
    }
}
