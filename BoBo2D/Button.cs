using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace BoBo2D
{
    class Button : GameObjects
    {
        public SpriteFont ButtonLabel;

        public Button(Rectangle bounds, SpriteFont label, Texture2D image, Color color)
        {
            this.Position = new Vector2 (430,250);
            this.Bounds = bounds;
            this.ButtonLabel = label;
            this.Image = image;
            this.ImageColor = color;
        }
    }
}
