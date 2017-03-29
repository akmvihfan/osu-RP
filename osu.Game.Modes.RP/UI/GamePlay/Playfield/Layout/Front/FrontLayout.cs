﻿using System.Collections.Generic;

namespace osu.Game.Modes.RP.UI.GamePlay.Playfield.Layout.Front
{
    /// <summary>
    ///     放置顯示在打擊物件之前的東西
    ///     例如DecisionLine
    /// </summary>
    internal class FrontLayout : BaseGamePlayLayout
    {
        /// <summary>
        ///     判定線，由
        /// </summary>
        public List<DecisionLine.DecisionLine> _decisionLine;

        /// <summary>
        ///     自動更新物件
        /// </summary>
        protected override void Update()
        {
            //_decisionLine.Position = PositionManager.GetPosition(Time.Current)-new OpenTK.Vector2(400,300);
        }
    }
}
