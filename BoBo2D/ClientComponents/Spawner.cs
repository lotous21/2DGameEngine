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
        Timer timer;
        Timer totalTimer;

        Levels levels;

        int spawnTimer;
        int velocity;

        public bool ActivateSpawn;

        public bool IsFire;
        public bool IsDamage;
        public bool IsReload;
        public bool IsShield;


        public Spawner(Levels l, List<SpawnerObject> spawnList, Texture2D image, int vel, Color color, int spawnTime, bool isIncrease, bool fire, bool damage, bool reload, bool shield)
        {
            Random rnd = new Random();
            this.IsFire = fire;
            this.IsDamage = damage;
            this.IsReload = reload;
            this.IsShield = shield;
            this.Image = image;
            this.spawnTimer = spawnTime;
            this.velocity = vel;
            this.Transform.Position = new Vector2(-700, -700);
            this.levels = l;
            this.Transform.Velocity = new Vector2(-vel, 0);
            this.ImageColor = color;
            this.Enable();
            this.Drawable = true;
            ActivateSpawn = true;
            timer = new Timer();
            totalTimer = new Timer();
            timer.Elapsed += delegate
            {
                if (this.IsEnable())
                {
                    if (levels.ActiveScene.IsSceneActive())
                    {
                        int xPos = rnd.Next(50, 650);
                        SpawnerObject s = new SpawnerObject(new Vector2(1280, xPos), image, this.velocity, color, new Rectangle(0, 0, 28, 28), this.IsFire, this.IsDamage, this.IsReload, this.IsShield);
                        spawnList.Add(s);
                        s.Enable();
                    }
                }
            };
            totalTimer.Elapsed += delegate
            {
                foreach (SpawnerObject s in spawnList.ToArray())
                {
                    s.Disable();
                }
                spawnList.Clear();
                timer.Enabled = false;
                if (isIncrease)
                {
                    spawnTimer += levels.DeceaseSpawnTime;
                }
                else spawnTimer -= levels.DeceaseSpawnTime;
                this.velocity += 20;
            };
        }

        public override void Update(float elapsed)
        {
            if (this.spawnTimer <= 100)
            {
                this.spawnTimer = 100;
            }
            if (levels.Level == 1 && levels.ActiveScene.IsSceneActive() && ActivateSpawn)
            {
                timer.Interval = spawnTimer;
                totalTimer.Interval = levels.LevelTime;
                timer.Enabled = true;
                totalTimer.Enabled = true;
                ActivateSpawn = false;
            }
            else if (levels.ActiveScene.IsSceneActive() && ActivateSpawn)
            {
                if (spawnTimer <= 100)
                {
                    spawnTimer = 100;
                }
                timer.Interval = spawnTimer;
                totalTimer.Interval = levels.LevelTime;
                timer.Enabled = true;
                totalTimer.Enabled = true;
                ActivateSpawn = false;
            }
            if (!levels.ActiveScene.IsSceneActive())
            {
                timer.Enabled = false;
                totalTimer.Enabled = false;
            }
        }
    }
}
