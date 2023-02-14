using System;
using System.Collections.Generic;
using System.IO;

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace Semestralni_prace.Enemies;
public abstract class Ant : Enemy
{
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
            new Vector2(Scale, Scale));
    }

    public void Attack()
    {
        _game.Player.AcceptAttack(this, AttackPower);
        AttackCooldownCounter = AttackCooldown;
    }
    
    public override void BulletCollision(IBullet bullet, int amountOfDamage)
    {
        Random rand = new Random();
        if (rand.Next(1, 5) == 3)
        {
            Dodge();
        }
        else
        {
            TakeDamage(amountOfDamage);   
        }
    }

    public void Dodge()
    {
        Random rnd = new Random();
        int x = (int)Position.X + rnd.Next(0, (int)(30*Scale));
        int y = (int)Position.Y + rnd.Next(0, (int)(30*Scale));
        Position = new Vector2(x, y);
    }
    
    public override void Update(GameTime gameTime)
    {
        int xdiff = (int)(_game.Player.Position.X - Position.X);
        int ydiff = (int)(_game.Player.Position.Y - Position.Y);
        double devider = Math.Sqrt(Math.Pow(xdiff, 2) + Math.Pow(ydiff, 2)) / MaxSpeed;
        Vector2 velocity = new Vector2((int)(xdiff / devider), (int)(ydiff / devider));

        if (xdiff < 0)
        {
            CurrentEffect = SpriteEffects.FlipHorizontally;
        }
        else
        {
            CurrentEffect = SpriteEffects.None;
        }

        _animatedSprite.Update(gameTime);

        Position += velocity;

        if (AttackCooldownCounter <= 0)
        {
            if ((this.Position.X + _animatedSprite.FrameWidth > _game.Player.Position.X - Player.PlayerAnimationFrameWidth/2) &&
                (this.Position.X - _animatedSprite.FrameWidth < _game.Player.Position.X + Player.PlayerAnimationFrameWidth/2) &&
                (this.Position.Y + _animatedSprite.FrameHeight > _game.Player.Position.Y - Player.PlayerAnimationFrameHeight/2) &&
                (this.Position.Y - _animatedSprite.FrameHeight < _game.Player.Position.Y + Player.PlayerAnimationFrameHeight/2))
            {
                Attack();
            }
        }
        else
        {
            AttackCooldownCounter -= gameTime.ElapsedGameTime.Milliseconds;
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _animatedSprite.Draw(spriteBatch, Position, CurrentEffect, Color);
    }

}


public class BaseAnt : Ant
{
    public BaseAnt(Vector2 position, Game1 game)
    {
        Color = Color.White;
        Position = position;
        _game = game;
        MaxSpeed = 1.5f;
        AttackCooldown = 2000;
        Scale = 2;

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
        MaxSpeed = 1.8f;
        AttackCooldown = 1000;
        Scale = 3;

        Hp = 30;
        AttackPower = 3;
        LoadSprite();
    }
    
}


public class BruteAnt : Ant
{
    public BruteAnt(Vector2 position, Game1 game)
    {
        Color = Color.IndianRed;
        Position = position;
        _game = game;
        MaxSpeed = 2;
        AttackCooldown = 500;
        Scale = 4;

        Hp = 100;
        AttackPower = 5;
        LoadSprite();
    }
    
}