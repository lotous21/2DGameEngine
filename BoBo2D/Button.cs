using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace BoBo2D
{
    class Button : Componenet
    {
        MouseState currentMouseState;
        MouseState prevMouseState;
        Rectangle mouseRec;
        bool clicked;

        Text ButtonLabel;

        public Button(Rectangle bounds, Text text, Texture2D image, Color color)
        {
            this.Transform.Position = new Vector2(430, 250);
            this.Bounds = bounds;
            this.ButtonLabel = text;
            text.Transform.Position = new Vector2(590,320);
            this.Image = image;
            this.ImageColor = color;
            this.Enable();
        }
        public void Invoke()
        {
            mouseRec = new Rectangle(1, 1, currentMouseState.X, currentMouseState.Y);
            clicked = false;
        }
        public bool IsClick()
        {
            if (clicked)
            {
                return true;
            }

            return false;
        }
        public void Update()
        {
            prevMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            if (mouseRec.Intersects(this.Bounds))
            {
                if (currentMouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    clicked = true;
                }
                else clicked = false;
            }
        }
    }
}
