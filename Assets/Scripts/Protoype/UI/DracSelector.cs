using System;
using UnityEngine;

public class DracSelector : MonoBehaviour
{
    [SerializeField] private DracSelectorSlot _selectorSlot;

    [SerializeField] private ConfiguradorDrac _configuradorDrac;


    public DracData GetSelectedDrac => _selectorSlot.GetData;
    
    private void Start()
    {
        if (_selectorSlot)
            _selectorSlot.Select();
        
        _configuradorDrac.ChangeSelectedDrac(_selectorSlot.GetData);
    }

    public void SelectDrac(DracSelectorSlot selectorSlot)
    {
        if (selectorSlot == _selectorSlot) return;
        
        if (_selectorSlot )
            _selectorSlot.Deselect();

        _selectorSlot = selectorSlot;
        _selectorSlot.Select();
        
        _configuradorDrac.ChangeSelectedDrac(_selectorSlot.GetData);
    }
}
