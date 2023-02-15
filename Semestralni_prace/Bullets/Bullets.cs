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
using System.Runtime.CompilerServices;
using Semestralni_prace.Enemies;

namespace Semestralni_prace;


public interface IBullet
{
    public Vector2 Position { get; set; }
    public float previousGameTime { get; set; }
    public BulletFlyweight FlyweightReference { get; set; }
    public float Angle { get; set; }
    public bool IsDeleted { get; set; }

    public void Update(GameTime gameTime);

    public void Draw();

    public void Delete();
}


public class RegularBullet : IBullet
{
    public Vector2 Position { get; set; }
    public float previousGameTime { get; set; }
    public BulletFlyweight FlyweightReference { get; set; }
    public float Angle { get; set; }
    public bool IsDeleted { get; set; }

    public RegularBullet(Game1 game, GameTime gameTime)
    {
        this.Position = new Vector2(game.Player.Position.X, game.Player.Position.Y);
        this.previousGameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
    
    public void Update(GameTime gameTime)
    {
        this.FlyweightReference.Update(gameTime, this);
    }

    public void Draw()
    {
        Game1 game = this.FlyweightReference.game;
        game._spriteBatch.Draw(this.FlyweightReference._texture, this.Position, Color.White);
    }

    public void Delete()
    {
        this.IsDeleted = true;
        this.FlyweightReference.Delete(this);
    }
}


public class BulletFlyweight
{
    public Texture2D _texture;
    public static readonly int BulletAnimationFrameWidth = 48;
    public static readonly int BulletAnimationFrameHeight = 48;
    public int Speed { get; set; }
    public int Damage { get; set; }
    public bool DamagePlayer { get; set; }
    public float Radius { get; set; }
    public Game1 game { get; set; }

    public BulletFlyweight(ContentManager content, Game1 input_game, Texture2D texture, int damage, float radius)
    {
        this.game = input_game;
        this._texture = texture;
        this.Damage = damage;
        this.Radius = radius;
    }

    public void Update(GameTime gameTime, IBullet _bullet)
    {
        game = this.game;
        
        // kontrola existence
        if (_bullet.IsDeleted)
        {
            return;
        }
        
        // spocitani nove pozice
        float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        float timeDiff = elapsedTime - _bullet.previousGameTime;
        Vector2 Velocity = new Vector2((float)Math.Cos(_bullet.Angle), (float)Math.Sin(_bullet.Angle)) * this.Speed;
        _bullet.Position = _bullet.Position + (Velocity * timeDiff);
        
        // kontrola kolize s enemakem
        foreach (var enemy in game.ActiveEnemies)
        {
            float distance = Vector2.Distance(_bullet.Position, enemy.Position);
            if (distance < this.Radius)
            {
                enemy.TakeDamage(this.Damage);
                _bullet.Delete();
            }
        }
        
        // kontrola hranic okna
        if (!( (_bullet.Position.X < 0) || 
               (_bullet.Position.X + BulletAnimationFrameWidth*2 > game._graphics.GraphicsDevice.Viewport.Width) || 
               (_bullet.Position.Y < 0) || 
               (_bullet.Position.Y + BulletAnimationFrameHeight*2 > game._graphics.GraphicsDevice.Viewport.Height) ))
        {
            _bullet.Delete();
        }
    }

    public void Draw()
    {
        foreach (var item in game.ActiveBullets)
        {
            if (!item.IsDeleted)
            {
                item.Draw();
            }
        }    
    }

    public void Delete(IBullet item)
    {
        item.IsDeleted = true;
        this.game.ActiveBullets.Remove(item);
    }

    public void GarbageCollector()
    {
        Game1 game = this.game;
        foreach (var item in game.ActiveBullets)
        {
            if (item.IsDeleted)
            {
                game.ActiveBullets.Remove(item);
            }
        }
    }
}