using System.Linq;

using LeagueSharp.SDK;
using LeagueSharp.SDK.Core.Utils;

using Settings = EzrealBuddy.Config.Modes.JungleClear;

namespace EzrealBuddy.Modes
{
    internal sealed class JungleClear : ModeBase
    {
        internal override bool ShouldBeExecuted()
        {
            return Config.Keys.JungleClearActive;
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
                var target = GameObjects.Jungle.OrderByDescending(x => x.MaxHealth).FirstOrDefault(x => x.InAutoAttackRange());
                if (target != null)
                {
                    Q.Cast(target);
                }
            }
        }
    }
}
