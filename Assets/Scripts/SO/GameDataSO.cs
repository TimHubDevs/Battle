using UnityEngine;
[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 0)]
public class GameDataSO : ScriptableObject
{
    public GameState gameState;
    public PlayerBattleConfigSO playerBattleConfigSo;
    public PlayerBattleConfigSO aiBattleConfigSo;

    private void OnValidate()
    {
        if (gameState != GameState.NEW) return;
        playerBattleConfigSo.ResetConfig();
        aiBattleConfigSo.ResetConfig();
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