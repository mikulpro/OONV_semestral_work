using System;

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Semestralni_prace.Enemies;

public abstract class Dragon : Enemy
{
    public int FastAttackCooldown;

    public void LoadSprite()
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
    }

    public void Attack()
    {
        _game.Player.AcceptAttack(this, AttackPower);
        AttackCooldownCounter = AttackCooldown;
    }

    public void FastAttack()
    {
        _game.Player.AcceptAttack(this, AttackPower/2);
        AttackCooldownCounter = FastAttackCooldown;
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
            if ((this.Position.X + _animatedSprite.FrameWidth >
                 _game.Player.Position.X - Player.PlayerAnimationFrameWidth / 2) &&
                (this.Position.X - _animatedSprite.FrameWidth <
                 _game.Player.Position.X + Player.PlayerAnimationFrameWidth / 2) &&
                (this.Position.Y + _animatedSprite.FrameHeight >
                 _game.Player.Position.Y - Player.PlayerAnimationFrameHeight / 2) &&
                (this.Position.Y - _animatedSprite.FrameHeight <
                 _game.Player.Position.Y + Player.PlayerAnimationFrameHeight / 2))
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
        Scale = 2;

        Hp = 10;
        AttackPower = 1;
        LoadSprite();
    }
    
    
}