using System;
using UnityEngine.Events;
using UnityEngine.Video;

namespace PlanA_Assets.Scripts.Core
{
    public interface IManageScoreAndMoves
    {
        event Action<int, int> OnScoreChange;
        event Action OnGameOver;

        void ResetValues();
        void HandleScoreAndMoves(int scoreToAdd = 1, bool shouldHandleMoves = true);
    }
}