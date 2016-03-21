using System;

using LeagueSharp.SDK;

using Settings = EzrealBuddy.Config.Auto;

namespace EzrealBuddy.Modes
{
    internal sealed class PermaActive : ModeBase
    {
        internal override bool ShouldBeExecuted()
        {
            return true;
        }

        internal override void Execute()
        {
            if (!Variables.Orbwalker.CanMove())
            {
                return;
            }

            if (GameObjects.Player.ManaPercent < Settings.AutoHarass.MinManaPer)
            {
                return;
            }

            if (Settings.AutoHarass.UseQ && Q.IsReady())
            {
                var target = Variables.TargetSelector.GetTarget(Q);
                if (target != null && (!GameObjects.Player.IsUnderEnemyTurret() || !target.IsUnderEnemyTurret()))
                {
                    Q.Cast(target);
                }
            }

            if (Settings.AutoHarass.UseW && W.IsReady())
            {
                var target = Variables.TargetSelector.GetTarget(W);
                if (target != null && (!GameObjects.Player.IsUnderEnemyTurret() || !target.IsUnderEnemyTurret()))
                {
                    W.Cast(target, false, true);
                }
            }
        }
    }
}
