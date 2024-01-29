using UnityEngine;

namespace NinjaJump.Character
{
    /// <summary>
    /// Character animation hashes, used to optimize accessing on parameters
    /// </summary>
    internal class AnimationHashes
    {
        public readonly int Idle;
        public readonly int Jump;
        public readonly int DoubleJump;
        public readonly int Run;
        public readonly int Die;

        public AnimationHashes(CharacterAnimationConfig config)
        {
            Idle = Animator.StringToHash(config.IdleName);
            Jump = Animator.StringToHash(config.JumpName);
            DoubleJump = Animator.StringToHash(config.DoubleJumpName);
            Run = Animator.StringToHash(config.RunName);
            Die = Animator.StringToHash(config.DieName);
        }
    }
}