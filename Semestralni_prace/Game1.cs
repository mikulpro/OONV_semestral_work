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
            _player = new Player();
            _player.setWindowSize(new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight));


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            
            Texture2D playerTexture = Content.Load<Texture2D>("ball");
            //  Load the AsepriteDocument
            AsepriteDocument aseDoc = Content.Load<AsepriteDocument>("CowBoy");

            //  Create a new AnimatedSprite from the document
            AnimatedSprite sprite = new AnimatedSprite(aseDoc);
            _player.LoadContent(_spriteBatch, sprite);
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
    
            _player.Draw();
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}