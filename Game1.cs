using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _12_Monogame_Summative_Breakout
{
    public enum Screen
    {
        Title,
        Game,
        Lose,
        Win
    }
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
        Screen screen;

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

            screen = Screen.Title;

            base.Initialize();

            ball = new Ball(ballTexture, new Rectangle(350, 260, 12, 12));
            ball.Speed = new Vector2(3, 3);

            bar = new Bar(barTexture, new Rectangle(325, 480, 70, 15));

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
            
            if (screen == Screen.Title)
            {
                if (keyboardState.IsKeyDown(Keys.Space))
                    screen = Screen.Game;
            }

            else if (screen == Screen.Game)
            {
                bar.Update(keyboardState, window);
                ball.Update(window, bar);
                hitBox.Update(keyboardState);
                
                
            }
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            if (screen == Screen.Title)
            {
                bar.Draw(_spriteBatch);
            }

            else if (screen == Screen.Game)
            {
                ball.Draw(_spriteBatch);
                bar.Draw(_spriteBatch);
                //hitBox.Draw(_spriteBatch);
            }

            base.Draw(gameTime);
        }
    }
}
