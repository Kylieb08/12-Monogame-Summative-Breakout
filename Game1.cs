using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _12_Monogame_Summative_Breakout
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D ballTexture, barTexture, brickTexture;
        Ball ball;
        Bar bar;
        BarHitBox hitBox;
        Rectangle window;
        KeyboardState keyboardState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            window = new Rectangle(0, 0, 700, 500);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            base.Initialize();

            ball = new Ball(ballTexture, new Rectangle(350, 260, 12, 12));

            bar = new Bar(barTexture, new Rectangle(325, 495, 50, 5));

            hitBox = new BarHitBox(brickTexture, new Rectangle(325, 495, 50, 5));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ballTexture = Content.Load<Texture2D>("Images/circle");
            barTexture = Content.Load<Texture2D>("Images/paddle");
            brickTexture = Content.Load<Texture2D>("Images/rectangle");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            keyboardState = Keyboard.GetState();

            bar.Update(keyboardState, window);
            ball.Update(window);
            hitBox.Update(keyboardState);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            ball.Draw(_spriteBatch);
            bar.Draw(_spriteBatch);
            //hitBox.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}
