using UnityEngine;

/*
 * USAGE:
 *  This attribute allow you to preview a sprite in the inspector, use it with a Sprite field.
 *  You can specify the preview sprite height as an argument.
 *  
 * */
namespace InspectorAttribute
{
    public class PreviewSpriteAttribute : PropertyAttribute
    {
        public int height;

        public PreviewSpriteAttribute(int height = 100)
        {
            this.height = height;
        }
    }
}