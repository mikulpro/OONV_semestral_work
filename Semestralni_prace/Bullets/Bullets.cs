using System;
using System.Collections.Generic;
using System.IO;
using MonoGame.Extended.TextureAtlases;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using Semestralni_prace.Enemies;

namespace Semestralni_prace;


public interface IBullet
{
    public Vector2 Position { get; set; }

    void Update();

    void Draw();

    void Delete();
}


public class RegularBullet
{
    public Vector2 Position { get; set; }

    RegularBullet(Game1 game)
    {
        this.Position = new Vector2(game.Player.Position.X, game.Player.Position.Y);
    }
    
    void Update()
    {
        throw new NotImplementedException();
    }

    void Draw()
    {
        throw new NotImplementedException();
    }

    void Delete()
    {
        throw new NotImplementedException();
    }
}


public class BulletFlyweight : IBullet
{
    private Texture2D _texture;
    public static readonly int BulletAnimationFrameWidth = 48;
    public static readonly int BulletAnimationFrameHeight = 48;
    public Vector2 Velocity { get; set; }
    public Vector2 Position { get; set; }  // k nicemu jen pro splneni IBullet
    public int damage;

    public BulletFlyweight(ContentManager content)
    {
        _texture = content.Load<Texture2D>("bullet");
        this.damage = 1;
    }

    public void Update(Game1 game)
    {
        // for bullet in ActiveBullets
        // this prepsat na ten konktretni ve foreach
        foreach (IBullet _bullet in game.ActiveBullets)
        {
            // kontrola kolize s enemakem
            foreach (IEnemy _enemy in game.ActiveEnemies)
            {
                if ((_bullet.Position.X > _enemy.Position.X - Game1.EnemyHitboxWidth) &&
                    (_bullet.Position.X < _enemy.Position.X + Game1.EnemyHitboxWidth) &&
                    (_bullet.Position.Y > _enemy.Position.Y - Game1.EnemyHitboxHeight) &&
                    (_bullet.Position.Y < _enemy.Position.Y + Game1.EnemyHitboxHeight))
                {
                    _enemy.BulletCollision(_bullet, this.damage);
                }
            }

            // kontrola hranic okna
            if (!( (_bullet.Position.X + this.Velocity.X < 0) || (_bullet.Position.X + this.Velocity.X + BulletAnimationFrameWidth*2 > Game1._graphics.GraphicsDevice.Viewport.Width) || (_bullet.Position.Y + this.Velocity.Y < 0) || (_bullet.Position.Y + this.Velocity.Y + BulletAnimationFrameHeight*2 > Game1._graphics.GraphicsDevice.Viewport.Height) ))
            {
                _bullet.Delete();
            }
        }
    }

    public void Update()
    {
        throw new NotImplementedException();
    }

    public void Draw()
    {
        Game1._spriteBatch.Draw(_texture, Position, Color.White);
    }

    public void Delete()
    {
        throw new NotImplementedException();
    }

    public static IBullet GenerateNewBullet()
    {
        throw new NotImplementedException();
    }
}