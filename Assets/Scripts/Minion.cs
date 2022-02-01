using UnityEngine;

public class Minion : MonoBehaviour
{
    [SerializeField] private MinionSO _minionSo;

    private void Awake()
    {
        gameObject.name = _minionSo.typeFighter.ToString();
    }
}
