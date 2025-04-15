using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatusEffectsController : MonoBehaviour
{
    [SerializeField] private List<StatusEffectInsatnce> _activeEffects = new List<StatusEffectInsatnce>();

    private void Update()
    {
        float dt = Time.deltaTime;

        for (int i = _activeEffects.Count - 1; i >= 0; i--)
        {
            if (!_activeEffects[i].Update(dt))
            {
                _activeEffects.RemoveAt(i);
            }
        }
    }

    public void ApplyEffect(StatusEffect effect, object source = null)
    {
        if (source != null)
        {
            var instance = new StatusEffectInsatnce(effect, source);
            _activeEffects.Add(instance);
            return;
        }

        var existing = _activeEffects.FirstOrDefault(e => e.MatchesEffect(effect) && e.IsFromSource(null));
        if (existing != null)
        {
            _activeEffects.Remove(existing);
        }
        
        var newInstance = new StatusEffectInsatnce(effect);
        _activeEffects.Add(newInstance);
    }

    public void RemoveEffect(StatusEffect effect, object source = null)
    {
        _activeEffects.RemoveAll(e => e.MatchesEffect(effect) && source == null || e.IsFromSource(source));
    }


    public float GetModifier(StatusType type)
    {
        var activeModifiers = _activeEffects
            .Where(e => e.Effect is IStatModifier modifier && modifier.Type == type)
            .Select(e => ((IStatModifier)e.Effect).GetModifier());
        
        return activeModifiers.Aggregate(1f, (current, modifier) => current * modifier);
    }
}