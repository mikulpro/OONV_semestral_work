using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Semestralni_prace.Enemies;

public interface IEnemy
{
    public int Health { get; set; }
    public Vector2 Position { get; set; }
    public void TakeDamage(int damage, Game1 game);
}

public class Enemy : IEnemy
{
    public int Health { get; set; }
    public Vector2 Position { get; set; }
    public bool IsDeleted { get; set; }

    Enemy()
    {
        this.IsDeleted = false;
    }

    public void TakeDamage(int amountOfDamage, Game1 game)
    {
        if (this.Health - amountOfDamage <= 0)
        {
            this.Delete(game);
        }
        else
        {
            this.Health = this.Health - amountOfDamage;
        }
    }

    public void Delete(Game1 game)
    {
        game.ActiveEnemies.Remove(this);
        this.IsDeleted = true;
    }
}