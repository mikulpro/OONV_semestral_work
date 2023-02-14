using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Aseprite.Graphics;


namespace Semestralni_prace;

public class Playerv1
{
       private Vector2 _coords;
       private SpriteBatch _spriteBatch;
       private MonoGame.Aseprite.Graphics.AnimatedSprite _sprite;
       private Texture2D _texture;
       private Vector2 _movementVector;
       private int _movementSpeed;
       private Vector2 _windowSize;
       

       public Playerv1(int initX=0, int initY=0)
       {
              _coords = new Vector2();
              _coords.X =initX;
              _coords.Y = initY;
              _movementVector = new Vector2(0, 0);
              _movementSpeed = 100;
       }

       public void LoadContent(SpriteBatch spriteBatch, Texture2D texture)
       {
              _spriteBatch = spriteBatch;
              _texture = texture;
              
       }

       public void setWindowSize(Vector2 windowSize)
       {
              _windowSize = windowSize;
       }

       public void Update(GameTime gameTime)
       {
              var kstate = Keyboard.GetState();
              
              if (kstate.IsKeyDown(Keys.Up))
              {
                     _coords.Y -= _movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
              }

              if(kstate.IsKeyDown(Keys.Down))
              {
                     _coords.Y += _movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
              }

              if (kstate.IsKeyDown(Keys.Left))
              {
                     _coords.X -= _movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
              }

              if(kstate.IsKeyDown(Keys.Right))
              {
                     _coords.X += _movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
              }

              // boundaries
              if(_coords.X > _windowSize.X - _texture.Width / 2)
              {
                     _coords.X = _windowSize.X - _texture.Width / 2;
                     _movementVector.X = -1;
              }
              else if(_coords.X < _texture.Width / 2)
              {
                     _coords.X = _texture.Width / 2;
                     _movementVector.X = 1;
              }

              if(_coords.Y > _windowSize.Y - _texture.Height / 2)
              {
                     _coords.Y = _windowSize.Y - _texture.Height / 2;
                     _movementVector.Y = -1;
              }
              else if (_coords.Y < _texture.Height / 2)
              {
                     _coords.Y = _texture.Height / 2;
                     _movementVector.Y = 1;
              }
              
       }

       public void Draw()
       {
              /*_sprite.Render(_spriteBatch);*/
              _spriteBatch.Draw(
                     _texture,
                     _coords,
                     null,
                     Color.White,
                     0f,
                     new Vector2(_texture.Width / 2, _texture.Height / 2),
                     Vector2.One,
                     SpriteEffects.None,
                     0f
              );
       }
}