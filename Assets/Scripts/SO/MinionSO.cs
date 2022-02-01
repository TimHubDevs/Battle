using UnityEngine;

[CreateAssetMenu(fileName = "Minion", menuName = "ScriptableObjects/Minion", order = 2)]
public class MinionSO : ScriptableObject
{
    public TypeFighter typeFighter;
    public GameObject prefab;
    public float health;
    public FloatVariable damage;
}

public enum TypeFighter
{
    CLOSER,
    FARER
}