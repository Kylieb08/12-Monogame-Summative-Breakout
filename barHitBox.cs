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
    internal class BarHitBox
    {
        private Vector2 _speed;
        private Rectangle _location;
        private Texture2D _texture;

        public BarHitBox(Texture2D texture, Rectangle location)
        {
            _speed = Vector2.Zero;
            _location = location;
            _texture = texture;
        }

        public void Update(KeyboardState keyboardState)
        {
            _speed = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {
                _speed.X = -4;
                if (_location.X < 0)
                    _location.X = 0;
            }

            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                _speed.X = 4;
                if (_location.X > 650)
                    _location.X = 650;
            }

            _location.Offset(_speed);
        }

        public Rectangle Rect
        {
            get { return _location; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(_texture, _location, null, Color.Red * 0.5f, 0f,
                Vector2.Zero, SpriteEffects.None, 1f);

        }
    }
}
