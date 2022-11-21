using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows;

namespace semestralni_prace;

abstract class Postava
{
    // Abstraktni promenne
    protected int _zivoty;
    protected int _mana;
    protected int _utok;
    protected int _obrana;
    protected int _x;
    protected int _y;

    // Abstraktni metody
    public abstract void Zautoc(Postava na_koho);
    public abstract void Pouzij(/*JAKY PREDMET*/);
    public abstract void Jdi(int new_x, int new_y);

    // Abstraktni properties
    public int Zivoty { get; protected set; }
    public int Mana { get; protected set; }
    public int Utok { get; protected set; }
    public int Obrana { get; protected set; }
    public int X { get; protected set; }
    public int Y { get; protected set; }
}

class PostavaInstance : Postava
{
    protected int _zivoty;
    protected int _mana;
    protected int _utok;
    protected int _obrana;
    protected int _x;
    protected int _y;

    public PostavaInstance(int zivoty, int mana, int utok, int obrana, int x, int y)
    {
        _zivoty = zivoty;
        _mana = mana;
        _utok = utok;
        _obrana = obrana;
        _x = x;
        _y = y;
    }

    public override void Pouzij()
    {
        throw new NotImplementedException();
    }
    public override void Jdi(int new_x, int new_y)
    {
        this.X = new_x;
        this.Y = new_y;
    }
    public override void Zautoc(Postava na_koho)
    {
        throw new NotImplementedException();
    }
}

class Program
{
    static void Main()
    {
        var Player1 = new PostavaInstance(100, 100, 1, 5, 0, 0);
        var SeznamMonster = new List<Postava>();

        // Herni smycka
        while (true)
        {
            Console.Write($"\rx = {Player1.X}, y = {Player1.Y}     ");
            var key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.W:
                    Player1.Jdi(Player1.X, Player1.Y + 1);
                    break;
                case ConsoleKey.A:
                    Player1.Jdi(Player1.X - 1, Player1.Y);
                    break;
                case ConsoleKey.S:
                    Player1.Jdi(Player1.X, Player1.Y - 1);
                    break;
                case ConsoleKey.D:
                    Player1.Jdi(Player1.X + 1, Player1.Y);
                    break;
                default:
                    break;
            }
            foreach (var monstrum in SeznamMonster)
            {
                if (Player1.X == monstrum.X && Player1.Y == monstrum.Y)
                {
                    Console.Write($"\rNachazite se u prisery {monstrum}");
                }
            }
        }
    }
}