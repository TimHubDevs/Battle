using UnityEngine;

public class MinionRef : MonoBehaviour
{
    [SerializeField] private TypeFighter _typeFighter;
    private Vector2 _position;

    private void Awake()
    {
        _position = gameObject.transform.position;
    }

    public Vector2 GetPosition()
    {
        return _position;
    }
    public TypeFighter GetTypeFighter()
    {
        return _typeFighter;
    }
}

