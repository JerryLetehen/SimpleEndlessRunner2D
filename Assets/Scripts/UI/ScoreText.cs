using System;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace NinjaJump.UI
{
    public class ScoreText : MonoBehaviour, IScoreText
    {
        [SerializeField] private TextMeshProUGUI _scoreField;
        [SerializeField] private TextMeshProUGUI _bestScoreField;

        private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

        public void SetScore(double value)
        {
            value = Math.Floor(value);
            _scoreField.SetText(value.ToString(Culture));
        }

        public void SetBestScore(double value)
        {
            value = Math.Floor(value);
            _bestScoreField.SetText(value.ToString(Culture));
        }
    }
}