using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Timers;
using System;
using System.Collections.Generic;
using System.Text;
using BoBo2D.ClientComponents;

namespace BoBo2D
{
    public class Bobo2D : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public const int GAME_HEIGHT = 720;
        public const int GAME_WIDTH = 1280;

        int score;
        bool playBackground;

        string upgradeLog;

        GameObjects introObject;
        GameObjects splashScreenObject;
        GameObjects mainMenuObject;
        GameObjects playerObject;
        GameObjects uiObject;
        GameObjects backgroundsObject;
        GameObjects gameManagerObj;
        GameObjects upgradeObj;
        GameObjects spawnerObj;
        GameObjects weaponsObj;

        SplashScreen splashScreen;
        Button playButton;
        Input gameControls;

        StaticBackground staticBackground1;
        StaticBackground staticBackground2;

        SpriteFont basicFont;
        SpaceShip player;
        Weapon missiles;
        Weapon bor;
        Levels levels;
        Spawner meteor;
        Spawner reloads;
        Spawner shields;
        Spawner enemy;

        List<Weapon> weapons = new List<Weapon>();
        List<Projectile> bullets = new List<Projectile>();
        List<Projectile> enemyBullet = new List<Projectile>();
        List<SpawnerObject> spawnList = new List<SpawnerObject>();
        List<Spawner> spawners = new List<Spawner>();

        Text levelText;
        Text bulletCountText;
        Text playerHealthText;
        Text playerShieldText;
        Text playerScoreText;
        Text selectedWeaponText;

        Text logUpgradeText;

        Upgrade speedUpgrade;
        Upgrade hpRegenUpgrade;
        Upgrade maxBulletUpgarde;

        Scenes logoScene;
        Scenes openingScene;
        Scenes gameScene;

        List<Scenes> allScenes;

        SoundEffect hitSound;
        SoundEffect reloadSound;

        BackgroundMusic backgroundMusic;

        Video intro;
        LogoIntro logoIntro;
        

        public Bobo2D()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = GAME_WIDTH;
            _graphics.PreferredBackBufferHeight = GAME_HEIGHT;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            allScenes = new List<Scenes>();
            logoScene = new Scenes();
            logoScene.ActivateScene();
            openingScene = new Scenes();
            gameScene = new Scenes();
            allScenes.Add(logoScene);
            allScenes.Add(openingScene);
            allScenes.Add(gameScene);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //PAY ATTENTION
            //All times are in milleseconds, 1 seonds = 1000 millesconds;

            #region Load\Declared Content

            basicFont = Content.Load<SpriteFont>("Font");
            intro = Content.Load<Video>("intro2");

            logoIntro = new LogoIntro(intro, logoScene, openingScene);
            splashScreen = new SplashScreen(new Vector2(0, 0), Content.Load<Texture2D>("SkyIsYours2"), Color.White, 3f, 3f, 6000);

            backgroundMusic = new BackgroundMusic(Content.Load<Song>("Master"), gameScene);

            Text playLabel = new Text(basicFont, "Play!", new Vector2(0, 0), Color.White);
            Background mainMenu = new Background(new Vector2(0, 0), new Rectangle(1280, 720, 0, 0), Content.Load<Texture2D>("Sky"), Color.White);

            playButton = new Button(new Rectangle(0, 0, 380, 160), playLabel, Content.Load<Texture2D>("Button2"), Color.White);
            playButton.Invoke();

            Projectile missileBullet = new Projectile(new Vector2(-50, -50), Content.Load<Texture2D>("Missile"), new Vector2(500, 0), Color.White);
            missileBullet.Disable();

            SoundEffect missileShot = Content.Load<SoundEffect>("ShotSound");

            hitSound = Content.Load<SoundEffect>("hit");
            reloadSound = Content.Load<SoundEffect>("reload");

            missiles = new Weapon("Missiles", missileBullet, Keys.D1, missileShot, weapons);
            bor = new Weapon("Bor", missileBullet, Keys.D2, missileShot, weapons);

            missiles.Disable();
            bor.Disable();

            missiles.IsSelected = true;
            bor.IsSelected = false;

            weapons.Add(missiles);
            weapons.Add(bor);

            bullets.Add(missileBullet);

            selectedWeaponText = new Text(basicFont, "Selected Weapon: " + getWeaponSelected().WeaponName, new Vector2(10, 10), Color.Green);

            staticBackground1 = new StaticBackground(new Vector2(0, 0), new Vector2(-50, 0), Content.Load<Texture2D>("City2d"), Color.White);
            staticBackground2 = new StaticBackground(new Vector2(1281, 0), new Vector2(-50, 0), Content.Load<Texture2D>("City2dRef"), Color.White);

            levels = new Levels(20000, 300, 5000, 1, 1, gameScene, staticBackground1, staticBackground2);

            meteor = new Spawner(levels, spawnList, Content.Load<Texture2D>("Meteor"), 50, Color.White, 2000, false, false, true, false, false);
            reloads = new Spawner(levels, spawnList, Content.Load<Texture2D>("Poweup"), 50, Color.White, 7100, true, false, false, true, false);
            shields = new Spawner(levels, spawnList, Content.Load<Texture2D>("Shield"), 50, Color.White, 11700, true, false, false, false, true);
            enemy = new Spawner(levels, spawnList, Content.Load<Texture2D>("Plane3"), 50, Color.White, 8200, false, true, true, false, false);

            spawners.Add(meteor);
            spawners.Add(reloads);
            spawners.Add(shields);
            spawners.Add(enemy);

            levels.SpawnersList = spawners;

            player = new SpaceShip(new Vector2(200, 400), 200, Content.Load<Texture2D>("Plane2"), Color.White, levels);

            bulletCountText = new Text(basicFont, "Bullets: " + player.BulletCount, new Vector2(10, 30), Color.White);
            playerHealthText = new Text(basicFont, "HP: " + player.HP, new Vector2(10, 600), Color.Red);
            playerShieldText = new Text(basicFont, "Shield: " + player.Shield, new Vector2(10, 620), Color.Red);
            playerScoreText = new Text(basicFont, "Score: " + score, new Vector2(10, 640), Color.White);
            levelText = new Text(basicFont, "Level: " + levels.Level.ToString(), new Vector2(600, 10), Color.Red);

            Text confirmText = new Text(basicFont, "Press 'Enter' to Continue", new Vector2(300, 360), Color.White);
            logUpgradeText = new Text(basicFont, "Log: " + upgradeLog, new Vector2(300, 420), Color.Orange);

            gameControls = new Input(Keys.Space, player, weapons, bullets);
            gameControls.Arrows();

            playBackground = true;

            #endregion

            #region GameObjects Modify

            introObject = new GameObjects();
            introObject.AddComponenet(logoIntro);

            splashScreenObject = new GameObjects();
            splashScreenObject.AddComponenet(splashScreen);

            mainMenuObject = new GameObjects();
            mainMenuObject.AddComponenet(mainMenu);
            mainMenuObject.AddComponenet(playButton);
            mainMenuObject.AddComponenet(playLabel);

            playerObject = new GameObjects();
            playerObject.AddComponenet(player);
            playerObject.AddComponenet(missiles);
            playerObject.AddComponenet(missileBullet);

            uiObject = new GameObjects();
            uiObject.AddComponenet(selectedWeaponText);
            uiObject.AddComponenet(levelText);
            uiObject.AddComponenet(bulletCountText);
            uiObject.AddComponenet(playerHealthText);
            uiObject.AddComponenet(playerShieldText);
            uiObject.AddComponenet(playerScoreText);

            backgroundsObject = new GameObjects();
            backgroundsObject.AddComponenet(staticBackground1);
            backgroundsObject.AddComponenet(staticBackground2);

            gameManagerObj = new GameObjects();
            gameManagerObj.AddComponenet(gameControls);
            gameManagerObj.AddComponenet(backgroundMusic);

            weaponsObj = new GameObjects();
            weaponsObj.AddComponenet(missiles);
            weaponsObj.AddComponenet(bor);

            upgradeObj = new GameObjects();

            speedUpgrade = new Upgrade("Moving Speed", 100, true, Keys.Z, 100, upgradeObj, logUpgradeText, basicFont, levels, new Vector2 (300,300), 1000);
            hpRegenUpgrade = new Upgrade("HP Regeneration", 200, false, Keys.X, 150, upgradeObj, logUpgradeText, basicFont, levels, new Vector2(300, 320), 100);
            maxBulletUpgarde = new Upgrade("Max Bullets", 1, true, Keys.C, 200, upgradeObj, logUpgradeText, basicFont, levels, new Vector2(300, 340), 15);

            upgradeObj.AddComponenet(logUpgradeText);
            upgradeObj.AddComponenet(confirmText);
            upgradeObj.AddComponenet(speedUpgrade);
            upgradeObj.AddComponenet(hpRegenUpgrade);
            upgradeObj.AddComponenet(maxBulletUpgarde);

            spawnerObj = new GameObjects();
            spawnerObj.AddComponenet(meteor);
            spawnerObj.AddComponenet(reloads);
            spawnerObj.AddComponenet(shields);
            spawnerObj.AddComponenet(enemy);
            spawnerObj.AddComponenet(levels);

            #endregion

            #region Scenes Modify

            logoScene.AddGameObject(introObject);

            openingScene.AddGameObject(mainMenuObject);
            openingScene.AddGameObject(splashScreenObject);

            gameScene.AddGameObject(gameManagerObj);
            gameScene.AddGameObject(backgroundsObject);
            gameScene.AddGameObject(playerObject);
            gameScene.AddGameObject(uiObject);
            gameScene.AddGameObject(spawnerObj);
            gameScene.AddGameObject(upgradeObj);
            gameScene.AddGameObject(weaponsObj);

            #endregion
        }

        protected override void Update (GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (playButton.IsClick())
            {
                openingScene.DeactivateScene();
                gameScene.ActivateScene();
            }

            foreach (Projectile p in bullets.ToArray())
            {
                p.Update(elapsed);
            }
            foreach (Projectile h in enemyBullet.ToArray())
            {
                h.Update(elapsed);
            }
            foreach (Scenes s in allScenes.ToArray())
            {
                s.UpdateAllObjects(elapsed);
            }
            foreach (SpawnerObject s in spawnList.ToArray())
            {
                s.Update(elapsed);

                if (s.IsFire)
                {
                    s.BulletImage = Content.Load<Texture2D>("Missile");
                    s.ProjectilesList = enemyBullet;
                }
            }

            speedUpgrade.FixedUpdate(ref player.MovingSpeed, ref score, ref upgradeLog);
            hpRegenUpgrade.FixedUpdate(ref player.HpRegenSpeed, ref score, ref upgradeLog);
            maxBulletUpgarde.FixedUpdate(ref player.MaxBullet, ref score, ref upgradeLog);

            if (score < 0)
            {
                score = 0;
            }

            player.Transform.Velocity = new Vector2(0, 0);

            foreach (SpawnerObject s in spawnList.ToArray())
            {
                s.Update(elapsed);

                if (s.IsFire)
                {
                    s.BulletImage = Content.Load<Texture2D>("Missile");
                    s.ProjectilesList = enemyBullet;
                }
            }

            checkCollisions();
            blankGame();

            selectedWeaponText.Label = "Selected Weapon: " + getWeaponSelected().WeaponName;
            levelText.Label = "Level: " + levels.Level;
            bulletCountText.Label = "Bullets: " + player.BulletCount;
            playerHealthText.Label = "HP: " + player.HP;
            playerShieldText.Label = "Shield: " + player.Shield;
            playerScoreText.Label = "Score: " + score;
            logUpgradeText.Label = "Log: " + upgradeLog;

            if (player.HP >= 0 && player.HP <= 30)
            {
                playerHealthText.ImageColor = Color.Red;
            }
            if (player.HP >= 31 && player.HP <= 70)
            {
                playerHealthText.ImageColor = Color.Orange;
            }
            if (player.HP >= 71 && player.HP <= 100)
            {
                playerHealthText.ImageColor = Color.Green;
            }

            if (player.Shield >= 0 && player.Shield <= 30)
            {
                playerShieldText.ImageColor = Color.Red;
            }
            if (player.Shield >= 31 && player.Shield <= 70)
            {
                playerShieldText.ImageColor = Color.Orange;
            }
            if (player.Shield >= 71 && player.Shield <= 100)
            {
                playerShieldText.ImageColor = Color.Green;
            }

            base.Update(gameTime);
        }

        protected override void Draw (GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            foreach(var scenes in allScenes.ToArray())
            {
                scenes.DrawAllObjects(_spriteBatch);
            }
            foreach(var bullet in bullets.ToArray())
            {
                bullet.Draw(_spriteBatch);
            }
            foreach (var spawn in spawnList.ToArray())
            {
                spawn.Draw(_spriteBatch);
            }
            foreach (var eBullet in enemyBullet.ToArray())
            {
                eBullet.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        Weapon getWeaponSelected()
        {
            foreach (Weapon w in weapons)
            {
                if (w.IsSelected)
                {
                    return w;
                }
            }

            return null;
        }
        void blankGame()
        {
            if (allScenes.Count == 0)
            {
                Text t = new Text(basicFont, "Please Add Scene to the game", new Vector2(510, 300), Color.Red);
                GameObjects g = new GameObjects();
                Scenes s = new Scenes();
                s.ActivateScene();
                allScenes.Add(s);
                s.AddGameObject(g);
                g.AddComponenet(t);
            }
        }
        void checkCollisions()
        {
            if (!spawners[0].ActivateSpawn)
            {
                foreach (var b in bullets.ToArray())
                {
                    foreach (var s in spawnList.ToArray())
                    {
                        if (b.Bounds.Intersects(s.Bounds))
                        {
                            if (s.IsFire)
                            {
                                score += 50;
                                bullets.Remove(b);
                                spawnList.Remove(s);
                                s.Disable();
                                b.Disable();
                            }
                            if (s.IsDamage)
                            {
                                score += 10;
                                bullets.Remove(b);
                                spawnList.Remove(s);
                                s.Disable();
                                b.Disable();
                            }
                            if (s.IsReload)
                            {
                                score -= 20;
                                bullets.Remove(b);
                                spawnList.Remove(s);
                                s.Disable();
                                b.Disable();
                            }
                            if (s.IsShield)
                            {
                                score -= 20;
                                bullets.Remove(b);
                                spawnList.Remove(s);
                                s.Disable();
                                b.Disable();
                            }
                        }
                    }
                }

                foreach (var s in spawnList.ToArray())
                {
                    if (player.Bounds.Intersects(s.Bounds))
                    {
                        if (s.IsDamage)
                        {
                            hitSound.Play();
                            if (player.Shield <= 0)
                            {
                                player.HP -= 20;
                            }
                            else if (player.Shield > 0)
                            {
                                player.Shield -= 20;
                            }
                            else
                            {
                                player.HP -= 20;
                            }
                            spawnList.Remove(s);
                            s.Disable();
                        }
                        if (s.IsReload)
                        {
                            reloadSound.Play();
                            player.BulletCount = player.MaxBullet;
                            spawnList.Remove(s);

                        }
                        if (s.IsShield)
                        {
                            player.ShieldDestroyed = false;
                            player.Shield = 100;
                            spawnList.Remove(s);
                        }
                    }
                }

                foreach (var h in enemyBullet.ToArray())
                {
                    if (player.Bounds.Intersects(h.Bounds))
                    {
                        hitSound.Play();
                        if (player.Shield <= 0)
                        {
                            player.HP -= 50;
                        }
                        else if (player.Shield > 0)
                        {
                            player.Shield -= 50;
                        }
                        else
                        {
                            player.HP -= 50;
                        }
                        enemyBullet.Remove(h);
                    }
                }

                foreach (var h in enemyBullet.ToArray())
                {
                    foreach (var c in bullets.ToArray())
                    {
                        if (h.Bounds.Intersects(c.Bounds))
                        {
                            bullets.Remove(c);
                            c.Disable();
                            enemyBullet.Remove(h);
                            h.Disable();
                        }
                    }
                }
            }

            if (player.HP <= 0)
            {
                allScenes.Clear();
                bullets.Clear();
                spawnList.Clear();
                enemyBullet.Clear();
                player.Disable(); 
                foreach (Spawner h in spawners.ToArray())
                {
                    h.ActivateSpawn = false;
                }
                Text t = new Text(basicFont, "Game Over", new Vector2(550, 300), Color.Red);
                Text t2 = new Text(basicFont, "Score: " + score, new Vector2(550, 320), Color.White);
                GameObjects g = new GameObjects();
                Scenes s = new Scenes();
                s.ActivateScene();
                allScenes.Add(s);
                s.AddGameObject(g);
                g.AddComponenet(t);
                g.AddComponenet(t2);
            }
        }
    }
}
