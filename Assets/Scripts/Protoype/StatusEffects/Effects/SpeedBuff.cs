using UnityEngine;

[CreateAssetMenu(fileName = "SpeedBuff", menuName = "StatusEffects/SpeedBuff")]
public class SpeedBuff : StatusEffect, IStatModifier
{
    [SerializeField] private float _multiplier = 0.5f;
    
    public StatusType Type => StatusType.Speed;
    
    public float GetModifier(){return _multiplier;}
    
    public override StatusEffect Clone()
    {
        var clone = CreateInstance<SpeedBuff>();
        clone._multiplier = _multiplier;
        return clone;
    }
}
