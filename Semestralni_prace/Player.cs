
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace Semestralni_prace
{

    public class Player
    {
        public static readonly int PlayerAnimationFrameWidth = 48;
        public static readonly int PlayerAnimationFrameHeight = 48;

        private float _shootDuration = 5 * 0.1f;
        private float _shootDurationWalking = 8 * 0.1f;
        private float _shootDelay = 0.1f;
        private float _shootDelayWalking = 2 * 0.1f;


        public int Hp { get; }
        public int Attack { get; }
        public int Defense { get; }


        public Vector2 Position { get; set; }

        private Vector2 _velocity;
        
        private Dictionary<string, AnimatedSprite> _animatedSprites;
        
        private int _facingDirection; // 1 = right, -1 = left
        private string _currentAnimation;
        private SpriteEffects _currentEffect;
        

        // inicializace hrace
        public Player()
        {
            _currentAnimation = "idle";
            _facingDirection = 1;
            _currentEffect = SpriteEffects.None;
        }

        public void Shoot()
        {
            
        }

        public void LoadContent(ContentManager content)
        {
            _animatedSprites = new Dictionary<string, AnimatedSprite>();
            _animatedSprites.Add("idle", new AnimatedSprite(
                content.Load<Texture2D>("CowBoyIdle"),
                Player.PlayerAnimationFrameWidth, 
                Player.PlayerAnimationFrameHeight, 
                7, 
                0.1f, true,
                new Vector2(2.0f, 2.0f)));
            _animatedSprites.Add("walk", new AnimatedSprite(
                content.Load<Texture2D>("CowBoyWalking"), 
                Player.PlayerAnimationFrameWidth, 
                Player.PlayerAnimationFrameHeight, 
                8, 
                0.1f, true,
                new Vector2(2.0f, 2.0f)));
            _animatedSprites.Add("shoot", new AnimatedSprite(
                content.Load<Texture2D>("CowBoyShoot"), 
                Player.PlayerAnimationFrameWidth,
                Player.PlayerAnimationFrameHeight, 
                5, 
                0.1f, true,
                new Vector2(2.0f, 2.0f)));
            _animatedSprites.Add("shoot_walk", new AnimatedSprite(
                content.Load<Texture2D>("CowBoyShootWalking"), 
                Player.PlayerAnimationFrameWidth, 
                Player.PlayerAnimationFrameHeight, 
                8, 
                0.1f, true,
                new Vector2(2.0f, 2.0f)));
            _animatedSprites.Add("rapid_fire", new AnimatedSprite(
                content.Load<Texture2D>("CowBoyRapidFire"), 
                Player.PlayerAnimationFrameWidth, 
                Player.PlayerAnimationFrameHeight, 
                11, 
                0.1f, true,
                new Vector2(2.0f, 2.0f)));

        }

        public void Update(GameTime gameTime)
        {
            _velocity.X = 0;
            _velocity.Y = 0;

            bool anythingPressed = false;
            // handle player input and update velocity
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _velocity.X = 2;
                _facingDirection = 1;
                _currentAnimation = "walk";
                anythingPressed = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _velocity.X = -2;
                _facingDirection = -1;
                _currentAnimation = "walk";
                anythingPressed = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _velocity.Y = 2;
                _currentAnimation = "walk";
                anythingPressed = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                _velocity.Y = -2;
                _currentAnimation = "walk";
                anythingPressed = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (anythingPressed)
                {
                    _currentAnimation = "shoot_walk";
                    
                }
                else
                {
                    _currentAnimation = "shoot";
                    anythingPressed = true;
                    
                }
            }

            if (!anythingPressed)
            {
                _currentAnimation = "idle";
            }

            if (_facingDirection == 1)
            {
                
                _currentEffect = SpriteEffects.None;
            }
            else
            {
                _currentEffect = SpriteEffects.FlipHorizontally;
            }


            _animatedSprites[_currentAnimation].Update(gameTime);
            if (!( (Position.X + _velocity.X < 0) || (Position.X + _velocity.X + PlayerAnimationFrameWidth*2 > Game1._graphics.GraphicsDevice.Viewport.Width) || (Position.Y + _velocity.Y < 0) || (Position.Y + _velocity.Y + PlayerAnimationFrameHeight*2 > Game1._graphics.GraphicsDevice.Viewport.Height) ))
            {
                Position += _velocity;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _animatedSprites[_currentAnimation].Draw(spriteBatch, Position, _currentEffect, Color.White);
        }
    }
}