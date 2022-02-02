using System;
using System.IO;
using System.Threading;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 0)]
public class GameDataSO : ScriptableObject
{
    public GameState gameState;
    public QueueType queueType;
    public int round;
    public PlayerBattleConfigSO playerBattleConfigSo;
    public PlayerBattleConfigSO aiBattleConfigSo;
    
    // private void OnEnable()
    // {
    //     Debug.Log("OnEnable");
    //     if (gameState != GameState.NEW) return;
    //     round = 1;
    //     playerBattleConfigSo.ResetConfig();
    //     aiBattleConfigSo.ResetConfig();
    //     ClearHPSO();
    //     RandomiseQueue();
    // }

    private void OnValidate()
    {
        Debug.Log("OnValidate");
        if (gameState != GameState.NEW) return;
        round = 1;
        playerBattleConfigSo.ResetConfig();
        aiBattleConfigSo.ResetConfig();
        ClearHPSO();
        RandomiseQueue();
    }

    private void ClearHPSO()
    {
        string path = "Assets/Scripts/SO/Assets/HP";
        Debug.Log("ClearHPSO");
        if (Directory.Exists(path))
        {
            FileUtil.DeleteFileOrDirectory(path);
            // DeleteDirectoryHard(path);
            // // incomplete!
            // try
            // {
            //     Directory.Delete(path, true);
            // }
            // catch (IOException)
            // {
            //     Thread.Sleep(0);
            //     Directory.Delete(path, true);
            // }
            // Directory.Delete("Assets/Scripts/SO/Assets/HP", true);
            Debug.Log("Delete");
        }
        Directory.CreateDirectory(path);
        Debug.Log("CreateDirectory");
    }

    private void DeleteDirectoryHard(string path)
    {
        for (int i = Directory.GetDirectories(path).Length-1; i >= 0; i--)
        {
            DeleteDirectoryHard(Directory.GetDirectories(path)[i]);

        }
        // foreach (string directory in Directory.GetDirectories(path))
        // {
        //     DeleteDirectoryHard(directory);
        // }

        try
        {
            Directory.Delete(path, true);
        }
        catch (IOException) 
        {
            Directory.Delete(path, true);
        }
        catch (UnauthorizedAccessException)
        {
            Directory.Delete(path, true);
        }
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
        OnValidate();
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
public enum LordType
{
    PLAYER,
    AI
}