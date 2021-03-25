using UnityEngine;
using UnityEditor;

namespace InspectorAttribute
{
    [CustomPropertyDrawer(typeof(RenameAttribute))]
    public class RenameEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string name = (this.RepresentAnArray() ?
                string.Format((attribute as RenameAttribute).newName, this.GetRepresentedArrayIndex(property)) :
                (attribute as RenameAttribute).newName);

            label = EditorGUI.BeginProperty(position, new GUIContent(name), property);
            EditorGUI.PropertyField(position, property, label);
            EditorGUI.EndProperty();
        }
    }
}