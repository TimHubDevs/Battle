using System.Collections.Generic;
using UnityEditor;
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

    private Vector2[] _enemyPositions = new[]
    {
        new Vector2(3, 1),
        new Vector2(3, 2),
        new Vector2(3, 3),
        new Vector2(4, 3),
        new Vector2(4, 2),
        new Vector2(4, 1),
    };

    private int countMinion = 0;
    private string PlayerPref = "Player";
    private string AIPref = "AI";

    private void OnEnable()
    {
        MinionSos = new[] {closerSO, farerSO};
        countMinion = 0;
    }

    public void SetMinions(Dictionary<Vector2, TypeFighter> minionData)
    {
        foreach (var fighterData in minionData)
        {
            switch (fighterData.Value)
            {
                case TypeFighter.CLOSER:
                {
                    AddNewMinion(closerSO, fighterData.Key, countMinion, PlayerPref);
                    break;
                }
                case TypeFighter.FARER:
                {
                    AddNewMinion(farerSO, fighterData.Key, countMinion, PlayerPref);
                    break;
                }
            }

            countMinion++;
        }
    }

    public void SetAIMinions()
    {
        int random = Random.Range(1, _enemyPositions.Length);
        var newArray = _enemyPositions.SubArray(random, _enemyPositions.Length - random);
        var rnd = new System.Random();
        foreach (var position in newArray)
        {
            int randomNext = rnd.Next(MinionSos.Length);
            AddNewMinion(MinionSos[randomNext], position, countMinion, AIPref);
            countMinion++;
        }
    }

    private void AddNewMinion(MinionSO minionSo, Vector2 position, int count, string pref)
    {
        FloatVariable hpInstance = CreateFloatVariableAsset(pref + "HP" + count);
        hpInstance.Init(minionSo.health);
        EditorUtility.SetDirty(hpInstance);

        var minionData = new MinionData
        {
            MinionSo = minionSo,
            position = position,
            health = hpInstance,
            maxhealth = minionSo.maxHealth,
            damage = minionSo.damage,
            attacked = false
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