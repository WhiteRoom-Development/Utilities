namespace UnityEngine
{
    public class FastMonoBehaviour : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            UpdateManager.AddToUpdate(FastUpdate);
            UpdateManager.AddToFixedUpdate(FastFixedUpdate);
            UpdateManager.AddToLateUpdate(FastLateUpdate);
        }

        protected virtual void OnDisable()
        {
            UpdateManager.RemoveToUpdate(FastUpdate);
            UpdateManager.RemoveToFixedUpdate(FastFixedUpdate);
            UpdateManager.RemoveToLateUpdate(FastLateUpdate);
        }

        protected virtual void FastUpdate(){}
        protected virtual void FastFixedUpdate(){}
        protected virtual void FastLateUpdate(){}
    }
}