using System;
using UnityEngine;
using UnityEngine.Events;

namespace PlanA_Assets.Scripts
{
    public class ManageScoreAndMoves: MonoBehaviour
    {
        // Added events to separate logic from view
        public UnityAction OnGameOver;
        public UnityAction<int, int> OnScoreChange;
        
        private int _movesAmount = 5;
        private int _scoreAmount;

        private void Start()
        {
            ResetValues();
        }

        public void ResetValues()
        {
            _movesAmount = 5;
            _scoreAmount = 0;
            
            OnScoreChange?.Invoke(_scoreAmount, _movesAmount);
        }

        private void Replay()
        {
            ResetValues();
            OnScoreChange?.Invoke(_scoreAmount, _movesAmount);
        }
        
        // Wrapped to avoid misleading into build
#if UNITY_EDITOR
        public void Test_MakeMoveOnClick()
        {
            HandleScoreAndMoves(scoreToAdd: 10);
        }

        public void Test_ReplayOnClick()
        {
            Replay();
        }
#endif
        public void HandleScoreAndMoves(int scoreToAdd = 1, bool shouldHandleMoves = true)
        {
            if(shouldHandleMoves)
            {
                _movesAmount--;
            }
            
            _scoreAmount += scoreToAdd;
            
            OnScoreChange?.Invoke(_scoreAmount, _movesAmount);
            
            if (_movesAmount <= 0)
            {
                OnGameOver?.Invoke();
            }
        }
    }
}