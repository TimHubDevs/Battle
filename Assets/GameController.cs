using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameDataSO _gameDataSo;
    [SerializeField] private Text _turnText;

    private void Awake()
    {
        _turnText.text = _gameDataSo.queueType.ToString() + " TURN";
    }
}
