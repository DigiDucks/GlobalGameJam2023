#region Header

// /*
//  * File: ScriptableObjectDrawer.cs
//  * Author: Arthur Baum
//  * Date: 12/2022
//  * © 2022 DigiPen(USA) Corporation
//  */

#endregion

using UnityEngine;
using UnityEditor;

/// <summary>
/// Property Drawer to allow editing/viewing of ScriptableObject properties in Inspector
/// </summary>
[CustomPropertyDrawer(typeof(ScriptableObject), true)]
public class ScriptableObjectDrawer : PropertyDrawer
{
    // Cached scriptable object editor
    private Editor _editor = null;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Draw label
        EditorGUI.PropertyField(position, property, label, true);

        // Draw foldout arrow
        if (property.objectReferenceValue != null)
        {
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, GUIContent.none);
        }

        // Draw foldout properties
        if (property.isExpanded)
        {
            // Make child fields be indented
            EditorGUI.indentLevel++;

            // Draw object properties
            if (!_editor)
                Editor.CreateCachedEditor(property.objectReferenceValue, null, ref _editor);
            _editor.OnInspectorGUI();

            // Set indent back to what it was
            EditorGUI.indentLevel--;
        }
    }
}