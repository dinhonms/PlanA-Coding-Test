using UnityEngine;

namespace PlanA_Assets.Scripts
{
    /// <summary>
    /// Sprite factory to deal with block sprites.
    /// I'd improve here to add blocks
    /// </summary>
    public class SpriteFactory: MonoBehaviour
    {
        [SerializeField] private Sprite[] _availableSprites;
        
        public Sprite GetSortedSprite()
        {
            return _availableSprites[Random.Range(0, _availableSprites.Length)];
        }
    }
}