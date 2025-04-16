using UnityEngine;

public class BenchedDrac
{
    private DracData _dracData;
    private float _timeRemaining = 0;

    public DracData DracData => _dracData;
    public float TimeRemaining => _timeRemaining;

    
    // No pots cridar al constructor a una classe Mono Behaviour 
    public BenchedDrac(DracData data, float timeRemaining)
    {
        _dracData = data;
        _timeRemaining = timeRemaining;
    }
    
    private void Update()
    {
        if(_timeRemaining < _dracData._time)
        {
            _timeRemaining += Time.deltaTime * 2;//TODO: Substituir aquest x2 per una variable global que pugui modificar disseny
        }
        else if(_timeRemaining > _dracData._time)
        {
            _timeRemaining = _dracData._time;
        }
    }
}
