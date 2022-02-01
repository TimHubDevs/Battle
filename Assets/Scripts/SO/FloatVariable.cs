using UnityEngine;

[CreateAssetMenu(fileName = "FloatVariable", menuName = "ScriptableObjects/FloatVariable", order = 5)]
public class FloatVariable : ScriptableObject
{
    public float Value;

    public void Init(float value)
    {
        Debug.Log("New " + value);
        value = Value;
        Debug.Log("Is " + Value);
    }
}