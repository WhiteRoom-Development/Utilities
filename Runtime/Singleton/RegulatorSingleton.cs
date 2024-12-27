using UnityEngine;

namespace Utilities.Runtime
{
    /// <summary>
    /// Instance call, if instance is not null, it returns.
    /// If instance is null, it searches all {T} types in the scene and if its size is greater than 1,
    /// it assigns any object of {T} Type to the instance variable and deletes the others,
    /// if its size is one, it takes the first value and if it is 0, it creates a new instance.
    /// In Awake, it manages the DontDestroyOnLoad operation.
    /// </summary>
    /// <remarks>Advanced Singleton for MonoBehaviour</remarks>
    /// <typeparam name="T"></typeparam>
    public class RegulatorSingleton<T> : MonoBehaviour  where T : MonoBehaviour
    {
        private static T _instance;
        
        public static bool HasInstance => _instance != null;
        public static T TryGetInstance() => HasInstance ? _instance : null;
        
        public static T Instance
        {
            get
            {
                if(_instance != null)
                    return _instance;
                
                var instances = FindObjectsByType<T>(FindObjectsSortMode.InstanceID);

                switch (instances.Length)
                {
                    case > 1:
                    {
                        // Get the first one
                        _instance = instances[0];
                        
                        // Destroy the others
                        for (var i = 1; i < instances.Length; i++)
                        {
                            Destroy(instances[i]);
                        }

                        break;
                    }
                    case 1:
                        _instance = instances[0];
                        break;
                    default:
                        _instance = CreateInstance();
                        break;
                }
                
                DontDestroyOnLoad(_instance);
                return _instance;
            }
        }

        private static T CreateInstance()
        {
            _instance = new GameObject($"{typeof(T).Name} Auto-Generated")
                .AddComponent<T>();

            return _instance;
        }

        private static void DontDestroyOnLoad(T instance)
        {
            if (instance.transform.parent != null)
            {
                instance.transform.parent = null;
            }
            
            Object.DontDestroyOnLoad(instance);
        }
    }
}