using System;
using System.Collections.Generic;
using NinjaJump.Character;
using NinjaJump.Environment;
using NinjaJump.Score;
using NinjaJump.States;
using NinjaJump.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace NinjaJump
{
    /// <summary>
    /// Entry point, used as a context to states
    /// </summary>
    public class Application : MonoBehaviour, IApplication
    {
        [SerializeField] private ApplicationConfig _applicationConfig;
        [SerializeField] private ScoreText _scoreText;
        [SerializeField] private HintText _hintText;
        [SerializeField] private TappableArea _tappableArea;
        [SerializeField] private Character.Character _character;
        [SerializeField] private Environment.Environment _environment;
        [SerializeField] private Platform _platform;

        public Score.Score Score { get; private set; }
        public ScoreStorage ScoreStorage { get; } = new();
        public IScoreText ScoreText => _scoreText;
        public IHintText HintText => _hintText;
        public ITappableArea TappableArea => _tappableArea;
        public ICharacter Character => _character;
        public IEnvironment Environment => _environment;
        public IPlatform Platform => _platform;
        public float GameSpeedIncrease => _applicationConfig.GameSpeedIncrease;

        private Dictionary<Type, State> _states;
        private State _state;

        private void Awake()
        {
            Score = new Score.Score(_applicationConfig.ScoreSpeed);
            Score.Add(ScoreStorage.LoadScore());
            
            _states = new Dictionary<Type, State>(2)
            {
                { typeof(StartState), new StartState(this) },
                { typeof(PlayingState), new PlayingState(this) },
            };

            SwitchState<StartState>();
        }

        public void SwitchState<T>() where T : State
        {
            _state?.Exit();
            _state = _states[typeof(T)];
            _state.Enter();
        }

        private void Update() => _state?.Update(Time.deltaTime);

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.Label(_platform.transform.position, "Speed: " + Time.timeScale);
        }
#endif
    }
}