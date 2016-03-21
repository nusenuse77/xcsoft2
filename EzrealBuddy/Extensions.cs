using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Core.Wrappers.Damages;

namespace EzrealBuddy
{
    internal static class Extensions
    {
        internal static float GetQDamage(Obj_AI_Base target)
        {
            return SpellManager.Q.GetDamage(target, Damage.DamageStage.Default);
        }

        internal static bool IsKillableWithQ(this Obj_AI_Base target, bool checkRange = true)
        {
            return target.IsValidTarget(checkRange ? SpellManager.Q.Range : 0f) && target.Health + target.PhysicalShield + target.HPRegenRate < GetQDamage(target);
        }
    }
}