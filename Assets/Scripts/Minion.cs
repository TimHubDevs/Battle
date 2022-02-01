using UnityEngine;

public class Minion : MonoBehaviour
{
    [SerializeField] private MinionSO _minionSo;

    // public Minion(MinionSO minionSo, Vector2 position)
    // {
    //     _minionSo = minionSo;
    //     gameObject.transform.position = position;
    // }

    private void Awake()
    {
        gameObject.name = _minionSo.typeFighter.ToString();
    }
}
