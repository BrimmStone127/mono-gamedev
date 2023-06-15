using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _playerTexture;
        private Vector2 _playerPosition;
        private Map _map;
        private Camera _camera;
        private float _playerSpeed;
        private const int TileWidth = 64;
        private const int TileHeight = 64;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1280; // Width of the game window
            _graphics.PreferredBackBufferHeight = 720; // Height of the game window
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // sample map data
        int[,] sampleMapData = new int[,]
        {
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 },
            { 3, 3, 3, 3, 3, 3, 0, 0, 0, 0, 0, 3, 3, 3, 3, 3, 3 },
            { 3, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 3, 3, 3 },
            { 3, 3, 4, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 5, 0, 3, 3 },
            { 3, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 3, 3 },
            { 3, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 3, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 3, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 3, 3, 3, 3 },
            { 3, 5, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 3, 3 },
            { 3, 0, 5, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 3, 3 },
            { 3, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 3, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 5, 3, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 3 },
            { 3, 0, 4, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3 },
            { 3, 5, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 3, 3 },
            { 3, 0, 0, 4, 1, 1, 1, 1, 1, 1, 0, 0, 5, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 5, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 5, 0, 5, 0, 0, 1, 0, 5, 0, 0, 4, 0, 0, 4, 0, 3 },
            { 3, 0, 4, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 5, 0, 0, 3 },
            { 3, 3, 0, 2, 2, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 3, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 3, 3 },
            { 3, 3, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3 },
            { 3, 3, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3 },
            { 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2, 2, 3 },
            { 3, 3, 3, 5, 0, 0, 0, 0, 0, 0, 5, 0, 0, 4, 3, 3, 3 },
            { 3, 3, 3, 0, 0, 0, 5, 0, 0, 0, 5, 0, 0, 3, 3, 3, 3 },
            { 3, 3, 4, 0, 0, 0, 0, 5, 0, 0, 0, 4, 5, 3, 3, 3, 3 },
            { 3, 3, 3, 3, 3, 0, 0, 0, 0, 0, 3, 3, 3, 3, 3, 3, 3 },
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 },
        };

        _map = new Map(sampleMapData);

        base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _playerTexture = Content.Load<Texture2D>("cloud1");
            _playerPosition = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            _playerSpeed = 200.0f;
            _map.LoadContent(Content);
            _camera = new Camera();
            _camera.SetMapDimensions(_map.Width, _map.Height, _map.TileWidth, _map.TileHeight);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState keyboardState = Keyboard.GetState();
            Vector2 playerMovement = Vector2.Zero;

            if (keyboardState.IsKeyDown(Keys.W))
                playerMovement.Y -= 1;
            if (keyboardState.IsKeyDown(Keys.S))
                playerMovement.Y += 1;
            if (keyboardState.IsKeyDown(Keys.A))
                playerMovement.X -= 1;
            if (keyboardState.IsKeyDown(Keys.D))
                playerMovement.X += 1;

            if (playerMovement != Vector2.Zero)
            {
                playerMovement.Normalize();
                playerMovement *= _playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Vector2 newPosition = _playerPosition + playerMovement;
                
                // Update the clamping values using the tile size and map dimensions
                newPosition.X = MathHelper.Clamp(newPosition.X, 0, _map.Width * TileWidth - _playerTexture.Width);
                newPosition.Y = MathHelper.Clamp(newPosition.Y, 0, _map.Height * TileHeight - _playerTexture.Height);
                
                _playerPosition = newPosition;
            }

            _camera.Update(_playerPosition, GraphicsDevice.Viewport);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: _camera.Transform, sortMode: SpriteSortMode.FrontToBack);
            _map.Draw(_spriteBatch);
            _spriteBatch.Draw(_playerTexture, _playerPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
