using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerConfig", order = 1)]
public class PlayerBattleConfigSO : ScriptableObject
{
    public int countCloser;
    public int countFarer;

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
    }
}
