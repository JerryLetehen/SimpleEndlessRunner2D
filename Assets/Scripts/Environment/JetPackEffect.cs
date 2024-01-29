using System;
using NinjaJump.Character;
using UniRx;

namespace NinjaJump.Environment
{
    /// <summary>
    /// Makes character fly for a duration upon touching
    /// </summary>
    public class JetPackEffect
    {
        private readonly ICharacter _character;
        private readonly TimeSpan _duration;

        public JetPackEffect(ICharacter character, TimeSpan duration)
        {
            _character = character;
            _duration = duration;
        }

        public void Run()
        {
            _character.Fly();
            Observable.Timer(_duration, Scheduler.MainThreadIgnoreTimeScale).Last().Subscribe(Finish);
        }

        private void Finish(long obj)
        {
            _character.Run();
        }
    }
}