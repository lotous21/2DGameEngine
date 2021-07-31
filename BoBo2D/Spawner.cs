﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace BoBo2D
{
    class Spawner : Componenet
    {
        Timer timer;
        Timer totalTimer;
        int spawnTimer;
        Levels levels;

        bool activateSpawn;

        public Spawner(Levels l, List<SpawnerObject> spawnList, Texture2D image, int vel, Color color, int spawnTime)
        {
            Random rnd = new Random();

            this.Image = image;
            this.spawnTimer = spawnTime;
            this.levels = l;
            this.Transform.Velocity = new Vector2(-vel, 0);
            this.ImageColor = color;
            this.Enable();
            this.Drawable = true;
            activateSpawn = true;
            timer = new Timer();
            totalTimer = new Timer();
            timer.Elapsed += delegate
            {
                if (levels.activeScene.IsSceneActive())
                {
                    int xPos = rnd.Next(50, 650);
                    SpawnerObject s = new SpawnerObject(new Vector2(1280, xPos), image, 50, color);
                    spawnList.Add(s);
                }
            };
            totalTimer.Elapsed += delegate
            {
                //spawnList.Clear();
                activateSpawn = true;
                spawnTimer -= levels.DeceaseSpawnTime;
            };
        }
        public void Update(float elapsed)
        {
            //this.Transform.Position += this.Transform.Velocity * elapsed;
            //this.Bounds.X = (int)this.Transform.Position.X;
            //this.Bounds.Y = (int)this.Transform.Position.Y;

            if (this.spawnTimer <= 100)
            {
                this.spawnTimer = 100;
            }
            if (levels.Level == 1 && levels.activeScene.IsSceneActive() && activateSpawn)
            {
                timer.Interval = spawnTimer;
                totalTimer.Interval = levels.LevelTime;
                timer.Enabled = true;
                totalTimer.Enabled = true;
                activateSpawn = false;
            }
            if (levels.levelChanged && levels.activeScene.IsSceneActive() && activateSpawn)
            {
                timer.Interval = spawnTimer;
                totalTimer.Interval = levels.LevelTime;
                timer.Enabled = true;
                totalTimer.Enabled = true;
                activateSpawn = false;
            }
        }

    }
}
