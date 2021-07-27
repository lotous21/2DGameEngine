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
    class MainMenu : GameObjects
    {
        Button PlayButton;

        public MainMenu(Vector2 location, Rectangle bounds, Texture2D image, Color color, Button button)
        {
            this.Position = location;
            this.Bounds = bounds;
            this.Image = image;
            this.ImageColor = color;
            this.PlayButton = button;
        }

        public void PrintMenu (SpriteBatch sb)
        {
            if (IsEnable())
            {
                sb.Draw(this.Image, this.Position, this.ImageColor);
                sb.Draw(PlayButton.Image, PlayButton.Position, PlayButton.ImageColor);
                sb.DrawString(PlayButton.ButtonLabel, "Play", new Vector2(600, 320), Color.OrangeRed);
            }
        }
    }
}
