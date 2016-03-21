using LeagueSharp;
using LeagueSharp.SDK;

using Settings = EzrealBuddy.Config.Modes.Flee;

namespace EzrealBuddy.Modes
{
    internal sealed class Flee : ModeBase
    {
        internal override bool ShouldBeExecuted()
        {
            return Config.Keys.FleeActive;
        }

        internal override void Execute()
        {
            Variables.Orbwalker.Move(Game.CursorPos);

            if (Settings.UseE && E.IsReady())
            {
                E.Cast(GameObjects.Player.Position.Extend(Game.CursorPos, E.Range));
            }
        }
    }
}
