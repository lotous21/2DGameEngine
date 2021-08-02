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
        public bool Drawable { get; set; }
        public bool IsText { get; set; }
        public string TextLabel { get; set; }
        public float Alpha { get; set; }

        public Transform Transform { get; set; }
        public SpriteFont TextFont { get; set; }

        public Texture2D Image { get; set; }
        public Color ImageColor { get; set; }

        public Rectangle Bounds;

        bool enabled;

        public Componenet()
        {
            Transform = new Transform();
            Alpha = 1f;
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
        public virtual void Draw(SpriteBatch sb)
        {
            if (this.Drawable && this.enabled && !IsText)
            {
                sb.Draw(this.Image, this.Transform.Position, this.ImageColor * Alpha);
            }
            else if (this.Drawable && this.enabled && IsText)
            {
                if (TextLabel == null)
                {
                    sb.DrawString(this.TextFont, "New Text", this.Transform.Position, this.ImageColor * Alpha);
                }
                else sb.DrawString(this.TextFont, this.TextLabel, this.Transform.Position, this.ImageColor * Alpha);
            }
        }
    }
}
