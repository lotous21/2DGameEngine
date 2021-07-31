using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace BoBo2D
{
    class Background : Componenet
    {
        public Background(Vector2 location, Rectangle bounds, Texture2D image, Color color)
        {
            this.Transform.Position = location;
            this.Bounds = bounds;
            this.Image = image;
            this.ImageColor = color;
            this.Drawable = true;
        }
    }
}
