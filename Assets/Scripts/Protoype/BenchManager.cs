using System.Collections.Generic;
using UnityEngine;

public class BenchManager : MonoBehaviour
{
    [SerializeField]
    private List<BenchedDrac> _deck = default;

    private List<BenchedDrac> _bench = default;

    private void Start()
    {
        //_bench = new List<BenchedDrac>();

        FillTheBench();        
    }

    private void Update()
    {
        foreach(BenchedDrac drac in _bench)
        {
            drac.Update();
        }
    }

    private void FillTheBench()
    {
        _bench = new List<BenchedDrac>();
        foreach (BenchedDrac drac in _deck)
        {
            _bench.Add(drac);
        }
    }
    
    public BenchedDrac PlaceBenchedDrac(DracSelector dracSelector)
    {
        foreach(BenchedDrac drac in _bench)
        {
            if(drac.Slot == dracSelector.GetSelectedDrac)
            {
                dracSelector.DeactivateDrac();
                _bench.Remove(drac);
                return drac;
            }
        }

        return null;
    }

    public void BenchDrac(Drac drac)
    {        
        foreach(BenchedDrac dracSlot in _deck)
        {
            if(!dracSlot.Slot.gameObject.activeSelf && dracSlot.Slot.GetData == drac.DracData)
            {
                dracSlot.GetOnBench(drac.TimeRemaining);
                _bench.Add(dracSlot);                
            }
        }
    }

}
