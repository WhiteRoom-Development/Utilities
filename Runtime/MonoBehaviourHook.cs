using UnityEngine;

namespace Utilities.Runtime
{
    public class MonoBehaviourHook : MonoBehaviour
    {
    }

    public static class HookUtility
    {
        public static MonoBehaviourHook Create(string name)
        {
            var obj = new GameObject($"{name}_Hook", typeof(MonoBehaviourHook));

            var hook = obj.GetComponent<MonoBehaviourHook>();

            Object.DontDestroyOnLoad(hook);

            return hook;
        }
    }
}