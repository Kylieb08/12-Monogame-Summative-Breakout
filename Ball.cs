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

        public void Update(Rectangle window, Bar bar)
        {
            if (_location.Right > window.Width || _location.X < window.Left)
                _speed.X *= -1;

            _location.X += (int)_speed.X;

            if ( _location.Y < window.Top)
                _speed.Y *= -1;

            if (bar.Intersects(_location))
            {
                if (_location.Top < bar.BarRect.Bottom || _location.Bottom > bar.BarRect.Top)
                    _speed.Y *= -1;
                if (_location.Left < bar.BarRect.Left || _location.Right > bar.BarRect.Right)
                    _speed.X *= -1;
            }
            
            _location.Y += (int)_speed.Y;
        }

        public Rectangle Rect
        {
            get { return _location; }
        }

        public Vector2 Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _location, null, Color.White, 0f, 
                Vector2.Zero, SpriteEffects.None, 1f);
        }
    }
}
