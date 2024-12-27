using UnityEngine;

#if ENABLED_UNITY_MATHEMATICS
using Unity.Mathematics;
#endif

namespace Utilities.Runtime
{
    public static class MathUtils
    {
        #region Min

#if ENABLED_UNITY_MATHEMATICS
        public static half Min(half a, half b)
        {
            return a < b ? a : b;
        }

        public static half Min(params half[] values)
        {
            var num = values.Length;
            if (num == 0)
                return (half) 0;

            var num2 = values[0];
            for (var i = 1; i < num; i++)
            {
                if (values[i] < num2)
                    num2 = values[i];
            }

            return num2;
        }
#endif
        public static double Min(double a, double b)
        {
            return a < b ? a : b;
        }

        public static double Min(params double[] values)
        {
            var num = values.Length;
            if (num == 0)
                return 0f;

            var num2 = values[0];
            for (var i = 1; i < num; i++)
            {
                if (values[i] < num2)
                    num2 = values[i];
            }

            return num2;
        }

        #endregion
        
        #region Max

#if ENABLED_UNITY_MATHEMATICS
        public static half Max(half a, half b)
        {
            return a > b ? a : b;
        }

        public static half Max(params half[] values)
        {
            var num = values.Length;
            if (num == 0)
                return (half) 0;

            var num2 = values[0];
            for (var i = 1; i < num; i++) {
                if (values[i] > num2) {
                    num2 = values[i];
                }
            }

            return num2;
        }
#endif

        public static double Max(double a, double b)
        {
            return a > b ? a : b;
        }

        public static double Max(params double[] values)
        {
            var num = values.Length;
            if (num == 0)
                return 0f;

            var num2 = values[0];
            for (var i = 1; i < num; i++)
            {
                if (values[i] > num2)
                    num2 = values[i];
            }

            return num2;
        }

        #endregion

        #region Short

        public readonly struct ShortVector3
        {
            public readonly short x;
            public readonly short y;
            public readonly short z;

            public ShortVector3(Vector3 vector)
            {
                x = (short)(vector.x * 100);
                y = (short)(vector.y * 100);
                z = (short)(vector.z * 100);
            }

            public Vector3 ToVector3()
            {
                return new Vector3(x / 100f, y / 100f, z / 100f);
            }
        }

        public readonly struct ShortQuaternion
        {
            public readonly short x;
            public readonly short y;
            public readonly short z;

            public ShortQuaternion(Quaternion quaternion)
            {
                var euler = quaternion.eulerAngles;
                x = (short)(euler.x * 100);
                y = (short)(euler.y * 100);
                z = (short)(euler.z * 100);
            }

            public Quaternion ToQuaternion()
            {
                return Quaternion.Euler(x / 100f, y / 100f, z / 100f);
            }
        }

        #endregion
    }
}