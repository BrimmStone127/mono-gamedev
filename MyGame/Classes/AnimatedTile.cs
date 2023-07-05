using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class AnimatedTile : Tile
{
    private readonly Animation _animation;

    public AnimatedTile(Vector2 position, Animation animation, bool isWalkable)
        : base(position, animation.CurrentFrame, isWalkable)
    {
        _animation = animation;
    }

    public void Update(GameTime gameTime)
    {
        _animation.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        this.Texture = _animation.CurrentFrame;
    }

    public void Draw(SpriteBatch spriteBatch, int currentFrame)
    {
        Rectangle destinationRectangle = new Rectangle(Position.ToPoint(), _animation.CurrentFrameRectangle.Size);
        spriteBatch.Draw(_animation.CurrentFrame, destinationRectangle, _animation.CurrentFrameRectangle, Color.White);
    }

}
