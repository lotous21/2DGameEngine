using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace BoBo2D
{
    class Spawner : Componenet
    {
        public int TimerTime { get; set; }

        public Spawner(int time, int leveltime, List<SpawnerObject> spawnList, Texture2D spawnImage, int vel, Color color, Scenes attachedScene)
        {
            Random rnd = new Random();
            this.TimerTime = time;
            Timer timer = new Timer(TimerTime);
            timer.Elapsed += delegate
            { 
                if (attachedScene.IsSceneActive())
                {
                    int xPos = rnd.Next(100, 700);
                    SpawnerObject spawn = new SpawnerObject(new Vector2(1280, xPos), spawnImage, vel, color);
                    spawnList.Add(spawn);
                    spawn.Enable();
                    spawn.Drawable = true;
                }
            };
            timer.Enabled = true;
            Timer lifetimelvel = new Timer(leveltime);
            lifetimelvel.Elapsed += delegate { timer.Enabled = false; lifetimelvel.Enabled = false; this.Disable(); };
            lifetimelvel.Enabled = true;
            this.Enable();
        }


    }
}
