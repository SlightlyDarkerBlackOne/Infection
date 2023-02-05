using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(MessageSubscriptionsLog))]
public class MessageSubscriptionsLogProperty : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        Draw(property);
        GUI.enabled = true; //disable enable to make read only 
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 0; //height comes from children
    }

    private void Draw(SerializedProperty _property)
    {
        bool Foldout = false; //Foldout = enum + draw list if Folded out 

        while (_property.Next(true))
        {
            if (_property.propertyType == SerializedPropertyType.Enum)
            {
                _property.isExpanded = EditorGUILayout.Foldout(_property.isExpanded, ((MessageType)_property.enumValueIndex).ToString(), true);
                Foldout = _property.isExpanded;
            }
            else if (_property.isArray)
            {
                if (Foldout)
                {
                    EditorGUI.indentLevel++;
                    for (int i = 0; i < _property.arraySize; i++)
                    {
                        var Child = _property.GetArrayElementAtIndex(i);
                        EditorGUILayout.PropertyField(Child);
                    }
                    EditorGUI.indentLevel--;
                }
                break;
            }
        }
    }
}
