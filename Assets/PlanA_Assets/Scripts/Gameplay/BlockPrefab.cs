using UnityEngine;
using UnityEngine.UI;

namespace PlanA_Assets.Scripts
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(RectTransform))]
    public class BlockPrefab: MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private BoxCollider2D boxCollider2D;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private float hitOffset = 0f;
        [SerializeField] private float rayDistance = 1f;

        private HudController _hudController;
        private bool _wasCollected;
        // We should rely on strong comparison (hash code)
        private int SpriteHash => icon.sprite.GetHashCode();
        public bool WasCollected => _wasCollected;

        public BlockPrefab SetData(Sprite getSortedSprite, HudController hudController)
        {
            icon.sprite = getSortedSprite;
            _hudController = hudController;

            return this;
        }
        
        public void SetEnabled(bool isEnabled = true)
        {
            icon.enabled = isEnabled;
            boxCollider2D.enabled = isEnabled;
            _wasCollected = !isEnabled;
        }

        
        public void OnTap()
        {
            _hudController.AddScore(shouldHandleMoves: true);
            HandleCollecting();
        }

        private void HandleCollecting()
        {
            if (_wasCollected)
            {
                return;
            }
            
            SetEnabled(false);
            
            // Opted out here to add all neighbors collision one by one due tight deadline 
            RaycastHit2D upHit = Physics2D.Raycast(rectTransform.MaxHeightCenter(hitOffset), Vector2.up * rayDistance);
            RaycastHit2D downHit = Physics2D.Raycast(rectTransform.MaxHeightCenter(hitOffset, isBottom: true), Vector2.down * rayDistance);
            RaycastHit2D leftHit = Physics2D.Raycast(rectTransform.MaxWidthCenter(hitOffset), Vector2.right * rayDistance);
            RaycastHit2D rightHit = Physics2D.Raycast(rectTransform.MaxWidthCenter(hitOffset, isLeft: true), Vector2.left * rayDistance);

            if (upHit.collider != null)
            {
                if (upHit.collider.TryGetComponent(out BlockPrefab ray))
                {
                    ray.CollectNeighborIfMatch(SpriteHash);
                }
            }
            
            
            if (downHit.collider != null)
            {
                if (downHit.collider.TryGetComponent(out BlockPrefab ray))
                {
                    ray.CollectNeighborIfMatch(SpriteHash);
                }
            }
            
            if (leftHit.collider != null)
            {
                if (leftHit.collider.TryGetComponent(out BlockPrefab ray))
                {
                    ray.CollectNeighborIfMatch(SpriteHash);
                }
            }
            
            
            if (rightHit.collider != null)
            {
                if (rightHit.collider.TryGetComponent(out BlockPrefab ray))
                {
                    ray.CollectNeighborIfMatch(SpriteHash);
                }
            }
        }

        private void CollectNeighborIfMatch(int spriteHash)
        {
            if (spriteHash == SpriteHash)
            {
                _hudController.AddScore(shouldHandleMoves: false);
                HandleCollecting();
            }
        }
        
#if UNITY_EDITOR
        // We need a visible way to see collision rays
        private void OnDrawGizmos()
        {
            if (rectTransform == null)
            {
                return;
            }
            
            Gizmos.color = Color.red;
            Gizmos.DrawRay(rectTransform.MaxHeightCenter(hitOffset), Vector2.up * rayDistance);
            Gizmos.DrawRay(rectTransform.MaxHeightCenter(hitOffset, isBottom: true), Vector2.down * rayDistance);
            Gizmos.DrawRay(rectTransform.MaxWidthCenter(hitOffset), Vector2.right * rayDistance);
            Gizmos.DrawRay(rectTransform.MaxWidthCenter(hitOffset, isLeft: true), Vector2.left * rayDistance);
        }
#endif
    }
}