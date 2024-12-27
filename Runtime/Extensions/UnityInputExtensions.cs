using System;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Utilities.Runtime
{
    public static class UnityInputExtensions
    {
#if ENABLE_INPUT_SYSTEM
        private static readonly Dictionary<InputActionReference, int> _enabledActions = new();

        #region Initialization

#if UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Reload()
        {
            foreach (var inputActionReference in _enabledActions.Keys)
            {
                inputActionReference.action.Disable();
            }
            
            _enabledActions.Clear();
        }
#endif

        #endregion

        public static void RegisterStarted(this InputActionReference actionRef,
            Action<InputAction.CallbackContext> callback)
        {
            CheckForNull(actionRef);
            
            Enable(actionRef);
            actionRef.action.started += callback;
        }
        
        public static void RegisterPerformed(this InputActionReference actionRef,
            Action<InputAction.CallbackContext> callback)
        {
            CheckForNull(actionRef);
            
            Enable(actionRef);
            actionRef.action.performed += callback;
        }
        
        public static void RegisterCanceled(this InputActionReference actionRef,
            Action<InputAction.CallbackContext> callback)
        {
            CheckForNull(actionRef);
            
            actionRef.action.canceled += callback;
            TryDisable(actionRef);
        }
        
        public static void UnregisterStarted(this InputActionReference actionRef,
            Action<InputAction.CallbackContext> callback)
        {
            CheckForNull(actionRef);

            actionRef.action.started -= callback;
            TryDisable(actionRef);
        }
        
        public static void UnregisterPerformed(this InputActionReference actionRef,
            Action<InputAction.CallbackContext> callback)
        {
            CheckForNull(actionRef);
            
            actionRef.action.performed -= callback;
            TryDisable(actionRef);
        }
        
        public static void UnregisterCanceled(this InputActionReference actionRef,
            Action<InputAction.CallbackContext> callback)
        {
            CheckForNull(actionRef);
            
            actionRef.action.canceled -= callback;
            TryDisable(actionRef);
        }

        public static void Enable(this InputActionReference actionRef)
        {
            CheckForNull(actionRef);

            if (_enabledActions.TryGetValue(actionRef, out var listenerCount))
            {
                _enabledActions[actionRef] = listenerCount + 1;
            }
            else
            {
                _enabledActions.Add(actionRef, 1);
                actionRef.action.Enable();
            }
        }

        public static void TryDisable(this InputActionReference actionRef)
        {
            CheckForNull(actionRef);

            if (_enabledActions.TryGetValue(actionRef, out var listenerCount))
            {
                listenerCount--;
                if (listenerCount == 0)
                {
                    _enabledActions.Remove(actionRef);
                    actionRef.action.Disable();
                }
                else
                {
                    _enabledActions[actionRef] = listenerCount;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void CheckForNull(InputActionReference actionRef)
        {
#if DEBUG
            if (actionRef == null)
            {
                Debug.LogError("The passed input action is null, you need to set it in the inspector");
            }
#endif
        }
#endif
    }
}