using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuController : MonoBehaviour
{
    [SerializeField] private Button _startNewGameButton;
    [SerializeField] private Button _loadGameButton;
    [SerializeField] private Button _quitGameButton;
    [SerializeField] private GameDataSO _gameDataSo;

    private void Awake()
    {
        _startNewGameButton.onClick.AddListener(() =>
        {
            //Clear SO Battle Config
            _gameDataSo.SetGameState(GameState.NEW);
            //Load Battle Config Scene
            SceneManager.LoadScene("ConfigGame");
        });
        _loadGameButton.onClick.AddListener(() =>
        {
            //Load Game Scene
            SceneManager.LoadScene("Game");
        });
        _quitGameButton.onClick.AddListener(() =>
        {
            Debug.Log("Quit the Game");
            //Quit the Game
            Application.Quit();
        });
    }
}