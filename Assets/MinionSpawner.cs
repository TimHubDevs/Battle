using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _minionParent;
    private Minion _minion;

    public void SetSelectedDefender(Minion minionToSelect)
    {
        _minion = minionToSelect;
    }

    private void OnMouseDown()
    {
        AttemptToPlaceDefenderAt(GetSquareClicked());
    }

    private void AttemptToPlaceDefenderAt(Vector2 gridPos)
    {
        SpawnMinion(gridPos);
    }

    private Vector2 GetSquareClicked()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        Vector2 gridPos = SnapToGrid(worldPos);
        return gridPos;
    }

    private Vector2 SnapToGrid(Vector2 rawWorldPos)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newY = Mathf.RoundToInt(rawWorldPos.y);
        return new Vector2(newX, newY);
    }

    private void SpawnMinion(Vector2 roundedPos)
    {
        Minion minion = Instantiate(_minion, roundedPos, Quaternion.identity, _minionParent.transform);
    }
}