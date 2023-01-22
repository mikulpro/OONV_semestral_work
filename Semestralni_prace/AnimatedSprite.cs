using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Aseprite.Graphics;

namespace Semestralni_prace;

class AnimatedSprite
{
    private Texture2D _spriteSheet;
    private int _frameWidth, _frameHeight;
    private int _frameCount;
    private int _currentFrame;
    private float _frameTime;
    private float _elapsedTime;
    private bool _isLooping;
    private Color _color;
    public Vector2 Scale { get; set; }

    public AnimatedSprite(Texture2D spriteSheet, int frameWidth, int frameHeight, int frameCount, float frameTime, bool isLooping, Vector2 scale)
    {
        _spriteSheet = spriteSheet;
        _frameWidth = frameWidth;
        _frameHeight = frameHeight;
        _frameCount = frameCount;
        _frameTime = frameTime;
        _isLooping = isLooping;
        Scale = scale;
    }

    public void Update(GameTime gameTime)
    {
        _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

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
        }
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffect)
    {
        int column = _currentFrame % (_spriteSheet.Width / _frameWidth);
        int row = _currentFrame / (_spriteSheet.Width / _frameWidth);

        Rectangle sourceRectangle = new Rectangle(column * _frameWidth, row * _frameHeight, _frameWidth, _frameHeight);
        Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, _frameWidth, _frameHeight);
        Vector2 origin = new Vector2(0, 0); // top-left corner of the image
        
        spriteBatch.Draw(_spriteSheet, position, sourceRectangle, Color.White,0, origin, Scale, spriteEffect,0);
        
    }
}
