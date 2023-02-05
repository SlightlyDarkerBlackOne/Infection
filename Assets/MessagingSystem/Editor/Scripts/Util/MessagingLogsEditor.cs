using UnityEditor;

[CustomEditor(typeof(MessagingLogsScriptableObject))]
public class MessagingLogsEditor : Editor
{
    private bool m_doFoldoutSubscriptions = false;
    private bool m_doFoldoutPublishings = false;

    public override void OnInspectorGUI()
    {
        m_doFoldoutSubscriptions = EditorGUILayout.Foldout(m_doFoldoutSubscriptions, "Subscriptions", true);

        bool updated = false;

        if (m_doFoldoutSubscriptions)
        {
            if (!updated)
            {
                serializedObject.Update();
                updated = true;
            }
            FoldOut("m_subscriptions");
        }

        m_doFoldoutPublishings = EditorGUILayout.Foldout(m_doFoldoutPublishings, "Publishings", true);

        if (m_doFoldoutPublishings)
        {
            if (!updated)
            {
                serializedObject.Update(); //to prevent updating twice in case both foldouts are open
            }
            FoldOut("m_publishings");
        }
    }

    private void FoldOut(string serializedPropertyName)
    {
        EditorGUI.indentLevel++;
        SerializedProperty prop = serializedObject.FindProperty(serializedPropertyName);

        for (int i = 0; i < prop.arraySize; i++)
        {
            SerializedProperty subscription = prop.GetArrayElementAtIndex(i); //MessageSubscriptionsLogProperty.cs
            EditorGUILayout.PropertyField(subscription);
        }

        EditorGUI.indentLevel--;
    }
}