using System;
using UnityEngine;

namespace NinjaJump.Environment
{
    /// <summary>
    /// Runs jet pack effect upon touching the character
    /// </summary>
    public class JetPack : EnvironmentObject
    {
        [SerializeField] private float _duration;

        protected override void OnCharacterTouchedHandler()
        {
            new JetPackEffect(_context.Character, TimeSpan.FromSeconds(_duration)).Run();
        }
    }
}