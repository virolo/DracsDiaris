using UnityEngine;

public abstract class StatusEffect : ScriptableObject
{
    [SerializeField] private float _duraction = 1f;
    public float Duration => _duraction;
    public abstract StatusEffect Clone();
    public virtual void Tick(float dt){}
    public virtual void OnEnd() {}
}