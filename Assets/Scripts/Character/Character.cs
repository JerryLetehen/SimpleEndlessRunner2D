using DG.Tweening;
using UnityEngine;

namespace NinjaJump.Character
{
    public class Character : MonoBehaviour, ICharacter
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private CharacterConfig _config;
        [SerializeField] private CharacterAnimationConfig _animationConfig;
        [SerializeField] private Transform _flyingPos;

        private AnimationHashes _animationHashes;
        private int _jumpValue;

        private AnimationHashes AnimationHashes => _animationHashes ??= new AnimationHashes(_animationConfig);

        /// <summary>
        /// Jumps is limited to 2, until reaches the ground 
        /// </summary>
        public void Jump()
        {
            if (_jumpValue >= 2) return;
            if (_jumpValue < 0) return;

            _jumpValue++;

            var force = Vector2.up * _config.JumpPower;
            _rigidbody.AddForce(force, _config.ForceMode);

            if (_jumpValue == 1) _animator.SetTrigger(_animationHashes.Jump);
            if (_jumpValue == 2) _animator.SetTrigger(_animationHashes.DoubleJump);
        }

        public void Fly()
        {
            DisableJump();
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.isKinematic = true;
            transform.DOMove(_flyingPos.position, _config.FlyingDuration);
        }

        public void Die() => _animator.SetTrigger(AnimationHashes.Die);

        public void Run()
        {
            _rigidbody.isKinematic = false;
            _animator.SetTrigger(AnimationHashes.Run);
        }

        public void Idle() => _animator.SetTrigger(AnimationHashes.Idle);

        public void EnableJump() => _jumpValue = 0;
        private void DisableJump() => _jumpValue = -1;
    }
}