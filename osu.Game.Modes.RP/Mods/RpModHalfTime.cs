namespace osu.Game.Modes.RP.Mods
{
    /// <summary>
    /// ����
    /// </summary>
    public class RpModHalfTime : ModHalfTime
    {
        public override string Description => @"Slow Down the Speed";

        public override double ScoreMultiplier => 0.5;
    }
}