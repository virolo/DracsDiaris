using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum TargetingMode
{
    ClosestToDrac,
    LowestHealth,
    HighestHealth,
    HighestProgress,
    LowestProgress,
}

public class Drac : MonoBehaviour
{
    
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private DracData _dracData;

    [SerializeField] private ProjectileDatabase _projectileDatabase;

    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Transform _projectileSpawnPoint;
    
    [SerializeField] private Projectile _projectilePrefab;

    [SerializeField] private StatusEffect _areaEffect;

    private float _lastShotTime = 0.0f;

    private float _lastRadius = 0.0f;
    
    private const int CIRCLE_SEGMENTS = 20;

    private LineRenderer _lineRenderer;
    
    private ProjectileFactory _projectileFactory;

    private float _timeRemaining = 0;

    private bool _isAttacking = false;

    private HashSet<Enemy> _previousEnemiesInRange = new HashSet<Enemy>();

    public DracData DracData => _dracData;

    public float TimeRemaining => _timeRemaining;
    
    public void Init(LevelManager levelManager, DracData dracData, float timeRemaining = -1)
    {
        _levelManager = levelManager;
        _dracData = dracData;

        if(timeRemaining != -1)
        {
            _timeRemaining = timeRemaining;
        }
        else
        {
            _timeRemaining = dracData._time;
        }

        Material material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        _meshRenderer.material = material;
        material.color = _dracData._color;

        _projectileFactory = new ProjectileFactory(_projectileDatabase);
    }
    
    private void Start()
    {
        _lineRenderer = gameObject.AddComponent<LineRenderer>();
        
        _lineRenderer.positionCount = CIRCLE_SEGMENTS + 1;
        _lineRenderer.useWorldSpace = false;
        _lineRenderer.loop = true;
        _lineRenderer.widthMultiplier = 0.05f;
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        _lineRenderer.startColor = _dracData._color;
        _lineRenderer.endColor = _dracData._color;

        DrawRangeCircle(_dracData._radius);
        _lastRadius= _dracData._radius;
    }

    private void DrawRangeCircle( float radius)
    {
        float angleStep = 360f / CIRCLE_SEGMENTS;

        for (int i = 0; i <= CIRCLE_SEGMENTS; i++)
        {
            float angle = Mathf.Deg2Rad * angleStep * i;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            _lineRenderer.SetPosition(i, new Vector3(x, 0, z));
        }
    }

    private void Update()
    {
        if (!_levelManager || !_dracData) return;
        
        if (_dracData._radius != _lastRadius)
        {
            DrawRangeCircle(_dracData._radius);
            _lastRadius = _dracData._radius;
        }

        if (_timeRemaining == 0) return;

        CheckEnemiesInRange();
        UpdateTimeRemaining();
    }

    private void UpdateTimeRemaining()
    {
        if (!_isAttacking) return;

        _timeRemaining -= Time.deltaTime;

        if(_timeRemaining < 0)
        {
            _timeRemaining = 0;
        }
    }

    private void ShootProjectile(Enemy target)
    {
        Projectile projectile = Instantiate(_projectileFactory.GetProjectile(_dracData._projectileType), transform);
        projectile.InitProjectile(_projectileSpawnPoint.position, target, _dracData._damage,_levelManager);
    }

    private void OnEnemyEnterRange(Enemy enemy)
    {
        if (_areaEffect)
            enemy.ApplyEffect(_areaEffect,this);
    }
    
    private void OnEnemyExitRange(Enemy enemy)
    {
        if (_areaEffect)
            enemy.RemoveEffect(_areaEffect,this);
    }

    private void CheckEnemiesInRange()
    {
        List<Enemy> enemiesList = _levelManager.Enemies.GetEnemiesInRange(transform.position, _dracData._radius);
        HashSet<Enemy> currentEnemiesInRange = new HashSet<Enemy>(enemiesList);


        foreach (Enemy enemy in currentEnemiesInRange)
        {
            if (!_previousEnemiesInRange.Contains(enemy))
                OnEnemyEnterRange(enemy);
        }

        foreach (Enemy previous in _previousEnemiesInRange)
        {
            if (!currentEnemiesInRange.Contains(previous))
                OnEnemyExitRange(previous);
        }
        
        _previousEnemiesInRange = currentEnemiesInRange;

        if (enemiesList.Count == 0)
        {
            _isAttacking = false;
            return;
        }

        _isAttacking = true;
        
        Enemy selectedTarget = SelectTarget(enemiesList);

        if (selectedTarget != null&& Time.time >= _lastShotTime + (1/_dracData._attackSpeed))
        {
            ShootProjectile(selectedTarget);
            _lastShotTime = Time.time;
            
            Vector3 direction = selectedTarget.transform.position - _meshRenderer.transform.position;
            direction.y = 0f; 

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                Quaternion modelCorrection = Quaternion.Euler(-90f, 0f, 180f);

                _meshRenderer.transform.rotation = targetRotation * modelCorrection;
            }
        }
    }
    
    private Enemy SelectTarget(List<Enemy> enemies)
    {
        switch (_dracData._targetingMode)
        {
            case TargetingMode.ClosestToDrac:
                return enemies
                    .OrderBy(e => Vector3.Distance(transform.position, e.transform.position))
                    .FirstOrDefault();

            case TargetingMode.LowestHealth:
                return enemies
                    .OrderBy(e => e.Health)
                    .FirstOrDefault();

            case TargetingMode.HighestHealth:
                return enemies
                    .OrderByDescending(e => e.Health)
                    .FirstOrDefault();
            
            case TargetingMode.HighestProgress:
                return enemies
                    .OrderByDescending(e => e.ProgressPercent)
                    .FirstOrDefault();
            
            case TargetingMode.LowestProgress:
                return enemies
                    .OrderBy(e => e.ProgressPercent)
                    .FirstOrDefault();

            default:
                return null;
        }
    }
}