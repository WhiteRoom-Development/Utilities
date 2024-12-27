using System;
using UnityEditor;
using UnityEngine;
using Toolbox.Editor;
using Utilities.Runtime;
using System.Text.RegularExpressions;

namespace Utilities.Editor
{
    public class ObjectEditor : ToolboxEditor
    {
        protected virtual string DefaultTitle => null;

        private IToolboxEditorDrawer _drawer;
        
        private Texture2D _titleBackground;
        private GUIStyle _titleStyle;

        private string _title;

        public sealed override IToolboxEditorDrawer Drawer
        {
            get
            {
                if (_drawer == null)
                {
                    _drawer = new ToolboxEditorDrawer(GetDrawingAction());
                    if (IgnoreScriptProperty)
                        _drawer.IgnoreProperty("m_Script");
                }

                return _drawer;
            }
        }

        protected virtual bool IgnoreScriptProperty => false;

        protected virtual Action<SerializedProperty> GetDrawingAction()
        {
            return ToolboxEditorGui.DrawToolboxProperty;
        }
        
        public override void DrawCustomInspector()
        {
            if (_title.IsNullOrEmpty())
            {
                if (DefaultTitle.IsBlank())
                {
                    _title = target.GetType().Name;
                    _title = Regex.Replace(_title, "([a-z])([A-Z])", "$1 $2");   
                }
                else
                {
                    _title = DefaultTitle;
                }
            }
                
            DrawLabel(_title);
            
            GUILayout.Space(10);
            
            DrawHeadInspector();
            DrawBodyInspector();
            DrawBottomInspector();
        }

        protected virtual void DrawHeadInspector()
        {
            
        }

        protected virtual void DrawBodyInspector()
        {
            base.DrawCustomInspector();
        }

        protected virtual void DrawBottomInspector()
        {
            
        }

        protected void DrawLabel(string title, int fontSize = 17, int labelHeight = 23, Color? backgroundColor = null)
        {
            _titleBackground = new Texture2D(1, 1);
            _titleBackground.SetPixel(0, 0, backgroundColor ?? Color.white);
            _titleBackground.Apply();

            _titleStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = fontSize,
                fontStyle = FontStyle.Bold,
                normal = { background = _titleBackground, textColor = Color.black }
            };

            EditorGUILayout.LabelField(title, _titleStyle, GUILayout.Height(labelHeight));
        }
        
        protected void DrawCustomProperty(string propertyPath)
        {
            if (Drawer.IsPropertyIgnored(propertyPath))
                return;

            var property = serializedObject.FindProperty(propertyPath);
            ToolboxEditorGui.DrawToolboxProperty(property);
        }

        protected void DrawCustomPropertySkipIgnore(string propertyPath)
        {
            var property = serializedObject.FindProperty(propertyPath);
            ToolboxEditorGui.DrawToolboxProperty(property);
        }
    }
}