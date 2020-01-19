using UnityEditor;
using UnityEngine;

namespace Game.ForEditor
{
    /// <summary>
    /// Атрибут для полей, которые не следует изменять в инспекторе, но следует показать.
    /// </summary>
    public class ReadOnlyAttribute : PropertyAttribute
    {
    }

#if UNITY_EDITOR
    /// <summary>
    /// Компоновщик для ReadOnlyAttribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        /// <summary>
        /// Возвращает высоту свойства.
        /// </summary>
        /// <param name="property">Свойство.</param>
        /// <param name="label">Лейбл отображения свойства.</param>
        /// <returns>Высота поля свойства в UI.</returns>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        /// <summary>
        /// Рисует UI.
        /// </summary>
        /// <param name="position">Позиция.</param>
        /// <param name="property">Сериализованное свойство.</param>
        /// <param name="label">Лейбл отображения свойство.</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
#endif
}