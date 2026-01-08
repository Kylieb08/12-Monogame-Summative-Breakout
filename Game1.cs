using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
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

        Random generator = new Random();

        Texture2D ballTexture, barTexture, brickTexture, powerUpTexture;
        Ball ball;
        Bar bar;
        Brick brick;
        BarHitBox hitBox;
        Rectangle window, ballRect, barRect, brickRect, powerUpRect;
        KeyboardState keyboardState, prevKeyboardState;
        Screen screen;
        Color brickColour;
        SpriteFont titleFont, speedFont;
        List<Brick> bricks;
        Vector2 ballSpeed, powerUpSpeed, powerUpPosition;
        float powerUpFall = 1.5f;
        bool powerUp = false, playAgain = false, randomBallxSpeed = false;
        SoundEffect boing, brickBreaking, itemCollect;
        Song bgMusic, gameLost, gameWon;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            window = new Rectangle(0, 0, 718, 530);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            screen = Screen.Title;

            ballRect = new Rectangle(10, 10, 12, 12);
            barRect = new Rectangle(325, 480, 70, 15);
            brickRect = new Rectangle(0, 0, 70, 20);
            powerUpRect = new Rectangle(339, 245, 20, 20);

            brickColour = Color.DarkCyan;
            bricks = new List<Brick>();

            ballSpeed = new Vector2(3, 2);
            powerUpPosition = new Vector2(339, 245);
            powerUpSpeed = Vector2.Zero;

            base.Initialize();

            ball = new Ball(ballTexture, ballRect, boing);
            ball.Speed = ballSpeed;

            bar = new Bar(barTexture, barRect);

            GenerateBricks();

            brick = new Brick(brickTexture, brickRect, brickColour);

            brick.Colour = brickColour;

            hitBox = new BarHitBox(brickTexture, new Rectangle(325, 495, 50, 5));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ballTexture = Content.Load<Texture2D>("Images/circle");
            barTexture = Content.Load<Texture2D>("Images/paddle");
            brickTexture = Content.Load<Texture2D>("Images/rectangle");
            powerUpTexture = Content.Load<Texture2D>("Images/ball_x_speed_effect");

            titleFont = Content.Load<SpriteFont>("Font/titleFont");
            speedFont = Content.Load<SpriteFont>("Font/speedFont");

            bgMusic = Content.Load<Song>("Sounds/Tetris");
            boing = Content.Load<SoundEffect>("Sounds/Boing");
            brickBreaking = Content.Load<SoundEffect>("Sounds/brick_breaking");
            itemCollect = Content.Load<SoundEffect>("Sounds/item_collect");
            gameLost = Content.Load<Song>("Sounds/game_over");
            gameWon = Content.Load<Song>("Sounds/game_cleared");
        }

        public void GenerateBricks()
        {
            for (int y = 0; y < 160; y += 22)
            {
                for (int x = 0; x < window.Width; x += 72)
                {
                    Rectangle brickRect = new Rectangle(x, y, 70, 20);

                    if (y < 22)
                        brickColour = Color.PaleVioletRed;

                    else if (y < 44)
                        brickColour = Color.Red;

                    else if (y < 66)
                        brickColour = Color.Orange;

                    else if (y < 88)
                        brickColour = Color.Yellow;

                    else if (y < 110)
                        brickColour = Color.Green;

                    else if (y < 132)
                        brickColour = Color.Blue;

                    else if (y < 154)
                        brickColour = Color.Purple;

                    else
                        brickColour = Color.MediumTurquoise;

                    bricks.Add(new Brick(brickTexture, brickRect, brickColour));
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            prevKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            if (screen == Screen.Title)
            {
                MediaPlayer.Stop();
                ballRect.X = 350;
                ballRect.Y = 260;
                barRect.X = 343;
                barRect.Y = 480;

                ball = new Ball(ballTexture, ballRect, boing);
                ball.Speed = new Vector2(3, 3);

                bar = new Bar(barTexture, barRect);
                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    screen = Screen.Game;
                    MediaPlayer.Play(bgMusic);
                    MediaPlayer.IsRepeating = true;
                }

                if (playAgain)
                {
                    bricks.Clear();
                    GenerateBricks();
                    powerUp = false;
                    powerUpPosition = new Vector2(339, 245);
                    powerUpSpeed = Vector2.Zero;
                    randomBallxSpeed = false;
                    playAgain = false;
                }
            }

            else if (screen == Screen.Game)
            {
                bar.Update(keyboardState, window);
                ball.Update(window, bar, boing);
                hitBox.Update(keyboardState);

                powerUpSpeed.Y = 0f;

                if (ball.Rect.Y > window.Bottom)
                {
                    screen = Screen.Lose;
                }

                for (int i = 0; i < bricks.Count; i++)
                {
                    if (bricks[i].Intersects(ball.Rect))
                    {
                        ball.YSpeed *= -1;
                        bricks.RemoveAt(i);
                        brickBreaking.Play();
                    }
                }

                if (randomBallxSpeed && keyboardState.IsKeyUp(Keys.Space) && prevKeyboardState.IsKeyDown(Keys.Space))
                {
                    if (ball.XSpeed > 0)
                        ball.XSpeed = generator.Next(3, 6);

                    else if (ball.XSpeed < 0)
                        ball.XSpeed = generator.Next(-5, -2);
                }

                if (bricks.Count == 0)
                    screen = Screen.Win;

                if (keyboardState.IsKeyUp(Keys.U) && prevKeyboardState.IsKeyDown(Keys.U))
                    ballRect.X = 347;

                if (bricks.Count < 79)
                    powerUp = true;

                if (powerUp)
                {
                    powerUpSpeed.Y += powerUpFall;

                    powerUpPosition.Y += powerUpSpeed.Y;
                    powerUpRect.Location = powerUpPosition.ToPoint();

                    if (bar.Intersects(powerUpRect))
                    {
                        randomBallxSpeed = true;
                        itemCollect.Play();
                        //powerUpRect.Y = 5000; <-- tried to use this to make the power up teleport off the screen. It didn't work
                        powerUp = false;
                    }

                }

            }

            else if (screen == Screen.Lose)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(gameLost);
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    playAgain = true;
                    screen = Screen.Title;
                }
            }

            else if (screen == Screen.Win)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(gameWon);
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    playAgain = true;
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
                _spriteBatch.DrawString(titleFont, "Press Space to Start", new Vector2(143, 250), Color.White);

                _spriteBatch.End();
            }

            else if (screen == Screen.Game)
            {
                _spriteBatch.Begin();

                if (powerUp)
                    _spriteBatch.Draw(powerUpTexture, powerUpRect, Color.White);

                ball.Draw(_spriteBatch);
                bar.Draw(_spriteBatch);

                foreach (Brick brick in bricks)
                    brick.Draw(_spriteBatch);

                //hitBox.Draw(_spriteBatch);

                _spriteBatch.DrawString(speedFont, $"Ball X speed = {ball.XSpeed}", new Vector2(10, 500), Color.White);
                _spriteBatch.DrawString(speedFont, $"Ball Y speed = {ball.YSpeed}", new Vector2(200, 500), Color.White);



                _spriteBatch.End();
            }

            else if (screen == Screen.Lose)
            {
                _spriteBatch.Begin();

                _spriteBatch.DrawString(titleFont, "You Lose :(", new Vector2(125, 150), Color.White);
                _spriteBatch.DrawString(titleFont, "Press Enter to Play Again", new Vector2(125, 200), Color.White);
                _spriteBatch.DrawString(titleFont, "Press Escape to Exit", new Vector2(125, 250), Color.White);

                _spriteBatch.End();
            }

            else if (screen == Screen.Win)
            {
                _spriteBatch.Begin();

                _spriteBatch.DrawString(titleFont, "You Win!", new Vector2(125, 150), Color.White);
                _spriteBatch.DrawString(titleFont, "Press Enter to Play Again", new Vector2(125, 200), Color.White);
                _spriteBatch.DrawString(titleFont, "Press Escape to Exit", new Vector2(125, 250), Color.White);

                _spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
