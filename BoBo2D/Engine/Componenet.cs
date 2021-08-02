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
        public bool Drawable;
        public bool IsText;
        bool enabled;
        public SpriteFont TextFont;
        public string TextLabel { get; set; }
        public Transform Transform;
        public Texture2D Image;
        public Rectangle Bounds;
        public Color ImageColor;
        public float Alpha;

        public Componenet()
        {
            Transform = new Transform();
            Alpha = 1f;
        }

        public virtual void Draw (SpriteBatch sb)
        {
            if (this.Drawable && this.enabled && !IsText)
            {
                sb.Draw(this.Image, this.Transform.Position, this.ImageColor* Alpha);
            }
            else if (this.Drawable && this.enabled && IsText)
            {
                if (TextLabel == null)
                {
                    sb.DrawString(this.TextFont, "New Text", this.Transform.Position, this.ImageColor* Alpha);
                }
                else sb.DrawString(this.TextFont, this.TextLabel, this.Transform.Position, this.ImageColor* Alpha);
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

        public virtual void Update(float elapsed)
        {

        }

    }
}
