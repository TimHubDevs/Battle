using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameDataSO _gameDataSo;
    [SerializeField] private GameObject _roundTextHolder;
    [SerializeField] private Text _turnText;
    [SerializeField] private Text _roundValueText;

    private void Start()
    {
        _roundValueText.text = "Round: " + _gameDataSo.round;
        StartCoroutine(ShowRoundHolder());
    }

    private IEnumerator ShowRoundHolder()
    {
        _roundTextHolder.SetActive(true);
        _turnText.text = _gameDataSo.queueType + " TURN";
        yield return new WaitForSeconds(2);
        _roundTextHolder.SetActive(false);
    }

    public void ShowQueue()
    {
        StartCoroutine(ShowRoundHolder());
    }
    
    public void ShowRound()
    {
        _roundValueText.text = "Round: " + _gameDataSo.round;
    }
}
