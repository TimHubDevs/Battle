using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameDataSO _gameDataSo;
    [SerializeField] private GameObject _roundTextHolder;
    [SerializeField] private Text _turnText;

    private void Start()
    {
        StartCoroutine(ShowRoundHolder());
    }

    private IEnumerator ShowRoundHolder()
    {
        _roundTextHolder.SetActive(true);
        _turnText.text = _gameDataSo.queueType.ToString() + " TURN";
        yield return new WaitForSeconds(2);
        _roundTextHolder.SetActive(false);
    }

    public void ShowRound()
    {
        StartCoroutine(ShowRoundHolder());
    }
}
