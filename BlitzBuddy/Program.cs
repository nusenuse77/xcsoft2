using System;
using System.Collections.Generic;

using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Core.Utils;
using LeagueSharp.SDK.Core.Wrappers.Damages;

using SharpDX;

namespace BlitzBuddy
{
    internal static class Program
    {
        private const string ChampName = "Blitzcrank";
        private static bool DontAutoAttack = false;

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

            DamageIndicator.Initialize(new List<DamageIndicator.DamageInfo>
            {
                new DamageIndicator.DamageInfo
                {
                    Tag = "Q",
                    Enabled = () => Config.Drawings.DrawQDamage,
                    Color = () => Config.Drawings.QDamageColor,
                    DamageCalcFunc = unit =>
                    {
                        if (SpellManager.Q.IsReady())
                        {
                            return SpellManager.Q.GetDamage(unit);
                        }
                        return 0;
                    }
                },
                new DamageIndicator.DamageInfo
                {
                    Tag = "R",
                    Enabled = () => Config.Drawings.DrawRDamage,
                    Color = () => Config.Drawings.RDamageColor,
                    DamageCalcFunc = unit =>
                    {
                        if (SpellManager.R.IsReady())
                        {
                            return SpellManager.R.GetDamage(unit);
                        }
                        return 0;
                    }
                },
                new DamageIndicator.DamageInfo
                {
                    Tag = "AutoAttack",
                    Enabled = () => Config.Drawings.DrawAutoAttackDamage,
                    Color = () => Config.Drawings.AutoAttackDamageColor,
                    DamageCalcFunc = unit =>
                    {
                        if (Variables.Orbwalker.CanAttack())
                        {
                            return (float)GameObjects.Player.GetAutoAttackDamage(unit);
                        }
                        return 0;
                    }
                }
            },
            () => Config.Drawings.DamageIndicatorEnabled,
            () => Config.Drawings.HerosEnabled,
            () => Config.Drawings.JunglesEnabled);

            Drawing.OnDraw += Drawing_OnDraw;
            Variables.Orbwalker.OnAction += Orbwalker_OnAction;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
            Obj_AI_Base.OnPlayAnimation += Obj_AI_Base_OnPlayAnimation;
            Obj_AI_Base.OnBuffAdd += Obj_AI_Base_OnBuffAdd;
            Events.OnGapCloser += Events_OnGapCloser;
            Events.OnInterruptableTarget += Events_OnInterruptableTarget;

            Logging.Write()(LogLevel.Info, $"BlitzBuddy Loaded successfully!");

            Notifications.Add(new Notification($"BlitzBuddy Loaded!", $"BlitzBuddy was loaded!", "Good luck, have fun!")
            {
                HeaderTextColor = Color.LightBlue,
                BodyTextColor = Color.White,
                Icon = NotificationIconType.Check,
                IconFlash = true
            });
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (Config.Drawings.DrawQRange)
            {
                Drawing.DrawCircle(GameObjects.Player.Position, SpellManager.Q.Range, System.Drawing.Color.DeepSkyBlue);
            }

            //if (Config.Drawings.DrawWRange)
            //{
            //    Drawing.DrawCircle(GameObjects.Player.Position, SpellManager.W.Range, Color.DeepSkyBlue);
            //}

            //if (Config.Drawings.DrawERange)
            //{
            //    Drawing.DrawCircle(GameObjects.Player.Position, SpellManager.E.Range, Color.DeepSkyBlue);
            //}

            if (Config.Drawings.DrawRRange)
            {
                Drawing.DrawCircle(GameObjects.Player.Position, SpellManager.R.Range, System.Drawing.Color.DeepSkyBlue);
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
                    Orbwalker_BeforeAttack(e);
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

        private static void Orbwalker_BeforeAttack(OrbwalkingActionArgs e)
        {
            var targetAsHero = e.Target as Obj_AI_Hero;

            if (DontAutoAttack)
            {
                e.Process = false;
            }

            switch (Variables.Orbwalker.ActiveMode)
            {
                case OrbwalkingMode.None:
                    break;
                case OrbwalkingMode.Combo:
                    if (targetAsHero != null)
                    {
                        if (Config.Modes.Combo.UseW && SpellManager.W.IsReady() && GameObjects.Player.ManaPercent > Config.Modes.Combo.WMinManaPer)
                        {
                            SpellManager.W.Cast();
                        }
                    }
                    break;
                case OrbwalkingMode.Hybrid:
                    break;
                case OrbwalkingMode.LastHit:
                    break;
                case OrbwalkingMode.LaneClear:
                    break;
            }
        }

        private static void Orbwalker_AfterAttack(OrbwalkingActionArgs e)
        {
            var targetAsHero = e.Target as Obj_AI_Hero;

            switch (Variables.Orbwalker.GetActiveMode())
            {
                case OrbwalkingMode.None:
                    break;
                case OrbwalkingMode.Combo:
                    if (targetAsHero != null)
                    {
                        if (Config.Modes.Combo.UseE && SpellManager.E.IsReady())
                        {
                            SpellManager.E.Cast();
                        }
                    }
                    break;
                case OrbwalkingMode.Hybrid:
                    if (targetAsHero != null)
                    {
                        if (GameObjects.Player.ManaPercent > Config.Modes.Harass.MinMana)
                        {
                            if (Config.Modes.Harass.UseE && SpellManager.E.IsReady())
                            {
                                SpellManager.E.Cast();
                            }
                        }
                    }
                    break;
                case OrbwalkingMode.LastHit:
                    break;
                case OrbwalkingMode.LaneClear:
                    break;
            }
        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe)
            {
                if (args.Slot == SpellSlot.E)
                {
                    Variables.Orbwalker.ResetSwingTimer();
                }
            }
        }

        private static void Obj_AI_Base_OnPlayAnimation(Obj_AI_Base sender, GameObjectPlayAnimationEventArgs args)
        {
            if (sender.IsMe)
            {
                if (args.Animation == "Spell1" || args.Animation == "Spell4")
                {
                    DontAutoAttack = true;
                }
                else
                {
                    DontAutoAttack = false;
                }
            }
        }

        private static void Obj_AI_Base_OnBuffAdd(Obj_AI_Base sender, Obj_AI_BaseBuffAddEventArgs args)
        {
            var senderAsHero = sender as Obj_AI_Hero;

            if (Config.Auto.AutoE.AutoE1 &&
                senderAsHero != null &&
                args.Buff.Name.Equals("rocketgrab2", StringComparison.OrdinalIgnoreCase) &&
                args.Buff.Caster.IsMe)
            {
                SpellManager.E.Cast();
            }
        }

        private static void Events_OnGapCloser(object sender, Events.GapCloserEventArgs e)
        {
            if (!Config.Misc.AntiGapcloser.Enabled)
            {
                return;
            }

            if (Config.Misc.AntiGapcloser.UseR &&
                SpellManager.R.IsReady() &&
                e.Sender.IsValidTarget(SpellManager.R.Range))
            {
                SpellManager.R.Cast();
            }
        }

        private static void Events_OnInterruptableTarget(object sender, Events.InterruptableTargetEventArgs e)
        {
            if (!Config.Misc.AutoInterrupt.Enabled)
            {
                return;
            }

            if (Config.Misc.AutoInterrupt.UseQ &&
                SpellManager.Q.IsReady() &&
                e.Sender.IsValidTarget(SpellManager.Q.Range))
            {
                SpellManager.Q.Cast();
            }

            if (Config.Misc.AutoInterrupt.UseR &&
                SpellManager.R.IsReady() &&
                e.Sender.IsValidTarget(SpellManager.R.Range))
            {
                SpellManager.R.Cast();
            }
        }
    }
}
