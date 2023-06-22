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
        private int _currentFrame;
        private int _totalFrames;
        private double _animationTimer;
        private const double TimePerFrame = 0.15; // Change to speed up/slow down animation
        private SpriteEffects _playerEffects = SpriteEffects.None; // New variable to store player sprite effects


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
            { 3, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3 },
            { 3, 0, 0, 0, 1, 0, 0, 0, 0, 0, 4, 0, 0, 5, 3, 3, 3 },
            { 3, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 3 },
            { 3, 0, 4, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3 },
            { 3, 5, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 3, 3 },
            { 3, 0, 0, 4, 1, 1, 0, 0, 1, 1, 0, 0, 5, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 1, 0, 6, 0, 1, 1, 0, 0, 0, 5, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 5, 0, 5, 0, 0, 1, 0, 5, 0, 0, 4, 0, 0, 4, 0, 3 },
            { 3, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 3 },
            { 3, 3, 0, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
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
            _totalFrames = 4; // Number of frames in your sprite sheet
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
            Vector2 previousPosition = _playerPosition;


            if (keyboardState.IsKeyDown(Keys.W))
                playerMovement.Y -= 1;
            if (keyboardState.IsKeyDown(Keys.S))
                playerMovement.Y += 1;
            if (keyboardState.IsKeyDown(Keys.A))
            {
                playerMovement.X -= 1;
                _playerEffects = SpriteEffects.FlipHorizontally; // Set to flip when moving left
            }
            if (keyboardState.IsKeyDown(Keys.D)){
                playerMovement.X += 1;
                _playerEffects = SpriteEffects.None; // Set to no flip when moving right
            }

            _animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (_animationTimer > TimePerFrame)
            {
                _currentFrame = (_currentFrame + 1) % _totalFrames; // Loop back to 0 when reaching _totalFrames
                _animationTimer -= TimePerFrame;
            }

            // Only animate if the player is moving
            if (playerMovement != Vector2.Zero)
            {
                _animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (_animationTimer > TimePerFrame)
                {
                    _currentFrame = (_currentFrame + 1) % _totalFrames; // Loop back to 0 when reaching _totalFrames
                    _animationTimer -= TimePerFrame;
                }

                playerMovement.Normalize();
                playerMovement *= _playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Vector2 newPosition = _playerPosition + playerMovement;

                newPosition.X = MathHelper.Clamp(newPosition.X, 0, _map.Width * TileWidth - _playerTexture.Width);
                newPosition.Y = MathHelper.Clamp(newPosition.Y, 0, _map.Height * TileHeight - _playerTexture.Height);

                _playerPosition = newPosition;
            }
            else // If the player is not moving, reset the animation
            {
                _currentFrame = 0;
                _animationTimer = 0;
            }

            _camera.Update(_playerPosition, GraphicsDevice.Viewport);

            _map.Update(gameTime);  // Update the map (includes the animated tiles)

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            int frameWidth = _playerTexture.Width / _totalFrames;
            Rectangle sourceRect = new Rectangle(frameWidth * _currentFrame, 0, frameWidth, _playerTexture.Height);

            _spriteBatch.Begin(transformMatrix: _camera.Transform, sortMode: SpriteSortMode.FrontToBack);
            _map.Draw(_spriteBatch);
            _spriteBatch.Draw(_playerTexture, _playerPosition, sourceRect, Color.White, 0f, Vector2.Zero, 1f, _playerEffects, 0.5f); // changed SpriteEffects.None to _playerEffects
            _spriteBatch.End();

            base.Draw(gameTime);
        }


    }
}
