using NinjaJump.States;

namespace NinjaJump.Environment
{
    /// <summary>
    /// Kills character and ends the game loop once touched the character
    /// </summary>
    public class Obstacle : EnvironmentObject
    {
        protected override void OnCharacterTouchedHandler()
        {
            _context.Character.Die();
            _context.SwitchState<StartState>();
        }
    }
}