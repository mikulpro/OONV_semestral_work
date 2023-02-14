using Microsoft.Xna.Framework;

namespace Semestralni_prace.Enemies;

public class BaseEnemyFactory : IEnemyFactory
{
    public Dragon CreateDragon(Vector2 position)
    {
        throw new System.NotImplementedException();
    }

    public GunMan CreateGunMan(Vector2 position)
    {
        throw new System.NotImplementedException();
    }

    public Ant CreateAnt(Vector2 position)
    {
        throw new System.NotImplementedException();
    }
}