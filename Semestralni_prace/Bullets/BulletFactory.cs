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
    private Dictionary<string, BulletFlyweight> _bullets;

    public BulletFactory()
    {
        _bullets = 
    }

    public IBullet GetBullet(string key, Texture2D texture)
    {
        if (!_bullets.ContainsKey(key))
        {
            _bullets[key] = new BulletFlyweight(texture);
        }
        return _bullets[key];
    }
}