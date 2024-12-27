using System;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using Utilities.Runtime;

namespace Utilities.Editor
{
    [CustomPropertyDrawer(typeof(SerializableGuid))]
    public class SerializableGuidDrawer : PropertyDrawer
    {
        private static readonly string[] GuidParts = { "Part1", "Part2", "Part3", "Part4" };

        private static SerializedProperty[] GetGuidParts(SerializedProperty property)
        {
            var values = new SerializedProperty[GuidParts.Length];
            for (var i = 0; i < GuidParts.Length; i++)
            {
                values[i] = property.FindPropertyRelative(GuidParts[i]);
            }

            return values;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (GetGuidParts(property).All(x => x != null))
            {
                EditorGUI.LabelField(position, BuildGuidString(GetGuidParts(property)));
            }
            else
            {
                EditorGUI.SelectableLabel(position, "GUID Not Initialized");
            }

            var hasClicked = Event.current.type == EventType.MouseUp && Event.current.button == 1;
            if (hasClicked && position.Contains(Event.current.mousePosition))
            {
                ShowContextMenu(property);
                Event.current.Use();
            }

            EditorGUI.EndProperty();
        }

        private static void ShowContextMenu(SerializedProperty property)
        {
            var menu = new GenericMenu();
            menu.AddItem(new GUIContent("Copy GUID"), false, () => CopyGuid(property));
            menu.AddItem(new GUIContent("Reset GUID"), false, () => ResetGuid(property));
            menu.AddItem(new GUIContent("Regenerate GUID"), false, () => RegenerateGuid(property));
            menu.ShowAsContext();
        }

        private static void CopyGuid(SerializedProperty property)
        {
            if (GetGuidParts(property).Any(x => x == null)) return;

            var guid = BuildGuidString(GetGuidParts(property));
            EditorGUIUtility.systemCopyBuffer = guid;
            
            Debug.Log($"GUID copied to clipboard: {guid}");
        }

        private static void ResetGuid(SerializedProperty property)
        {
            const string warning = "Are you sure you want to reset the GUID?";
            if (!EditorUtility.DisplayDialog("Reset GUID", warning, "Yes", "No")) return;

            foreach (var part in GetGuidParts(property))
            {
                part.uintValue = 0;
            }

            property.serializedObject.ApplyModifiedProperties();
            
            Debug.Log("GUID has been reset.");
        }

        private static void RegenerateGuid(SerializedProperty property)
        {
            const string warning = "Are you sure you want to regenerate the GUID?";
            if (!EditorUtility.DisplayDialog("Reset GUID", warning, "Yes", "No")) return;

            var bytes = Guid.NewGuid().ToByteArray();
            var guidParts = GetGuidParts(property);

            for (var i = 0; i < GuidParts.Length; i++)
            {
                guidParts[i].uintValue = BitConverter.ToUInt32(bytes, i * 4);
            }

            property.serializedObject.ApplyModifiedProperties();
            
            Debug.Log("GUID has been regenerated.");
        }

        private static string BuildGuidString(SerializedProperty[] guidParts)
        {
            return new StringBuilder()
                .AppendFormat("{0:X8}", guidParts[0].uintValue)
                .AppendFormat("{0:X8}", guidParts[1].uintValue)
                .AppendFormat("{0:X8}", guidParts[2].uintValue)
                .AppendFormat("{0:X8}", guidParts[3].uintValue)
                .ToString();
        }
    }
}