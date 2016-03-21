using LeagueSharp;
using LeagueSharp.SDK;

namespace EzrealBuddy
{
    internal static class SpellManager
    {
        internal static readonly Spell Q, W, E, R;

        static SpellManager()
        {
            Q = new Spell(SpellSlot.Q, 1200f) { MinHitChance = Config.Hitchance.QHitChance };
            W = new Spell(SpellSlot.W, 1000f) { MinHitChance = Config.Hitchance.WHitChance };
            E = new Spell(SpellSlot.E, 475f);
            R = new Spell(SpellSlot.R, 3000f) { MinHitChance = Config.Hitchance.RHitChance };

            Q.SetSkillshot(0.25f, 60f, 2000f, true, SkillshotType.SkillshotLine);
            W.SetSkillshot(0.25f, 80f, 1600f, false, SkillshotType.SkillshotLine);
            R.SetSkillshot(1.0f, 160f, 2000f, false, SkillshotType.SkillshotLine);
        }

        internal static void Initialize() { }
    }
}