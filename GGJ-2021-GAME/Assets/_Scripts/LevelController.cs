using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private int _startEnemies = 5;
    [SerializeField] private List<Transform> _originalSpawnpoints;
    private List<Transform> _freeSpawnpoints = new List<Transform>();
    private List<Transform> _usedSpawnpoints;
    private Transform _spawnLocation;

    [SerializeField] private GameObject _enemyRat;
    [SerializeField] private GameObject _enemyBat;
    [SerializeField] private GameObject _enemyGhost;



    // Start is called before the first frame update
    void Start()
    {
        _usedSpawnpoints = new List<Transform>();
        _freeSpawnpoints = _originalSpawnpoints;
        if (_startEnemies > 0) { SpawnEnemies(); }
        
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < _startEnemies; i++)
        {
            Instantiate(ChooseEnemy(), ChooseSpawn());

        }
    }

    private GameObject ChooseEnemy()
    {
        var randomMonster = Random.Range(0, 3);
        if (randomMonster == 2) { return _enemyGhost; }
        if (randomMonster == 1) { return _enemyBat; }
        if (randomMonster == 0) { return _enemyRat; }
        else { return _enemyRat; }
    }

    private Transform ChooseSpawn()
    {
        _spawnLocation = _freeSpawnpoints[Random.Range(0, _freeSpawnpoints.Count)];

        if (_usedSpawnpoints.Contains(_spawnLocation))
        {
            ChooseSpawn();
        }

        _freeSpawnpoints.Remove(_spawnLocation);
        _usedSpawnpoints.Add(_spawnLocation);
        return _spawnLocation;
    }
}
