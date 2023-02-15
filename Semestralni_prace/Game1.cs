using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        public ContentManager _content;
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public BulletList<IBullet> ActiveBullets;
        public int MaximumNumberOfVisualisedBullets;
        public List<IEnemy> ActiveEnemies;
        private BaseEnemyFactory bef;
        private AdvancedEnemyFactory aef;
        private BruteEnemyFactory bref;
        public BulletFactory bf;
        public GameTime gameTime;
        private Texture2D bulletTexture;
        
        public Vector2 GenerateRandomVector2(float minX, float maxX, float minY, float maxY, Player hrac,
            int SafeRegion)
        {
            Random random = new Random();
            float x = (float)random.NextDouble() * (maxX - minX) + minX;
            float y = (float)random.NextDouble() * (maxY - minY) + minY;

            int px = (int)hrac.Position.X;
            int py = (int)hrac.Position.Y;

            if ((Math.Abs(x - px) < SafeRegion) || (Math.Abs(y - py) < SafeRegion))
            {
                return this.GenerateRandomVector2(minX, maxX, minY, minX, hrac, SafeRegion);
            }
            else {
                return new Vector2((int)x, (int)y);
            }
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();
            MaximumNumberOfVisualisedBullets = 100;
            this.bf = null;
        }

        protected override void Initialize()
        {
            // hrac
            Player = new Player();
            Player.Position = new Vector2(860, 540);

            // init listu
            ActiveEnemies = new List<IEnemy>();
            ActiveBullets = BulletList<IBullet>.Instance;
            
            // init factories
            this.gameTime = new GameTime();
            this.bef = new BaseEnemyFactory(this);
            this.aef = new AdvancedEnemyFactory(this);
            this.bref = new BruteEnemyFactory(this);

            base.Initialize();
        }

        protected override void LoadContent()
        {

            _content = new ContentManager(Services, "Content");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _backgroundTile = Content.Load<Texture2D>("background_tile");
            this.bulletTexture = this._content.Load<Texture2D>("bullet");
            
            // load player animations
            Player.LoadContent(Content);
            
            // load texture manager with textures
            TextureManager textureManager = TextureManager.Instance;
            textureManager.Load(_content);

        }

        protected override void Update(GameTime gameTime)
        {
            // tvorba bullet factory, pokud neexistuje
            if (this.bf == null)
            {
                this.bf = new BulletFactory(this, this.gameTime, new BulletFlyweight(this._content, this, this.bulletTexture, 1, 7));
            }
            
            // konec Escapem
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this._content.Unload();
                Exit();
            }

            // vykresleni hrace
            Player.Update(gameTime, this);

            // debugovaci spawnovani enemaku
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {

                ActiveEnemies.Add(this.bef.CreateAnt(new Vector2(10, 10)));
                ActiveEnemies.Add(aef.CreateAnt(new Vector2(10, 10)));
                ActiveEnemies.Add(bref.CreateAnt(new Vector2(10, 10)));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {

                ActiveEnemies.Add(bef.CreateDragon(new Vector2(10, 10)));
                ActiveEnemies.Add(aef.CreateDragon(new Vector2(10, 10)));
                ActiveEnemies.Add(bref.CreateDragon(new Vector2(10, 10)));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {

                ActiveEnemies.Add(bef.CreateScorpion(new Vector2(10, 10)));
                ActiveEnemies.Add(aef.CreateScorpion(new Vector2(10, 10)));
                ActiveEnemies.Add(bref.CreateScorpion(new Vector2(10, 10)));
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.B))
            {

                ActiveBullets.Add(bf.GetRegularBullet(new Vector2(10, 10), MathHelper.ToRadians(270)), this);
            }

            // aktivovani novych enemaku, pokud je cas
            float minX = 50;
            float maxX = 1870;
            float minY = 50;
            float maxY = 1030;
            int SafeRegion = 50;
            int elapsedTime = (int)gameTime.ElapsedGameTime.TotalSeconds;
            bool ant_s = (elapsedTime % 2 == 1);
            bool ant_m = (elapsedTime % 10 == 1);
            bool ant_l = (elapsedTime % 30 == 1);
            bool dragon_s = false;
            bool dragon_m = false;
            bool dragon_l = false;
            bool scorp_s = false;
            bool scorp_m = false;
            bool scorp_l = false;
            if (ant_s)
            {
                ActiveEnemies.Add(
                    this.bef.CreateAnt(this.GenerateRandomVector2(minX, maxX, minY, minX, this.Player, SafeRegion)));
            }

            if (ant_m)
            {
                ActiveEnemies.Add(
                    this.aef.CreateAnt(this.GenerateRandomVector2(minX, maxX, minY, minX, this.Player, SafeRegion)));
            }

            if (ant_l)
            {
                ActiveEnemies.Add(
                    this.bref.CreateAnt(this.GenerateRandomVector2(minX, maxX, minY, minX, this.Player, SafeRegion)));
            }

            if (dragon_s)
            {
                ActiveEnemies.Add(
                    this.bef.CreateDragon(this.GenerateRandomVector2(minX, maxX, minY, minX, this.Player, SafeRegion)));
            }

            if (dragon_m)
            {
                ActiveEnemies.Add(
                    this.aef.CreateDragon(this.GenerateRandomVector2(minX, maxX, minY, minX, this.Player, SafeRegion)));
            }

            if (dragon_l)
            {
                ActiveEnemies.Add(
                    this.bref.CreateDragon(this.GenerateRandomVector2(minX, maxX, minY, minX, this.Player,
                        SafeRegion)));
            }

            if (scorp_s)
            {
                ActiveEnemies.Add(
                    this.bef.CreateScorpion(this.GenerateRandomVector2(minX, maxX, minY, minX, this.Player,
                        SafeRegion)));
            }

            if (scorp_m)
            {
                ActiveEnemies.Add(
                    this.aef.CreateScorpion(this.GenerateRandomVector2(minX, maxX, minY, minX, this.Player,
                        SafeRegion)));
            }

            if (scorp_l)
            {
                ActiveEnemies.Add(
                    this.bref.CreateScorpion(
                        this.GenerateRandomVector2(minX, maxX, minY, minX, this.Player, SafeRegion)));
            }


            // update aktivnich enemaku
            foreach (var enemy in ActiveEnemies)
            {
                enemy.Update(gameTime);
            }

            // spawnovani novych kulek, pokud hrac klikne
            MouseState mouseState = Mouse.GetState();
            if ((mouseState.LeftButton == ButtonState.Pressed) || (Keyboard.GetState().IsKeyDown(Keys.Space)))
            {
                Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);
                float deltaX = mousePosition.X - Player.Position.X;
                float deltaY = mousePosition.Y - Player.Position.Y;
                float bulletAngle = (float)Math.Atan2(deltaY, deltaX);
                ActiveBullets.Add(bf.GetRegularBullet(Player.Position, bulletAngle), this);
            }
            
            // update aktivnich kulek
            foreach (var bullet in ActiveBullets)
            {
                bullet.Update(gameTime);
            }
            
            // kontrola kolizi enemaku a kulek
            // TODO:
            
            // posunuti na vykreslovani dalsiho framu
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            _spriteBatch.Begin();
            
            // vykresleni pozadi
            for (int x = 0; x < GraphicsDevice.Viewport.Width; x += _backgroundTile.Width)
            {
                for (int y = 0; y < GraphicsDevice.Viewport.Height; y += _backgroundTile.Height)
                {
                    _spriteBatch.Draw(_backgroundTile, new Vector2(x, y), Color.White);
                }
            }

            // vykresleni hrace
            Player.Draw(_spriteBatch);
            
            // vykresleni nepratel
            foreach (var enemy in ActiveEnemies)
            {
                enemy.Draw(_spriteBatch);
            }
            
            // vykresleni projektilu
            foreach (var bullet in ActiveBullets)
            {
                bullet.Draw(_spriteBatch);
                
                if (!bullet.IsDeleted)
                {
                    bullet.Draw(_spriteBatch);
                    _spriteBatch.Draw(bulletTexture, bullet.Position, Color.White);
                }
            }
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}