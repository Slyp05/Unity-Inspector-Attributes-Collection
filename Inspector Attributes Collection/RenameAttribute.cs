using UnityEngine;

/*
 * USAGE:
 *  This attribute let you rename a field in the inspector.
 *  If used on an array, you can specify a format that will be applied to every array element;
 *  
 * EXAMPLE:
 *      [Rename("Bar")] float foo;                      Rename the field foo to "Bar" in the inspector.
 *      [Rename("Elem {0}")] float[] fooArray;          Keep the array name but rename every element of the array "Elem X" in the inspector.
 *    
 * SOURCE:
 *  https://github.com/Slyp05/Unity-Inspector-Attributes-Collection
 *
 * */
namespace InspectorAttribute
{
    public class RenameAttribute : PropertyAttribute
    {
        public string newName { get; private set; }
        public RenameAttribute(string name)
        {
            newName = name;
        }
    }
}
