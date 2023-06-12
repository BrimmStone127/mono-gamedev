using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Camera
{
    private Vector2 _position;
    private float _smoothness;
    private int _mapWidth;
    private int _mapHeight;

    public Camera(float smoothness = 0.1f)
    {
        _position = Vector2.Zero;
        _smoothness = smoothness;
    }

  public void SetMapDimensions(int mapWidthInTiles, int mapHeightInTiles, int tileWidth, int tileHeight)
    {
        _mapWidth = mapWidthInTiles * tileWidth;
        _mapHeight = mapHeightInTiles * tileHeight;
    }

    public void Update(Vector2 playerPosition, Viewport viewport)
    {
            Vector2 targetPosition = playerPosition - new Vector2(viewport.Width / 2, viewport.Height / 2);

            _position = Vector2.Lerp(_position, targetPosition, _smoothness);
            _position.X = MathHelper.Clamp(_position.X, 0, _mapWidth - viewport.Width);
            _position.Y = MathHelper.Clamp(_position.Y, 0, _mapHeight - viewport.Height);
    }

    public Matrix Transform
    {
        get
        {
            return Matrix.CreateTranslation(new Vector3(-_position, 0));
        }
    }
}
