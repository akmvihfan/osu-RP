﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.RP.Objects.Drawables.Component;
using osu.Game.Rulesets.RP.Objects.Drawables.Extension;
using osu.Game.Rulesets.RP.Objects.Drawables.Interface;

namespace osu.Game.Rulesets.RP.Objects.Drawables.Play
{
    /// <summary>
    ///     匁E��RP物件
    /// </summary>
    public class DrawableRpContainerLineGroup : DrawableBaseContainableObject<IHasTemplate> , IHasParentDrawable
    {
        /// <summary>
        /// </summary>
        public new RpContainerLineGroup HitObject
        {
            get { return (RpContainerLineGroup)base.HitObject; }
        }

        public Container ParentGroupContainer { get; set; }

        private bool _startFadeont;

        /// <summary>
        /// </summary>
        /// <param name="hitObject"></param>
        public DrawableRpContainerLineGroup(BaseRpObject hitObject)
            : base(hitObject)
        {
            //Position = (HitObject as RpContainerLineGroup).Position;
        }

        //建構樣板物件
        protected override void ConstructObject()
        {
            //Background
            Components.Add(new ContainerGroupBackground());
            //Object contain area
            Components.Add(new ContainerGroupContainArea());
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
            //BaseRpObjectTemplate single = (BaseRpObjectTemplate)Template;
            //FadeIn(FadeInTime);
            //開始特效
            //Template.FadeIn(FadeInTime);

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
    }
}
