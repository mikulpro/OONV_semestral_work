using System;
using System.Collections.Generic;
using System.Linq;
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

        private float shootCooldown;
        private float shootTimer;
        private float spawnCooldown;
        private float spawnTimer;
        private float scoreTimer;
        
        public Random random;
        public int MaximumNumberOfEnemies;
        public bool IsLost { get; set; }

        public Vector2 GenerateRandomVector2(float minX, float maxX, float minY, float maxY, Player hrac,
            int SafeRegion)
        {
            float x = (float)this.random.NextDouble() * (maxX - minX) + minX;
            float y = (float)this.random.NextDouble() * (maxY - minY) + minY;

            int px = (int)hrac.Position.X;
            int py = (int)hrac.Position.Y;

            if ((Math.Abs(x - px) < SafeRegion) || (Math.Abs(y - py) < SafeRegion))
            {
                // return this.GenerateRandomVector2(minX, maxX, minY, minX, hrac, SafeRegion);
                return new Vector2(-50, -50);
            }
            else
            {
                return new Vector2((int)x, (int)y);
            }
        }

        public Game1()
        {
            this.random = new Random();
            this.MaximumNumberOfEnemies = 1000;
            
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();
            MaximumNumberOfVisualisedBullets = 10000;
            this.bf = null;

            shootCooldown = 0.5f;
            spawnCooldown = 5.0f;
            scoreTimer = 0.0f;
            this.IsLost = false;
        }

        protected override void Initialize()
        {
            // hrac
            Player = new Player();
            Player.Position = new Vector2(860, 540);
            Player.gameref = this;

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
            this.bulletTexture = Content.Load<Texture2D>("bullet");
            
            // load player animations
            Player.LoadContent(Content);
            
            // load texture manager with textures
            TextureManager textureManager = TextureManager.Instance;
            textureManager.Load(_content);
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: zobrazit cas hry
            if (this.IsLost)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                    Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    this._content.Unload();
                    Exit();
                }

                if (Keyboard.GetState().IsKeyDown(Keys.P))
                {
                    this.IsLost = false;
                }
            }
            else
            {
                this.scoreTimer += (float)this.gameTime.ElapsedGameTime.TotalSeconds;

                // cooldown timer na kulky
                if (shootTimer > 0)
                {
                    shootTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                // cooldown timer na spawnovani enemaku
                spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;


                // tvorba bullet factory, pokud neexistuje
                if (this.bf == null)
                {
                    this.bf = new BulletFactory(this, this.gameTime, new BulletFlyweight(
                        this._content,
                        this,
                        this.bulletTexture,
                        5,
                        7,
                        4));
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

                if (Keyboard.GetState().IsKeyDown(Keys.H))
                {
                    this.Player.Hp = 100;
                }

                // aktivovani novych enemaku jednou za cas
                if ((spawnTimer >= spawnCooldown) && (this.ActiveEnemies.Count() < this.MaximumNumberOfEnemies))
                {
                    int randomEnemy = this.random.Next(0, 9);
                    Vector2 randomPosition = this.GenerateRandomVector2(50, 1870, 50, 1030, this.Player, 100);
                    switch (randomEnemy)
                    {
                        case 0:
                            ActiveEnemies.Add(this.bef.CreateAnt(randomPosition));
                            break;
                        case 1:
                            ActiveEnemies.Add(this.aef.CreateAnt(randomPosition));
                            break;
                        case 2:
                            ActiveEnemies.Add(this.bref.CreateAnt(randomPosition));
                            break;
                        case 3:
                            ActiveEnemies.Add(this.bef.CreateDragon(randomPosition));
                            break;
                        case 4:
                            ActiveEnemies.Add(this.aef.CreateDragon(randomPosition));
                            break;
                        case 5:
                            ActiveEnemies.Add(this.bref.CreateDragon(randomPosition));
                            break;
                        case 6:
                            ActiveEnemies.Add(this.bef.CreateScorpion(randomPosition));
                            break;
                        case 7:
                            ActiveEnemies.Add(this.aef.CreateScorpion(randomPosition));
                            break;
                        case 8:
                            ActiveEnemies.Add(this.bref.CreateScorpion(randomPosition));
                            break;
                        default:
                            break;
                    }

                    spawnTimer = 0;
                }

                // update aktivnich enemaku
                foreach (var enemy in ActiveEnemies)
                {
                    enemy.Update(gameTime);
                }

                // kontrola dosahu enemies na hrace
                // TODO:

                // spawnovani novych kulek, pokud hrac klikne
                MouseState mouseState = Mouse.GetState();
                if (((mouseState.LeftButton == ButtonState.Pressed) || (Keyboard.GetState().IsKeyDown(Keys.Space))) &&
                    (shootTimer <= 0))
                {
                    shootTimer = shootCooldown;
                    Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);
                    float deltaX = mousePosition.X - (Player.Position.X + 48);
                    float deltaY = mousePosition.Y - (Player.Position.Y + 48);
                    float bulletAngle = (float)Math.Atan2(deltaY, deltaX);
                    Vector2 bulletPosition = new Vector2(Player.Position.X + 48, Player.Position.Y + 48);
                    ActiveBullets.Add(bf.GetRegularBullet(bulletPosition, bulletAngle), this);
                }

                // update aktivnich kulek
                foreach (var bullet in ActiveBullets)
                {
                    bullet.Update(gameTime);
                }

                // smazani kulek, co maji byt smazany
                List<IBullet> BulletsToBeDeleted = new List<IBullet>();
                foreach (var item in this.ActiveBullets)
                {
                    if (item.IsDeleted)
                    {
                        BulletsToBeDeleted.Add(item);
                    }
                }

                foreach (var item in BulletsToBeDeleted)
                {
                    this.ActiveBullets.Remove(item);
                }

                // smazani nepratel, co maji byt smazani
                List<IEnemy> EnemiesToBeDeleted = new List<IEnemy>();
                foreach (var item in this.ActiveEnemies)
                {
                    if (item.IsDeleted)
                    {
                        EnemiesToBeDeleted.Add(item);
                    }
                }

                foreach (var item in EnemiesToBeDeleted)
                {
                    this.ActiveEnemies.Remove(item);
                }
            }

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

            if (this.IsLost)
            {
                /*
                SpriteFont font = Content.Load<SpriteFont>("font");
                
                string message = "You lost";
                Vector2 messageSize = font.MeasureString(message);
                Vector2 messagePosition = new Vector2(GraphicsDevice.Viewport.Width / 2 - messageSize.X / 2, GraphicsDevice.Viewport.Height / 2 - messageSize.Y / 2);
                _spriteBatch.DrawString(font, message, messagePosition, Color.White);

                string scoreMessage = "Score: " + this.scoreTimer;
                Vector2 scoreMessageSize = font.MeasureString(scoreMessage);
                Vector2 scoreMessagePosition = new Vector2(GraphicsDevice.Viewport.Width / 2 - scoreMessageSize.X / 2, GraphicsDevice.Viewport.Height / 2 + scoreMessageSize.Y);
                _spriteBatch.DrawString(font, scoreMessage, scoreMessagePosition, Color.White);
                */
            }
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}