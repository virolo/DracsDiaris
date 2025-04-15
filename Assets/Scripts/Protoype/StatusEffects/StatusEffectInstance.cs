using System;

[Serializable]
public class StatusEffectInsatnce
{
    public StatusEffect Effect { get; private set; }
    private float _remainingDuration;
    private object _source;
    private object _target;
    
    public StatusEffectInsatnce(StatusEffect effect,object target, object source = null)
    {
        Effect = effect.Clone();
        _source = source;
        _target = target;
        _remainingDuration = _source == null ? effect.Duration : float.PositiveInfinity;
    }
    
    public bool IsFromSource(object source) => _source == source;
    
    public bool Update(object target, float deltaTime)
    {
        Effect.Tick(target, deltaTime);
        _remainingDuration -= deltaTime;
        
        if (float.IsPositiveInfinity(_remainingDuration)) return true;
        
        if (_remainingDuration <= 0)
        {
            Effect.OnEnd();
            return false;
        }

        return true;
    }
    
    public bool MatchesEffect(StatusEffect effect) => Effect.GetType() == effect.GetType();
}
