using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Player : GameObject
{
    public Player(Texture2D texture, Vector2 position, float layerDepth)
        : base(texture, position, layerDepth)
    {
        _layerDepth = layerDepth;
    }

    public Vector2 Position
    {
        get { return _position; }
    }


    public override void Update(GameTime gameTime)
    {
        
    }

    public void Move(Vector2 direction)
    {
        _position += direction;
    }

}