using System;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{

    [SerializeField] private LevelManager _levelManager;
    
    [SerializeField] private List<DracSlot> _slots = new List<DracSlot>();
    [SerializeField] private DracSelector _dracSelector;

    [SerializeField] private Drac _dracRef;
    
    private void Start()
    {
        foreach (DracSlot slot in _slots)
        {
            slot.OnSelectedSlot += OnSelectedSlot;
        }
    }

    private void OnSelectedSlot(DracSlot selectedSlot)
    {
        Drac _currentDrac = selectedSlot.GetComponentInChildren<Drac>();
        if ( !_currentDrac)
        {
            Drac drac = Instantiate(_dracRef, selectedSlot.transform);
            drac.Init(_levelManager,_dracSelector.GetSelectedDrac);
        }
        else if (_currentDrac.DracData != _dracSelector.GetSelectedDrac)
        {
            Destroy(_currentDrac.gameObject);
            Drac drac = Instantiate(_dracRef, selectedSlot.transform);
            drac.Init(_levelManager,_dracSelector.GetSelectedDrac);
        }
        else
        {
            Destroy(_currentDrac.gameObject);
        }
    }
}
