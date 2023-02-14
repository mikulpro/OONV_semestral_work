using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Semestralni_prace.Enemies;

public interface IEnemy
{
    public int Hp { get; }
    public Vector2 Position { get; }
    public void TakeDamage(int damage);
    public void Update(GameTime gameTime);
    public void Draw(SpriteBatch spriteBatch);
    public bool IsDeleted { get; }
}

/*
public class Enemy :IEnemy
{
    public int Hp { get; set; }
    public Vector2 Position { get; set; }
    public bool IsDeleted { get; set; }

    Enemy()
    {
        this.IsDeleted = false;
    }

    public void TakeDamage(int amountOfDamage, Game1 game)
    {
        if (this.Hp - amountOfDamage <= 0)
        {
            this.Delete(game);
        }
        else
        {
            this.Hp = this.Hp - amountOfDamage;
        }
    }

    public void Update(GameTime gameTime)
    {
        throw new System.NotImplementedException();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        throw new System.NotImplementedException();
    }

    public void Delete(Game1 game)
    {
        game.ActiveEnemies.Remove(this);
        this.IsDeleted = true;
    }
}
*/
