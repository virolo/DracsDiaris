using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab = null;

    private List<Enemy> _waitingEnemies = new List<Enemy>();

    [ContextMenu("Generate Enemies")]
    public void AddEnemies(int enemiesAmount)
    {
        for (int i = 0; i < enemiesAmount; i++)
        {
            Enemy newEnemy = Instantiate(_enemyPrefab,
                transform.position + new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-11f, 11f)),
                Quaternion.identity);

            newEnemy.transform.parent = transform;

            _waitingEnemies.Add(newEnemy);
        }
    }

    public void AddEnemies(int enemiesAmount, EnemyData enemyData)
    {
        for (int i = 0; i < enemiesAmount; i++)
        {
            Enemy newEnemy = Instantiate(_enemyPrefab,
                transform.position + new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-11f, 11f)),
                Quaternion.identity);

            newEnemy.transform.parent = transform;

            newEnemy.SetupEnemy(enemyData);
            _waitingEnemies.Add(newEnemy);
        }
    }

    public List<Enemy> GetEnemies
    {
        get
        {
            List<Enemy> enemiesList = new List<Enemy>(_waitingEnemies);

            _waitingEnemies.Clear();
            _waitingEnemies = new List<Enemy>();
            return enemiesList;
        }
    }
    
}