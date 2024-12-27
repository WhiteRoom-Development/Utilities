using System;
using UnityEngine;

namespace Utilities.Runtime
{
    public static class DirectionExtensions
    {
        public static Vector2 ConvertToVector2(this Direction direction)
        {
            return direction switch
            {
                Direction.Center => new Vector2(0, 0),
                Direction.Up => new Vector2(0, 1),
                Direction.Down => new Vector2(0, -1),
                Direction.Left => new Vector2(-1, 0),
                Direction.Right => new Vector2(1, 0),
                Direction.UpLeft => new Vector2(-1, 1),
                Direction.UpRight => new Vector2(1, 1),
                Direction.DownLeft => new Vector2(-1, -1),
                Direction.DownRight => new Vector2(1, -1),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
        
        public static Vector3 ConvertToVector3(this Direction direction)
        {
            return direction switch
            {
                Direction.Center => new Vector3(0, 0, 0),
                Direction.Up => new Vector3(0, 0, 1),
                Direction.Down => new Vector3(0, 0, -1),
                Direction.Left => new Vector3(-1, 0, 0),
                Direction.Right => new Vector3(1, 0, 0),
                Direction.UpLeft => new Vector3(-1, 0, 1),
                Direction.UpRight => new Vector3(1, 0, 1),
                Direction.DownLeft => new Vector3(-1, 0, -1),
                Direction.DownRight => new Vector3(1, 0, -1),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
    }
}