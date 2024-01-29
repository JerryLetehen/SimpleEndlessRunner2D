using NinjaJump.Character;
using NinjaJump.Environment;
using NinjaJump.Score;
using NinjaJump.States;
using NinjaJump.UI;

namespace NinjaJump
{
    public interface IApplication
    {
        Score.Score Score { get; }
        ScoreStorage ScoreStorage { get; }
        IScoreText ScoreText { get; }
        IHintText HintText { get; }
        ITappableArea TappableArea { get; }
        ICharacter Character { get; }
        IEnvironment Environment { get; }
        IPlatform Platform { get; }
        float GameSpeedIncrease { get; }
        void SwitchState<T>() where T : State;
    }
}