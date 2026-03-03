using System;
using UnityEngine;

namespace PlanA_Assets.Scripts
{
    public class HudController: MonoBehaviour
    {
        [SerializeField] private Canvas _gameOverCanvas;

        private void Start()
        {
            _gameOverCanvas.enabled = false;
        }

        public void HandleGameOver()
        {
            _gameOverCanvas.enabled = true;
        }
    }
}