using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const float REACH_THRESHOLD = .1f;

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
    
    public void SetupEnemy(EnemyData enemyData)
    {
        _currentHealth = enemyData._health;

        _enemyData = enemyData;
        
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Material material = new Material(meshRenderer.material);
        material.SetColor("_BaseColor", enemyData._color);
        meshRenderer.material = material;
    }
    
    public void Init(List<Vector3> pathPoints)
    {
        _pathPoints = new Queue<Vector3>(pathPoints);

        if (_pathPoints.Count > 0)
        {
            transform.position = _pathPoints.Dequeue();
            SetNextTarget();
        }


        _totalPathLength = 0;

        for (int i = 0; i < pathPoints.Count - 1; i++)
        {
            _totalPathLength += Vector3.Distance(pathPoints[i], pathPoints[i + 1]);
        }
    }

    private void Update()
    {
        if (!_hasTarget) return;

        Vector3 direction = _currentTarget - transform.position;
        float distance = direction.magnitude;
        direction.Normalize();

        _traveledDistance += _enemyData._speed * Time.deltaTime;
        transform.position += direction * (_enemyData._speed * Time.deltaTime);

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