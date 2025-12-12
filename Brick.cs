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
        private Color _colour;
        //private List<Rectangle> _bricks;

        //public List<Rectangle> rectangles
        //{
        //    get { return _bricks; }
        //}

        public Color Colour
        {
            get { return _colour; }
            set { _colour = value; }
        }

        public Brick(Texture2D texture, Rectangle location, Color colour)
        {
            _location = location;
            _texture = texture;
            _colour = colour;
        }

        public bool Intersects(Rectangle brick)
        {
            return _location.Intersects(brick);
        }

        //public void RemoveBrick(int index)
        //{
        //    if (index >= 0 && index < _bricks.Count)
        //    {
        //        _bricks.RemoveAt(index);
        //    }
        //}

        public void Draw (SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _location, _colour);
            
        }
    }
}
