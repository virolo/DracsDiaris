
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Enemy _target = null;
    private float _speed = 20.0f;
    private float _damage = 1.0f;
    
    
    Vector3 direction = Vector3.zero;
    LevelManager _levelManager = null;
    
    public void InitProjectile(Vector3 origin, Enemy target, float damage, LevelManager levelManager)
    {
        transform.position = origin;

        _target = target;
        _damage = damage;
        
        direction = (_target.transform.position - transform.position).normalized;

        _levelManager = levelManager;
    }


    private void Update()
    {
        if (_target)
        {
            direction = (_target.transform.position - transform.position).normalized;
        }
        else
        {
            List<Enemy> enemy = _levelManager.GetEnemiesInRange(transform.position, 2.0f);

            if (enemy.Count > 0)
            {
                _target = enemy[0];
                direction = (_target.transform.position - transform.position).normalized;
            }
        }
        
        transform.position += direction * (_speed * Time.deltaTime);

        if (_target && Vector3.Distance(transform.position, _target.transform.position) < 0.1f)
        {
            _target.AddDamage(_damage);
            Destroy(gameObject);
        }
        else if (transform.position.y <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
