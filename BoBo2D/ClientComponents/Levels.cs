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

        public Scenes ActiveScene;
        public int Level { get; set; }
        public int LevelTime { get; set; }
        public int UpgradesToProceed { get; set; }
        public int DeceaseSpawnTime { get; set; }
        public int IncreaseLevelTime { get; set; }
        public int IncreaseUpgradeToProceed { get; set; }
        public bool IsLevelCompleted { get; set; }
        public bool levelChanged { get; set; }

        public List<Spawner> SpawnersList;

        public Levels(int _levelTime, int _decreaseSpawnTime, int _inccreaseLevelTime, int _upgradeToProceed, int _increaseUpgradeToProcceed,Scenes attachedScene, StaticBackground stBack1, StaticBackground stBack2)
        {
            Random rnd = new Random();
            this.DeceaseSpawnTime = _decreaseSpawnTime;
            this.IncreaseLevelTime = _inccreaseLevelTime;
            this.UpgradesToProceed = _upgradeToProceed;
            this.IncreaseUpgradeToProceed = _increaseUpgradeToProcceed;
            this.LevelTime = _levelTime;
            this.Level = 1;
            this.ActiveScene = attachedScene;
            levelTimer = new Timer();
            levelChanged = true;
            this.Enable();
            levelTimer.Elapsed += delegate
            {
                levelTimer.Enabled = false;
                Level++;
                levelChanged = false;
                LevelTime += IncreaseLevelTime;
                UpgradesToProceed += IncreaseUpgradeToProceed;
                IsLevelCompleted = true;
                stBack1.Transform.Velocity.X -= 20;
                stBack2.Transform.Velocity.X -= 20;
                foreach (Spawner s in SpawnersList.ToArray())
                {
                    s.ActivateSpawn = false;
                }
            };
        }

        public override void Update(float elapsed)
        {
            if (levelChanged && ActiveScene.IsSceneActive())
            {
                IsLevelCompleted = false;
                levelTimer.Enabled = true;
                levelTimer.Interval = LevelTime;
                levelChanged = false;
            }
        }
    }


}
