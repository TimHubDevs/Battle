using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConfigGameController : MonoBehaviour
{
    [SerializeField] private Button _refreshMinion;
    [SerializeField] private Button _loadGameButton;
    [SerializeField] private GameObject _minionParent;
    [SerializeField] private PlayerBattleConfigSO _playerBattleConfigSo;
    [SerializeField] private PlayerBattleConfigSO _AIBattleConfigSo;
    [SerializeField] private GameDataSO _gameDataSO;
    private Dictionary<Vector2, TypeFighter> _minionData = new Dictionary<Vector2, TypeFighter>();

    private void Awake()
    {
        _refreshMinion.onClick.AddListener(() =>
        {
            //clear all minion
            Debug.Log("clear all minion");
            ClearMinion();
        });
        _loadGameButton.onClick.AddListener(() =>
        {
            Debug.Log("save minion pos and type to SO Battle Config \n Load Game Scene");
            //save minion pos and type to SO Battle Config
            SetAllMinion();
            //Load Game Scene
            SceneManager.LoadScene("Game");
        });
    }

    private void SetAllMinion()
    {
        int closerCount = 0;
        int farerCount = 0;
        var minions = FindObjectsOfType<MinionRef>();
        foreach (var minion in minions)
        {
            if (minion.GetTypeFighter() == TypeFighter.FARER)
            {
                //add minion information to SO Player
                _minionData.Add(minion.GetPosition(), minion.GetTypeFighter());
                farerCount++;
            }
            if (minion.GetTypeFighter() == TypeFighter.CLOSER)
            {
                //add minion information to SO Player
                _minionData.Add(minion.GetPosition(), minion.GetTypeFighter());
                closerCount++;
            }
        }
        SOWriteData(closerCount, farerCount);
    }

    private void SOWriteData(int closerCount, int farerCount)
    {
        _gameDataSO.SetGameState(GameState.GAME);
        _playerBattleConfigSo.SetMinions(_minionData);
        _AIBattleConfigSo.SetAIMinions();
        _playerBattleConfigSo.SetCountCloser(closerCount);
        _playerBattleConfigSo.SetCountFarer(farerCount);
        _AIBattleConfigSo.SetCountCloser(closerCount);
        _AIBattleConfigSo.SetCountFarer(farerCount);
    }

    private void ClearMinion()
    {
        _minionData.Clear();
        var childs = _minionParent.transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            Destroy(_minionParent.transform.GetChild(i).gameObject);
        }
    }
}