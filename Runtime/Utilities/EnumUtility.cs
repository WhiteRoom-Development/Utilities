using System;

namespace Utilities.Runtime
{
    public static class EnumUtility
    {
        public static T GetRandomValue<T>() where T : Enum
        {
            Random random = new();
            
            var enumValues = Enum.GetValues(typeof(T));
            var randomIndex = random.Next(enumValues.Length);
            return (T)enumValues.GetValue(randomIndex);
        }
    }
}