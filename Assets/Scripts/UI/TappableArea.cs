using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace NinjaJump.UI
{
    /// <summary>
    /// Provides user input to the game
    /// </summary>
    public class TappableArea : MonoBehaviour, ITappableArea
    {
        [SerializeField] private Button _button;

        public IObservable<Unit> Tap => _button.OnClickAsObservable();
    }
}