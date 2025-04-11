using UnityEngine;

[CreateAssetMenu(fileName = "DracData", menuName = "Prototype/DracData")]
public class DracData : ScriptableObject
{
    public float _damage = 1;
    public float _attackSpeed = 1;
    public float _radius = 1;

    public TargetingMode _targetingMode = TargetingMode.ClosestToDrac;

    public Color _color = default;
}
