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
        Drac currentDrac = selectedSlot.GetComponentInChildren<Drac>();
        BenchedDrac benchedDrag = _levelManager.Bench.PlaceBenchedDrac(_dracSelector.GetSelectedDrac);

        if (!currentDrac && benchedDrag != null)
        {
            Drac drac = Instantiate(_dracRef, selectedSlot.transform);
            drac.Init(_levelManager, benchedDrag.DracData, benchedDrag.TimeRemaining);
        }
        else if (currentDrac != null && currentDrac.DracData != _dracSelector.GetSelectedDrac && benchedDrag != null)
        {
            _levelManager.Bench.BenchDrac(currentDrac);
            Destroy(currentDrac.gameObject);

            Drac drac = Instantiate(_dracRef, selectedSlot.transform);
            drac.Init(_levelManager, benchedDrag.DracData, benchedDrag.TimeRemaining);
        }
        else if(currentDrac && benchedDrag != null)
        {
            _levelManager.Bench.BenchDrac(currentDrac);
            Destroy(currentDrac.gameObject);
        }




    }
}
