using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;

namespace Semestralni_prace;

public class BulletFactory
{
    protected Game1 game { get; }
    protected GameTime gameTime { get; }
    public BulletFlyweight flyweight { get; }

    public BulletFactory(Game1 game, GameTime gameTime, BulletFlyweight flwght)
    {
        this.game = game;
        this.gameTime = gameTime;
        this.flyweight = flwght;
    }
    
    public IBullet GetRegularBullet(Vector2 inPosition, float inAngle)
    {
        RegularBullet bullet = new RegularBullet(this.game, this.gameTime);
        bullet.previousGameTime = (float)this.gameTime.ElapsedGameTime.TotalSeconds;
        bullet.Position = inPosition;
        bullet.Angle = inAngle;
        bullet.FlyweightReference = this.flyweight;
        bullet.IsDeleted = false;

        if (this.game.ActiveBullets.NumberOfBulletsActive + 1 >= this.game.MaximumNumberOfVisualisedBullets)
        {
            bullet.IsDeleted = true;
        }

        return bullet;
    }
}