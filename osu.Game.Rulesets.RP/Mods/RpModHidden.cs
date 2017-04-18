using System;

namespace osu.Game.Modes.RP.Mods
{
    /// <summary>
    /// ��c�����U�N��
    /// </summary>
    public class RpModHidden : ModHidden
    {
        public override string Description => @"Play with no approach RP HitObject and fading notes for a slight score advantage.";
        public override double ScoreMultiplier => 1.06;
        public override Type[] IncompatibleMods  => new Type[] { };
    }
}