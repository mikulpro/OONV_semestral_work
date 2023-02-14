using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Semestralni_prace.Enemies;

public class BruteEnemyFactory : IEnemyFactory
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