using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoBo2D
{
    class Text : Componenet
    {
        public Text(SpriteFont font, string label, Vector2 Location, Color color)
        {
            this.IsText = true;
            this.TextFont = font;
            this.TextLabel = label;
            this.ImageColor = color;
            this.Transform.Position = Location;
            this.Drawable = true;
            this.Enable();
        }
    }
}
