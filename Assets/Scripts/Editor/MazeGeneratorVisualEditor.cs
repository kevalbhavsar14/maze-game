using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MazeGeneratorVisual))]
public class MazeGeneratorVisualEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MazeGeneratorVisual myTarget = (MazeGeneratorVisual)target;
        GUILayout.Space(10);
        if (GUILayout.Button("Generate"))
        {
            myTarget.GenerateAsync();
        }
        if (GUILayout.Button("Clear"))
        {
            myTarget.Clear();
        }
    }
}