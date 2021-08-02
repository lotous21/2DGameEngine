using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoBo2D
{
    class Upgrade: Componenet
    {
        string upgradeName;
        private int valueToChange;
        private int maxValue;
        private bool isIncreasing;
        private Keys upgradeKey;
        private int costUpgrade;
        private GameObjects attachedObj;
        private Text logText;
        private SpriteFont upgradeTextFont;
        KeyboardState PrevState;
        Text upgradeText;
        Levels levels;
        List<Spawner> spawners;

        public Upgrade(string name, int changeValue, bool increase, Keys input, int cost, GameObjects attached, Text text, SpriteFont font, Levels _level, Vector2 upgradeTextPos, int max, List<Spawner> _spawners)
        {
            this.spawners = _spawners;
            this.upgradeName = name;
            this.maxValue = max;
            this.valueToChange = changeValue;
            this.isIncreasing = increase;
            this.upgradeKey = input;
            this.costUpgrade = cost;
            this.attachedObj = attached;
            this.logText = text;
            this.upgradeTextFont = font;
            this.levels = _level;
            upgradeText = new Text(upgradeTextFont, "Press " + upgradeKey + " to Upgrade Your " + upgradeName + " || Cost = " + costUpgrade + " || Curren value: " + valueToChange, upgradeTextPos, Color.PapayaWhip);
            attachedObj.AddComponenet(upgradeText);
        }

        public override void Update(float elapsed)
        {
            upgradeText.Update(elapsed);
            base.Update(elapsed);
        }

        public void FixedUpdate(ref int value, ref int score, ref string upgradeLog)
        {
            upgradeText.Label = "Press " + upgradeKey + " to Upgrade Your " + upgradeName + " || Cost = " + costUpgrade + " || Curren value: " + value;
            if (attachedObj.IsEnable())
            {
                if (Keyboard.GetState().IsKeyDown(upgradeKey) && PrevState.IsKeyUp(upgradeKey))
                {
                    if (isIncreasing)
                    {
                        if (score < costUpgrade)
                        {
                            upgradeLog = "Not Enough Coins !";
                            logText.ImageColor = Color.Red;
                        }
                        else if (value >= maxValue)
                        {
                            upgradeLog = "Value at the highest level !";
                            logText.ImageColor = Color.Red;
                        }
                        else
                        {
                            upgradeLog = "Moving Speed Upgraded Succesfuly !";
                            logText.ImageColor = Color.Green;
                            value += valueToChange;
                            score -= costUpgrade;
                            costUpgrade = (int)(costUpgrade * 1.1f);
                        }
                    }
                    else
                    {
                        if (score < costUpgrade)
                        {
                            upgradeLog = "Not Enough Coins !";
                            logText.ImageColor = Color.Red;
                        }
                        else if (value <= maxValue)
                        {
                            upgradeLog = "Value at the highest level !";
                            logText.ImageColor = Color.Red;
                        }
                        else
                        {
                            upgradeLog = "Moving Speed Upgraded Succesfuly !";
                            logText.ImageColor = Color.Green;
                            value -= valueToChange;
                            score -= costUpgrade;
                            costUpgrade = (int)(costUpgrade * 1.1f);
                        }
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && PrevState.IsKeyUp(Keys.Enter))
                {
                    upgradeLog = "Upgrade your arttibutes";
                    attachedObj.Disable();
                    levels.levelChanged = true;
                    foreach (Spawner s in spawners)
                    {
                        s.ActivateSpawn = true;
                    }
                }

                PrevState = Keyboard.GetState();
            }
        }
    }
}
