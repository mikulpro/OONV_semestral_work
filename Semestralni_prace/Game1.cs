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
        Playerv2 _player;

        
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            /*_player = new Player();
            _player.setWindowSize(new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight));*/
            _player = new Playerv2(new Vector2(0,0));


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            /*_player.LoadContent(Content.Load<Texture2D>("spritesheetlarger"));*/
            
            // load player animations
            Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
            animations.Add("idle", new Animation(
                Content.Load<Texture2D>("CowBoyIdle"),
                Playerv2.player_animation_frame_width, 
                Playerv2.player_animation_frame_height, 
                7, 
                0.1f, true));
            animations.Add("walk", new Animation(
                Content.Load<Texture2D>("CowBoyWalking"), 
                Playerv2.player_animation_frame_width, 
                Playerv2.player_animation_frame_height, 
               8, 
                0.1f, true));
            animations.Add("shoot", new Animation(
                Content.Load<Texture2D>("CowBoyShoot"), 
                Playerv2.player_animation_frame_width,
                Playerv2.player_animation_frame_height, 
                5, 
                0.1f, true));
            animations.Add("rapid_fire", new Animation(
                Content.Load<Texture2D>("CowBoyRapidFire"), 
                Playerv2.player_animation_frame_width, 
                Playerv2.player_animation_frame_height, 
                11, 
                0.1f, true));

            _player.LoadContent(animations);
            /*Texture2D playerTexture = Content.Load<Texture2D>("ball");
            //  Load the AsepriteDocument
            Texture2D aseDoc = Content.Load<Texture2D>("ball");*/


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
    
            _player.Draw(_spriteBatch);
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}