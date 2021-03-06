// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Allocation;
using osu.Game.Graphics;
using osu.Game.Graphics.UserInterface;

namespace osu.Game.Screens.Play.ReplaySettings
{
    public class ReplayCheckbox : OsuCheckbox
    {
        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            Nub.AccentColour = colours.Yellow;
            Nub.GlowingAccentColour = colours.YellowLighter;
            Nub.GlowColour = colours.YellowDarker;
        }
    }
}
