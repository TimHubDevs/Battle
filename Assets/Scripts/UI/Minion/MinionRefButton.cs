using UnityEngine;

public class MinionRefButton : MonoBehaviour
{
    [SerializeField] private MinionRef _minionPrefab;
    [SerializeField] private MinionSpawner _minionSpawner;

    private void OnMouseDown()
    {
        var buttons = FindObjectsOfType<MinionRefButton>();
        foreach (MinionRefButton button in buttons)
        {
            button.GetComponent<SpriteRenderer>().color = new Color32(41, 41, 41, 255);
        }

        GetComponent<SpriteRenderer>().color = Color.white;
        _minionSpawner.SetSelectedDefender(_minionPrefab);
    }
}
