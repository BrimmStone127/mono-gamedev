using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class AnimatedTile : Tile
{
    private readonly Animation _animation;
    private float _elapsedTime;

    public AnimatedTile(Vector2 position, Animation animation, bool isWalkable)
        : base(position, animation.CurrentFrame, isWalkable)
    {
        _animation = animation;
    }

    public void Update(GameTime gameTime)
    {
        _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
        _animation.Update(_elapsedTime);
        this.Texture = _animation.CurrentFrame;
    }
}
