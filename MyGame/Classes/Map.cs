using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


public class Map
{
    private readonly int[,] _mapData;
    private readonly Tile[,] _tiles;
    public int Width { get; private set; }
    public int Height { get; private set; }
    public int TileWidth { get; } = 64;
    public int TileHeight { get; } = 64;

    public Map(int[,] mapData)
    {
        _mapData = mapData;
        Width = mapData.GetLength(1) * TileWidth;
        Height = mapData.GetLength(0) * TileHeight;

        _tiles = new Tile[mapData.GetLength(0), mapData.GetLength(1)];
    }

    public void LoadContent(ContentManager content)
    {
        for (int x = 0; x < _mapData.GetLength(0); x++)
        {
            for (int y = 0; y < _mapData.GetLength(1); y++)
            {
                int tileType = _mapData[x, y];
                Texture2D texture;
                bool isWalkable;

                switch (tileType)
                {
                    case 0: //Grass
                        texture = content.Load<Texture2D>("grass2");
                        isWalkable = true;
                        Vector2 position = new Vector2(x * texture.Width, y * texture.Height);
                        _tiles[x, y] = new Tile(position, texture, isWalkable);
                        break;
                    case 1: //Soil
                        texture = content.Load<Texture2D>("dirt2");
                        isWalkable = true;
                        position = new Vector2(x * texture.Width, y * texture.Height);
                        _tiles[x, y] = new Tile(position, texture, isWalkable);
                        break;
                    case 2: //Water
                        texture = content.Load<Texture2D>("water2");
                        isWalkable = false;
                        position = new Vector2(x * texture.Width, y * texture.Height);
                        _tiles[x, y] = new Tile(position, texture, isWalkable);
                        break;
                    case 3: //tree1
                        texture = content.Load<Texture2D>("tree1");
                        isWalkable = false;
                        position = new Vector2(x * texture.Width, y * texture.Height);
                        _tiles[x, y] = new Tile(position, texture, isWalkable);
                        break;
                    case 4: //tree2
                        texture = content.Load<Texture2D>("tree2");
                        isWalkable = false;
                        position = new Vector2(x * texture.Width, y * texture.Height);
                        _tiles[x, y] = new Tile(position, texture, isWalkable);
                        break;
                    case 5: //tree3
                        texture = content.Load<Texture2D>("tree3");
                        isWalkable = false;
                        position = new Vector2(x * texture.Width, y * texture.Height);
                        _tiles[x, y] = new Tile(position, texture, isWalkable);
                        break;
                    case 6: //fire animation
                        var fireTexture = content.Load<Texture2D>("fire1");
                        int frameWidth = fireTexture.Width / 22; // I'm assuming there are 22 frames.
                        var fireAnimation = new Animation(fireTexture, frameWidth, fireTexture.Height, 22, 0.1f);
                        Vector2 positionF = new Vector2(x * frameWidth, y * fireTexture.Height);
                        _tiles[x, y] = new AnimatedTile(positionF, fireAnimation, false);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"Invalid tile type: {tileType}");
                }
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int x = 0; x < _tiles.GetLength(0); x++)
        {
            for (int y = 0; y < _tiles.GetLength(1); y++)
            {
                _tiles[x, y].Draw(spriteBatch);
            }
        }
    }

    public void Update(GameTime gameTime)
    {
        foreach (var tile in _tiles)
        {
            if (tile is AnimatedTile animatedTile)
            {
                animatedTile.Update(gameTime);
            }
        }
    }

}

