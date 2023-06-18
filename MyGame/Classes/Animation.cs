using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Animation
{
    private Texture2D _texture; // Sprite sheet.
    private Rectangle[] _frames; // Frames extracted from the sprite sheet.
    private int _currentFrameIndex;
    private float _frameTime;
    private float _elapsedTime;

    public Texture2D CurrentFrame => _texture; // Now we return the whole texture.
    public Rectangle CurrentFrameRectangle => _frames[_currentFrameIndex]; // And the current frame rectangle.

    public Animation(Texture2D texture, int frameWidth, int frameHeight, int frameCount, float frameTime)
    {
        _texture = texture;
        _frameTime = frameTime;
        _frames = new Rectangle[frameCount];

        int framesPerRow = _texture.Width / frameWidth;

        for (int i = 0; i < frameCount; i++)
        {
            int x = i % framesPerRow * frameWidth;
            int y = i / framesPerRow * frameHeight;
            _frames[i] = new Rectangle(x, y, frameWidth, frameHeight);
        }
    }

    public void Update(float deltaTime)
    {
        _elapsedTime += deltaTime;

        if (_elapsedTime >= _frameTime)
        {
            _currentFrameIndex = (_currentFrameIndex + 1) % _frames.Length;
            _elapsedTime = 0f;
        }
    }
}
