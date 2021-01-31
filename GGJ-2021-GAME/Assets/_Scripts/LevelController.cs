using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private int _startEnemies = 5;
    [SerializeField] private List<Transform> _originalSpawnpoints;   
    private Transform _spawnLocation;

    [SerializeField] private GameObject _enemyRat;
    [SerializeField] private GameObject _enemyBat;
    [SerializeField] private GameObject _enemyGhost;



    // Start is called before the first frame update
    void Start()
    {      
        if (_startEnemies > 0)
        { 
            SpawnEnemies();
        }

        GameManager.Instance.AssignLevelController(this);
        
    }

    private void Update()
    {
        if (GameManager.Instance.enemiesAlive <= 1)
        {
            _startEnemies = 3;
            SpawnEnemies();
        }
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < _startEnemies; i++)
        {
            Instantiate(ChooseEnemy(), ChooseSpawn());
            GameManager.Instance.enemiesAlive += 1;
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
        _spawnLocation = _originalSpawnpoints[Random.Range(0, _originalSpawnpoints.Count)];
        return _spawnLocation;
    }
}
