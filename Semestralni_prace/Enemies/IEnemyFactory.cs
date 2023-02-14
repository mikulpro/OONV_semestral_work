using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Semestralni_prace.Enemies;

public interface IEnemyFactory
{
    public Dragon CreateDragon(Vector2 position);
    public Scorpion CreateScorpion(Vector2 position);
    public Ant CreateAnt(Vector2 position);
}