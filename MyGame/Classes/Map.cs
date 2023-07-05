using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


public class Map
{
    private int[,] _mapData;
    private Tile[,] _tiles;
    private int _currentFrame;

    public int Width { get; private set; }
    public int Height { get; private set; }
    public int TileWidth { get; } = 64;
    public int TileHeight { get; } = 64;

    public Map()
    {

    }

    public void LoadMapData(int[,] mapData)
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
                float depth;

                switch (tileType)
                {
                    case 0: //Empty
                        break;
                    case 1: //Grass
                        texture = content.Load<Texture2D>("grass2");
                        isWalkable = true;
                        Vector2 position = new Vector2(x * texture.Width, y * texture.Height);
                        depth = 0.0f;
                        _tiles[x, y] = new Tile(position, texture, isWalkable, depth);
                        break;
                    case 2: //Soil
                        texture = content.Load<Texture2D>("dirt2");
                        isWalkable = true;
                        position = new Vector2(x * texture.Width, y * texture.Height);
                        depth = 0.0f;
                        _tiles[x, y] = new Tile(position, texture, isWalkable, depth);
                        break;
                    case 3: //Water
                        texture = content.Load<Texture2D>("water2");
                        isWalkable = false;
                        position = new Vector2(x * texture.Width, y * texture.Height);
                        depth = 0.0f;
                        _tiles[x, y] = new Tile(position, texture, isWalkable, depth);
                        break;
                    case 4: //tree1
                        texture = content.Load<Texture2D>("tree1");
                        isWalkable = false;
                        position = new Vector2(x * texture.Width, y * texture.Height);
                        depth = 1.0f;
                        _tiles[x, y] = new Tile(position, texture, isWalkable, depth);
                        break;
                    case 5: //tree2
                        texture = content.Load<Texture2D>("tree2");
                        isWalkable = false;
                        position = new Vector2(x * texture.Width, y * texture.Height);
                        depth = 1.0f;
                        _tiles[x, y] = new Tile(position, texture, isWalkable, depth);
                        break;
                    case 6: //tree3
                        texture = content.Load<Texture2D>("tree3");
                        isWalkable = false;
                        position = new Vector2(x * texture.Width, y * texture.Height);
                        depth = 1.0f;
                        _tiles[x, y] = new Tile(position, texture, isWalkable, depth);
                        break;
                    case 7: //fire animation
                        var fireTexture = content.Load<Texture2D>("fire1");
                        int frameWidth = fireTexture.Width / 22; // I'm assuming there are 22 frames.
                        var fireAnimation = new Animation(fireTexture, frameWidth, fireTexture.Height, 22, 0.1f);
                        Vector2 positionF = new Vector2(x * frameWidth, y * fireTexture.Height);
                        depth = 0.0f;
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
                if (_mapData[x, y] != 0)
                {
                    if (_tiles[x, y] is AnimatedTile animatedTile)
                    {
                        animatedTile.Draw(spriteBatch, _currentFrame); // Pass the currentFrame to the Draw method
                    }
                    else
                    {
                        _tiles[x, y].Draw(spriteBatch);
                    }
                }
            }
        }
    }



    public void Update(GameTime gameTime)
    {
        for (int x = 0; x < _tiles.GetLength(0); x++)
        {
            for (int y = 0; y < _tiles.GetLength(1); y++)
            {
                if (_mapData[x, y] != 0 && _tiles[x, y] is AnimatedTile animatedTile)
                {
                    animatedTile.Update(gameTime);
                }
            }
        }
    }




}

