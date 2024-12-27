using UnityEngine;

namespace Utilities.Runtime
{
    public class ShowFPS : MonoBehaviour
    {
        [BeginGroup(Style = GroupStyle.Boxed), EndGroup]
        [SerializeField] private Color _textColor = Color.white;
        
        [BeginGroup(Style = GroupStyle.Boxed), EndGroup]
        [SerializeField] private Vector2 _offset;
        
        [BeginGroup(Style = GroupStyle.Boxed), EndGroup]
        [SerializeField] private float _updateInterval = 0.5f;

        private float _lastInterval;
        private int _frames;
        private float _fps;

        private void Start() 
        {
            _lastInterval = Time.realtimeSinceStartup;

            _frames = 0;
        }

        private void OnGUI()
        {
            GUI.color = _textColor;
            GUI.Label(new Rect(_offset.x, _offset.y, 200, 200), $"FPS: {_fps:f2}");
        }

        private void Update() 
        {
            ++_frames;

            if (Time.realtimeSinceStartup > _lastInterval + _updateInterval) 
            {
                _fps = _frames / (Time.realtimeSinceStartup - _lastInterval);

                _frames = 0;

                _lastInterval = Time.realtimeSinceStartup;
            }
        }
    }
}