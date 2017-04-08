﻿using osu.Framework.Graphics;
using osu.Game.Modes.Objects.Drawables;
using osu.Game.Modes.RP.Objects;
using osu.Game.Modes.RP.ScoreProcessor;
using osu.Game.Modes.RP.UI.GamePlay.Playfield.Layout.HitObjects.Drawables.Template;
using OpenTK;

namespace osu.Game.Modes.RP.UI.GamePlay.Playfield.Layout.HitObjects.Drawables
{
    internal class DrawableBaseRpObject : DrawableHitObject<BaseRpObject, RpJudgement>
    {
        //物件出現需要花的時間
        public readonly float TIME_FADEIN = 100;
        //物件在打擊多久前提早出現
        public readonly float TIME_PREEMPT = 2000;
        //打擊過後多久會消失
        public readonly float TIME_FADEOUT = 100;

        //物件出現需要花的時間
        public static float TIME_FADEIN_Connector = 100;

        /// <summary>
        ///     打擊物件，DrawableHitCircle 會根據打擊物件把 物件繪製出來
        /// </summary>
        public new BaseRpObject HitObject;


        /// <summary>
        ///     樣板，把物件綁上去就對了
        /// </summary>
        public RpDrawBaseObjectTemplate Template;

        public DrawableBaseRpObject(BaseRpObject hitObject)
            : base(hitObject)
        {
            TIME_FADEIN = hitObject.TIME_FADEIN;
            TIME_PREEMPT = hitObject.TIME_PREEMPT;
            TIME_FADEOUT = hitObject.TIME_FADEOUT;

            HitObject = hitObject;

            Origin = Anchor.Centre;
            Position = HitObject.Position;
            Scale = new Vector2(HitObject.Scale);
        }

        /// <summary>
        ///     更新狀態
        /// </summary>
        /// <param name="state"></param>
        protected override void UpdateState(ArmedState state)
        {
            state = ArmedState.Hit;

            if (!IsLoaded) return;

            Flush();

            UpdateInitialState();

            Delay(HitObject.StartTime - Time.Current - TIME_PREEMPT + Judgement.TimeOffset, true);

            UpdatePreemptState();

            Delay(TIME_PREEMPT, true);
        }

        /// <summary>
        /// </summary>
        protected virtual void UpdateInitialState()
        {
            //要設定成0 ，第一針才不會有殘影
            Alpha = 0;
        }

        /// <summary>
        /// </summary>
        protected virtual void UpdatePreemptState()
        {
            FadeIn(TIME_FADEIN);
            //開始特效
            Template.FadeIn(TIME_FADEIN);
        }

        /// <summary>
        ///     持續一直更新物件
        /// </summary>
        protected override void Update()
        {
            base.Update();

            //更新物件位置
            Template.UpdateTemplate(Time.Current);
        }

        protected override RpJudgement CreateJudgement() => new RpJudgement { Score = RpScoreResult.Cool };
    }
}
