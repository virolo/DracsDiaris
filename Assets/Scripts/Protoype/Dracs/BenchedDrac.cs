using UnityEngine;

public class BenchedDrac : MonoBehaviour
{
    [SerializeField]
    private DracData _dracData;

    [SerializeField]
    private DracSelectorSlot _slot;

    private float _timeRemaining = 0;

    public DracData DracData => _dracData;
    public float TimeRemaining => _timeRemaining;
    public DracSelectorSlot Slot => _slot;


    private void Start()
    {
        _timeRemaining = _dracData._time;
    }

    public void GetOnBench(float timeRemaining)
    {
        _timeRemaining = timeRemaining;
        _slot.Activate(timeRemaining);
    }
    
    public void Update()
    {
        if(_timeRemaining < _dracData._time)
        {
            _timeRemaining += Time.deltaTime * 2;//TODO: Substituir aquest x2 per una variable global que pugui modificar disseny
            _slot.UpdateTimerBar(_timeRemaining/_dracData._time);
        }
        else if(_timeRemaining > _dracData._time)
        {
            _timeRemaining = _dracData._time;
            _slot.UpdateTimerBar(_timeRemaining / _dracData._time);
        }
    }
}
