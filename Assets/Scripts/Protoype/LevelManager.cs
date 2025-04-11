using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    
    [SerializeField] private UIManager _uiManager;
    
    [SerializeField] private PathManager _pathManager;
    [SerializeField] private EnemiesManager _enemies;
    
    public EnemiesManager Enemies { get { return _enemies; } }
    
    private bool randomWave = false;
    
    [ContextMenu("Initialize Wave")]
    public void InitWave()
    {
        List<Enemy> enemies = _enemies.GetEnemies;
        
        
        if (enemies.Count > 0)
        {
            
            if (randomWave)
                ShuffleList(enemies);
            
            StartCoroutine(SpawnEnemies(enemies));
        }
            
    }
    
    public void RandomizeWave(bool value) => randomWave = value;
    
    private void ShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    private IEnumerator SpawnEnemies(List<Enemy> enemies)
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.transform.parent = _pathManager.transform;
            enemy.Init(_pathManager.GetPathPoints());

            // Esperar entre 0.1s y 0.3s antes de instanciar el siguiente
            yield return new WaitForSeconds(Random.Range(0.2f, 0.8f));
        }
    }


    public List<Enemy> GetEnemiesInRange(Vector3 origin, float range)
    {
        List<Enemy> enemiesinPath = _pathManager.GetComponentsInChildren<Enemy>().ToList();

        List<Enemy> enemiesInRange = new List<Enemy>();

        foreach (Enemy enemy in enemiesinPath)
        {
            if (enemy != null && Vector3.Distance(origin, enemy.transform.position) <= range)
            {
                enemiesInRange.Add(enemy);
            }
        }

        return enemiesInRange
            .OrderBy(enemy => Vector3.Distance(origin, enemy.transform.position))
            .ToList();
    }
}
