using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const float REACH_THRESHOLD = .1f;

    private LevelManager _levelManager;
    private Queue<Vector3> _pathPoints;
    private Vector3 _currentTarget;

    private bool _hasTarget = false;

    private EnemyData _enemyData;


    public event Action OnPathComplete;

    
    private float _totalPathLength = 0.0f;    
    private float _traveledDistance = 0.0f;

    private float _currentHealth = 0f;
    public float ProgressPercent { get; private set; }

    public float Health => _currentHealth;
    
    [SerializeField] private StatusEffectsController _statusEffectsController;



    public void ApplyEffect(StatusEffect effect, object source = null)
    {
        _statusEffectsController.ApplyEffect(effect, source);
    }

    public void RemoveEffect(StatusEffect effect, object source = null)
    {
        _statusEffectsController.RemoveEffect(effect, source);
    }

    private float Speed
    {
        get
        {
            float speedMultiplier = _statusEffectsController.GetModifier(StatusType.Speed);
            return _enemyData._speed * speedMultiplier * Time.deltaTime;
        }
    }
    
    public void SetupEnemy(EnemyData enemyData)
    {
        _currentHealth = enemyData._health;

        _enemyData = enemyData;
        
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Material material = new Material(meshRenderer.material);
        material.SetColor("_BaseColor", enemyData._color);
        meshRenderer.material = material;
    }
    
    public void Init(List<Vector3> pathPoints, LevelManager levelManager)
    {
        _pathPoints = new Queue<Vector3>(pathPoints);
        _levelManager = levelManager;

        _totalPathLength = 0;

        for (int i = 0; i < pathPoints.Count - 1; i++)
        {
            _totalPathLength += Vector3.Distance(pathPoints[i], pathPoints[i + 1]);
        }
        
        _levelManager.Enemies.RegisterEnemy(this);
        StartMoving();
    }

    private void StartMoving()
    {
        if (_pathPoints.Count > 0)
        {
            transform.position = _pathPoints.Dequeue();
            SetNextTarget();
        }
    }

    private void OnDestroy()
    {
        if (_levelManager != null)
        {
            _levelManager.Enemies.UnregisterEnemy(this);
        }
    }

    private void Update()
    {
        if (!_hasTarget) return;

        Vector3 direction = _currentTarget - transform.position;
        float distance = direction.magnitude;
        direction.Normalize();

        _traveledDistance += Speed;
        transform.position += direction * Speed;

        if (distance <= REACH_THRESHOLD)
        {
            SetNextTarget();
        }
        
        ProgressPercent = _traveledDistance / _totalPathLength;
    }

    private void SetNextTarget()
    {
        if (_pathPoints.Count > 0)
        {
            _currentTarget = _pathPoints.Dequeue();
            _hasTarget = true;
        }
        else
        {
            _hasTarget = false;
            OnPathComplete?.Invoke();
            Destroy(gameObject);
        }
    }

    public void AddDamage(float damage)
    {
        _currentHealth -= damage;
        
        if (_currentHealth <= 0)
            Destroy(gameObject);
    }
}