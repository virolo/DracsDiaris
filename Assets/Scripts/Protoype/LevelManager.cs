using System;
using Unity.Collections;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    public enum LevelState
    {
        Setup,
        Wave,
        Shop,
        GameOver,
        Win
    }

    [Header("Components")] [SerializeField]
    private EnemiesManager _enemies;
    [SerializeField] private WaveSystem _waveSystem;

    [SerializeField] private BenchManager _bench;
    
    [Header("Values")] [SerializeField] private float _princessHealth = 100f;
    [SerializeField] private float _waveCount = 3f;


    [ReadOnly] private LevelState _levelState;

    public EnemiesManager Enemies => _enemies;
    public BenchManager Bench => _bench;

    private void Start()
    {
        _levelState = LevelState.Setup;
    }

    private void Update()
    {
        UpdateState();
    }
    
    public void ChangeState(LevelState newState)
    {
        EndState(_levelState,newState);
        StartState(_levelState,newState);
    }
    
    private void StartState(LevelState currentState, LevelState newState)
    {
     
        switch (_levelState)
        {
            case LevelState.Setup:
                
                Enemies.SpawnEnemies(_waveSystem.GetNextWave());
                break;
            case LevelState.Wave:
                InitWave();
                break;
            case LevelState.Shop:
                break;
            case LevelState.GameOver:
                break;
            case LevelState.Win:
                break;
        }
    }

    private void UpdateState()
    {
        switch (_levelState)
        {
            case LevelState.Setup:
                break;
            case LevelState.Wave:

                if (_enemies.HasEnemies)
                {
                    
                }
                else
                {
                    ChangeState(LevelState.Shop);
                }
                break;
            case LevelState.Shop:
                break;
            case LevelState.GameOver:
                break;
            case LevelState.Win:
                break;
        }
    }
    
    private void EndState(LevelState currentState, LevelState newState)
    {
        switch (_levelState)
        {
            case LevelState.Setup:
                break;
            case LevelState.Wave:
                break;
            case LevelState.Shop:
                break;
            case LevelState.GameOver:
                break;
            case LevelState.Win:
                break;
        }
    }
    public void InitWave()
    {
        _enemies.InitWave(this);
    }
}