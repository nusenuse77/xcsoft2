using System.Linq;

using LeagueSharp.SDK;
using LeagueSharp.SDK.Core.Utils;

using Settings = KalistaBuddy.Config.Auto;

namespace KalistaBuddy.Modes
{
    internal sealed class PermaActive : ModeBase
    {
        internal override bool ShouldBeExecuted()
        {
            return true;
        }

        internal override void Execute()
        {
            if (E.IsReady())
            {
                if (Settings.AutoE.KillEnemyHeros && GameObjects.EnemyHeroes.Any(x => x.IsKillableWithE(true)))
                {
                    if (E.Cast())
                    {
                        return;
                    }
                }

                if (Settings.AutoE.KillMinionsToHarassEnemyHeros && GameObjects.Player.ManaPercent > Settings.AutoE.KillMinionsToHarassEnemyHerosMinMana && GameObjects.EnemyMinions.Any(x => x.IsKillableWithE(true)) && GameObjects.EnemyHeroes.Any(x => x.IsValidTarget(E.Range) && x.HasEBuff()))
                {
                    if (E.Cast())
                    {
                        return;
                    }
                }

                if (Settings.AutoE.KillSiegeMinions && GameObjects.Player.ManaPercent > Settings.AutoE.KillSiegeMinionsMinMana && GameObjects.EnemyMinions.Any(x => x.GetMinionType().HasFlag(MinionTypes.Siege) && x.IsKillableWithE(true)))
                {
                    if (E.Cast())
                    {
                        return;
                    }
                }

                if (Settings.AutoE.KillSuperMinions && GameObjects.EnemyMinions.Any(x => x.GetMinionType().HasFlag(MinionTypes.Super) && x.IsKillableWithE(true)))
                {
                    if (E.Cast())
                    {
                        return;
                    }
                }

                if (Settings.AutoE.KillSmallJungle && GameObjects.JungleSmall.Any(x => x.IsKillableWithE(true)))
                {
                    if (E.Cast())
                    {
                        return;
                    }
                }

                if (Settings.AutoE.KillBigJungle && GameObjects.JungleLarge.Any(x => x.IsKillableWithE(true)))
                {
                    if (E.Cast())
                    {
                        return;
                    }
                }

                if (Settings.AutoE.KillLegendaryJungle && GameObjects.JungleLegendary.Any(x => x.IsKillableWithE(true)))
                {
                    if (E.Cast())
                    {
                        return;
                    }
                }
            }
        }
    }
}
