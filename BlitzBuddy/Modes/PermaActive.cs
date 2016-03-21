using System.Collections.Generic;
using System.Linq;

using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Core.UI.IMenu.Values;

using Settings = BlitzBuddy.Config.Auto;

namespace BlitzBuddy.Modes
{
    internal sealed class PermaActive : ModeBase
    {
        internal override bool ShouldBeExecuted()
        {
            return true;
        }

        internal override void Execute()
        {
            if (Settings.AutoQ.Enabled && Q.IsReady())
            {
                var ignorechamps = GameObjects.EnemyHeroes.Where(x => !Settings.AutoQ.Menu.GetValue<MenuBool>(x.ChampionName).Value);
                var target = Variables.TargetSelector.GetTargetNoCollision(Q, false, ignorechamps);
                if (target != null)
                {
                    Q.Cast(target);
                }
            }
        }
    }
}
