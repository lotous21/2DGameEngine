using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BoBo2D
{
    class Levels: Componenet
    {
        Timer spawnTimer;
        Timer levelTimer;
        Scenes activeScene;
        List<SpawnerObject> spawnerObjects;
        Texture2D objImage;
        int objVel;
        Color objColor;

        public int Level { get; set; }
        int spawnerTime;
        int levelTime;
        int upgradesToProceed;
        int deceaseSpawnTime;
        int decreaseLevelTime;
        int increaseUpgradeToProceed;

        bool levelChanged;

        public Levels(int _spawnTime, int _levelTime, int _decreaseSpawnTime, int _decreaseLevelTime, int _upgradeToProceed, int _increaseUpgradeToProcceed, List<SpawnerObject> spawnList, Texture2D spawnImage, int vel, Color color, Scenes attachedScene)
        {
            Random rnd = new Random();
            this.deceaseSpawnTime = _decreaseSpawnTime;
            this.decreaseLevelTime = _decreaseLevelTime;
            this.upgradesToProceed = _upgradeToProceed;
            this.increaseUpgradeToProceed = _increaseUpgradeToProcceed;
            this.levelTime = _levelTime;
            this.Level = 1;
            this.activeScene = attachedScene;
            this.spawnerTime = _spawnTime;
            this.spawnerObjects = spawnList;
            this.objImage = spawnImage;
            this.objVel = vel;
            this.objColor = color;
            levelTimer = new Timer();
            levelChanged = true;
            this.Enable();
            spawnTimer = new Timer();
            spawnTimer.Elapsed += delegate
            {
                if (activeScene.IsSceneActive())
                {
                    int xPos = rnd.Next(100, 700);
                    SpawnerObject spawn = new SpawnerObject(new Vector2(1280, xPos), objImage, objVel, objColor);
                    this.spawnerObjects.Add(spawn);
                    spawn.Enable();
                    spawn.Drawable = true;
                }
            };
            levelTimer.Elapsed += delegate
            {
                spawnTimer.Enabled = false;
                levelTimer.Enabled = false;
                this.spawnerObjects.Clear();
                Level++;
                levelChanged = true;
                spawnerTime -= deceaseSpawnTime;
                levelTime -= decreaseLevelTime;
                upgradesToProceed += increaseUpgradeToProceed;
                levelChanged = true;
            };
        }

        public void Update()
        {
            if (levelChanged)
            {
                spawnTimer.Enabled = true;
                levelTimer.Enabled = true;
                spawnTimer.Interval = spawnerTime;
                levelTimer.Interval = levelTime;
                levelChanged = false;
            }
        }
    }


}
