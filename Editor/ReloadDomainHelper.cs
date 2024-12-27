using UnityEngine;
using UnityEditor;

namespace Utilities.Editor
{
    public static class ReloadDomainHelper
    {
        /// Must be used after BeforeSceneLoad
        public static bool IsReloadedDomain = true;
        private static byte _initializeGrabber;

        [InitializeOnLoadMethod]
        private static void OnInitialize()
        {
            _initializeGrabber = EditorApplication.isPlayingOrWillChangePlaymode ? (byte)1 : (byte)0;
        }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnRuntimeInitialize()
        {
            if (_initializeGrabber == 1)
            {
                _initializeGrabber = 0;
                IsReloadedDomain = true;
            }
            else
            {
                IsReloadedDomain = false;
            }
        }
    }
}