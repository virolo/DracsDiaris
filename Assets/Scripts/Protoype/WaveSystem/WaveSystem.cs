using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private WavesConfig _wavesConfig;
    
    private Queue<Wave> _wavesQueue = new Queue<Wave>();
    
    public bool HasWavesRemaining => WavesRemaining > 0;
    public int WavesRemaining => _wavesQueue.Count;


    private void Awake()
    {
        _wavesQueue = new Queue<Wave>(_wavesConfig.GetWaves);
    }

    public Wave GetNextWave()
    {
        return _wavesQueue.Count > 0 ? _wavesQueue.Dequeue() : default(Wave);
    }
}