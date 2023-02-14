using System;

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Semestralni_prace.Enemies;

public abstract class Dragon : Enemy
{
    public int FastAttackCooldown;
    public int AttackReach;

    protected AnimatedSprite FireSprite;
    protected int FireAnimationDuration;
    protected int FireAnimationCounter;

    public void LoadSprites()
    {
        TextureManager textureManager = TextureManager.Instance;
        Texture2D texture = textureManager.GetTexture("dragon");
     
        _animatedSprite = new AnimatedSprite(
            texture,
            16, 
            16, 
            4, 
            0.1f, true,
            new Vector2(Scale, Scale));

        Texture2D fire_texture = textureManager.GetTexture("fire");

        FireSprite = new AnimatedSprite(
            fire_texture,
            30,
            12,
            6,
            0.05f, true,
            new Vector2(Scale, Scale));
    }

    public void Attack()
    {
        _game.Player.AcceptAttack(this, AttackPower);
        AttackCooldownCounter = AttackCooldown;
        FireAnimationCounter = FireAnimationDuration;
    }

    public void FastAttack()
    {
        _game.Player.AcceptAttack(this, AttackPower/2);
        AttackCooldownCounter = FastAttackCooldown;
        FireAnimationCounter = FireAnimationDuration;
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
            if (Math.Abs(this.Position.X  - _game.Player.Position.X) <= AttackReach
                 &&
                 Math.Abs(this.Position.Y - _game.Player.Position.Y) <= AttackReach)
                
            {
                if ((gameTime.ElapsedGameTime.Milliseconds % 5) == 0)
                {
                    FastAttack();
                }
                else
                {
                    Attack();   
                }
                
            }
        }
        else
        {
            AttackCooldownCounter -= gameTime.ElapsedGameTime.Milliseconds;
        }

        if (FireAnimationCounter >= 0)
        {
            FireAnimationCounter -= gameTime.ElapsedGameTime.Milliseconds;
        }
    }
    
    public override void Draw(SpriteBatch spriteBatch)
    {
        _animatedSprite.Draw(spriteBatch, Position, CurrentEffect, Color);
        if (FireAnimationCounter >= 0)
        {
            FireSprite.Draw(spriteBatch, Position, CurrentEffect, Color);
        }
    }
}


public class BaseDragon : Dragon
{
    public BaseDragon(Vector2 position, Game1 game)
    {
        Color = Color.White;
        Position = position;
        _game = game;
        MaxSpeed = 1.7f;
        AttackCooldown = 3000;
        FastAttackCooldown = 1000;
        FireAnimationDuration = 500;
        Scale = 2;
        AttackReach = 70;

        Hp = 10;
        AttackPower = 1;
        LoadSprites();
    }
    
    
}