using System;
using System.Globalization;
using UnityEngine;

namespace NinjaJump.Score
{
    public class ScoreStorage
    {
        private const string ScoreKey = "score";
        private const string BestScoreKey = "best_score";
        private const string DefaultValue = "0";
        private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

        public void SaveScore(double score) => PlayerPrefs.SetString(ScoreKey, score.ToString(Culture));

        public void SaveBestScore(double score) => PlayerPrefs.SetString(BestScoreKey, score.ToString(Culture));

        public double LoadScore() => Convert.ToDouble(PlayerPrefs.GetString(ScoreKey, DefaultValue));

        public double LoadBestScore() => Convert.ToDouble(PlayerPrefs.GetString(BestScoreKey, DefaultValue));
    }
}