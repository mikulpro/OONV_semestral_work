using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Aseprite.Graphics;

namespace Semestralni_prace;

public class AnimatedSprite
{
    private Texture2D _spriteSheet;
    public int FrameWidth, FrameHeight;
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
        FrameWidth = frameWidth;
        FrameHeight = frameHeight;
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

    public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffect, Color color)
    {
        int column = _currentFrame % (_spriteSheet.Width / FrameWidth);
        int row = _currentFrame / (_spriteSheet.Width / FrameWidth);

        Rectangle sourceRectangle = new Rectangle(column * FrameWidth, row * FrameHeight, FrameWidth, FrameHeight);
        Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, FrameWidth, FrameHeight);
        Vector2 origin = new Vector2(0, 0); // top-left corner of the image
        
        spriteBatch.Draw(_spriteSheet, position, sourceRectangle, color,0, origin, Scale, spriteEffect,0);
        
    }
}
