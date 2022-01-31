using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConfigGameController : MonoBehaviour
{
    [SerializeField] private Button _refreshMinion;
    [SerializeField] private Button _loadGameButton;
    [SerializeField] private GameObject _minionParent;

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
            //Load Game Scene
            SceneManager.LoadScene("Game");
        });
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