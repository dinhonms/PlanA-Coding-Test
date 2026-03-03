using System;
using UnityEngine;

namespace PlanA_Assets.Scripts
{
    public class ManageScoreAndMoves: MonoBehaviour
    {
        [SerializeField] private HudController _hudController;
        [SerializeField] private TMPro.TextMeshProUGUI scoreValue;
        [SerializeField] private TMPro.TextMeshProUGUI movesValue;

        private int _movesAmount = 5;
        private int _scoreAmount;

        private void Start()
        {
            ResetValues();
        }

        private void ResetValues()
        {
            _movesAmount = 5;
            _scoreAmount = 0;
            UpdateUi();
        }

        private void UpdateUi()
        {
            scoreValue.SetText(_scoreAmount.ToString());
            movesValue.SetText(_movesAmount.ToString());
        }

        public void Replay()
        {
            ResetValues();
            UpdateUi();
        }
        
#if UNITY_EDITOR
        public void Test_MakeMoveOnClick()
        {
            _movesAmount--;
            _scoreAmount += 10;
            
            UpdateUi();
            
            if (_movesAmount <= 0)
            {
                _hudController.HandleGameOver();
            }

        }

        public void Test_ReplayOnClick()
        {
            Replay();
        }
#endif
    }
}