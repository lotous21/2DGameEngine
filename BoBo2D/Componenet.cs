using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoBo2D
{
    class Componenet : IComponents
    {
        public bool IsText;
        bool enabled;
        public SpriteFont TextFont;
        public string TextLabel { get; set; }
        public Transform Transform;
        public Texture2D Image;
        public Rectangle Bounds;
        public Color ImageColor;

        public Componenet()
        {
            Transform = new Transform();
        }

        public void Draw(SpriteBatch sb)
        {
            if (this.enabled && !IsText)
            {
                sb.Draw(this.Image, this.Transform.Position, this.ImageColor);
            }
            else if (this.enabled && IsText)
            {
                if (TextLabel == null)
                {
                    sb.DrawString(this.TextFont, "New Text", this.Transform.Position, this.ImageColor);
                }
                else sb.DrawString(this.TextFont, this.TextLabel, this.Transform.Position, this.ImageColor);
            }
        }

        public bool IsEnable()
        {
            if (enabled)
            {
                return true;
            }
            return false;
        }
        public void Enable()
        {
            enabled = true;
        }
        public void Disable()
        {
            enabled = false;
        }
    }
}
