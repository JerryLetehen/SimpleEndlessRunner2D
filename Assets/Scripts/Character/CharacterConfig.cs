using UnityEngine;

namespace NinjaJump.Character
{
    [CreateAssetMenu(fileName = nameof(CharacterConfig), menuName = ConfigNames.Character + nameof(CharacterConfig))]
    public class CharacterConfig : ScriptableObject
    {
        [field: SerializeField] public float JumpPower { get; private set; } = 3;
        [field: SerializeField] public ForceMode2D ForceMode { get; private set; } = ForceMode2D.Impulse;
        [field: SerializeField] public float FlyingDuration { get; private set; } = 0.2f;
    }
}