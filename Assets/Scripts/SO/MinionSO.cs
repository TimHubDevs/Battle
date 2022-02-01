using UnityEngine;

[CreateAssetMenu(fileName = "Minion", menuName = "ScriptableObjects/Minion", order = 2)]
public class MinionSO : ScriptableObject
{
    // public string fighterName;
    public TypeFighter typeFighter;
    public GameObject prefab;
    // public Vector2 position;
    public int health;
    public int damage;
}

public enum TypeFighter
{
    CLOSER,
    FARER
}