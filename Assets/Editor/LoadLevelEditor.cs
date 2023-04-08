using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LoadLevel))]
public class LoadLevelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LoadLevel level = (LoadLevel)target;
        if (GUILayout.Button("Build"))
        {
            level.BuildLevelInEditor();
        }
    }
}
