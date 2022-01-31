using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BattleConfig", order = 2)]
public class BattleConfig : ScriptableObject
{
    public int countCloser;
    public int countFarer;
    public BattlerIndex[] BattlerIndicces = new BattlerIndex[6];
    public List<BattlerIndex> BattlerIndices = new List<BattlerIndex>();
    private void OnValidate()
    {
        if (countCloser + countFarer > 6)
        {
            countCloser = 0;
            countFarer = 0;
            BattlerIndices.Clear();
            BattlerIndicces = new BattlerIndex[6];
            return;
        }
        else if (countCloser > 0)
        {
            for (int i = 0; i < countCloser; i++)
            {
                BattlerIndex battlerIndex = new BattlerIndex();
                battlerIndex.TypeFighter = TypeFighter.CLOSER;
                battlerIndex.indexPosition = i;
                BattlerIndices.Add(battlerIndex);
                BattlerIndicces[i] = battlerIndex;
            }
        }
        else if (countFarer > 0)
        {
            for (int i = 0 ; i < countFarer; i++)
            {
                BattlerIndex battlerIndex = new BattlerIndex();
                battlerIndex.TypeFighter = TypeFighter.FARER;
                battlerIndex.indexPosition = i + BattlerIndices.Count;
                BattlerIndices.Add(battlerIndex);
                BattlerIndicces[i+BattlerIndicces.Length] = battlerIndex;
            }
        }
    }
}

public class BattlerIndex
{
    public TypeFighter TypeFighter;
    public int indexPosition;
}