using UnityEditor;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace Utilities.Runtime
{
    internal static class UpdateBootstrapper
    {
        private static PlayerLoopSystem _updateSystem;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void RuntimeInitializeOnLoad()
        {
            var playerLoop = PlayerLoop.GetCurrentPlayerLoop();

            if (!TryInitializeLoops(ref playerLoop))
            {
                Debug.LogWarning(
                    "Update Manager not initialized, unable to register UpdateManager into the Update loops.");
                return;
            }

            PlayerLoop.SetPlayerLoop(playerLoop);
            
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged -= OnPlayModeState;
            EditorApplication.playModeStateChanged += OnPlayModeState;

            static void OnPlayModeState(PlayModeStateChange state)
            {
                if (state == PlayModeStateChange.ExitingPlayMode)
                {
                    var currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();
                    RemoveLoops(ref currentPlayerLoop);
                    PlayerLoop.SetPlayerLoop(currentPlayerLoop);
                }
            }
#endif
        }

        private static bool TryInitializeLoops(ref PlayerLoopSystem playerLoop)
        {
            if (!Insert<Update>(ref playerLoop, 0, OnUpdate))
                return false;
            
            if (!Insert<FixedUpdate>(ref playerLoop, 0, OnFixedUpdate))
                return false;
            
            if (!Insert<PreLateUpdate>(ref playerLoop, 0, OnLateUpdate))
                return false;

            return true;
        }

        private static void RemoveLoops(ref PlayerLoopSystem loop)
        {
            Remove<Update>(ref loop);
            Remove<FixedUpdate>(ref loop);
            Remove<PreLateUpdate>(ref loop);
        }

        private static void Remove<T>(ref PlayerLoopSystem loop)
        {
            PlayerLoopUtils.RemoveSystem<T>(ref loop, in _updateSystem);
        }

        private static bool Insert<T>(ref PlayerLoopSystem loop, int index, PlayerLoopSystem.UpdateFunction updateFunc)
        {
            _updateSystem = new PlayerLoopSystem
            {
                type = typeof(UpdateManager),
                updateDelegate = updateFunc,
                subSystemList = null
            };
            return PlayerLoopUtils.InsertSystem<T>(ref loop, in _updateSystem, index);
        }
        
        private static void OnUpdate()
        {
            //Only run in the play mod
            if (!Application.isPlaying) return;
            
            UpdateManager.OnUpdate();
        }

        private static void OnFixedUpdate()
        {
            //Only run in the play mod
            if (!Application.isPlaying) return;
            
            UpdateManager.OnFixedUpdate();
        }

        private static void OnLateUpdate()
        {
            //Only run in the play mod
            if (!Application.isPlaying) return;
            
            UpdateManager.OnLateUpdate();
        }
    }
}