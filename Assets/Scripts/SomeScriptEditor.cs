using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEvent))]
public class SomeScriptEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.HelpBox("This is a help box", MessageType.Info);
        if (GUILayout.Button("Raise!"))
        {
            foreach (var gameEvent in FindObjectsOfType<GameEvent>())
            {
                gameEvent.Raise();
            }
        }
    }
}