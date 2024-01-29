using UnityEngine;

namespace NinjaJump.Environment
{
    [CreateAssetMenu(fileName = nameof(EnvironmentConfig),
        menuName = ConfigNames.Environment + nameof(EnvironmentConfig))]
    public class EnvironmentConfig : ScriptableObject
    {
        [field: SerializeField] public float SpawnDelay { get; private set; } = 2f;
        [field: SerializeField] public float Speed { get; private set; } = 3f;
        [field: SerializeField] public EnvironmentObject[] Prefabs { get; private set; }
    }
}