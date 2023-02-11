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

// muší váha na kulky

public interface IBullet
{
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }

    void Update();

    void Draw();
}


public class BulletFlyweight : IBullet
{
    private Texture2D _texture;
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }

    public BulletFlyweight(Texture2D texture)
    {
        _texture = texture;
    }

    public void Update()
    {
        Position += Velocity;
    }

    public void Draw()
    {
        Game1._spriteBatch.Draw(_texture, Position, Color.White);
    }
}