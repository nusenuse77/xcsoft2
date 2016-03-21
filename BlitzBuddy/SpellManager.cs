using LeagueSharp;
using LeagueSharp.SDK;

namespace BlitzBuddy
{
    internal static class SpellManager
    {
        internal static readonly Spell Q,
                                       W,
                                       E,
                                       R;

        static SpellManager()
        {
            Q = new Spell(SpellSlot.Q, 925f) { MinHitChance = Config.Hitchance.QHitChance };
            W = new Spell(SpellSlot.W);
            E = new Spell(SpellSlot.E);
            R = new Spell(SpellSlot.R, 550f);

            Q.SetSkillshot(0.25f, 70f, 1800f, true, SkillshotType.SkillshotLine);
        }

        internal static void Initialize() { }
    }
}
