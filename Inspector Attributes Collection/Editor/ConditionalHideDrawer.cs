using UnityEngine;
using UnityEditor;

namespace InspectorAttribute
{
    [CustomPropertyDrawer(typeof(ConditionalHideAttribute))]
    public class ConditionalHideDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ConditionalHideAttribute condHAtt = (ConditionalHideAttribute)attribute;
            bool show = GetConditionalHideAttributeResult(condHAtt, property);
            bool readOnly = condHAtt.hideType == HideType.HideOrReadonly || !show;
            bool hide = condHAtt.hideType != HideType.Readonly && !show;

            if (!hide)
            {
                bool wasGuiEnabled = GUI.enabled;
                GUI.enabled = !readOnly;

                EditorGUI.PropertyField(position, property, label, true);

                GUI.enabled = wasGuiEnabled;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ConditionalHideAttribute condHAtt = (ConditionalHideAttribute)attribute;
            bool show = GetConditionalHideAttributeResult(condHAtt, property);
            bool readOnly = condHAtt.hideType == HideType.HideOrReadonly || !show;
            bool hide = condHAtt.hideType != HideType.Readonly && !show;

            if (!hide)
                return EditorGUI.GetPropertyHeight(property, label);
            else // (hide)
                return -EditorGUIUtility.standardVerticalSpacing;
        }

        private bool GetConditionalHideAttributeResult(ConditionalHideAttribute condHAtt, SerializedProperty property)
        {
            switch (condHAtt.type)
            {
                case HideCondition.Boolean:
                case HideCondition.Enum:
                    break;
                case HideCondition.IsPlaying:
                    return !Application.isPlaying;
                case HideCondition.IsntPlaying:
                    return Application.isPlaying;
                default:
                    Debug.LogError("You forgot a type in ConditionalHideDrawer !");
                    return true;
            }

            string propertyName = condHAtt.ConditionalSourceField;
            bool invert = false;
            bool bitmaskAnd = false;
            if ((propertyName[0] == ConditionalHideAttribute.invertChar && propertyName[1] == ConditionalHideAttribute.bitmaskAndChar)
             || (propertyName[1] == ConditionalHideAttribute.invertChar && propertyName[0] == ConditionalHideAttribute.bitmaskAndChar))
            {
                invert = true;
                bitmaskAnd = true;
                propertyName = propertyName.Substring(2);
            }
            else if (propertyName[0] == ConditionalHideAttribute.invertChar)
            {
                invert = true;
                propertyName = propertyName.Substring(1);
            }
            else if (propertyName[0] == ConditionalHideAttribute.bitmaskAndChar)
            {
                bitmaskAnd = true;
                propertyName = propertyName.Substring(1);
            }

            SerializedProperty sourcePropertyValue;
            if (this.RepresentAnArray())
                sourcePropertyValue = property.serializedObject.FindProperty(propertyName);
            else
            {
                string propertyPath = property.propertyPath; //returns the property path of the property we want to apply the attribute to
                string conditionPath = propertyPath.Replace(property.name, propertyName); //changes the path to the conditionalsource property path
                sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);
            }

            if (sourcePropertyValue != null)
            {
                if (condHAtt.type == HideCondition.Enum && bitmaskAnd)
                {
                    if (invert)
                        return (sourcePropertyValue.intValue & condHAtt.enumValue) != 0;
                    else
                        return (sourcePropertyValue.intValue & condHAtt.enumValue) == 0;
                }
                else if (condHAtt.type == HideCondition.Enum) // (&& !bitmaskAnd)
                {
                    if (invert)
                        return sourcePropertyValue.intValue == condHAtt.enumValue;
                    else
                        return sourcePropertyValue.intValue != condHAtt.enumValue;
                }
                else // (condHAtt.type == HideCondition.Boolean)
                {
                    if (invert)
                        return sourcePropertyValue.boolValue;
                    else
                        return !(sourcePropertyValue.boolValue);
                }
            }
            else
                Debug.LogError("Attempting to use a ConditionalHideAttribute but no matching " +
                    "SourcePropertyValue found in object: " + propertyName);

            return true;
        }
    }
}