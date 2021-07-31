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
        public int DecreaseLevelTime;
        public int IncreaseUpgradeToProceed;

        public bool levelChanged { get; set; }

        public Levels(int _levelTime, int _decreaseSpawnTime, int _decreaseLevelTime, int _upgradeToProceed, int _increaseUpgradeToProcceed,Scenes attachedScene)
        {
            Random rnd = new Random();
            this.DeceaseSpawnTime = _decreaseSpawnTime;
            this.DecreaseLevelTime = _decreaseLevelTime;
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
                LevelTime -= DecreaseLevelTime;
                UpgradesToProceed += IncreaseUpgradeToProceed;
                levelChanged = true;
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
