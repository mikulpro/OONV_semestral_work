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

public sealed class BulletList // singleton
{
    private BulletList() { } // privatni konstruktor k zajisteni nevygenerovatelnosti nove instance
    private static readonly object _lock = new object(); // k zajisteni pristupu ke konstruktoru pouze v jednom vlakne
    private static BulletList instance; // property na ukladani jedine instance BulletListu

    public static BulletList Instance
    {
        get
        {
            lock (_lock) // zajisti, ze kod uvnitr lock je v jednu chvili vykonavat soubezne nejvyse jednou
            {
                if (instance == null)
                {
                    instance = new BulletList();
                }
                return instance;
            }
        }
    }
}