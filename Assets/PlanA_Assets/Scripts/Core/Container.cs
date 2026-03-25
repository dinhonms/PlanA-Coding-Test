using System;
using UnityEngine;

namespace PlanA_Assets.Scripts.Core
{
    public class Container: MonoBehaviour
    {
        private ObjectResolver _objectResolver;
        
        [SerializeField] private HudController _hudController;

        private ManageScoreAndMovesV2 _manageScoreAndMoves;
        
        private void Awake()
        {
            _objectResolver = new ObjectResolver();
            _manageScoreAndMoves = new ManageScoreAndMovesV2();
            
            _objectResolver.RegisterInstance(_manageScoreAndMoves);
            _objectResolver.RegisterInstance(_hudController);
            
            _hudController.InjectResolver(_objectResolver);
        }

        private void OnDestroy()
        {
            if (_objectResolver != null)
            {
                
            }
        }
    }
}