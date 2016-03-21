using LeagueSharp.SDK;

using Settings = EzrealBuddy.Config.Modes.Combo;

namespace EzrealBuddy.Modes
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
                var target = Variables.TargetSelector.GetTargetNoCollision(Q);
                if (target != null)
                {
                    Q.Cast(target);
                }
            }

            if (Settings.UseW && W.IsReady())
            {
                W.CastOnBestTarget(0f, true);
            }

            if (Settings.UseR && R.IsReady())
            {
                R.Cast(R.GetTarget(), false, true, Settings.UseRNum);
            }
        }
    }
}
