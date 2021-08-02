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

        bool clicked;
        bool hoover;

        public Button(Rectangle bounds, Text text, Texture2D image, Color color)
        {
            this.Transform.Position = new Vector2(430, 250);
            this.Bounds = bounds;
            text.Transform.Position = new Vector2(590, 320);
            this.Image = image;
            this.ImageColor = color;
            this.Enable();
            this.Drawable = true;
        }
        public void Invoke()
        {
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
        public bool IsHover()
        {
            if (hoover)
            {
                return true;
            }
            return false;
        }
        public override void Update(float elapsed)
        {
            prevMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            if (currentMouseState.X < this.Transform.Position.X + this.Image.Width && currentMouseState.X > this.Transform.Position.X && currentMouseState.Y < this.Transform.Position.Y + this.Image.Height && currentMouseState.Y > this.Transform.Position.Y)
            {
                hoover = true;
                if (currentMouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    clicked = true;
                }
                else clicked = false;
            }
            else
            {
                hoover = false;
            }

            if (hoover)
            {
                this.ImageColor = Color.Red;
            }
            else this.ImageColor = Color.White;
        }
    }
}
