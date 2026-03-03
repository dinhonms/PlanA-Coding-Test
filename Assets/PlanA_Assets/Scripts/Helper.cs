using UnityEngine;

namespace PlanA_Assets.Scripts
{
    /// <summary>
    /// Helper class for general purposes
    /// </summary>
    public static class Helper
    {
        public static Vector2 MaxHeightCenter(this RectTransform rectTransform, float hitOffset, bool isBottom = false)
        {
            float maxY = rectTransform.rect.yMax;
            Vector2 vector2 = new Vector2(0f, isBottom ? maxY * -1 - hitOffset : maxY + hitOffset);
            Vector2 topYCenter = rectTransform.TransformPoint(vector2);
            
            return topYCenter;
        }
        
        public static Vector2 MaxWidthCenter(this RectTransform rectTransform, float hitOffset, bool isLeft = false)
        {
            float maxX = rectTransform.rect.xMax;
            Vector2 vector2 = new Vector2(isLeft ? maxX * -1 - hitOffset : maxX + hitOffset, 0f);
            Vector2 maxXCenter = rectTransform.TransformPoint(vector2);
            
            return maxXCenter;
        }
    }
}