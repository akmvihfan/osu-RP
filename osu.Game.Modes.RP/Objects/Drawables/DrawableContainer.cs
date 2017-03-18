﻿using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Game.Modes.RP.Objects.Drawables.Pieces;
using osu.Game.Modes.RP.Objects.Drawables.Template.Container;
using osu.Game.Modes.Objects.Drawables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Modes.Judgements;

namespace osu.Game.Modes.RP.Objects.Drawables
{
    /// <summary>
    /// 包住RP物件
    /// </summary>
    class DrawableContainer : DrawableBaseRpObject
    {
        

        /// <summary>
        /// 樣板，把物件綁上去就對了
        /// </summary>
        public new ContainerTemplate ContainerTemplate
        {
            get
            {
                return (ContainerTemplate)Template;
            }
            set
            {
                Template = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public new ObjectContainer HitObject;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hitObject"></param>
        public DrawableContainer(BaseRpObject hitObject) : base(hitObject)
        {
            HitObject = (ObjectContainer)hitObject;

            

            Template = new ContainerTemplate(HitObject)
            {
                Position = new Vector2(0, 0),
                Alpha = 1,
            };

            Children = new Drawable[]
            {
                Template,
            };

            //may not be so correct
            //Size = _rpDetectPress.DrawSize;
            Scale = new Vector2(HitObject.Scale);
        }
        

        /// <summary>
        /// 更新初始狀態
        /// </summary>
        protected override void UpdateInitialState()
        {
            base.UpdateInitialState();

            //sane defaults
            //ring.Alpha = _detectPress.Alpha = number.Alpha = glow.Alpha = 1;
            //初始化指標
            Template.Initial();
            //explode.Alpha = 0;
        }

        /// <summary>
        /// 這裡估計會一直更新
        /// </summary>
        protected override void UpdatePreemptState()
        {
            base.UpdatePreemptState();
        }

        /// <summary>
        /// 持續一直更新物件
        /// </summary>
        protected override void Update()
        {
            base.Update();

            //如果時間超過就執行
            if (HitObject.EndTime < Time.Current)
            {
                FadeOut(TIME_FADEOUT);
                Template.FadeOut(TIME_FADEOUT);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override RPJudgementInfo CreateJudgementInfo()
        {
            return new RPJudgementInfo();
        }

        /// <summary>
        /// 從這邊去更新狀態
        /// </summary>
        /// <param name="userTriggered"></param>
        protected override void CheckJudgement(bool userTriggered)
        {

            
                
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="state"></param>
        protected override void UpdateState(ArmedState state)
        {
            base.UpdateState(state);
            Delay(HitObject.Duration + TIME_PREEMPT + TIME_FADEOUT);

            if (state == ArmedState.Hit)
            {
               
            }
            else
            {
                
            }

            
        }
    }
}
