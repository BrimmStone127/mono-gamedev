using Microsoft.Xna.Framework.Graphics;

public class Animation
{
    private Texture2D[] _frames;
    private int _currentFrameIndex;
    private float _frameTime;
    private float _timer;

    public Texture2D CurrentFrame => _frames[_currentFrameIndex];

    public Animation(Texture2D[] frames, float frameTime)
    {
        _frames = frames;
        _frameTime = frameTime;
        _currentFrameIndex = 0;
        _timer = 0;
    }

    public void Update(float elapsedTime)
    {
        _timer += elapsedTime;

        if (_timer >= _frameTime)
        {
            _timer = 0;
            _currentFrameIndex = (_currentFrameIndex + 1) % _frames.Length;
        }
    }
}
