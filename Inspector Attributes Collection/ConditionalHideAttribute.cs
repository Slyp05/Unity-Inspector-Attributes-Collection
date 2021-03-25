using UnityEngine;
using System;

/*
 * 
 * USAGE:
 *  This attribute allow you to hide a field in the insepector depending on a condition
 *  
 * EXAMPLE:
 *      [ConditionalHide(HideCondition.IsPlaying)]              disable editing this field when playing the game
 *      
 *      [ConditionalHide("nameOfABool")]                        disable editing this field when nameOfABool is true
 *      [ConditionalHide("!nameOfABool")]                       disable editing this field when nameOfABool is false
 *      
 *      [ConditionalHide("nameOfAnEnum", (int)Enum.Value)]      disable editing this field when nameOfAnEnum is Enum.Value
 *      [ConditionalHide("!nameOfAnEnum", (int)Enum.Value)]     disable editing this field when nameOfAnEnum is NOT Enum.Value
 *      [ConditionalHide("&nameOfABitmask", (int)Enum.Value)]   disable editing this field when nameOfABitmask has flag Enum.Value
 *      [ConditionalHide("&!nameOfABitmask", (int)Enum.Value)]  disable editing this field when nameOfABitmask DOESNT has flag Enum.Value
 *  
 *  Aditionaly, you can specify a HideType if you want the field to be:
 *      HideType.Readonly                                       readonly if the condition is true (default) 
 *      HideType.Hide                                           hidden if the condition is true
 *      HideType.HideOrReadonly                                 hidden if the condition is true and readonly otherwise
 *      
 * */
namespace InspectorAttribute
{
    public enum HideCondition
    {
        Boolean = 0,
        Enum = 1,
        IsPlaying = 2,
        IsntPlaying = 3,
    }

    public enum HideType
    {
        Readonly = 0,
        Hide = 1,
        HideOrReadonly = 2,
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
        AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
    public class ConditionalHideAttribute : PropertyAttribute
    {
        public const char invertChar = '!';
        public const char bitmaskAndChar = '&';

        // The hide condition
        public HideCondition type;
        // The name of the bool or enum field that will be in control
        public string ConditionalSourceField = "";
        // The hide type
        public HideType hideType = HideType.Readonly;
        // The enum value
        public int enumValue;

        // Specific conditions
        public ConditionalHideAttribute(HideCondition conditionalType)
        {
            this.type = conditionalType;
        }

        public ConditionalHideAttribute(HideCondition conditionalType, HideType hideType)
        {
            this.type = conditionalType;
            this.hideType = hideType;
        }

        // bool condition
        public ConditionalHideAttribute(string conditionalBoolField)
        {
            this.type = HideCondition.Boolean;
            this.ConditionalSourceField = conditionalBoolField;
        }

        public ConditionalHideAttribute(string conditionalBoolField, HideType hideType)
        {
            this.type = HideCondition.Boolean;
            this.ConditionalSourceField = conditionalBoolField;
            this.hideType = hideType;
        }

        // enum condition
        public ConditionalHideAttribute(string conditionalEnumField, int enumValue)
        {
            this.type = HideCondition.Enum;
            this.ConditionalSourceField = conditionalEnumField;
            this.enumValue = enumValue;
        }

        public ConditionalHideAttribute(string conditionalEnumField, int enumValue, HideType hideType)
        {
            this.type = HideCondition.Enum;
            this.ConditionalSourceField = conditionalEnumField;
            this.enumValue = enumValue;
            this.hideType = hideType;
        }
    }
}