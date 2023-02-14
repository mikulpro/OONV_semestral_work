using System;
using System.Collections.Generic;
using System.IO;

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace Semestralni_prace.Enemies;
public abstract class Ant : IEnemy
{
    protected Game1 _game;
    public int Hp { get; protected set; }
    public int AttackPower { get; protected set; }
    public float MaxSpeed { get; protected set; }
    public bool IsDeleted { get; protected set;  }

    public Vector2 Position { get; protected set; }
    public Color Color { get; protected set; }
    
    protected AnimatedSprite _animatedSprite;
    public void LoadSprite()
    {
        TextureManager textureManager = TextureManager.Instance;
        Texture2D texture = textureManager.GetTexture("ant");
     
        _animatedSprite = new AnimatedSprite(
            texture,
            16, 
            16, 
            4, 
            0.1f, true,
            new Vector2(2.0f, 2.0f));
    }

    public void Attack()
    {
        
    }

    public void Dodge()
    {
        
    }
    
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

    public void Update(GameTime gameTime)
    {
        Vector2 Velocity = new Vector2();
       
        int Xdiff = (int)(_game.Player.Position.X - Position.X);
        int Ydiff = (int)(_game.Player.Position.Y - Position.Y);
        double devider = Math.Sqrt(Math.Pow(Xdiff, 2)+Math.Pow(Ydiff, 2))/1.5;
        Velocity = new Vector2((int)(Xdiff / devider), (int)(Ydiff / devider));
            
        _animatedSprite.Update(gameTime);
        
        Position += Velocity;
        }

    public void Draw(SpriteBatch spriteBatch)
    {
        _animatedSprite.Draw(spriteBatch, Position, SpriteEffects.None, Color);
    }
    

    public void Delete()
    {
        _game.ActiveEnemies.Remove(this);
        this.IsDeleted = true;
    }
    
}


public class BaseAnt : Ant
{
    public BaseAnt(Vector2 position, Game1 game)
    {
        Color = Color.Gray;
        Position = position;
        _game = game;

        Hp = 10;
        AttackPower = 1;
        LoadSprite();
    }
    
}


public class AdvancedAnt : Ant
{
    public AdvancedAnt(Vector2 position, Game1 game)
    {
        Color = Color.DarkGray;
        Position = position;
        _game = game;

        Hp = 30;
        AttackPower = 3;
        LoadSprite();
    }
    
}


public class BruteAnt : Ant
{
    public BruteAnt(Vector2 position, Game1 game)
    {
        Color = Color.White;
        Position = position;
        _game = game;

        Hp = 100;
        AttackPower = 5;
        LoadSprite();
    }
    
}