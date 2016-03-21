using System;

using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Core.Utils;

using Color = System.Drawing.Color;

namespace EzrealBuddy
{
    internal static class Program
    {
        private const string ChampName = "Ezreal";

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

            Drawing.OnDraw += Drawing_OnDraw;
            Variables.Orbwalker.OnAction += Orbwalker_OnAction;
            Events.OnGapCloser += Events_OnGapCloser;

            Logging.Write()(LogLevel.Info, $"{ChampName}Buddy Loaded successfully!");

            Notifications.Add(new Notification($"{ChampName}Buddy Loaded!", $"{ChampName}Buddy was loaded!", "Good luck, have fun!")
            {
                HeaderTextColor = SharpDX.Color.LightBlue,
                BodyTextColor = SharpDX.Color.White,
                Icon = NotificationIconType.Check,
                IconFlash = true
            });

            Game.PrintChat($"<font size='25' color='#FFE400'>{ChampName}Buddy</font> Loaded.");
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
                case OrbwalkingType.None:
                    break;
                case OrbwalkingType.Movement:
                    break;
                case OrbwalkingType.StopMovement:
                    break;
                case OrbwalkingType.BeforeAttack:
                    break;
                case OrbwalkingType.AfterAttack:
                    Orbwalker_AfterAttack(e);
                    break;
                case OrbwalkingType.OnAttack:
                    break;
                case OrbwalkingType.NonKillableMinion:
                    break;
                case OrbwalkingType.TargetSwitch:
                    break;
            }
        }

        private static void Orbwalker_AfterAttack(OrbwalkingActionArgs e)
        {
            var target = e.Target as Obj_AI_Hero;
            if (target == null)
            {
                return;
            }

            switch (Variables.Orbwalker.GetActiveMode())
            {
                case OrbwalkingMode.None:
                    break;
                case OrbwalkingMode.Combo:
                    if (Config.Modes.Combo.UseQ && SpellManager.Q.IsReady())
                    {
                        SpellManager.Q.Cast(target);
                    }
                    else
                    if (Config.Modes.Combo.UseW && SpellManager.W.IsReady())
                    {
                        SpellManager.W.Cast(target, false, true);
                    }
                    break;
                case OrbwalkingMode.Hybrid:
                    if (Config.Modes.Harass.UseQ && SpellManager.Q.IsReady())
                    {
                        SpellManager.Q.Cast(target);
                    }
                    else
                    if (Config.Modes.Harass.UseW && SpellManager.W.IsReady())
                    {
                        SpellManager.W.Cast(target, false, true);
                    }
                    break;
                case OrbwalkingMode.LastHit:
                    break;
                case OrbwalkingMode.LaneClear:
                    break;
            }
        }

        private static void Events_OnGapCloser(object sender, Events.GapCloserEventArgs e)
        {
            if (Config.Misc.AntiGapcloser.UseE &&
                GameObjects.Player.Distance(e.End) < 200 &&
                !e.Sender.ChampionName.Equals("masteryi", StringComparison.OrdinalIgnoreCase))
            {
                if (SpellManager.E.IsReady())
                {
                    SpellManager.E.Cast(GameObjects.Player.Position.Extend(Game.CursorPos, SpellManager.E.Range));
                }
            }
        }
    }
}
