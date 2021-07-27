using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Timers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoBo2D
{
    class SplashScreen : GameObjects
    {
        float fadeIn;
        float fadeOut;

        float initialFadeIn;
        float initialFadeOut;
        int totalTime;

        float alpha = 1;

        public SplashScreen (Vector2 Location, Texture2D image, Color color, float FadeIn, float FadeOut, int TotalTime)
        {
            this.Position = Location;
            this.Image = image;
            this.ImageColor = color;
            this.Bounds = new Rectangle(1280, 720, 0, 0);
            this.fadeIn = FadeIn;
            this.fadeOut = FadeOut;
            this.totalTime = TotalTime;
            this.initialFadeIn = FadeIn;
            this.initialFadeOut = FadeOut;
        }

        public void DrawScreen(SpriteBatch sb)
        {
            Timer timer = new Timer(totalTime);
            timer.Elapsed += delegate { this.Disable(); timer.Enabled = false; };
            timer.Enabled = true;
            
            if (this.IsEnable())
            {
                sb.Draw(this.Image, this.Position, this.ImageColor * this.alpha);
            }

            if (this.fadeIn > 0) { this.alpha += 1 / this.initialFadeIn; this.fadeIn--; }
            else if (this.fadeOut > 0) { this.alpha -= 1 / this.initialFadeOut; this.fadeOut--; }

        }

    }
}
