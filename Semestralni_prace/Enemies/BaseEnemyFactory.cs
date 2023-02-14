using Microsoft.Xna.Framework;

namespace Semestralni_prace.Enemies;

public class BaseEnemyFactory : IEnemyFactory
{
    private Game1 _game;
    public BaseEnemyFactory(Game1 game)
    {
        _game = game;
    }
    public Dragon CreateDragon(Vector2 position)
    {
        BaseDragon dragon = new BaseDragon(position, _game);
        dragon.LoadSprites();
        return dragon;
    }

    public GunMan CreateGunMan(Vector2 position)
    {
        throw new System.NotImplementedException();
    }

    public Ant CreateAnt(Vector2 position)
    {
       BaseAnt ant = new BaseAnt(position, _game);
       ant.LoadSprite();
       return ant;
    }
}