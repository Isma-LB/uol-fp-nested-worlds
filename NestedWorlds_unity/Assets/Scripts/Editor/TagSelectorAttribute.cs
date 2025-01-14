using UnityEngine;
using UnityEditor;

/// <summary>
/// Custom attribute used to make a string variable display a dropdown with the available tags in Unity
/// </summary>
public class TagSelectorAttribute : PropertyAttribute
{
    public TagSelectorAttribute() { }
}
namespace IsmaLB.Editors
{
    [CustomPropertyDrawer(typeof(TagSelectorAttribute))]
    public class TagSelectorAttributePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            if (fieldInfo.FieldType != typeof(string))
            {
                EditorGUI.LabelField(position, new GUIContent("Use TagSelector with string"), EditorStyles.boldLabel);
                return;
            }
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);
            property.stringValue = EditorGUI.TagField(position, property.stringValue);
            EditorGUI.EndProperty();
        }
    }
}