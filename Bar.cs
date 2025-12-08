using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12_Monogame_Summative_Breakout
{
    internal class Bar
    {
        private Vector2 _speed;
        private Rectangle _location;
        private Texture2D _texture;

        public Bar(Texture2D texture, Rectangle location)
        {
            _speed = Vector2.Zero;
            _location = location;
            _texture = texture;
        }

        public void Update(KeyboardState keyboardState, Rectangle window)
        {
            _speed = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {
                _speed.X = -4;
                if (_location.X < 5)
                    _location.X = 5;
            }

            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                _speed.X = 4;
                if (_location.Right > (window.Width) - 5)
                    _location.X = (window.Width - _location.Width) - 5;
            }

            _location.Offset(_speed);
        }

        public Rectangle BarRect
        {
            get { return _location; }
        }

        public bool Contains(Point bar)
        {
            return _location.Contains(bar);
        }

        public bool Intersects(Rectangle bar)
        {
            return _location.Intersects(bar);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _location, null, Color.White, 0f,
                Vector2.Zero, SpriteEffects.None, 1f);
        }
    }
}
