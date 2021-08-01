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
        Timer levelTimer;
        public Scenes activeScene;

        public int Level { get; set; }
        public int LevelTime { get; set; }
        public int UpgradesToProceed;
        public int DeceaseSpawnTime;
        public int IncreaseLevelTime;
        public int IncreaseUpgradeToProceed;

        public bool levelChanged { get; set; }

        public Levels(int _levelTime, int _decreaseSpawnTime, int _inccreaseLevelTime, int _upgradeToProceed, int _increaseUpgradeToProcceed,Scenes attachedScene)
        {
            Random rnd = new Random();
            this.DeceaseSpawnTime = _decreaseSpawnTime;
            this.IncreaseLevelTime = _inccreaseLevelTime;
            this.UpgradesToProceed = _upgradeToProceed;
            this.IncreaseUpgradeToProceed = _increaseUpgradeToProcceed;
            this.LevelTime = _levelTime;
            this.Level = 1;
            this.activeScene = attachedScene;
            levelTimer = new Timer();
            levelChanged = true;
            this.Enable();
            levelTimer.Elapsed += delegate
            {
                levelTimer.Enabled = false;
                Level++;
                levelChanged = true;
                LevelTime += IncreaseLevelTime;
                UpgradesToProceed += IncreaseUpgradeToProceed;
            };
        }

        public void Update()
        {
            if (levelChanged && activeScene.IsSceneActive())
            {
                levelTimer.Enabled = true;
                levelTimer.Interval = LevelTime;
                levelChanged = false;
            }
        }
    }


}
