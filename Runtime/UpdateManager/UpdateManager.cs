using System;

namespace UnityEngine
{
    public static class UpdateManager
    {
        private static Action UpdateEvent;
        private static Action FixedUpdateEvent;
        private static Action LateUpdateEvent;
        
        public static void AddToUpdate(Action action) => UpdateEvent += action;
        public static void AddToFixedUpdate(Action action) => FixedUpdateEvent += action;
        public static void AddToLateUpdate(Action action) => LateUpdateEvent += action;

        public static void RemoveToUpdate(Action action) => UpdateEvent -= action;
        public static void RemoveToFixedUpdate(Action action) => FixedUpdateEvent -= action;
        public static void RemoveToLateUpdate(Action action) => LateUpdateEvent -= action;
        
        internal static void OnUpdate() => UpdateEvent?.Invoke();
        internal static void OnFixedUpdate() => FixedUpdateEvent?.Invoke();
        internal static void OnLateUpdate() => LateUpdateEvent?.Invoke();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void InitializeBeforeLoad() => ResetEvents();

        private static void ResetEvents()
        {
            UpdateEvent = null;
            FixedUpdateEvent = null;
            LateUpdateEvent = null;
        }
    }
}