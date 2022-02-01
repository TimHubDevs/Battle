using UnityEngine;
using UnityEditor;

public class MakeScriptableObject {
    [MenuItem("Assets/Create/My Scriptable Object")]
    public static FloatVariable CreateFloatVariableAsset(string name)
    {
        FloatVariable asset = ScriptableObject.CreateInstance<FloatVariable>();

        AssetDatabase.CreateAsset(asset, "Assets/Scripts/SO/Assets/HP/"+name+".asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
        
        return asset;
    }
}