using UnityEngine;

[CreateAssetMenu(fileName = "DracData", menuName = "Prototype/DracData")]
public class DracData : ScriptableObject
{
    public float _damage = 1;
    public float _attackSpeed = 1;
    public float _radius = 1;
    public float _time = 10;

    public TargetingMode _targetingMode = TargetingMode.ClosestToDrac;
    
    public PrjectileType _projectileType;

    public Color _color = default;
}
