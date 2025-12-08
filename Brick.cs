using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace _12_Monogame_Summative_Breakout
{
    public class Brick
    {
        private Rectangle _location;
        private Texture2D _texture;
        //private List<Color> _colours;
        private List<Rectangle> _bricks;

        public List<Rectangle> rectangles
        {
            get { return _bricks; }
        }

        public bool Intersects(Rectangle ballLocation)
        {
            return _location.Intersects(ballLocation);
        }

        public Brick(Texture2D textures, Rectangle location)
        {
            _location = location;
            _texture = textures;
            

            
        }

        public bool Intersects(Rectangle brick)
        {
            return _location.Intersects(brick);
        }

        public void RemoveBrick(int index)
        {
            if (index >= 0 && index < _bricks.Count)
            {
                _bricks.RemoveAt(index);
            }
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            foreach (var brick in _bricks)
            {
                spriteBatch.Draw(_texture, brick, Color.DarkCyan);
            }
        }
    }
}
