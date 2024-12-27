using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Utilities.Runtime
{
    public static class CoroutineRunner
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Reset()
        {
            _hooks = new Dictionary<int, MonoBehaviourHook>();
        }
        
        private static GameObject _parent;
        
        private static Dictionary<int, MonoBehaviourHook> _hooks = new();
        
        public static Coroutine Start(IEnumerator enumerator, int categoryID = 0)
        {
            if (!_hooks.TryGetValue(categoryID, out var hook))
            {
                hook = CreateHook(categoryID);
                _hooks[categoryID] = hook;
            }
            
            var coroutine = hook.StartCoroutine(enumerator);
            
            return coroutine;
        }
        
        public static void Stop(ref Coroutine coroutine, int categoryID = 0)
        {
            if (coroutine == null) return;

            if (_hooks.TryGetValue(categoryID, out var hook))
            {
                hook.StopCoroutine(coroutine);
                coroutine = null;
            }
        }
        
        public static void StopAllCategory(int categoryID = 0)
        {
            if (_hooks.TryGetValue(categoryID, out var hook))
            {
                hook.StopAllCoroutines();
            }
        }
        
        public static void StopAllCoroutines()
        {
            foreach (var hook in _hooks.Values)
            {
                hook.StopAllCoroutines();
            }
        }

        private static MonoBehaviourHook CreateHook(int categoryID)
        {
            InitParent();
            
            var hook = _parent.AddComponent<MonoBehaviourHook>();
            _hooks.Add(categoryID, hook);
            
            return hook;
        }

        private static void InitParent()
        {
            if (_parent == null)
            {
                _parent = new GameObject("Coroutine Runner");
                Object.DontDestroyOnLoad(_parent);
            }
        }
    }
}