﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Linq;
using osu.Framework.Audio;
using osu.Framework.Timing;
using osu.Game.Beatmaps;
using osu.Game.Graphics;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.RP.Mods.ModsElement;
using osu.Game.Rulesets.RP.Objects;
using osu.Game.Rulesets.RP.Scoreing.Result;
using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.RP.Mods
{
    /// <summary>
    ///     不會死掉
    /// </summary>
    public class RpModNoFail : ModNoFail
    {
        public override double ScoreMultiplier => 0.5;
        public override Type[] IncompatibleMods => base.IncompatibleMods.Concat(new[] { typeof(RpModAutoplay) }).ToArray();
    }

    /// <summary>
    ///     更簡單
    /// </summary>
    public class RpModEasy : ModEasy
    {
        public override double ScoreMultiplier => 0.5;
        public override string Description => @"Reduces overall difficulty - larger RP HitObject, more forgiving HP drain, less accuracy required.";
    }

    /// <summary>
    ///     會把物件藏起來
    /// </summary>
    public class RpModHidden : ModHidden
    {
        public override string Description => @"Play with no approach RP HitObject and fading notes for a slight score advantage.";
        public override double ScoreMultiplier => 1.06;
        public override Type[] IncompatibleMods => new Type[] { };
    }

    /// <summary>
    ///     物件會更小
    /// </summary>
    public class RpModHardRock : ModHardRock
    {
        public override double ScoreMultiplier => 1.06;
        public override bool Ranked => true;
    }

    /// <summary>
    ///     一個 miss 馬上死翹翹
    /// </summary>
    public class RpModSuddenDeath : ModSuddenDeath
    {
        public override double ScoreMultiplier => 1.06;
        public override Type[] IncompatibleMods => base.IncompatibleMods.Concat(new[] { typeof(RpModAutoplay), typeof(RpModNoFail) }).ToArray();
    }

    /// <summary>
    ///     完美模式
    ///     只要一個不是perfect 就會死亡
    ///     比 ModSuddenDeath 還嚴格
    /// </summary>
    public class RpModPerfect : ModPerfect
    {
        public override double ScoreMultiplier => 1.20;

        public override Type[] IncompatibleMods => base.IncompatibleMods.Concat(new[] { typeof(RpModAutoplay), typeof(RpModNoFail) }).ToArray();
        public override string Description => @"Perfect Hit or quit.";
    }

    /// <summary>
    ///     兩倍速度
    /// </summary>
    public class RpModDoubleTime : ModDoubleTime
    {
        public override string Description => @"Speed Up";
        public override double ScoreMultiplier => 1.12;
        public override Type[] IncompatibleMods => base.IncompatibleMods.Concat(new[] { typeof(RpModHalfTime) }).ToArray();


        public override void ApplyToClock(IAdjustableClock clock)
        {
            clock.Rate = 1.3;
        }
    }

    /// <summary>
    ///     休閒模式，其實我也不太確定這是三洨
    /// </summary>
    public class RpModRelax : ModRelax
    {
        public override string Description => "You don't need to click.\nJust prepaer popcorn and see osu! play the game.";
        public override bool Ranked => false;
        public override double ScoreMultiplier => 0;
    }

    /// <summary>
    ///     半速
    /// </summary>
    public class RpModHalfTime : ModHalfTime
    {
        public override string Description => @"Slow Down the Speed";

        public override double ScoreMultiplier => 0.5;

        public override Type[] IncompatibleMods => base.IncompatibleMods.Concat(new[] { typeof(RpModDoubleTime) }).ToArray();
    }

    /// <summary>
    ///     提高音調 + 加速
    /// </summary>
    public class RpModNightcore : ModNightcore
    {
        public override string Description => @"Play RP highhhhhhhhhhhhhhhhhh";
        public override double ScoreMultiplier => 1.12;

        public override void ApplyToClock(IAdjustableClock clock)
        {
            var pitchAdjust = clock as IHasPitchAdjust;
            if (pitchAdjust != null)
                pitchAdjust.PitchAdjust = 1.5;
            else
                base.ApplyToClock(clock);
            clock.Rate = 1.3/1.5;
        }
    }

    /// <summary>
    ///     只有判定線附近會發亮
    /// </summary>
    public class RpModFlashlight : ModFlashlight
    {
        public override double ScoreMultiplier => 1.12;
        public override Type[] IncompatibleMods => new Type[] { };
    }

    /// <summary>
    ///     Auto play the RP Mode
    /// </summary>
    public class RpModAutoplay : ModAutoplay<BaseRpObject>
    {
        protected override Score CreateReplayScore(Beatmap<BaseRpObject> beatmap) => new RpScore
        {
            Replay = new RpAutoReplay(beatmap)
        };
    }

    /// <summary>
    ///     RP : 背景按壓的物件會自動完成
    /// </summary>
    public class RpModContainerHitObjectPressOut : Mod
    {
        public override string Name => "SpunOut";
        public override FontAwesome Icon => FontAwesome.fa_osu_mod_spunout;
        public override string Description => @"Background HitObject will be automatically completed";
        public override double ScoreMultiplier => 0.9;
        public override bool Ranked => true;

        public override Type[] IncompatibleMods => new[] { typeof(ModAutoplay), typeof(RpModRelax) };
    }

    /// <summary>
    ///     Shape Coco，會把單手的 Mode 變成雙手的
    ///     會參考預設譜面設定做調整
    /// </summary>
    public class RpModShapeHitObjectCoco : Mod
    {
        public override string Name => "KeyCoop";
        public override FontAwesome Icon => FontAwesome.fa_osu_mod_autopilot;
        public override string Description => @"RP Shape Object all become co-co mode";
        public override double ScoreMultiplier => 1;
        public override bool Ranked => true;
        public override Type[] IncompatibleMods => new Type[] { };
    }

    /// <summary>
    ///     Container Press Coco，會把單手的 Mode 變成雙手的
    ///     會參考譜面設定做調整
    /// </summary>
    public class RpModContainerHitObjectCoco : Mod
    {
        public override string Name => "KeyCoop";
        public override FontAwesome Icon => FontAwesome.fa_osu_mod_target;
        public override string Description => @"RP Background Object all become co-co mode";
        public override double ScoreMultiplier => 1;
        public override bool Ranked => true;
        public override Type[] IncompatibleMods => new Type[] { };
    }

    /// <summary>
    /// Set the Rp Key Number
    /// </summary>
    public abstract class RpKeyMod : Mod
    {
        public abstract int KeyCount { get; }
        public override double ScoreMultiplier => 1; // TODO: Implement the mania key mod score multiplier
        public override bool Ranked => true;
    }

    /// <summary>
    /// 2K
    /// </summary>
    public class RpKeyMod2K : RpKeyMod
    {
        public override int KeyCount => 2;
        public override string Name => "2K";
    }

    /// <summary>
    /// 3K
    /// </summary>
    public class RpKeyMod3K : RpKeyMod
    {
        public override int KeyCount => 3;
        public override string Name => "3K";
    }

    /// <summary>
    /// 4K
    /// </summary>
    public class RpKeyMod4K : RpKeyMod
    {
        public override int KeyCount => 4;
        public override string Name => "4K";
    }
}
