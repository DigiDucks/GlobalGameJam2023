#region Header

// /*
//  * File: MonoBehaviourEditor.cs
//  * Author: Arthur Baum
//  * Date: 12/2022
//  * © 2022 DigiPen(USA) Corporation
//  */

#endregion

using UnityEngine;
using UnityEditor;

/// <summary>
/// Needed at the Moment for the ScriptableObjectDrawer
/// </summary>
[CanEditMultipleObjects]
[CustomEditor(typeof(MonoBehaviour), true)]
public class MonoBehaviourEditor : Editor
{
}