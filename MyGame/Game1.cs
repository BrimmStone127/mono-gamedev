using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Map _map;
    private Camera _camera;
    private Texture2D _playerTexture;
    private Texture2D _farmObjectTexture;
    private Player _player;
    private FarmObject _farmObject;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // sample map data
        int[,] sampleMapData = new int[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 1, 1, 0, 0, 0, 0, 0 },
            { 0, 1, 1, 1, 1, 1, 1, 0, 0, 0 },
            { 0, 1, 1, 1, 1, 1, 1, 0, 0, 0 },
            { 0, 0, 1, 1, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        };

        _map = new Map(sampleMapData);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _playerTexture = Content.Load<Texture2D>("player");
        
        _farmObjectTexture = Content.Load<Texture2D>("farmObject");

        _player = new Player(_playerTexture, new Vector2(100, 100), 0.6f);
       
        _farmObject = new FarmObject(_farmObjectTexture, new Vector2(200, 200), 0.5f);
        
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _map.LoadContent(Content);

        _camera = new Camera();
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
            
        //Normalize the mvmnt vector and apply speed
        if(playerMovement != Vector2.Zero)
        {
            playerMovement.Normalize();
            float speed = 200.0f;// Modify this value to change the player's speed
            playerMovement *= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        _player.Move(playerMovement);

        _player.Update(gameTime);

        _farmObject.Update(gameTime);

        _camera.Update(_player.Position, GraphicsDevice.Viewport);

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(transformMatrix: _camera.Transform, sortMode: SpriteSortMode.FrontToBack);
        
        _map.Draw(_spriteBatch);

        _farmObject.Draw(_spriteBatch);

        _player.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
