using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuController : MonoBehaviour
{
    [SerializeField] private Button _startNewGameButton;
    [SerializeField] private Button _loadGameButton;
    [SerializeField] private Button _quitGameButton;

    private void Awake()
    {
        _startNewGameButton.onClick.AddListener(() =>
        {
            Debug.Log("Clear SO Battle Config\nLoad Battle Config Scene");
            //Clear SO Battle Config
            //Load Battle Config Scene
            SceneManager.LoadScene("ConfigGame");
        });
        _loadGameButton.onClick.AddListener(() =>
        {
            Debug.Log("Load Game Scene");
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