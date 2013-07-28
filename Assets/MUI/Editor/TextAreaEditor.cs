using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MUI_AutoType_Text)), CanEditMultipleObjects]
public class TextAreaEditor : Editor
{
    public SerializedProperty longStringProp;
    void OnEnable()
    {
        longStringProp = serializedObject.FindProperty("longString");
    }

    public override void OnInspectorGUI()
    {
       
        serializedObject.Update();

        longStringProp.stringValue = EditorGUILayout.TextArea(longStringProp.stringValue, GUILayout.MaxHeight(100));
      
        serializedObject.ApplyModifiedProperties();
    }
}