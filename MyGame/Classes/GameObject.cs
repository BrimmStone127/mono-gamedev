using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class GameObject
{
    protected Texture2D _texture;
    protected Vector2 _position;
    protected float _layerDepth;

    public GameObject(Texture2D texture, Vector2 position, float layerDepth)
    {
        _texture = texture;
        _position = position;
        _layerDepth = layerDepth;
    }
    
    public abstract void Update(GameTime gameTime);

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, _layerDepth);
    }
}