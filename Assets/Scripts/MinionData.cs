using System;
using UnityEngine;

[Serializable]
public class MinionData
{
    public MinionSO MinionSo;
    public Vector2 position;
    public FloatVariable health;
    public FloatVariable maxhealth;
    public FloatVariable damage;
    public bool attacked;
}