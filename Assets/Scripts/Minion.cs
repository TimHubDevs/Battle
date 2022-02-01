using UnityEngine;

public class Minion : MonoBehaviour
{
    [SerializeField] private MinionSO _minionSo;
    [SerializeField] private GameEvent _attackEvent;

    private void Awake()
    {
        gameObject.name = _minionSo.typeFighter.ToString();
    }

    public Vector2 GetMinionPosition()
    {
        return transform.position;
    }

    public void Attack()
    {
        Debug.Log("Attacking "+ GetMinionPosition());
        _attackEvent.Raise();
    }
}
