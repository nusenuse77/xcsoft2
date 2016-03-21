using LeagueSharp.SDK;

using System.Linq;

using LeagueSharp;
using LeagueSharp.SDK.Core.UI.IMenu.Values;

using Settings = BlitzBuddy.Config.Modes.Combo;

namespace BlitzBuddy.Modes
{
    internal sealed class Combo : ModeBase
    {
        internal override bool ShouldBeExecuted()
        {
            return Config.Keys.ComboActive;
        }

        internal override void Execute()
        {
            if (!Variables.Orbwalker.CanMove())
            {
                return;
            }

            if (Settings.UseQ && Q.IsReady())
            {
                var ignorechamps = GameObjects.EnemyHeroes.Where(x => Settings.QIgnoreChamps.Menu.GetValue<MenuBool>(x.ChampionName).Value);
                var target = Variables.TargetSelector.GetTarget(Q, false, ignorechamps);
                if (target != null)
                {
                    Q.Cast(target);
                }
            }

            if (Settings.UseR && R.IsReady())
            {
                if (GameObjects.EnemyHeroes.Any(x => x.IsValidTarget(R.Range) && (x.HasBuffOfType(BuffType.Knockup) || x.HasBuff("rocketgrab2") || x.IsKillablewithR())))
                {
                    R.Cast();
                }
            }
        }
    }
}
