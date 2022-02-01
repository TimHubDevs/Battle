using UnityEngine;

[CreateAssetMenu(fileName = "List", menuName = "ScriptableObjects/List", order = 3)]
public class ListMinionDataSO : RuntimeSetSO<MinionData>
{
    public void ClearList()
    {
        Items.Clear();
    }
}