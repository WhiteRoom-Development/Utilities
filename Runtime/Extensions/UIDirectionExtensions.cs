using System;
using UnityEngine;

namespace Utilities.Runtime
{
    public static class UIDirectionExtensions
    {
        public static Vector2 ConvertToVector2(this UIDirection uiDirection)
        {
            return uiDirection switch
            {
                UIDirection.Center => new Vector2(0.5f, 0.5f),
                UIDirection.Top => new Vector2(0.5f, 1),
                UIDirection.Bottom => new Vector2(0.5f, 0),
                UIDirection.Left => new Vector2(0, 0.5f),
                UIDirection.Right => new Vector2(1, 0.5f),
                UIDirection.TopLeft => new Vector2(0, 1),
                UIDirection.TopRight => new Vector2(1, 1),
                UIDirection.BottomLeft => new Vector2(0, 0),
                UIDirection.BottomRight => new Vector2(1, 0),
                _ => throw new ArgumentOutOfRangeException(nameof(uiDirection), uiDirection, null)
            };
        }
    }
}