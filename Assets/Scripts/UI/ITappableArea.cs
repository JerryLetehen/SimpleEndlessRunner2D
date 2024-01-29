using System;
using UniRx;

namespace NinjaJump.UI
{
    public interface ITappableArea
    {
        IObservable<Unit> Tap { get; }
    }
}