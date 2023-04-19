using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FarmObject : GameObject
{
    public FarmObject(Texture2D texture, Vector2 position, float layerDepth)
        : base(texture, position, layerDepth)
    {
    }

    public override void Update(GameTime gameTime)
    {
        // Implement farm object-specific update logic, like growth or interaction
    }
}