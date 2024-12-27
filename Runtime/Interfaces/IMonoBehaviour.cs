using UnityEngine;

namespace Utilities.Runtime
{
    /// <summary>
    /// Interface representing MonoBehaviour-like functionality.
    /// </summary>
    public interface IMonoBehaviour
    {
        /// <summary>
        /// Gets the game object this component is attached to.
        /// </summary>
        public GameObject gameObject { get; }
        
        /// <summary>
        /// Gets the transform of the game object this component is attached to.
        /// </summary>
        public Transform transform { get; }
        
        /// <summary>
        /// Enabled Behaviours are Updated, disabled Behaviours are not.
        /// </summary>
        public bool enabled { get; }
    }
}