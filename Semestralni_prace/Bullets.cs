using System;
using System.Collections.Generic;
using System.IO;

namespace Semestralni_prace;

// muší váha na kulky

interface IBullet
{
    void Move(String direction);
}

public class Bullet : IBullet // jen koordinace
{
    public int[] Coords = new int[2];
    public BulletFlyweight flyweight;

    public Bullet(Int32 x, Int32 y)
    {
        this.Coords[0] = x;
        this.Coords[1] = y;
    }

    public void Move(String direction)
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
    
    
}