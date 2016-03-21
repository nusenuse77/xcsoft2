using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Core.Utils;

namespace KalistaBuddy
{
    internal static class Program
    {
        private const string ChampName = "Kalista";

        internal static void Main(string[] args)
        {
            Events.OnLoad += Load_OnLoad;
        }

        private static void Load_OnLoad(object sender, EventArgs e)
        {
            if (ObjectManager.Player.ChampionName != ChampName)
            {
                return;
            }

            Config.Initialize();
            SpellManager.Initialize();
            ModeManager.Initialize();
            OathswornManager.Initialize();
            SoulHandler.Initialize();

            DamageIndicator.Initialize(new List<DamageIndicator.DamageInfo>
            {
                new DamageIndicator.DamageInfo("Q", () => Config.Drawings.QDamageColor, unit => SpellManager.Q.IsReady() ? (float)unit.GetQDamage() : 0f, () => Config.Drawings.DrawQDamage),
                new DamageIndicator.DamageInfo("E", () => Config.Drawings.EDamageColor, unit => SpellManager.E.IsReady() ? (float)unit.GetEDamage() : 0f, () => Config.Drawings.DrawEDamage)
            }, () => Config.Drawings.DamageIndicatorEnabled, () => Config.Drawings.HerosEnabled, () => Config.Drawings.JunglesEnabled);

            Drawing.OnDraw += Drawing_OnDraw;
            Variables.Orbwalker.OnAction += Orbwalker_OnAction;

            Logging.Write()(LogLevel.Info, "KalistaBuddy Loaded successfully!");

            Notifications.Add(new Notification("KalistaBuddy Loaded!", "KalistaBuddy was loaded!", "Good luck, have fun!")
            {
                HeaderTextColor = SharpDX.Color.LightBlue,
                BodyTextColor = SharpDX.Color.White,
                Icon = NotificationIconType.Check,
                IconFlash = true
            });
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (Config.Drawings.DrawQRange)
            {
                Drawing.DrawCircle(GameObjects.Player.Position, SpellManager.Q.Range, Color.DeepSkyBlue);
            }

            if (Config.Drawings.DrawWRange)
            {
                Drawing.DrawCircle(GameObjects.Player.Position, SpellManager.W.Range, Color.DeepSkyBlue);
            }

            if (Config.Drawings.DrawERange)
            {
                Drawing.DrawCircle(GameObjects.Player.Position, SpellManager.E.Range, Color.DeepSkyBlue);
            }

            if (Config.Drawings.DrawRRange)
            {
                Drawing.DrawCircle(GameObjects.Player.Position, SpellManager.R.Range, Color.DeepSkyBlue);
            }
        }

        private static void Orbwalker_OnAction(object sender, OrbwalkingActionArgs e)
        {
            switch (e.Type)
            {
                case OrbwalkingType.NonKillableMinion:
                    Orbwalker_NonKillableMinion(e);
                    break;
            }
        }

        private static void Orbwalker_NonKillableMinion(OrbwalkingActionArgs e)
        {
            var target = e.Target as Obj_AI_Minion;

            if (target != null &&
                Config.Auto.AutoE.KillUnkillableMinions &&
                SpellManager.E.IsReady() &&
                GameObjects.Player.ManaPercent > Config.Auto.AutoE.KillUnkillableMinionsMinMana &&
                target.IsKillableWithE(true))
            {
                SpellManager.E.Cast();
            }
        }
    }
}
