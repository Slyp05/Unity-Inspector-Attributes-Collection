using System;
using UnityEngine;

/*
 * USAGE:
 *  This attribute will create a button next to a field in the inspector that will call a given method with the field as it's parameter.
 *  You can use the type "InspectorTrigger" if your method doesn't take any parameters.
 *  You can also input a button text as an argument (default is to use the method name).
 *  
 * EXAMPLE:
 *  [ButtonParameter("NoParamFunc")]                                Will create a button named "NoParamFunc" that will call the method 
 *  [SerializeField] InspectorTrigger inspectorTrigger;                of the same name on click 
 *  
 *  [ButtonParameter("IntParamFunc", "Button Text")]                Will create a button named "Button Text" that will call the method
 *  [SerializeField] int intParam;                                     IntParamFunc with intParam as an argument on click
 *  
 *  void NoParamFunc() => Debug.Log("No parameters function called");
 *  
 *  void IntParamFunc(int i) => Debug.Log("Int parameters function called with: " + i);
 *  
 * SOURCE:
 *  https://github.com/Slyp05/Unity-Inspector-Attributes-Collection
 *
 * */
namespace InspectorAttribute
{
    [Serializable]
    public struct InspectorTrigger
    {

    }

    [AttributeUsage(AttributeTargets.Field)]
    public class ButtonParameterAttribute : PropertyAttribute
    {
        public readonly string methodName;
        public readonly string buttonText;

        public ButtonParameterAttribute(string methodName)
        {
            this.methodName = methodName;
            this.buttonText = methodName;
        }

        public ButtonParameterAttribute(string methodName, string buttonText)
        {
            this.methodName = methodName;
            this.buttonText = buttonText;
        }
    }
}
