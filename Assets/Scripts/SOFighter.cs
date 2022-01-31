using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SOFighter", order = 1)]
public class SOFighter : ScriptableObject
{
    public string fighterName;
    public TypeFighter typeFighter;
    public GameObject prefab;
    public int health;
    public int damage;
}

public enum TypeFighter
{
    CLOSER,
    FARER
}