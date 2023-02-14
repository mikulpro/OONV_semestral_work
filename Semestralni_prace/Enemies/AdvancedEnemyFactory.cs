using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Semestralni_prace.Enemies;

public class AdvancedEnemyFactory : IEnemyFactory
{
    private Game1 _game;
    public AdvancedEnemyFactory(Game1 game)
    {
        _game = game;
    }
    public Dragon CreateDragon(Vector2 position)
    {
        AdvancedDragon dragon = new AdvancedDragon(position, _game);
        dragon.LoadSprites();
        return dragon;    }

    public Scorpion CreateScorpion(Vector2 position)
    {
       AdvancedScorpion scorpion = new AdvancedScorpion(position, _game);
        scorpion.LoadSprite();
        return scorpion;
    }

    public Ant CreateAnt(Vector2 position)
    {
        AdvancedAnt ant = new AdvancedAnt(position, _game);
        ant.LoadSprite();
        return ant;
    }
}