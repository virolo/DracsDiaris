using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab = null;
    [SerializeField] private PathManager _pathManager = null;

    private List<Enemy> _waitingEnemies = new List<Enemy>();
    private List<Enemy> _activeEnemies = new List<Enemy>();
    
    public List<Enemy> ActiveEnemies => _activeEnemies;
    
    public void SpawnEnemies(int amount, EnemyData data)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            Enemy newEnemy = Instantiate(_enemyPrefab, spawnPosition, quaternion.identity);
            newEnemy.SetupEnemy(data);
            _waitingEnemies.Add(newEnemy);
        }
    }

    public void InitWave(LevelManager levelManager)
    {
        if (_waitingEnemies.Count > 0)
        {
            StartCoroutine(StartWaveEnemies(levelManager));
        }
    }

    private IEnumerator StartWaveEnemies(LevelManager levelManager)
    {
        List<Enemy> toStart = new List<Enemy>(_waitingEnemies);
        _waitingEnemies.Clear(); 
        
        ShuffleList(toStart);
        
        foreach (Enemy enemy in toStart)
        {
            enemy.Init(_pathManager.GetPathPoints(),levelManager);
            RegisterEnemy(enemy);
            
            //TODO
            yield return new WaitForSeconds(Random.Range(0.2f, 0.5f));
        }
    }

    public void RegisterEnemy(Enemy enemy)
    {
        if (!_activeEnemies.Contains(enemy))
        {
            _activeEnemies.Add(enemy);
        }
    }
    
    public void UnregisterEnemy(Enemy enemy)
    {
        _activeEnemies.Remove(enemy);
    }
    
    private void ShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
    
    private Vector3 GetRandomSpawnPosition()
    {
        return transform.position + new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-11f, 11f));
    }
}