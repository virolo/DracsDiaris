using System;
using UnityEngine;

public class EnemiesSelector : MonoBehaviour
{
    [SerializeField] private EnemySelectorSlot _selectorSlot;

    [SerializeField] private EnemiesGeneratorUI _enemiesGeneratorUI;


    public EnemyData GetEnemyData => _selectorSlot.GetData;


    private void Start()
    {
        if (_selectorSlot)
            _selectorSlot.Select();


        _enemiesGeneratorUI.ChangeSelectedEnemy(_selectorSlot.GetData);
    }


    public void SelectEnemy(EnemySelectorSlot selectorSlot)
    {
        if (_selectorSlot == selectorSlot) return;

        if (_selectorSlot)
            _selectorSlot.Deselect();
        
        _selectorSlot = selectorSlot;
        _selectorSlot.Select();
        
        _enemiesGeneratorUI.ChangeSelectedEnemy(_selectorSlot.GetData);
    }
}
