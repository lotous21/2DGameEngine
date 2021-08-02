using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoBo2D.ClientComponents
{
    class BackgroundMusic: Componenet
    {
        Song music;
        Scenes scene;

        bool playMusic;

        public BackgroundMusic (Song _music, Scenes _scene)
        {
            this.music = _music;
            this.scene = _scene;
            playMusic = true;
        }

        public override void Update(float elapsed)
        {
            if (scene.IsSceneActive() && playMusic)
            {
                MediaPlayer.Play(music);
                playMusic = false;
            }
            base.Update(elapsed);
        }
    }
}
