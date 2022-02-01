using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMinionController : MonoBehaviour
{
    [SerializeField] private GameDataSO _gameDataSo;
    [SerializeField] private GameEvent _roundEvent;
    [SerializeField] private GameEvent _queueEvent;
    [SerializeField] private ListMinionDataSO _listPlayerMinionDataSo;
    [SerializeField] private ListMinionDataSO _listAIMinionDataSo;
    [SerializeField] private GameObject _blockCollider;
    private GameObject _selectedPlayerMinion;
    private List<GameObject> _playerMinions = new List<GameObject>();
    private List<GameObject> _aIMinions = new List<GameObject>();

    private void Start()
    {
        foreach (var minionData in _listPlayerMinionDataSo.Items)
        {
            GameObject minion = Instantiate(minionData.MinionSo.prefab, minionData.position, Quaternion.identity);
            minion.GetComponent<MinionHealth>().SetSOHealth(minionData.health, minionData.maxhealth);
            minion.GetComponent<MinionHealth>().onDeath += () =>
            {
                DeleteMinionFromList(_listPlayerMinionDataSo, _playerMinions, minionData);
                Destroy(minion);
            };
            _playerMinions.Add(minion);
        }

        foreach (var minionData in _listAIMinionDataSo.Items)
        {
            GameObject minion = Instantiate(minionData.MinionSo.prefab, minionData.position, Quaternion.identity);
            minion.GetComponent<MinionButton>().enabled = false;
            minion.GetComponent<MinionHealth>().SetSOHealth(minionData.health, minionData.maxhealth);
            minion.GetComponent<MinionHealth>().onDeath += () =>
            {
                DeleteMinionFromList(_listAIMinionDataSo, _aIMinions, minionData);
                Destroy(minion);
            };
            _aIMinions.Add(minion);
        }

        if (_gameDataSo.round != 0)
        {
            StartCoroutine(ChooseMinionForAttack());
            return;
        }

        StartNewRound();
    }

    private void RoundChecker()
    {
        int countAttackers = _listPlayerMinionDataSo.Items.Count + _listAIMinionDataSo.Items.Count;
        int countAttackYet = 0;
        foreach (var minionData in _listPlayerMinionDataSo.Items)
        {
            if (minionData.attacked)
            {
                countAttackYet++;
            }
        }

        foreach (var minionData in _listAIMinionDataSo.Items)
        {
            if (minionData.attacked)
            {
                countAttackYet++;
            }
        }

        if (countAttackers == countAttackYet)
        {
            //clean list data about attacking
            foreach (var minionData in _listPlayerMinionDataSo.Items)
            {
                minionData.attacked = false;
            }

            foreach (var minionData in _listAIMinionDataSo.Items)
            {
                minionData.attacked = false;
            }

            //new Round
            StartNewRound();
        }
        else
        {
            StartCoroutine(ChooseMinionForAttack());
        }
    }

    private void StartNewRound()
    {
        Debug.Log("New Round");
        _gameDataSo.round++;
        _roundEvent.Raise();

        StartCoroutine(ChooseMinionForAttack());
    }

    private void DeleteMinionFromList(ListMinionDataSO minionList, List<GameObject> gameObjectsMinions,
        MinionData minionData)
    {
        minionList.Remove(minionData);
        foreach (var minion in gameObjectsMinions)
        {
            if (minion.GetComponent<Minion>().GetMinionPosition() == minionData.position)
            {
                gameObjectsMinions.Remove(minion);
            }
        }
    }

    private IEnumerator ChooseMinionForAttack()
    {
        var rnd = new System.Random();
        Debug.LogError(_gameDataSo.queueType);
        if (_gameDataSo.queueType == QueueType.AI)
        {
            //check if AI have a minion for attack yet
            int countAIMinion = _listAIMinionDataSo.Items.Count;
            int attackerYet = 0;
            foreach (var minionData in _listAIMinionDataSo.Items)
            {
                if (minionData.attacked)
                {
                    attackerYet++;
                }
            }

            if (countAIMinion == attackerYet)
            {
                _gameDataSo.queueType = QueueType.PLAYER;
                StartCoroutine(ChooseMinionForAttack());
                yield break;
            }

            //queue event invoke
            _queueEvent.Raise();

            //block chosen minion for player (off/on collider)
            _blockCollider.SetActive(true);

            int randomNext = rnd.Next(_listAIMinionDataSo.Items.Count);

            if (_listAIMinionDataSo.Items[randomNext].attacked == true)
            {
                StartCoroutine(ChooseMinionForAttack());
                yield break;
            }

            foreach (var aIMinion in _aIMinions)
            {
                if (aIMinion.GetComponent<Minion>().GetMinionPosition() ==
                    _listAIMinionDataSo.Items[randomNext].position)
                {
                    //attack
                    aIMinion.GetComponent<Minion>().Attack();
                    _listAIMinionDataSo.Items[randomNext].attacked = true;

                    //change queue
                    _gameDataSo.queueType = QueueType.PLAYER;

                    RoundChecker();
                    yield break;
                }
            }
        }

        if (_gameDataSo.queueType == QueueType.PLAYER)
        {
            //check if Player have a minion for attack yet
            int countPlayerMinion = _listPlayerMinionDataSo.Items.Count;
            int attackerYet = 0;
            foreach (var minionData in _listPlayerMinionDataSo.Items)
            {
                if (minionData.attacked)
                {
                    attackerYet++;
                }
            }

            if (countPlayerMinion == attackerYet)
            {
                _gameDataSo.queueType = QueueType.AI;
                StartCoroutine(ChooseMinionForAttack());
                yield break;
            }

            //queue event invoke
            _queueEvent.Raise();

            //unblock chosen minion for player (off/on collider)
            _blockCollider.SetActive(false);

            yield return new WaitUntil((() => _selectedPlayerMinion != null));

            for (int i = _playerMinions.Count - 1; i >= 0; i--)
            {
                if (_playerMinions[i] == _selectedPlayerMinion)
                {
                    if (_listPlayerMinionDataSo.Items[i].attacked == true)
                    {
                        //clear selected Minion
                        _selectedPlayerMinion = null;
                        StartCoroutine(ChooseMinionForAttack());
                        yield break;
                    }

                    //attack
                    _playerMinions[i].GetComponent<Minion>().Attack();
                    _listPlayerMinionDataSo.Items[i].attacked = true;

                    //change queue
                    _gameDataSo.queueType = QueueType.AI;

                    //clear selected Minion
                    _selectedPlayerMinion = null;

                    RoundChecker();
                    yield break;
                }
            }
        }
    }

    public void SetSelectedMinion(GameObject minionGO)
    {
        _selectedPlayerMinion = minionGO;
    }
}