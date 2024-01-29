using UnityEngine;

namespace NinjaJump.Environment
{
    /// <summary>
    /// Used to detect character on ground and enable its jump
    /// </summary>
    public class Platform : MonoBehaviour, IPlatform
    {
        [SerializeField] private Character.Character _character;

        private bool _enabled;
        private Collision2D _currentCollision;

        private void TryEnableCharacterJump(Collision2D collision)
        {
            if (_enabled && collision?.gameObject == _character.gameObject)
            {
                _character.EnableJump();
                _character.Run();
            }
        }

        private void OnCollisionEnter2D(Collision2D other) => TryEnableCharacterJump(other);

        private void OnCollisionStay2D(Collision2D other) => _currentCollision = other;

        private void OnCollisionExit2D(Collision2D other) => _currentCollision = null;

        public void SetEnabled(bool value)
        {
            _enabled = value;
            TryEnableCharacterJump(_currentCollision);
        }
    }
}