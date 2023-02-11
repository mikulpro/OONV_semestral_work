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

public sealed class BulletList<IBullet> : IEnumerable<IBullet> // singleton
{
    private static readonly object _lock = new object(); // k zajisteni pristupu ke konstruktoru pouze v jednom vlakne
    private static BulletList<IBullet> instance; // property na ukladani jedine instance BulletListu
    private IBullet[] items;

    private BulletList() /* privatni konstruktor k zajisteni nevygenerovatelnosti nove instance */
    {
    }

    public static BulletList<IBullet> Instance
    {
        get
        {
            if (instance == null)
            {
                lock (_lock) // zajisti, ze kod uvnitr lock je v jednu chvili vykonavat soubezne nejvyse jednou
                {
                    if (instance == null)
                    {
                        instance = new BulletList<T>();
                    }
                }
            }
            return instance;
        }
    }

    public IEnumerator<IBullet> GetEnumerator()
    {
        
    }
    
    private class MyEnumerator : IEnumerator<IBullet>
    {
        private IBullet[] items;
        private int index = -1;

        public MyEnumerator(IBullet[] items)
        {
            this.items = items;
        }

        public IBullet Current
        {
            get
            {
                if (index == -1 || index == items.Length)
                {
                    throw new InvalidOperationException();
                }

                return items[index];
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        public bool MoveNext()
        {
            if (index < items.Length - 1)
            {
                index++;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            index = -1;
        }

        public void Dispose()
        {
        }
    }
}    