using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Semestralni_prace.Enemies;

public class BruteEnemyFactory : IEnemyFactory
{
    private Game1 _game;
    public BruteEnemyFactory(Game1 game)
    {
        _game = game;
    }
    public Dragon CreateDragon(Vector2 position)
    {
        BruteDragon dragon = new BruteDragon(position, _game);
        dragon.LoadSprites();
        return dragon;
    }

    public Scorpion CreateScorpion(Vector2 position)
    {
        BruteScorpion scorpion = new BruteScorpion(position, _game);
        scorpion.LoadSprite();
        return scorpion;
    }

    public Ant CreateAnt(Vector2 position)
    {
        BruteAnt ant = new BruteAnt(position, _game);
        ant.LoadSprite();
        return ant;    }
}