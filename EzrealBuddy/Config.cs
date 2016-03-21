using LeagueSharp.SDK;
using LeagueSharp.SDK.Core.UI.IMenu;
using LeagueSharp.SDK.Core.UI.IMenu.Values;

namespace EzrealBuddy
{
    internal static class Config
    {
        private const string MenuName = "EzrealBuddy";

        internal static readonly Menu Menu;

        static Config()
        {
            Menu = new Menu(MenuName, MenuName, true).Attach();

            Keys.Initialize();
            Modes.Initialize();
            Auto.Initialize();
            Hitchance.Initialize();
            Misc.Initialize();
            SkinManager.Initialize(Menu);
            Drawings.Initialize();
        }

        internal static void Initialize() { }

        internal static class Keys
        {
            internal static readonly Menu Menu;

            private static readonly MenuKeyBind _comboKey;
            private static readonly MenuKeyBind _harassKey;
            private static readonly MenuKeyBind _laneClearKey;
            private static readonly MenuKeyBind _jugnleClearKey;
            private static readonly MenuKeyBind _fleeKey;

            internal static bool ComboActive => _comboKey.Active;

            internal static bool HarassActive => _harassKey.Active;

            internal static bool LaneClearActive => _laneClearKey.Active;

            internal static bool JungleClearActive => _jugnleClearKey.Active;

            internal static bool FleeActive => _fleeKey.Active;

            static Keys()
            {
                Menu = Config.Menu.Add(new Menu("Keys", "Keys"));

                _comboKey = Menu.Add(new MenuKeyBind("ComboKey", "Combo", System.Windows.Forms.Keys.Space, KeyBindType.Press));
                _harassKey = Menu.Add(new MenuKeyBind("HarassKey", "Harass", System.Windows.Forms.Keys.C, KeyBindType.Press));
                _laneClearKey = Menu.Add(new MenuKeyBind("LaneClearKey", "LaneClear", System.Windows.Forms.Keys.V, KeyBindType.Press));
                _jugnleClearKey = Menu.Add(new MenuKeyBind("JungleClearKey", "JungleClear", System.Windows.Forms.Keys.V, KeyBindType.Press));
                _fleeKey = Menu.Add(new MenuKeyBind("FleeKey", "Flee", System.Windows.Forms.Keys.T, KeyBindType.Press));
            }

            internal static void Initialize() { }
        }

        internal static class Modes
        {
            internal static readonly Menu Menu;

            static Modes()
            {
                Menu = Config.Menu.Add(new Menu("Modes", "Modes"));

                Combo.Initialize();
                Harass.Initialize();
                LaneClear.Initialize();
                JungleClear.Initialize();
                Flee.Initialize();
            }

            internal static void Initialize() { }

            internal static class Combo
            {
                internal static readonly Menu Menu;

                private static readonly MenuBool _useQ;
                private static readonly MenuBool _useW;
                //private static readonly MenuBool _useE;
                private static readonly MenuSliderButton _useR;

                internal static bool UseQ => _useQ.Value;

                internal static bool UseW => _useW.Value;

                //internal static bool UseE => _useE.Value;

                internal static bool UseR => _useR.BValue;

                internal static int UseRNum => _useR.SValue;

                static Combo()
                {
                    Menu = Modes.Menu.Add(new Menu("Combo", "Combo"));

                    _useQ = Menu.Add(new MenuBool("UseQ", "Use Q", true));
                    _useW = Menu.Add(new MenuBool("UseW", "Use W", true));
                    //_useE = Menu.Add(new MenuBool("UseE", "Use E", true));
                    _useR = Menu.Add(new MenuSliderButton("UseR", "Use R", 3, 1, 6, true));
                }

                internal static void Initialize() { }
            }

            internal static class Harass
            {
                internal static readonly Menu Menu;

                private static readonly MenuBool _useQ;
                private static readonly MenuBool _useW;
                private static readonly MenuSliderButton _minMana;

                internal static bool UseQ => _useQ.Value;

                internal static bool UseW => _useW.Value;

                internal static int MinManaPer => _minMana.Value;

                static Harass()
                {
                    Menu = Modes.Menu.Add(new Menu("Harass", "Harass"));

                    _useQ = Menu.Add(new MenuBool("UseQ", "Use Q", true));
                    _useW = Menu.Add(new MenuBool("UseW", "Use W", true));

                    _minMana = Menu.Add(new MenuSliderButton("Mana", "Min Mana %", 70, 0, 100) { BValue = true });
                }

                internal static void Initialize() { }
            }

            internal static class LaneClear
            {
                internal static readonly Menu Menu;

                private static readonly MenuBool _useQ;
                private static readonly MenuSliderButton _minMana;

                internal static bool UseQ => _useQ.Value;

                internal static int MinManaPer => _minMana.Value;

                static LaneClear()
                {
                    Menu = Modes.Menu.Add(new Menu("LaneClear", "LaneClear"));

                    _useQ = Menu.Add(new MenuBool("UseQ", "Use Q", false));

                    _minMana = Menu.Add(new MenuSliderButton("MinMana", "Min Mana %", 70, 0, 100) { BValue = true });
                }

                internal static void Initialize() { }
            }

            internal static class JungleClear
            {
                internal static readonly Menu Menu;

                private static readonly MenuBool _useQ;
                private static readonly MenuSliderButton _minMana;

                internal static bool UseQ => _useQ.Value;

                internal static int MinManaPer => _minMana.Value;

                static JungleClear()
                {
                    Menu = Modes.Menu.Add(new Menu("JungleClear", "JungleClear"));

                    _useQ = Menu.Add(new MenuBool("UseQ", "Use Q", true));

                    _minMana = Menu.Add(new MenuSliderButton("MinMana", "Min Mana %", 0, 0, 100) { BValue = true });
                }

                internal static void Initialize() { }
            }

            internal static class Flee
            {
                internal static readonly Menu Menu;

                private static readonly MenuBool _useE;

                internal static bool UseE => _useE.Value;

                static Flee()
                {
                    Menu = Modes.Menu.Add(new Menu("Flee", "Flee"));

                    _useE = Menu.Add(new MenuBool("UseE", "Use E", false));
                }

                internal static void Initialize() { }
            }
        }

        internal static class Auto
        {
            internal static readonly Menu Menu;

            static Auto()
            {
                Menu = Config.Menu.Add(new Menu("Auto", "Auto"));

                AutoHarass.Initialize();
            }

            internal static void Initialize() { }

            internal static class AutoHarass
            {
                internal static readonly Menu Menu;

                private static readonly MenuBool _useQ;
                private static readonly MenuBool _useW;
                private static readonly MenuSliderButton _minMana;

                internal static bool UseQ => _useQ.Value;

                internal static bool UseW => _useW.Value;

                internal static int MinManaPer => _minMana.Value;

                static AutoHarass()
                {
                    Menu = Auto.Menu.Add(new Menu("AutoHarass", "Auto Harass"));

                    _useQ = Menu.Add(new MenuBool("UseQ", "Use Q", true));
                    _useW = Menu.Add(new MenuBool("UseW", "Use W", false));

                    _minMana = Menu.Add(new MenuSliderButton("MinMana", "Min Mana %", 50, 0, 100) { BValue = true });
                }

                internal static void Initialize() { }
            }
        }

        internal static class Hitchance
        {
            internal static readonly Menu Menu;

            private static readonly MenuList<HitChance> _QHitchance;
            private static readonly MenuList<HitChance> _WHitchance;
            private static readonly MenuList<HitChance> _RHitchance;

            internal static HitChance QHitChance => _QHitchance.SelectedValue;

            internal static HitChance WHitChance => _WHitchance.SelectedValue;

            internal static HitChance RHitChance => _RHitchance.SelectedValue;

            static Hitchance()
            {
                Menu = Config.Menu.Add(new Menu("Hitchance", "Hitchance"));

                _QHitchance = Menu.Add(new MenuList<HitChance>("QHitchance", "Q Hitchance", new[] { HitChance.Medium, HitChance.High, HitChance.VeryHigh }) { SelectedValue = HitChance.High });
                _QHitchance.ValueChanged += (sender, args) =>
                {
                    SpellManager.Q.MinHitChance = _QHitchance.SelectedValue;
                };

                _WHitchance = Menu.Add(new MenuList<HitChance>("WHitchance", "W Hitchance", new[] { HitChance.Medium, HitChance.High, HitChance.VeryHigh }) { SelectedValue = HitChance.High });
                _WHitchance.ValueChanged += (sender, args) =>
                {
                    SpellManager.W.MinHitChance = _WHitchance.SelectedValue;
                };

                _RHitchance = Menu.Add(new MenuList<HitChance>("RHitchance", "R Hitchance", new[] { HitChance.Medium, HitChance.High, HitChance.VeryHigh }) { SelectedValue = HitChance.High });
                _RHitchance.ValueChanged += (sender, args) =>
                {
                    SpellManager.R.MinHitChance = _RHitchance.SelectedValue;
                };
            }

            internal static void Initialize() { }
        }

        internal static class Misc
        {
            internal static readonly Menu Menu;

            static Misc()
            {
                Menu = Config.Menu.Add(new Menu("Misc", "Misc"));

                AntiGapcloser.Initialize();
            }

            internal static void Initialize() { }

            internal static class AntiGapcloser
            {
                internal static readonly Menu Menu;

                private static readonly MenuBool _useE;
                private static readonly MenuSliderButton _minMana;

                internal static bool UseE => _useE.Value;

                static AntiGapcloser()
                {
                    Menu = Misc.Menu.Add(new Menu("AntiGapcloser", "Anti-Gapcloser"));

                    _useE = Menu.Add(new MenuBool("UseE", "Use E to cursor", true));
                }

                internal static void Initialize() { }
            }
        }

        internal static class Drawings
        {
            internal static readonly Menu Menu;

            private static readonly MenuBool _drawQRange;
            private static readonly MenuBool _drawWRange;
            private static readonly MenuBool _drawERange;
            private static readonly MenuBool _drawRRange;

            internal static bool DrawQRange => _drawQRange.Value;

            internal static bool DrawWRange => _drawWRange.Value;

            internal static bool DrawERange => _drawERange.Value;

            internal static bool DrawRRange => _drawRRange.Value;

            static Drawings()
            {
                Menu = Config.Menu.Add(new Menu("Drawings", "Drawings"));

                Menu.Add(new MenuSeparator("SpellRange", "Spell Range"));

                _drawQRange = Menu.Add(new MenuBool("drawQRange", "Draw Q Range"));
                _drawWRange = Menu.Add(new MenuBool("drawWRange", "Draw W Range"));
                _drawERange = Menu.Add(new MenuBool("drawERange", "Draw E Range"));
                _drawRRange = Menu.Add(new MenuBool("drawRRange", "Draw R Range"));
            }

            internal static void Initialize() { }
        }
    }
}
