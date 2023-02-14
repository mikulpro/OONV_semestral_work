using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace Semestralni_prace;
public abstract class Ant
{
    public int Hp { get; }
    public int AttackPower { get; }
    
    protected AnimatedSprite _animatedSprited;
    public void LoadSprite() {}
    public void Update()
    {
        
    }

    public void Attack()
    {
        
    }

    public void Dodge()
    {
        
    }
    
    
}


public class BaseAnt : Ant
{
    public BaseAnt()
    {
        
    }
    
    public void LoadSprite()
    {
        TextureManager textureManager = TextureManager.Instance;
         _animatedSprited = new AnimatedSprite(
            textureManager.GetTexture("ant"),
            Player.PlayerAnimationFrameWidth, 
            Player.PlayerAnimationFrameHeight, 
            7, 
            0.1f, true,
            new Vector2(2.0f, 2.0f));
    }


}