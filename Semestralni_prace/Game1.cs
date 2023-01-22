using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//  Add using statements
using MonoGame.Aseprite.Documents;
using MonoGame.Aseprite.Graphics;

namespace Semestralni_prace
{
    public class Game1 : Game
    {
        Player _player;
        Texture2D _backgroundTile;

        
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1920;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = 1080;   // set this value to the desired height of your window
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            _player = new Player(new Vector2(0,0));


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _backgroundTile = Content.Load<Texture2D>("background_tile");
            
            // load player animations
            Dictionary<string, AnimatedSprite> animatedSprites = new Dictionary<string, AnimatedSprite>();
            animatedSprites.Add("idle", new AnimatedSprite(
                Content.Load<Texture2D>("CowBoyIdle"),
                Player.PlayerAnimationFrameWidth, 
                Player.PlayerAnimationFrameHeight, 
                7, 
                0.1f, true,
                new Vector2(2.0f, 2.0f)));
            animatedSprites.Add("walk", new AnimatedSprite(
                Content.Load<Texture2D>("CowBoyWalking"), 
                Player.PlayerAnimationFrameWidth, 
                Player.PlayerAnimationFrameHeight, 
               8, 
                0.1f, true,
                new Vector2(2.0f, 2.0f)));
            animatedSprites.Add("shoot", new AnimatedSprite(
                Content.Load<Texture2D>("CowBoyShoot"), 
                Player.PlayerAnimationFrameWidth,
                Player.PlayerAnimationFrameHeight, 
                5, 
                0.1f, true,
                new Vector2(2.0f, 2.0f)));
            animatedSprites.Add("shoot_walk", new AnimatedSprite(
                Content.Load<Texture2D>("CowBoyShootWalking"), 
                Player.PlayerAnimationFrameWidth, 
                Player.PlayerAnimationFrameHeight, 
                8, 
                0.1f, true,
                new Vector2(2.0f, 2.0f)));
            animatedSprites.Add("rapid_fire", new AnimatedSprite(
                Content.Load<Texture2D>("CowBoyRapidFire"), 
                Player.PlayerAnimationFrameWidth, 
                Player.PlayerAnimationFrameHeight, 
                11, 
                0.1f, true,
                new Vector2(2.0f, 2.0f)));
            
 

            _player.LoadContent(animatedSprites);
            
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