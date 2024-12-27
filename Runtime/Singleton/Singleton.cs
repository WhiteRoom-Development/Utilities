using UnityEngine;

namespace Utilities.Runtime
{
    /// <summary>
    /// If instance is null in the instantiation call,
    /// it checks if there is an instance in the scene.
    /// If there is not an instance in the scene,it creates a new object.
    /// Awake sets the instance value.
    /// </summary>
    /// <remarks>Basic Singleton for MonoBehaviour</remarks>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static bool HasInstance => _instance != null;
        public static T TryGetInstance() => HasInstance ? _instance : null;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<T>();
                    if (_instance == null)
                    {
                        var go = new GameObject($"{typeof(T).Name} Auto-Generated");
                        _instance = go.AddComponent<T>();
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// Make sure to call base.Awake() in override if you need awake.
        /// </summary>
        protected virtual void Awake()
        {
            InitializeSingleton();
        }

        protected virtual void InitializeSingleton()
        {
            if (!Application.isPlaying) return;

            _instance = this as T;
        }
    }
}