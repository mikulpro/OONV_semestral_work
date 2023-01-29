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

namespace VYPNUTO;

// muší váha na kulky

interface IBullet
{
    void Update(GameTime gameTime);

    void Draw();
    /*void Move(String direction);*/
}

public class Bullet : IBullet // jen koordinace
{
    public Vector2 Coords = new Vector2();
    public Vector2 MovementVector;
    public BulletFlyweight flyweight;
    public int MaxSpeed = 5;

    public Bullet(Vector2 sourceCoords, Vector2 destinationCoords)
    {
        Coords.X = sourceCoords.X;
        Coords.Y = sourceCoords.Y;
        int Xdiff = (int)Math.Abs(destinationCoords.X - sourceCoords.X);
        int Ydiff = (int)Math.Abs(destinationCoords.Y - sourceCoords.Y);
        double devider = Math.Sqrt(Math.Pow(Xdiff, 2)+Math.Pow(Ydiff, 2));
        MovementVector = new Vector2((int)(Xdiff/devider), (int)(Ydiff/devider));
    }

    public void Update(GameTime gameTime)
    {
        Coords.X += MovementVector.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
        Coords.Y += MovementVector.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public void Draw()
    {
        flyweight.Draw(this);
    }

    /*public void Move(String direction)
    {
        switch (direction)
        {
            case "n":
                break;
            case "ne":
                break;
            case "e":
                break;
            case "se":
                break;
            case "s":
                break;
            case "sw":
                break;
            case "w":
                break;
            case "nw":
                break;
            default:
                Console.WriteLine("Reached default case.");
                break;
        }
    }
*/
}

public class BulletFlyweightFactory // factory na bullets propojene se statickou musi vahou
{

    public BulletFlyweightFactory()
    {
        
    }
}

interface IFlyweightBullet
{
    
}

public class BulletFlyweight : IFlyweightBullet
{
    public String flwght_type;
    public Int32 bullet_velocity;
    public Int32 bullet_power;
    public Int32 bullet_damage;
    public Texture2D BulletTexture;
    public List<Bullet> Bullets;
    private SpriteBatch _spriteBatch;
    

    public void Draw(Bullet bullet)
    {
        _spriteBatch.Draw(BulletTexture, bullet.Coords, Color.White);
       
    }

}