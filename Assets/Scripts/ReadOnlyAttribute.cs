using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ReadOnlyAttribute : PropertyAttribute {}

#if UNITY_EDITOR

[UnityEditor.CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyPropertyDrawer : UnityEditor.PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
    }
}
#endif
