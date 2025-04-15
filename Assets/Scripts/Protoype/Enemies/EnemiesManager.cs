using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab = null;
    [SerializeField] private PathManager _pathManager = null;

    private List<Enemy> _waitingEnemies = new List<Enemy>();
    private List<Enemy> _activeEnemies = new List<Enemy>();


    public bool HasEnemies =>
        _activeEnemies.Count > 0 
        && _waitingEnemies.Count > 0;


    public void SpawnEnemies(Wave wave)
    {
        // Todo Create a Enemy Factory
        for (int i = 0; i < wave.WaveData.Count; i++)
        {
            for (int j = 0; j < wave.WaveData[i].EnemiesAmount; j++)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                Enemy newEnemy = Instantiate(_enemyPrefab, spawnPosition, quaternion.identity);
                newEnemy.SetupEnemy(wave.WaveData[i].EnemyData);
                _waitingEnemies.Add(newEnemy);
            }
        }
    }
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
        
        //Todo- revisar si voleum un ordre determinat
        ShuffleList(toStart);
        
        foreach (Enemy enemy in toStart)
        {
            enemy.Init(_pathManager.GetPathPoints(),levelManager);
            RegisterEnemy(enemy);
            
            //TODO
            yield return new WaitForSeconds(Random.Range(1f, 2f));
        }
    }
    
    public List<Enemy> GetEnemiesInRange(Vector3 origin, float range)
    {
        float sqrRange = range * range;
        
        List<(Enemy enemy, float sqrDist)> enemiesInRange = new List<(Enemy,float)>();

        foreach (Enemy enemy in _activeEnemies)
        {
            
            if (enemy == null) continue;
            
            float sqrDist = (enemy.transform.position - origin).sqrMagnitude;
            
            if (sqrDist <= sqrRange)
                enemiesInRange.Add((enemy,sqrDist));
        }

        enemiesInRange.Sort((a,b) => a.sqrDist.CompareTo(b.sqrDist));
        return enemiesInRange.Select(pair => pair.enemy).ToList();
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