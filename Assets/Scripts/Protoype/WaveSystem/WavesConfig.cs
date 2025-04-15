using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelWavesConfig", menuName = "DracsDiaris/WavesConfig")]
public class WavesConfig : ScriptableObject
{
    [SerializeField] private List<Wave> _waves;
    public List<Wave> GetWaves => _waves;
}