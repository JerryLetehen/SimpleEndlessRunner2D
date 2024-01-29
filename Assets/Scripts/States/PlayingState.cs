using System;
using UniRx;
using UnityEngine;

namespace NinjaJump.States
{
    public class PlayingState : State
    {
        private readonly CompositeDisposable _subscriptions = new();
        private IDisposable _environmentSpawnSubscription;
        private int _envSpawnCounter;

        public PlayingState(IApplication application) : base(application)
        {
        }

        public override void Enter()
        {
            Time.timeScale = 1;
            _envSpawnCounter = 0;
            _application.Platform.SetEnabled(true);
            _application.Score.Reset();
            _application.ScoreText.SetScore(_application.Score.Value);
            _application.HintText.Hide();
            _application.TappableArea.Tap.Subscribe(ProcessTap).AddTo(_subscriptions);
            _application.Character.Run();

            _environmentSpawnSubscription = _application.Environment.Spawned.Subscribe(OnEnvironmentSpawned);
            _application.Environment.Run(_application);
        }

        private void OnEnvironmentSpawned(Unit obj)
        {
            const string tapToJumpText = "Tap to jump";
            const string doubleTapToDoubleJumpText = "Double tap to double jump";
            
            _envSpawnCounter++;

            switch (_envSpawnCounter)
            {
                case 1:
                    _application.HintText.Show(tapToJumpText);
                    break;
                case 2:
                    _application.HintText.Show(doubleTapToDoubleJumpText);
                    break;
                default:
                    _application.HintText.Hide();
                    _environmentSpawnSubscription.Dispose();
                    break;
            }
        }

        public override void Update(float deltaTime)
        {
            _application.Score.Update(deltaTime);
            _application.ScoreText.SetScore(_application.Score.Value);
            Time.timeScale += _application.GameSpeedIncrease * deltaTime;
        }

        public override void Exit()
        {
            Time.timeScale = 1;
            _application.Environment.Stop();
            _subscriptions.Clear();
            _environmentSpawnSubscription?.Dispose();
            _application.ScoreStorage.SaveScore(_application.Score.Value);

            if (_application.Score.Value > _application.ScoreStorage.LoadBestScore())
            {
                _application.ScoreStorage.SaveBestScore(_application.Score.Value);
                _application.ScoreText.SetBestScore(_application.Score.Value);
            }
        }

        private void ProcessTap(Unit obj) => _application.Character.Jump();
    }
}