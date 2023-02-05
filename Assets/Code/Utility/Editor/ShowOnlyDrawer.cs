#region Header

// /*
//  * File: ShowOnlyDrawer.cs
//  * Author: Arthur Baum
//  * Date: 12/2022
//  * © 2022 DigiPen(USA) Corporation
//  */

#endregion

using UnityEngine;
using UnityEditor;

/// <summary>
/// Adds an Attribute that allows variables to be visible to the Inspector without exposing it to editing
/// </summary>
[CustomPropertyDrawer(typeof(ShowOnlyAttribute))]
public class ShowOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property,
        GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position,
        SerializedProperty property,
        GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}