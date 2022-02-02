using System;
using UnityEngine;

public class MinionButton : MonoBehaviour
{
    private GameMinionController _gameMinionController;
    [SerializeField] private GameObject _minionGO;
    [SerializeField] private Minion _minion;
    public Action<GameObject> onPress;

    private void Awake()
    {
        _gameMinionController = FindObjectOfType<GameMinionController>();
    }

    private void OnMouseDown()
    {
        if (_minion._lordType == LordType.AI)
        {
            onPress.Invoke(gameObject);
            return;
        }
        _gameMinionController.SetSelectedMinion(_minionGO);
    }

}