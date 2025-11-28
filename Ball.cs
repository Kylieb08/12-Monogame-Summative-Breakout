using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12_Monogame_Summative_Breakout
{
    internal class Ball
    {
        private Vector2 _speed;
        private Rectangle _location;
        private Texture2D _texture;

        public Ball(Texture2D texture, Rectangle location)
        {
            _speed = Vector2.Zero;
            _location = location;
            _texture = texture;
        }

        public void Update(Rectangle window)
        {
            _speed = Vector2.Zero;
            _speed.X = 3;
            _speed.Y = -3;

            if (_location.X < window.Left || _location.Right > window.Width)
                _speed.X *= -1;

            if (_location.Y < window.Top || _location.Bottom > window.Bottom)
                _speed.Y *= -1;

            _location.Offset(_speed);
        }

        public Rectangle Rect
        {
            get { return _location; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(_texture, _location, null, Color.White, 0f, 
                Vector2.Zero, SpriteEffects.None, 1f);

            spriteBatch.End();
        }
    }
}
