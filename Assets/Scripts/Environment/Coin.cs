using UnityEngine;

namespace NinjaJump.Environment
{
    /// <summary>
    /// Adds score when touches a character
    /// </summary>
    public class Coin : EnvironmentObject
    {
        [SerializeField] private double _scoreToAdd = 10;

        protected override void OnCharacterTouchedHandler()
        {
            _context.Score.Add(_scoreToAdd);
        }
    }
}