using System.IO;
using UnityEngine;
[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 0)]
public class GameDataSO : ScriptableObject
{
    public GameState gameState;
    public QueueType queueType;
    public int round;
    public PlayerBattleConfigSO playerBattleConfigSo;
    public PlayerBattleConfigSO aiBattleConfigSo;

    private void OnValidate()
    {
        if (gameState != GameState.NEW) return;
        round = 0;
        playerBattleConfigSo.ResetConfig();
        aiBattleConfigSo.ResetConfig();
        ClearHPSO();
        RandomiseQueue();
    }

    private void ClearHPSO()
    {
        Debug.Log("ClearHPSO");
        if (Directory.Exists("Assets/Scripts/SO/Assets/HP"))
        {
            Directory.Delete("Assets/Scripts/SO/Assets/HP", true);
            Debug.Log("Delete");
        }
        Directory.CreateDirectory("Assets/Scripts/SO/Assets/HP");
        Debug.Log("CreateDirectory");
    }

    private void RandomiseQueue()
    {
        var rnd = new System.Random();
        QueueType[] queueArray = {QueueType.PLAYER, QueueType.AI};
        int randomNext = rnd.Next(queueArray.Length);
        queueType = queueArray[randomNext];
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
    }
}

public enum GameState
{
    NEW,
    GAME
}
public enum QueueType
{
    PLAYER,
    AI
}