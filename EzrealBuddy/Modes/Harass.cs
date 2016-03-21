using LeagueSharp.SDK;

using Settings = EzrealBuddy.Config.Modes.Harass;

namespace EzrealBuddy.Modes
{
    internal sealed class Harass : ModeBase
    {
        internal override bool ShouldBeExecuted()
        {
            return Config.Keys.HarassActive;
        }

        internal override void Execute()
        {
            if (!Variables.Orbwalker.CanMove())
            {
                return;
            }

            if (GameObjects.Player.ManaPercent < Settings.MinManaPer)
            {
                return;
            }

            if (Settings.UseQ && Q.IsReady())
            {
                Q.CastOnBestTarget();
            }

            if (Settings.UseW && W.IsReady())
            {
                W.CastOnBestTarget(0f, true);
            }
        }
    }
}
