using UnityEngine;

[CreateAssetMenu(fileName = "Poison", menuName = "StatusEffects/Poison")]
public class Poison : StatusEffect
{
    [SerializeField] private float _dps = 2.0f;

    public StatusType Type => StatusType.Damage;

    public override void Tick(object target, float dt)
    {
        if (target is IDamagable damagable)
        {
            float damage = _dps * dt;
            damagable.ApplyDamage(damage);
        }
    }

    public override StatusEffect Clone()
    {
        var clone = CreateInstance<Poison>();
        clone._dps = _dps;
        return clone;
    }
}