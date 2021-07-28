using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Timers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoBo2D
{
    class SplashScreen : Componenet
    {
        float fadeIn;
        float fadeOut;

        float initialFadeIn;
        float initialFadeOut;
        int totalTime;

        public SplashScreen (Vector2 Location, Texture2D image, Color color, float FadeIn, float FadeOut, int TotalTime)
        {
            this.Transform.Position = Location;
            this.Image = image;
            this.ImageColor = color;
            this.Bounds = new Rectangle(1280, 720, 0, 0);
            this.fadeIn = FadeIn;
            this.fadeOut = FadeOut;
            this.totalTime = TotalTime;
            this.initialFadeIn = FadeIn;
            this.initialFadeOut = FadeOut;
            this.Alpha = 1;
            this.Drawable = true;
            Timer timer = new Timer(totalTime);
            timer.Elapsed += delegate { this.Disable(); timer.Enabled = false; };
            timer.Enabled = true;
        }

        public void Update()
        {
            if (this.fadeIn > 0) { this.Alpha += 1 / this.initialFadeIn; this.fadeIn--; this.ImageColor *= this.Alpha; }
            else if (this.fadeOut > 0) { this.Alpha -= 1 / this.initialFadeOut; this.fadeOut--; this.ImageColor *= this.Alpha; }
        }

    }
}
