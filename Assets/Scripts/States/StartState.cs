using System;
using UniRx;

namespace NinjaJump.States
{
    public class StartState : State
    {
        private const string TapToStartText = "Tap to start";

        private readonly CompositeDisposable _subscriptions = new();

        public StartState(IApplication application) : base(application)
        {
        }

        public override void Enter()
        {
            _application.Platform.SetEnabled(false);
            _application.ScoreText.SetBestScore(_application.ScoreStorage.LoadBestScore());
            _application.HintText.Show(TapToStartText);
            _application.TappableArea.Tap.Subscribe(SwitchToPlayingState).AddTo(_subscriptions);
        }

        public override void Exit() => _subscriptions.Clear();

        private void SwitchToPlayingState(Unit unit) => _application.SwitchState<PlayingState>();
        
        protected bool Equals(StartState other) => Equals(_subscriptions, other._subscriptions);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((StartState)obj);
        }

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), _subscriptions);
    }
}