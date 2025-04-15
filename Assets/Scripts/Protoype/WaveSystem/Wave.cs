using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct WaveData
{
    public EnemyData EnemyData;
    public int EnemiesAmount;
}

[Serializable]
public struct Wave
{
    public List<WaveData> WaveData;
}

[CreateAssetMenu(fileName = "Wave", menuName = "DracsDiaris/Wave")]
public class Waves : ScriptableObject
{
    [SerializeField] private List<Wave>_waves = new List<Wave>();

    public List<Wave> GetWaves => _waves;
}