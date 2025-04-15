using System.Collections.Generic;
using UnityEngine;

public class BenchManager : MonoBehaviour
{
    [SerializeField]
    private List<DracData> _dracDeck = default; //En un futur aixo ha de vindre donat pel level manager: quins dracs tindrà el jugador per jugar el nivell

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
    
    public BenchedDrac PlaceBenchedDrac(DracData data)
    {
        foreach(BenchedDrac drac in _bench)
        {
            if(drac.DracData == data)
            {
                _bench.Remove(drac);
                return drac;
            }
        }

        return null;
    }

    public void BenchDrac(Drac drac)
    {
        _bench.Add(new BenchedDrac(drac.DracData, drac.TimeRemaining));
    }

}
