using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConfigGameController : MonoBehaviour
{
    [SerializeField] private Button _refreshMinion;
    [SerializeField] private Button _loadGameButton;
    [SerializeField] private GameObject _minionParent;
    [SerializeField] private PlayerBattleConfigSO _playerBattleConfigSo;
    [SerializeField] private GameDataSO _gameDataSO;

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
        var minions = FindObjectsOfType<Minion>();
        foreach (var minion in minions)
        {
            if (minion._typeFighter == TypeFighter.FARER)
            {
                //add minion information to SO Player
                farerCount++;
            }
            if (minion._typeFighter == TypeFighter.CLOSER)
            {
                //add minion information to SO Player
                closerCount++;
            }
        }
        _gameDataSO.SetGameState(GameState.GAME);
        _playerBattleConfigSo.SetCountCloser(closerCount);
        _playerBattleConfigSo.SetCountFarer(farerCount);
    }

    private void ClearMinion()
    {
        var childs = _minionParent.transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            Destroy(_minionParent.transform.GetChild(i).gameObject);
        }
    }
}