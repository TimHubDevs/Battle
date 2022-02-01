using UnityEngine;

public class MinionButton : MonoBehaviour
{
    private GameMinionController _gameMinionController;
    [SerializeField] private GameObject _minionGO;

    private void Awake()
    {
        _gameMinionController = FindObjectOfType<GameMinionController>();
    }

    private void OnMouseDown()
    {
        Debug.Log("Choose " + _minionGO.GetComponent<Minion>().GetMinionPosition());
        _gameMinionController.SetSelectedMinion(_minionGO);
    }
}