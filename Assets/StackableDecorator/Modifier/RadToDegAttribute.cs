using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace StackableDecorator
{
    public class RadToDegAttribute : StackableDecoratorAttribute
    {
#if UNITY_EDITOR
        private float m_Value;
#endif
        public RadToDegAttribute()
        {
#if UNITY_EDITOR
#endif
        }
#if UNITY_EDITOR
        public override float GetHeight(SerializedProperty property, GUIContent label, float height)
        {
            return height;
        }

        public override bool BeforeGUI(ref Rect position, ref SerializedProperty property, ref GUIContent label, ref bool includeChildren, bool visible)
        {
            if (!IsVisible()) return visible;
            if (!visible) return false;

            if (property.propertyType == SerializedPropertyType.Float)
                property.floatValue = property.floatValue * Mathf.Rad2Deg;

            return true;
        }

        public override void AfterGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!IsVisible()) return;

            if (property.propertyType == SerializedPropertyType.Float)
                property.floatValue = property.floatValue * Mathf.Deg2Rad;
        }
#endif
    }
}