using System.Linq;

using LeagueSharp.SDK;

using Settings = EzrealBuddy.Config.Modes.LaneClear;

namespace EzrealBuddy.Modes
{
    internal sealed class LaneClear : ModeBase
    {
        internal override bool ShouldBeExecuted()
        {
            return Config.Keys.LaneClearActive;
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
                //Lasthit with Q
                foreach (var minion in GameObjects.EnemyMinions.Where(x => x.IsKillableWithQ()).OrderByDescending(x => x.Health))
                {
                   Q.Cast(minion);
                }
            }
        }
    }
}
