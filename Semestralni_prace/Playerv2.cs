
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Aseprite.Graphics;

namespace Semestralni_prace
{

    class Playerv2
    {
        public static int player_animation_frame_width = 48;
        public static int player_animation_frame_height = 48;

        /*private Texture2D _spriteSheet;
        private int _frameWidth, _frameHeight;
        private int _frameCount;
        private int _currentFrame;
        private float _frameTime;
        private float _elapsedTime;
        private bool _isLooping;*/
        private Dictionary<string, Animation> _animations;
        private Vector2 _position;
        private Vector2 _velocity;
        private int _facingDirection = 1; // 1 = right, -1 = left
        private Vector2 _movementVector;
        private string _currentAnimation;
        private SpriteEffects _currentEffect;

        public Playerv2(Vector2 position)
        {
            this._position = position;
            
            _currentAnimation = "idle";
            _facingDirection = 1;
            _currentEffect = SpriteEffects.None;
        }

        public void LoadContent(Dictionary<string, Animation> animations)
        {
            _animations = animations;
        }

        public void Update(GameTime gameTime)
        {
            // handle player input and update velocity
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _velocity.X = 2;
                _facingDirection = 1;
                _currentAnimation = "walk";
                _currentEffect = SpriteEffects.None;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _velocity.X = -2;
                _facingDirection = -1;
                _currentAnimation = "walk";
                _currentEffect = SpriteEffects.FlipHorizontally;
            }
            else
            {
                _velocity.X = 0;
                if (_facingDirection == 1)
                {
                    _currentAnimation = "idle";
                    _currentEffect = SpriteEffects.None;
                }
                else
                {
                    _currentAnimation = "idle";
                    _currentEffect = SpriteEffects.FlipHorizontally;
                }
            }

            /*_elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_elapsedTime >= _frameTime)
            {
                _currentFrame++;

                if (_currentFrame >= _frameCount)
                {
                    if (_isLooping)
                    {
                        _currentFrame = 0;
                    }
                    else
                    {
                        _currentFrame = _frameCount - 1;
                    }
                }

                _elapsedTime = 0;
            }*/
            
            _animations[_currentAnimation].Update(gameTime);
            _position += _velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _animations[_currentAnimation].Draw(spriteBatch, _position, _currentEffect);
            /*int row = 0, column = 0;
            if (_currentAnimation == "walk_right" || _currentAnimation == "idle_right")
            {
                row = 0;
            }
            else if (_currentAnimation == "walk_left" || _currentAnimation == "idle_left")
            {
                row = 1;
            }

            column = _currentFrame;
            Rectangle sourceRectangle = new Rectangle(column * _frameWidth, row * _frameHeight, _frameWidth, _frameHeight);
            Rectangle destinationRectangle = new Rectangle((int)_position.X, (int)_position.Y, _frameWidth, _frameHeight);
            spriteBatch.Draw(_spriteSheet, destinationRectangle, sourceRectangle, Color.White);*/
        }
    }
}