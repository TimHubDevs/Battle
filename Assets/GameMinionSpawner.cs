using UnityEngine;

public class GameMinionSpawner : MonoBehaviour
{
    [SerializeField] private ListMinionDataSO _listPlayerMinionDataSo;
    [SerializeField] private ListMinionDataSO _listAIMinionDataSo;
    private void Start()
    {
        foreach (var minionData in _listPlayerMinionDataSo.Items)
        {
            GameObject minion = Instantiate(minionData.MinionSo.prefab, minionData.position, Quaternion.identity);
        }
        foreach (var minionData in _listAIMinionDataSo.Items)
        {
            GameObject minion = Instantiate(minionData.MinionSo.prefab, minionData.position, Quaternion.identity);
        }
    }

}
