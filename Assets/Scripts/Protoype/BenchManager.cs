using System.Collections.Generic;
using UnityEngine;

public class BenchManager : MonoBehaviour
{
    [SerializeField]
    private List<DracData> _dracDeck = default; //En un futur aixo ha de vindre donat pel level manager: quins dracs tindr� el jugador per jugar el nivell

    [SerializeField]
    private List<DracSelectorSlot> _dracSelectorSlots = default;

    private List<BenchedDrac> _bench = default;

    private void Start()
    {
        _bench = new List<BenchedDrac>();
        FillTheBench();
    }

    private void FillTheBench()
    {
        foreach(DracData data in _dracDeck)
        {
           _bench.Add(new BenchedDrac(data, data._time));
        }
    }
    
    public BenchedDrac PlaceBenchedDrac(DracSelector dracSelector)
    {
        foreach(BenchedDrac drac in _bench)
        {
            if(drac.DracData == dracSelector.GetSelectedDrac)
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
        _bench.Add(new BenchedDrac(drac.DracData, drac.TimeRemaining));
        
        foreach(DracSelectorSlot dracSlot in _dracSelectorSlots)
        {
            if(dracSlot.GetData == drac.DracData)
            {
                dracSlot.Activate(drac.TimeRemaining);
            }
        }
    }

}
