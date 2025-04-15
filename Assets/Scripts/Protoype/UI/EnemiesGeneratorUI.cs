using UnityEngine;
using UnityEngine.UI;

public class EnemiesGeneratorUI : MonoBehaviour
{
    [SerializeField] private EnemyData _selectedEnemy;

    [SerializeField] private CustomSlider _amountEnemiesSlider;
    [SerializeField] private CustomSlider _speedEnemiesSlider;
    [SerializeField] private CustomSlider _healthEnemiesSlider;

    
    private int _amount = 50;
    
    [SerializeField] private EnemiesManager _enemiesManager;


    private void Start()
    {
        _amountEnemiesSlider.OnValueChanged += SetAmount;
        _speedEnemiesSlider.OnValueChanged += SaveEnemiesSpeed;
        _healthEnemiesSlider.OnValueChanged += SaveEnemiesHealth;
        
        ChangeSelectedEnemy(_selectedEnemy);
    }

    private void SaveEnemiesSpeed(float obj)
    {
        _selectedEnemy._speed = obj;
    }

    private void SaveEnemiesHealth(float obj)
    {
        _selectedEnemy._health = obj;
    }

    private void SetAmount(float obj)
    {
        _amount = (int)obj;
    }
    
    public void SpawnEnemies()
    {
        _enemiesManager.SpawnEnemies(_amount,_selectedEnemy);
    }


    public void ChangeSelectedEnemy(EnemyData enemyData)
    {
        _selectedEnemy = enemyData;
        
        _healthEnemiesSlider.SetValue(enemyData._health);
        _speedEnemiesSlider.SetValue(enemyData._speed);
    }
}