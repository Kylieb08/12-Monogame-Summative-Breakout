using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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
        Brick brick;
        BarHitBox hitBox;
        Rectangle window, ballRect, barRect, brickRect;
        KeyboardState keyboardState;
        Screen screen;
        SpriteFont titleFont;
        List<Brick> bricks;

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

            ballRect = new Rectangle(10, 10, 12, 12);
            barRect = new Rectangle(325, 480, 70, 15);
            brickRect = new Rectangle(0, 0, 70, 20);

            base.Initialize();

            ball = new Ball(ballTexture, ballRect);
            ball.Speed = new Vector2(3, 2);

            bar = new Bar(barTexture, barRect);

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    Rectangle brickRect = new Rectangle(
                        x * 70 + 2,
                        y * 20 + 2,
                        70,
                        20);

                    bricks.Add(new Brick(brickTexture, brickRect));
                }
            }

            brick = new Brick(brickTexture, brickRect, brickColour);

            hitBox = new BarHitBox(brickTexture, new Rectangle(325, 495, 50, 5));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ballTexture = Content.Load<Texture2D>("Images/circle");
            barTexture = Content.Load<Texture2D>("Images/paddle");
            brickTexture = Content.Load<Texture2D>("Images/rectangle");

            titleFont = Content.Load<SpriteFont>("Font/titleFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            keyboardState = Keyboard.GetState();
            
            if (screen == Screen.Title)
            {
                ballRect.X = 350;
                ballRect.Y = 260;
                barRect.X = 325;
                barRect.Y = 480;

                ball = new Ball(ballTexture, ballRect);
                ball.Speed = new Vector2(3, 3);

                bar = new Bar(barTexture, barRect);
                if (keyboardState.IsKeyDown(Keys.Space))
                    screen = Screen.Game;
            }

            else if (screen == Screen.Game)
            {
                bar.Update(keyboardState, window);
                ball.Update(window, bar, brick);
                hitBox.Update(keyboardState);
                
                if (ball.Rect.Y > window.Bottom)
                {
                    screen = Screen.Lose;
                }
            }

            else if (screen == Screen.Lose)
            {
                if (keyboardState.IsKeyDown (Keys.Enter))
                {
                    screen = Screen.Title;
                }
            }

                base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            if (screen == Screen.Title)
            {
                _spriteBatch.Begin();

                bar.Draw(_spriteBatch);
                _spriteBatch.DrawString(titleFont, "Press Space to Start", new Vector2(125, 250), Color.White);

                _spriteBatch.End();

                
            }

            else if (screen == Screen.Game)
            {
                _spriteBatch.Begin();

                ball.Draw(_spriteBatch);
                bar.Draw(_spriteBatch);
                brick.Draw(_spriteBatch);
                //hitBox.Draw(_spriteBatch);

                _spriteBatch.End();
            }

            else if (screen == Screen.Lose)
            {
                _spriteBatch.Begin();

                _spriteBatch.DrawString(titleFont, "Press Enter to Play Again", new Vector2(125, 200), Color.White);
                _spriteBatch.DrawString(titleFont, "Press Escape to Exit", new Vector2(125, 250), Color.White);

                _spriteBatch.End();
            }

                base.Draw(gameTime);
        }
    }
}
