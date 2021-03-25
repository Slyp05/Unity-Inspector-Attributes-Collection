using UnityEditor;
using System;

namespace InspectorAttribute
{
    public static class PropertyDrawerArrayExtension
    {
        public static bool RepresentAnArray(this PropertyDrawer drawer)
        {
            return drawer.fieldInfo.FieldType.IsArray;
        }

        public static int GetRepresentedArrayIndex(this PropertyDrawer drawer, SerializedProperty property)
        {
            if (!drawer.RepresentAnArray())
                return -1;

            string propertyPath = property.propertyPath;
            int startIndex = propertyPath.Length - 1;
            int length = 0;

            while (propertyPath[startIndex - 1] != '[')
            {
                startIndex--;
                length++;
            }

            if (!int.TryParse(propertyPath.Substring(startIndex, length), out int ret))
                return -1;
            else
                return ret;
        }

        public static Type GetArrayType(this PropertyDrawer drawer)
        {
            return drawer.fieldInfo.FieldType.GetElementType();
        }

        public static object GetArray(this PropertyDrawer drawer, SerializedProperty property)
        {
            return drawer.fieldInfo.GetValue(property.serializedObject.targetObject);
        }
    }
}