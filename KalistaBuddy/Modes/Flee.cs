using System.Collections.Generic;
using System.Linq;

using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Core.Utils;

using Settings = KalistaBuddy.Config.Modes.Flee;

namespace KalistaBuddy.Modes
{
    internal sealed class Flee : ModeBase
    {
        internal override bool ShouldBeExecuted()
        {
            return Config.Keys.FleeActive;
        }

        internal override void Execute()
        {
            Variables.Orbwalker.Orbwalk(Variables.Orbwalker.GetTarget(), Game.CursorPos);

            if (Settings.AttackMinion && Variables.Orbwalker.CanAttack() && !GameObjects.EnemyHeroes.Any(x => x.InAutoAttackRange()))
            {
                var units = new List<Obj_AI_Minion>();
                units.AddRange(GameObjects.EnemyMinions);
                units.AddRange(GameObjects.Jungle);
                var bestUnit = units.Where(x => x.IsValidTarget() && x.InAutoAttackRange()).OrderByDescending(x => x.Distance(GameObjects.Player)).FirstOrDefault();
                if (bestUnit != null)
                {
                    Variables.Orbwalker.Attack(bestUnit);
                }
            }

            if (Settings.UseQ && Q.IsReady())
            {
                Q.Cast(GameObjects.Player.Position.Extend(Game.CursorPos, Q.Range - 300));
            }
        }
    }
}
