using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Camera
{
    private Matrix _transform;
    private Vector2 _position;
    private float _zoom;

    public Camera()
    {
        _position = Vector2.Zero;
        _zoom = 1.0f;
    }

    public Matrix Transform
    {
        get { return _transform; }
    }

    public void Update(Vector2 targetPosition, Viewport viewport)
    {
        _position = targetPosition - new Vector2(viewport.Width / 2f, viewport.Height / 2f);

        _transform = Matrix.CreateTranslation(new Vector3(-_position, 0)) * Matrix.CreateScale(_zoom) * Matrix.CreateTranslation(new Vector3(viewport.Width * 0.5f, viewport.Height * 0.5f, 0));
    }

    internal void Update(object position, Viewport viewport)
    {
        throw new NotImplementedException();
    }
}