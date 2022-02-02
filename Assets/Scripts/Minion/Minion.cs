using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Minion : MonoBehaviour
{
    [SerializeField] public MinionSO _minionSo;
    [SerializeField] private GameEvent _attackEvent;
    [SerializeField] public LordType _lordType;
    [SerializeField] private List<GameObject> _enemyMinions;
    [SerializeField] private List<GameObject> _closestEnemies = new List<GameObject>();

    public Vector2 GetMinionPosition()
    {
        return transform.position;
    }

    public void ShowOurEnemy(Action callback)
    {
        _closestEnemies.Clear();
        if (_minionSo.typeFighter == TypeFighter.FARER)
        {
            ShowAndChooseEnemy(_enemyMinions, callback);
        }

        if (_minionSo.typeFighter == TypeFighter.CLOSER)
        {
            FindClosestEnemies(3f);

            if (_closestEnemies.Count == 0)
            {
                FindClosestEnemies(4f);
            }

            ShowAndChooseEnemy(_closestEnemies, callback);
        }
    }

    private void ShowAndChooseEnemy(List<GameObject> enemyMinions, Action callback)
    {
        // show
        foreach (var enemy in enemyMinions)
        {
            enemy.GetComponent<SpriteRenderer>().color = new Color32(41, 41, 41, 255);
        }

        //subscribe
        foreach (var enemyMinion in enemyMinions)
        {
            enemyMinion.GetComponent<MinionButton>().onPress += enemyGO =>
            {
                foreach (var enemy in enemyMinions)
                {
                    if (enemy!=null)
                        enemy.GetComponent<SpriteRenderer>().color = Color.white;
                }

                Attack(enemyGO);
                callback.Invoke();
            };
        }
    }

    private void Attack(GameObject enemyGO)
    {
        enemyGO.GetComponent<MinionHealth>().GetDamage(_minionSo.damage.Value);
        Debug.Log($"{gameObject.name} {_lordType} peew in {enemyGO.name}");
        _attackEvent.Raise();
    }

    public void CloserAttack()
    {
        _closestEnemies.Clear();

        FindClosestEnemies(2f);

        if (_closestEnemies.Count == 0)
        {
            FindClosestEnemies(1f);
        }

        int random = Random.Range(0, _closestEnemies.Count);
        Attack(_closestEnemies[random]);
    }

    private void FindClosestEnemies(float x)
    {
        for (int i = _enemyMinions.Count - 1; i >= 0; i--)
        {
            Vector2 enemyPos = _enemyMinions[i].gameObject.transform.position;
            if (enemyPos.x == x)
            {
                if (!_closestEnemies.Contains(_enemyMinions[i]))
                {
                    _closestEnemies.Add(_enemyMinions[i]);
                }
            }
        }
    }

    public void RandomAttack()
    {
        int random = Random.Range(0, _enemyMinions.Count);
        Attack(_enemyMinions[random]);
    }

    public void Init(string name, LordType lordType)
    {
        gameObject.name = name;
        _lordType = lordType;
    }

    public void SetEnemyInfo(List<GameObject> enemyMinions)
    {
        _enemyMinions = enemyMinions;
    }
}