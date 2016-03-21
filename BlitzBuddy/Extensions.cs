using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Core.Utils;
using LeagueSharp.SDK.Core.Wrappers.Damages;

namespace BlitzBuddy
{
    internal static class Extensions
    {
        internal static float GetQDamage(this Obj_AI_Base target)
        {
            return SpellManager.Q.GetDamage(target, Damage.DamageStage.Default);
        }

        internal static float GetRDamage(this Obj_AI_Base target)
        {
            return SpellManager.R.GetDamage(target, Damage.DamageStage.Default);
        }

        internal static bool IsKillablewithR(this Obj_AI_Hero target, bool rangeCheck = false)
        {
            return target.IsValidTarget(rangeCheck ? SpellManager.R.Range : float.MaxValue) &&
                target.GetRDamage() > target.Health + target.MagicalShield + target.HPRegenRate &&
                !Invulnerable.Check(target, DamageType.Magical, false);
        }
    }
}
