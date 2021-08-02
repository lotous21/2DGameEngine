using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace BoBo2D
{
    class LogoIntro : Componenet
    {
        Video intro;
        VideoPlayer video = new VideoPlayer();
        Scenes scene;
        Scenes nextScenes;

        public LogoIntro (Video introVid, Scenes _scene, Scenes _nextScene)
        {
            this.intro = introVid;
            this.scene = _scene;
            this.nextScenes = _nextScene;
            video.Play(intro);
        }

        public override void Update(float elapsed)
        {
            if (video.State == MediaState.Stopped)
            {
                scene.DeactivateScene();
                nextScenes.ActivateScene();
            }

            base.Update(elapsed); 
        }

        public override void Draw(SpriteBatch sb)
        {
            Texture2D videoTexture = null;
            if (video.State != MediaState.Stopped)
            {
                videoTexture = video.GetTexture();
            }
            if (videoTexture != null)
            {
                sb.Draw(videoTexture, new Rectangle(0, 0, 1280, 720), Color.White);
            }

            base.Draw(sb);
        }
    }
}
