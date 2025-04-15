using System;
using System.Collections.Generic;

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