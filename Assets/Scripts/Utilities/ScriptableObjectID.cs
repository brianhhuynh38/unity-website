using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Website.Utilities {

    /// <summary>
    ///    Property attribute meant to generate a new unique ID for each applicable ScriptableObject
    /// </summary>
    public class ScriptableObjectIDAttribute : PropertyAttribute { }

    /// <summary>
    ///    Custom property that adds a proprrty that can generate the UUID for a ScriptableObject
    /// </summary>
    #if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ScriptableObjectIDAttribute))]
    public class ScriptableObjectPropertyDrawer : PropertyDrawer
    {
        /// <summary>
        ///    Generate UUID on GUI activation
        /// </summary>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            GUI.enabled = false;
            // Generate new GUID
            if (string.IsNullOrEmpty(property.stringValue)) {
                property.stringValue = GUID.Generate().ToString();
            }
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
    #endif
}
