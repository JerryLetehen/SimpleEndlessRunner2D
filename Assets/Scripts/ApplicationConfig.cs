using UnityEngine;

namespace NinjaJump
{
    [CreateAssetMenu(fileName = nameof(ApplicationConfig),
        menuName = ConfigNames.Application + nameof(ApplicationConfig))]
    public class ApplicationConfig : ScriptableObject
    {
        [field: SerializeField] public float ScoreSpeed { get; private set; } = 1f;
        [field: SerializeField] public float GameSpeedIncrease { get; private set; } = 1f / 60;
    }
}