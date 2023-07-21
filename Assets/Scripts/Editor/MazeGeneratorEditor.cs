using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MazeGenerator))]
public class MazeGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MazeGenerator myTarget = (MazeGenerator)target;
        GUILayout.Space(10);
        if (GUILayout.Button("Generate"))
        {
            myTarget.Generate();
        }
        if (GUILayout.Button("Clear"))
        {
            myTarget.Clear();
        }
    }
}