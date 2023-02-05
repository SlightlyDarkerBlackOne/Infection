using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SubscriptionLog))]
public class SubscriptionLogProperty : PropertyDrawer
{
    private static int s_expandedLineCount = 5; //SubscriptionLog property count + title

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true; //disable enable to make read only 
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int lineCount = property.isExpanded ? s_expandedLineCount : 1;
        return EditorGUIUtility.singleLineHeight * lineCount + EditorGUIUtility.standardVerticalSpacing * (lineCount - 1);
    }
}
