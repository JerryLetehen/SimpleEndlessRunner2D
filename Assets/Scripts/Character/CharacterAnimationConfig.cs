using UnityEngine;

namespace NinjaJump.Character
{
    [CreateAssetMenu(fileName = nameof(CharacterAnimationConfig), menuName = ConfigNames.Character + nameof(CharacterAnimationConfig))]
    public class CharacterAnimationConfig : ScriptableObject
    {
        [field: SerializeField] public string IdleName { get; private set; } = "Idle";
        [field: SerializeField] public string JumpName { get; private set; } = "Jump";
        [field: SerializeField] public string DoubleJumpName { get; private set; } = "DoubleJump";
        [field: SerializeField] public string RunName { get; private set; } = "Run";
        [field: SerializeField] public string DieName { get; private set; } = "Die";
    }
}