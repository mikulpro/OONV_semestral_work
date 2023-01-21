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

    public void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 scale, SpriteEffects spriteEffect)
    {
        int column = currentFrame % (spriteSheet.Width / frameWidth);
        int row = currentFrame / (spriteSheet.Width / frameWidth);

        Rectangle sourceRectangle = new Rectangle(column * frameWidth, row * frameHeight, frameWidth, frameHeight);
        Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, frameWidth, frameHeight);
        Vector2 origin = new Vector2(0, 0); // top-left corner of the image
        
        spriteBatch.Draw(spriteSheet, position, sourceRectangle, Color.White,0, origin, scale, spriteEffect,0);

        /*spriteBatch.Draw(spriteSheet, destinationRectangle, sourceRectangle, Color.White,0,Vector2.Zero, spriteEffect,0);*/
    }
}
