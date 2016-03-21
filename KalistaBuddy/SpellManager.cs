using LeagueSharp;
using LeagueSharp.SDK;

namespace KalistaBuddy
{
    internal static class SpellManager
    {
        internal static readonly Spell Q, W, E, R;
        internal static readonly PredictionInput QCollisionInput;

        static SpellManager()
        {
            Q = new Spell(SpellSlot.Q, 1180f) { MinHitChance = Config.Hitchance.QHitChance };
            W = new Spell(SpellSlot.W, 5100f);
            E = new Spell(SpellSlot.E, 1000f);
            R = new Spell(SpellSlot.R, 1400f);

            Q.SetSkillshot(0.25f, 40f, 2400f, true, SkillshotType.SkillshotLine);

            QCollisionInput = new PredictionInput
            {
                Delay = Q.Delay,
                Radius = Q.Width,
                Speed = Q.Speed,
                Range = Q.Range,
                CollisionObjects = CollisionableObjects.Minions | CollisionableObjects.Heroes
            };
        }

        internal static void Initialize() { }
    }
}
