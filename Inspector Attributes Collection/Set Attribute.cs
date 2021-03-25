using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * USAGE:
 *      /!\ Doesn't work when used on an array !
 *
 *  This attribute allow you to use the setter of a property when modifying a field in the inspector.
 *  
 * EXAMPLE:
 *      [SerializeField, GetSet("publicVarName")]
 *      private float _privateVarName;
 *      
 *      public float publicVarName
 *      {   
 *          get { return _privateVarName; }
 *          set { _privateVarName = value; } 
 *      }
 * 
 * SOURCE:
 *  https://github.com/Slyp05/Unity-Inspector-Attributes-Collection
 *
 * */
namespace InspectorAttribute
{
    public class SetAttribute : PropertyAttribute
    {
        public readonly string name;
        public bool dirty;

        public SetAttribute(string name)
        {
            this.name = name;
        }
    }
}
