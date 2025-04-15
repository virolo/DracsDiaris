using System;

[Serializable]
public class StatusEffectInsatnce
{
    public StatusEffect Effect { get; private set; }
    private float _remainingDuration;
    private object _source;
    
    public StatusEffectInsatnce(StatusEffect effect, object source = null)
    {
        Effect = effect.Clone();
        _source = source;
        
        _remainingDuration = _source == null ? effect.Duration : float.PositiveInfinity;
    }
    
    public bool IsFromSource(object source) => _source == source;
    
    public bool Update(float deltaTime)
    {
        Effect.Tick(deltaTime);
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
