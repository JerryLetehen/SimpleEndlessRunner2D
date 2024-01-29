using System;
using UniRx;

namespace NinjaJump.Environment
{
    public interface IEnvironment
    {
        IObservable<Unit> Spawned { get; }
        void Run(IApplication context);
        void Stop();
    }
}