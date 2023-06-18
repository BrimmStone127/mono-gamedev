using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

public class Tile {
    public Vector2 Position { get; set; }
    public Texture2D Texture { get; set; }
    public bool IsWalkable { get; set; }

    public Tile(Vector2 position, Texture2D texture, bool isWalkable) {
        Position = position;
        Texture = texture;
        IsWalkable = isWalkable;
    }

    public virtual void Draw(SpriteBatch spriteBatch) {
        spriteBatch.Draw(Texture, Position, Color.White);
    }
}