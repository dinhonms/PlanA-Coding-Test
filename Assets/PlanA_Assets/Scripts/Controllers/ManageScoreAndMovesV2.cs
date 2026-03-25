using System;
using PlanA_Assets.Scripts.Core;

namespace PlanA_Assets.Scripts
{
    public class ManageScoreAndMovesV2: IManageScoreAndMoves
    {
        public event Action<int, int> OnScoreChange;
        public event Action OnGameOver;
        
        private int _movesAmount;
        private int _scoreAmount;

        public ManageScoreAndMovesV2()
        {
            ResetValues();
        }

        public void ResetValues()
        {
            _movesAmount = 5;
            _scoreAmount = 0;
        }

        // Wrapped to avoid misleading into build
#if UNITY_EDITOR
        public void Test_MakeMoveOnClick()
        {
            HandleScoreAndMoves(scoreToAdd: 10);
        }

        public void Test_ReplayOnClick()
        {
            ResetValues();
            OnScoreChange?.Invoke(_scoreAmount, _movesAmount);
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