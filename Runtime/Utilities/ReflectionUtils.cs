using System;
using UnityEngine;

namespace Utilities.Runtime
{
    public static class ReflectionUtils
    {
        /// <summary>
        /// Creates an instance of the specified type name with the given arguments.
        /// </summary>
        /// <typeparam name="T">The type of the instance to create.</typeparam>
        /// <param name="typeName">The name of the type to create.</param>
        /// <param name="args">The arguments to pass to the constructor.</param>
        /// <returns>An instance of the specified type, or null if the type is not found.</returns>
        public static T CreateInstance<T>(string typeName, params object[] args) where T : class
        {
            var type = Type.GetType(typeName);

            if (type != null)
            {
                return (T)Activator.CreateInstance(type, args);
            }

            Debug.LogWarning($"Type {typeName} not found");

            return null;
        }
    }
}