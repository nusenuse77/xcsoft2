using System.Drawing;
using System.Linq;

using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Core.Utils;
using LeagueSharp.SDK.Core.Wrappers.Damages;

using Settings = KalistaBuddy.Config.Auto.AutoR;

namespace KalistaBuddy
{
    internal static class OathswornManager
    {
        private static Obj_AI_Hero _oathsworn;
        private static readonly TickOperation OathswornDetector;

        static OathswornManager()
        {
            Obj_AI_Base.OnBuffAdd += Obj_AI_Base_OnBuffAdd;
            Drawing.OnDraw += Drawing_OnDraw;

            OathswornDetector = new TickOperation(0x3E8, () =>
            {
                //Detect Oathsworn
                if (_oathsworn == null)
                {
                    _oathsworn = GameObjects.AllyHeroes.FirstOrDefault(x => x.IsMyOathsworn());
                }
                else
                {
                    OathswornDetector.Dispose();
                }
            }, true).Start(true);

            //new TickOperation(0x42, () =>
            //{
            //    //Save Oathsworn
            //    if (_oathsworn == null || !Settings.SaveOathsworn)
            //    {
            //        return;
            //    }

            //    foreach (var skillshot in Tracker.DetectedSkillshots.Where(x => x.Caster.IsEnemy && x.IsAboutToHit(_oathsworn, 1000)))
            //    {
            //        Logging.Write()(LogLevel.Debug, (skillshot.Caster as Obj_AI_Hero).GetSpellDamage(_oathsworn, skillshot.SData.Slot));
            //    }

            //}).Start();
        }

        internal static void Initialize() { }

        private static bool IsMyOathsworn(this Obj_AI_Base unit)
        {
            var buff = unit?.GetBuff("kalistacoopstrikeally");
            return buff != null && buff.Caster.IsMe;
        }

        private static void Obj_AI_Base_OnBuffAdd(Obj_AI_Base sender, Obj_AI_BaseBuffAddEventArgs args)
        {
            var senderHero = sender as Obj_AI_Hero;
            if (senderHero == null)
                return;

            if (_oathsworn == null && args.Buff.Name == "kalistacoopstrikeally" && args.Buff.Caster.IsMe)
            {
                _oathsworn = senderHero;
            }

            if (_oathsworn == null)
                return;

            if (Config.Auto.AutoR.Balista && args.Buff.Name == "rocketgrab2" && args.Buff.Caster.NetworkId == _oathsworn.NetworkId && !sender.IsDead)
            {
                SpellManager.R.Cast();
                Logging.Write()(LogLevel.Info, "Trying Balista");
            }
        }

        private static void Drawing_OnDraw(System.EventArgs args)
        {
            if (Config.Drawings.DrawOathswornPosition && _oathsworn != null)
            {
                Drawing.DrawCircle(_oathsworn.Position, _oathsworn.BoundingRadius, Color.Aqua);
            }

            //Tracker.DetectedSkillshots.ForEach(skillshot =>
            //{
            //    skillshot.Draw(Color.AliceBlue, Color.Aqua);
            //});
        }
    }
}
