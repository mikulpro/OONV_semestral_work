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
        public Player _player;
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
            _player = new Player();
            _player._position = new Vector2(860, 540);

            base.Initialize();
        }

        protected override void LoadContent()
        {

            _content = this.Content;
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _backgroundTile = Content.Load<Texture2D>("background_tile");
            
            // load player animations
            _player.LoadContent(Content);
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _player.Update(gameTime);

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
    
            _player.Draw(_spriteBatch);
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}