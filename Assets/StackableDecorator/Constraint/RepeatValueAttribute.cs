using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace StackableDecorator
{
    public class RepeatValueAttribute : StackableDecoratorAttribute
    {
        public bool lowerInclusive = true;
        public bool upperInclusive = false;
#if UNITY_EDITOR
        private float m_Min;
        private float m_Max;
#endif
        public RepeatValueAttribute(float min, float max)
        {
#if UNITY_EDITOR
            m_Min = min;
            m_Max = max;
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

            return true;
        }

        public override void AfterGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!IsVisible()) return;

            float newvalue, value = 0;
            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    value = property.intValue;
                    break;
                case SerializedPropertyType.Float:
                    value = property.floatValue;
                    break;
            }

            bool warpUpper = upperInclusive ? value > m_Max : value >= m_Max;
            bool warpLower = lowerInclusive ? value < m_Min : value <= m_Min;
            newvalue = !(warpUpper || warpLower) ? value : m_Min + Mathf.Repeat(value - m_Min, m_Max - m_Min);

            if (newvalue != value)
            {
                if (property.propertyType == SerializedPropertyType.Integer)
                    property.intValue = (int)newvalue;
                if (property.propertyType == SerializedPropertyType.Float)
                    property.floatValue = newvalue;
            }
        }
#endif
    }
}