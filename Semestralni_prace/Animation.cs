using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Aseprite.Graphics;

namespace Semestralni_prace;

class Animation
{
    private Texture2D spriteSheet;
    private int frameWidth, frameHeight;
    private int frameCount;
    private int currentFrame;
    private float frameTime;
    private float elapsedTime;
    private bool isLooping;

    public Animation(Texture2D spriteSheet, int frameWidth, int frameHeight, int frameCount, float frameTime, bool isLooping)
    {
        this.spriteSheet = spriteSheet;
        this.frameWidth = frameWidth;
        this.frameHeight = frameHeight;
        this.frameCount = frameCount;
        this.frameTime = frameTime;
        this.isLooping = isLooping;
    }

    public void Update(GameTime gameTime)
    {
        elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (elapsedTime >= frameTime)
        {
            currentFrame++;

            if (currentFrame >= frameCount)
            {
                if (isLooping)
                {
                    currentFrame = 0;
                }
                else
                {
                    currentFrame = frameCount - 1;
                }
            }

            elapsedTime = 0;
        }
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffect)
    {
        int column = currentFrame % (spriteSheet.Width / frameWidth);
        int row = currentFrame / (spriteSheet.Width / frameWidth);

        Rectangle sourceRectangle = new Rectangle(column * frameWidth, row * frameHeight, frameWidth, frameHeight);
        Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, frameWidth, frameHeight);

        /*spriteBatch.Draw(spriteSheet, destinationRectangle, sourceRectangle, Color.White);*/
        spriteBatch.Draw(spriteSheet, destinationRectangle, sourceRectangle, Color.White,0,Vector2.Zero, spriteEffect,0);
    }
}
