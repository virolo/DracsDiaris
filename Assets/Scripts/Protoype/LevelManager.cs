using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private EnemiesManager _enemies;

    public EnemiesManager Enemies => _enemies;
    
    public void InitWave()
    {
        //Start Wave Change Level State
        
        _enemies.InitWave(this);
    }
    
    public List<Enemy> GetEnemiesInRange(Vector3 origin, float range)
    {
        float sqrRange = range * range;
        
        List<(Enemy enemy, float sqrDist)> enemiesInRange = new List<(Enemy,float)>();

        foreach (Enemy enemy in _enemies.ActiveEnemies)
        {
            
            if (enemy == null) continue;
            
            float sqrDist = (enemy.transform.position - origin).sqrMagnitude;
            
            if (sqrDist <= sqrRange)
                enemiesInRange.Add((enemy,sqrDist));
        }

        enemiesInRange.Sort((a,b) => a.sqrDist.CompareTo(b.sqrDist));
        return enemiesInRange.Select(pair => pair.enemy).ToList();
    }
}
