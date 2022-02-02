using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEvent))]
public class SomeScriptEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Raise!"))
        {
            var evenTest = target as GameEvent;
            evenTest.Raise();
        }
    }
}