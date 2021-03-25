using UnityEngine;
using System;
using System.Reflection;
using UnityEditor;

namespace InspectorAttribute
{
    [CustomPropertyDrawer(typeof(ButtonParameterAttribute))]
    public class ButtonParameterDrawer : PropertyDrawer
    {
        const string couldntFindMethodFormat = "ButtonParameter: Unable to find method {0} in {1}";
        const float spaceWidth = 2;

        private MethodInfo _eventMethodInfo = null;

        public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
        {
            Rect contentPosition = EditorGUI.PrefixLabel(position, new GUIContent(prop.displayName));

            ButtonParameterAttribute inspectorButtonAttribute = (ButtonParameterAttribute)attribute;

            if (prop.type == typeof(InspectorTrigger).Name)
            {
                if (GUI.Button(contentPosition, inspectorButtonAttribute.buttonText))
                {
                    Type eventOwnerType = prop.serializedObject.targetObject.GetType();
                    string eventName = inspectorButtonAttribute.methodName;

                    if (_eventMethodInfo == null)
                        _eventMethodInfo = eventOwnerType.GetMethod(eventName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

                    if (_eventMethodInfo != null)
                        _eventMethodInfo.Invoke(prop.serializedObject.targetObject, null);
                    else
                        Debug.LogError(string.Format(couldntFindMethodFormat, eventName, eventOwnerType));
                }
            }
            else
            {
                int cacheIndent = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 0;

                contentPosition.width = (contentPosition.width - spaceWidth) / 2;

                EditorGUI.BeginProperty(contentPosition, label, prop);
                {
                    EditorGUI.PropertyField(contentPosition, prop, new GUIContent());
                }
                EditorGUI.EndProperty();

                contentPosition.x += contentPosition.width + spaceWidth;

                if (GUI.Button(contentPosition, inspectorButtonAttribute.buttonText))
                {
                    Type eventOwnerType = prop.serializedObject.targetObject.GetType();
                    string eventName = inspectorButtonAttribute.methodName;

                    if (_eventMethodInfo == null)
                        _eventMethodInfo = eventOwnerType.GetMethod(eventName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

                    if (_eventMethodInfo != null)
                        _eventMethodInfo.Invoke(prop.serializedObject.targetObject,
                            new object[1] { prop.GetTargetObjectOfProperty() });
                    else
                        Debug.LogError(string.Format(couldntFindMethodFormat, eventName, eventOwnerType));
                }

                EditorGUI.indentLevel = cacheIndent;
            }
        }
    }
}