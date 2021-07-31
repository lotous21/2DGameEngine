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

        public string label { get; set; }

        public Text(SpriteFont font, string textlabel, Vector2 Location, Color color)
        {
            this.IsText = true;
            this.TextLabel = textlabel;
            this.TextFont = font;
            this.ImageColor = color;
            this.Transform.Position = Location;
            this.Drawable = true;
            this.Enable();
        }

        public void Update()
        {
            this.TextLabel = label;
        }
    }
}
