using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerConfig", order = 1)]
public class PlayerBattleConfigSO : ScriptableObject
{
    public int countCloser;
    public int countFarer;
    public MinionSO closerSO;
    public MinionSO farerSO;
    public ListMinionDataSO listMinionDataSo;
    private MinionSO[] MinionSos;
    private Vector2[] _enemyPositions = new []
    {
        new Vector2(3,1),
        new Vector2(3,2),
        new Vector2(3,3),
        new Vector2(4,3),
        new Vector2(4,2),
        new Vector2(4,1),
    };

    private void OnEnable()
    {
        MinionSos = new[] {closerSO, farerSO};
    }

    public void SetMinions(Dictionary<Vector2, TypeFighter> minionData)
    {
        int count = 1;
        foreach (var fighterData in minionData)
        {
            switch (fighterData.Value)
            {
                case TypeFighter.CLOSER:
                {
                    AddNewMinion(closerSO, fighterData.Key, count);
                    break;
                }
                case TypeFighter.FARER:
                {
                    AddNewMinion(farerSO, fighterData.Key, count);
                    break;
                }
            }

            count++;
        }
    }
   
    public void SetAIMinions()
    {
        int random = UnityEngine.Random.Range(1, _enemyPositions.Length);
        var newArray = _enemyPositions.SubArray(random, _enemyPositions.Length - random);
        var rnd = new System.Random();
        int count = 1;
        foreach (var position in newArray)
        {
            int randomNext = rnd.Next(MinionSos.Length);
            AddNewMinion(MinionSos[randomNext], position, count);
            count++;
        }
    }

    private void AddNewMinion(MinionSO minionSo, Vector2 position, int count)
    {
        FloatVariable hpInstance = MakeScriptableObject.CreateFloatVariableAsset("HP" + count);
        hpInstance.Init(minionSo.health);
        FloatVariable maxHpInstance = MakeScriptableObject.CreateFloatVariableAsset("MaxHP" + count);
        maxHpInstance.Init(minionSo.health);
        
        var minionData = new MinionData
        {
            MinionSo = minionSo,
            position = position,
            health = hpInstance,
            damage = minionSo.damage
        };
        listMinionDataSo.Items.Add(minionData);
    }

    public void SetCountCloser(int amount)
    {
        countCloser = amount;
    }
    public void SetCountFarer(int amount)
    {
        countFarer = amount;
    }

    public void ResetConfig()
    {
        countCloser = 0;
        countFarer = 0;
        listMinionDataSo.ClearList();
    }
}
