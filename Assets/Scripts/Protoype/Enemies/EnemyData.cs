using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Prototype/EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField] public float _speed;
    [SerializeField] public float _health;
    [SerializeField] public Color _color;
}
