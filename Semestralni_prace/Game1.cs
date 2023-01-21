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
        Texture2D backgroundTile;

        
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
            backgroundTile = Content.Load<Texture2D>("background_tile");
            
            // load player animations
            Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
            animations.Add("idle", new Animation(
                Content.Load<Texture2D>("CowBoyIdle"),
                Player.player_animation_frame_width, 
                Player.player_animation_frame_height, 
                7, 
                0.1f, true));
            animations.Add("walk", new Animation(
                Content.Load<Texture2D>("CowBoyWalking"), 
                Player.player_animation_frame_width, 
                Player.player_animation_frame_height, 
               8, 
                0.1f, true));
            animations.Add("shoot", new Animation(
                Content.Load<Texture2D>("CowBoyShoot"), 
                Player.player_animation_frame_width,
                Player.player_animation_frame_height, 
                5, 
                0.1f, true));
            animations.Add("rapid_fire", new Animation(
                Content.Load<Texture2D>("CowBoyRapidFire"), 
                Player.player_animation_frame_width, 
                Player.player_animation_frame_height, 
                11, 
                0.1f, true));

            _player.LoadContent(animations);
            
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
            for (int x = 0; x < GraphicsDevice.Viewport.Width; x += backgroundTile.Width)
            {
                for (int y = 0; y < GraphicsDevice.Viewport.Height; y += backgroundTile.Height)
                {
                    _spriteBatch.Draw(backgroundTile, new Vector2(x, y), Color.White);
                }
            }
    
            _player.Draw(_spriteBatch);
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}