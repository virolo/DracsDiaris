using System;


public interface IProjectileFactory
{
    Projectile GetProjectile(PrjectileType type);
}

[Serializable]
public class ProjectileFactory : IProjectileFactory
{
    private readonly ProjectileDatabase _database;


    public ProjectileFactory(ProjectileDatabase database)
    {
        _database = database;
    }

    public Projectile GetProjectile(PrjectileType type)
    {
        foreach (var projectileEntry in _database.entries)
        {
            if (projectileEntry.type == type)
            {
                return projectileEntry.prefab;
            }
        }
        
        return null;
    }
}