using System;

namespace Semestralni_prace;

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//  Add using statements
using MonoGame.Aseprite.Documents;
using MonoGame.Aseprite.Graphics;
using Semestralni_prace.Enemies;

public class TextureManager
{
    private static TextureManager _instance = null;
    private static readonly object _instancelock = new object();

    private Boolean _loaded;
    private Dictionary<string, Texture2D> _textures;

    public static TextureManager Instance
    {
        get
        {
            lock (_instancelock)
            {
                if (_instance == null)
                {
                    _instance = new TextureManager();
                }
                return _instance;
            }
        }
    }

    private TextureManager()
    {
        _textures = new Dictionary<string, Texture2D>();
        _loaded = false;
    }
    
    public void Load(ContentManager contentManager)
    {
        _textures.Add("bullet", contentManager.Load<Texture2D>("bullet"));
        _textures.Add("fire", contentManager.Load<Texture2D>("Enemies/fire2"));
        
        _textures.Add("ant", contentManager.Load<Texture2D>("Enemies/LavaAntIdleSide"));
        _textures.Add("dragon", contentManager.Load<Texture2D>("Enemies/GreatWyvernIdleSide"));
        _textures.Add("gunman", contentManager.Load<Texture2D>("Enemies/ArcheologistShooting"));
        
        _loaded = true;
    }

    public Texture2D GetTexture(string name)
    {
        if (_loaded)
        {
            try
            {
                return _textures[name];
            }
            catch (KeyNotFoundException)
            {
                throw new Exception("Texture wasn't found in texture manager.");
            }
        }
        else
        {
            throw new Exception("Textures weren!t loaded yet.");
        }
    }
}