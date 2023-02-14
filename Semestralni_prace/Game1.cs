using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//  Add using statements
using MonoGame.Aseprite.Documents;
using MonoGame.Aseprite.Graphics;
using Semestralni_prace.Enemies;

namespace Semestralni_prace
{
    public class Game1 : Game
    {
        public Player Player;
        Texture2D _backgroundTile;


        // je vhodne mit je jako public static, aby bylo mozne si je kdekoliv volat a nemusely s porad vkladat do funkci
        public static ContentManager _content;
        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;
        public BulletList<IBullet> ActiveBullets;
        public static int MaximumNumberOfVisualisedBullets;
        public List<IEnemy> ActiveEnemies;
        public static int EnemyHitboxWidth;
        public static int EnemyHitboxHeight;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();
            MaximumNumberOfVisualisedBullets = 100;
        }

        protected override void Initialize()
        {
            Player = new Player();
            Player.Position = new Vector2(860, 540);

            ActiveEnemies = new List<IEnemy>();

            base.Initialize();
        }

        protected override void LoadContent()
        {

            _content = this.Content;
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _backgroundTile = Content.Load<Texture2D>("background_tile");
            
            // load player animations
            Player.LoadContent(Content);
            
            // load texture manager with textures
            TextureManager textureManager = TextureManager.Instance;
            textureManager.Load(_content);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Player.Update(gameTime);
            
            BaseEnemyFactory bef = new BaseEnemyFactory(this);
            AdvancedEnemyFactory aef = new AdvancedEnemyFactory(this);
            BruteEnemyFactory bref = new BruteEnemyFactory(this);

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                
                ActiveEnemies.Add(bef.CreateAnt(new Vector2(10, 10)));
                ActiveEnemies.Add(aef.CreateAnt(new Vector2(10, 10)));
                ActiveEnemies.Add(bref.CreateAnt(new Vector2(10, 10)));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                
                ActiveEnemies.Add(bef.CreateDragon(new Vector2(10, 10)));
                ActiveEnemies.Add(aef.CreateDragon(new Vector2(10, 10)));
                ActiveEnemies.Add(bref.CreateDragon(new Vector2(10, 10)));
            }

            foreach (var enemy in ActiveEnemies)
            {
                enemy.Update(gameTime);
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            
            _spriteBatch.Begin();
            for (int x = 0; x < GraphicsDevice.Viewport.Width; x += _backgroundTile.Width)
            {
                for (int y = 0; y < GraphicsDevice.Viewport.Height; y += _backgroundTile.Height)
                {
                    _spriteBatch.Draw(_backgroundTile, new Vector2(x, y), Color.White);
                }
            }
    
            Player.Draw(_spriteBatch);
            
            foreach (var enemy in ActiveEnemies)
            {
                enemy.Draw(_spriteBatch);
            }

            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}