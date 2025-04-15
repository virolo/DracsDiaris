using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private Waves _waves;
    
    private Queue<Wave> _wavesQueue = new Queue<Wave>();
    
    public bool HasWavesRemaining => WavesRemaining > 0;
    public int WavesRemaining => _wavesQueue.Count;


    private void Start()
    {
        _wavesQueue = new Queue<Wave>(_waves.GetWaves);
    }

    public Wave GetNextWave()
    {
        return _wavesQueue.Dequeue();
    }
}