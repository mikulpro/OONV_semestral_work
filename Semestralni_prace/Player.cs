
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Aseprite.Graphics;

namespace Semestralni_prace
{

    class Player
    {
        public static int player_animation_frame_width = 48;
        public static int player_animation_frame_height = 48;
        
        private Dictionary<string, Animation> _animations;
        private Vector2 _position;
        private Vector2 _velocity;
        private Vector2 _scale;
        private int _facingDirection = 1; // 1 = right, -1 = left
        private string _currentAnimation;
        private SpriteEffects _currentEffect;
        

        public Player(Vector2 position)
        {
            this._position = position;
            
            _currentAnimation = "idle";
            _facingDirection = 1;
            _currentEffect = SpriteEffects.None;
            _scale = new Vector2(2.0f, 2.0f); // scale factor of 2x
        }

        public void Shoot()
        {
            
        }

        public void LoadContent(Dictionary<string, Animation> animations)
        {
            _animations = animations;
        }

        public void Update(GameTime gameTime)
        {
            _velocity.X = 0;
            _velocity.Y = 0;

            bool anything_pressed = false;
            // handle player input and update velocity
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _velocity.X = 2;
                _facingDirection = 1;
                _currentAnimation = "walk";
                anything_pressed = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _velocity.X = -2;
                _facingDirection = -1;
                _currentAnimation = "walk";
                anything_pressed = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _velocity.Y = 2;
                _currentAnimation = "walk";
                anything_pressed = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                _velocity.Y = -2;
                _currentAnimation = "walk";
                anything_pressed = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (anything_pressed)
                {
                    _currentAnimation = "shoot_walk";
                }
                else
                {
                    _currentAnimation = "shoot";
                    anything_pressed = true;
                }
            }

            if (!anything_pressed)
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


            _animations[_currentAnimation].Update(gameTime);
            _position += _velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _animations[_currentAnimation].Draw(spriteBatch, _position, _scale, _currentEffect);
        }
    }
}