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
        int valueToChange;
        int maxValue;
        int costUpgrade;
        bool isIncreasing;

        Keys upgradeKey;
        KeyboardState PrevState;

        GameObjects attachedObj;
        SpriteFont upgradeTextFont;
        Text logText;
        Text upgradeText;
        Levels levels;

        public Upgrade(string name, int changeValue, bool increase, Keys input, int cost, GameObjects attached, Text _logText, SpriteFont font, Levels _level, Vector2 upgradeTextPos, int max)
        {
            this.upgradeName = name;
            this.maxValue = max;
            this.valueToChange = changeValue;
            this.isIncreasing = increase;
            this.upgradeKey = input;
            this.costUpgrade = cost;
            this.attachedObj = attached;
            this.upgradeTextFont = font;
            this.levels = _level;
            this.logText = _logText;
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
                    foreach (Spawner s in levels.SpawnersList.ToArray())
                    {
                        s.ActivateSpawn = true;
                    }
                }

                PrevState = Keyboard.GetState();
            }
            if (levels.IsLevelCompleted)
            {
                attachedObj.Enable();
            }
            else attachedObj.Disable();
        }
    }
}
