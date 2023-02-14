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

public abstract class Enemy : IEnemy
{
    protected Game1 _game;
    public int Hp { get; protected set; }
    public int AttackPower { get; protected set; }
    public float MaxSpeed { get; protected set; }
    public int AttackCooldown { get; protected set; }
    protected int AttackCooldownCounter = 0;
    public bool IsDeleted { get; protected set;  }

    public Vector2 Position { get; protected set; }
    public float Scale;
    protected SpriteEffects CurrentEffect = SpriteEffects.None;
    public Color Color { get; protected set; }
    
    protected AnimatedSprite _animatedSprite;

    public void TakeDamage(int amountOfDamage) {
        {
            if (this.Hp - amountOfDamage <= 0)
            {
                this.Delete();
            }
            else
            {
                this.Hp = this.Hp - amountOfDamage;
            }
        }   
    }

    public virtual void Update(GameTime gameTime)
    {
        throw new System.NotImplementedException();
    }

    public virtual  void Draw(SpriteBatch spriteBatch)
    {
        throw new System.NotImplementedException();
    }

    public void Delete()
    {
        _game.ActiveEnemies.Remove(this);
        this.IsDeleted = true;
    }
}

