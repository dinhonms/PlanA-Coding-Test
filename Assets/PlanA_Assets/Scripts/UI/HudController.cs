using System.Threading.Tasks;
using PlanA_Assets.Scripts.Core;
using UnityEngine;

namespace PlanA_Assets.Scripts
{
    public class HudController: MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private SpriteFactory spriteFactory;
        [SerializeField] private Canvas gameOverCanvas;
        [SerializeField] private Canvas transientCanvas;
        [SerializeField] private BlockPrefab[] blockPrefabs;
        [SerializeField] private int transientScreenDelaySeconds = 1;

        [Header("UI References")]
        [SerializeField] private TMPro.TextMeshProUGUI scoreValue;
        [SerializeField] private TMPro.TextMeshProUGUI gameOverScoreValue;
        [SerializeField] private TMPro.TextMeshProUGUI movesValue;

        private IManageScoreAndMoves _manageScoreAndMoves;
        
        private void Start()
        {
            InitializeGame();
        }

        private void OnDestroy()
        {
            DeInitializeGame();
        }

        public void Replay()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            _manageScoreAndMoves.OnGameOver += OnGameOver;
            _manageScoreAndMoves.OnScoreChange += OnScoreChange;
            
            gameOverCanvas.enabled = false;
            
            InitializeBlockColors();
        }

        private void OnGameOver()
        {
            gameOverCanvas.enabled = true;
        }

        private void DeInitializeGame()
        {
            _manageScoreAndMoves.OnGameOver -= OnGameOver;
            _manageScoreAndMoves.OnScoreChange -= OnScoreChange;
            _manageScoreAndMoves = null;
        }

        /// <summary>
        /// I'd improve here separating this into a logic components, hud ideally deals with UI
        /// </summary>
        private void InitializeBlockColors()
        {
            foreach (BlockPrefab blockPrefab in blockPrefabs)
            {
                blockPrefab
                    .SetData(spriteFactory.GetSortedSprite(), this)
                    .SetEnabled();
            }
        }

        public void AddScore(bool shouldHandleMoves = true)
        {
            _manageScoreAndMoves.HandleScoreAndMoves(shouldHandleMoves: shouldHandleMoves);
        }

        private void OnScoreChange(int scoreAmount, int movesAmount)
        {
            scoreValue.SetText(scoreAmount.ToString());
            gameOverScoreValue.SetText(scoreAmount.ToString());
            movesValue.SetText(movesAmount.ToString());

            if(movesAmount >0)
            {
                _ = HandleTransientScreen();
            }
        }

        /// <summary>
        /// Tasks are better than Coroutines (avoid than as much as possible)
        /// </summary>
        private async Task HandleTransientScreen()
        {
            await Task.Delay(100);
            transientCanvas.enabled = true;

            await Task.Delay(transientScreenDelaySeconds * 1000);
            
            RespawnCollectedBlocks();
            transientCanvas.enabled = false;
        }

        /// <summary>r
        /// We surely don't need to iterate over all blocks, for sure something to improve
        /// </summary>
        private void RespawnCollectedBlocks()
        {
            foreach (BlockPrefab blockPrefab in blockPrefabs)
            {
                if(blockPrefab.WasCollected)
                {
                    blockPrefab
                        .SetData(spriteFactory.GetSortedSprite(), this)
                        .SetEnabled();
                }
            }
        }

        public void InjectResolver(ObjectResolver objectResolver)
        {
            _manageScoreAndMoves = objectResolver.Resolve<IManageScoreAndMoves>();
        }
    }
}