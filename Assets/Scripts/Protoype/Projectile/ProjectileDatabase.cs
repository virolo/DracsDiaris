using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileDatabase", menuName = "dB/ProjectileDatabase")]
public class ProjectileDatabase : ScriptableObject
{
    [Serializable]
    public struct ProjectileEntry
    {
        public PrjectileType type;
        public Projectile prefab;
    }

    public ProjectileEntry[] entries;
}