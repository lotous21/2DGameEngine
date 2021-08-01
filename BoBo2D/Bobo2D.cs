using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Timers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoBo2D
{
    public class Bobo2D : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public const int GAME_HEIGHT = 720;
        public const int GAME_WIDTH = 1280;

        int score;

        GameObjects splashScreenObject;
        GameObjects mainMenuObject;
        GameObjects playerObject;
        GameObjects uiObject;
        GameObjects backgroundsObject;
        GameObjects GameManagerObj;

        SplashScreen splashScreen;
        Button playButton;
        Input GameControls;

        StaticBackground staticBackground1;
        StaticBackground staticBackground2;

        SpriteFont basicFont;
        SpaceShip player;
        Weapon Missiles;
        Weapon Bor;
        Levels level1;
        Spawner meteor;
        Spawner reloads;
        Spawner shields;
        Spawner enemy;

        KeyboardState PrevState;
        List<Weapon> weapons = new List<Weapon>();
        List<Projectile> bullets = new List<Projectile>();
        List<Projectile> enemyBullet = new List<Projectile>();
        List<SpawnerObject> spawnList = new List<SpawnerObject>();

        Text LevelText;
        Text BulletCountText;
        Text playerHealthText;
        Text playerShieldText;
        Text playerScoreText;
        Text selectedWeaponText;

        Scenes OpeningScene;
        Scenes GameScene;
        Scenes FinalScene;

        List<Scenes> AllScenes;

        SoundEffect HitSound;
        SoundEffect ReloadSound;

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
            AllScenes = new List<Scenes>();
            OpeningScene = new Scenes();
            OpeningScene.ActivateScene();
            GameScene = new Scenes();
            AllScenes.Add(OpeningScene);
            AllScenes.Add(GameScene);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            basicFont = Content.Load<SpriteFont>("Font");
            splashScreen = new SplashScreen(new Vector2(0, 0), Content.Load<Texture2D>("SkyIsYours"), Color.White, 3f, 3f, 1000);
            Text playLabel = new Text(basicFont, "Play!", new Vector2(0, 0), Color.White);
            Background mainMenu = new Background(new Vector2(0, 0), new Rectangle(1280, 720, 0, 0), Content.Load<Texture2D>("Sky"), Color.White);
            playButton = new Button(new Rectangle(0, 0, 380, 160), playLabel, Content.Load<Texture2D>("Button2"), Color.White);
            playButton.Invoke();

            Projectile MissileBullet = new Projectile(new Vector2(-50, -50), Content.Load<Texture2D>("Missile"), new Vector2(500, 0), Color.White);
            MissileBullet.Disable();
            SoundEffect MissileShot = Content.Load<SoundEffect>("ShotSound");
            HitSound = Content.Load<SoundEffect>("hit");
            ReloadSound = Content.Load<SoundEffect>("reload");

            Missiles = new Weapon("Missiles", MissileBullet, Keys.D1, MissileShot);
            Bor = new Weapon("Bor", MissileBullet, Keys.D2, MissileShot);
            Missiles.Disable();
            Bor.Disable();
            Missiles.IsSelected = true;
            Bor.IsSelected = false;
            weapons.Add(Missiles);
            weapons.Add(Bor);
            bullets.Add(MissileBullet);

            selectedWeaponText = new Text(basicFont, "Selected Weapon: " + GetWeaponSelected().WeaponName, new Vector2(10, 10), Color.Green);
            Text WeaponText1 = new Text(basicFont, Missiles.WeaponName + " / Input: " + Missiles.KeyboardInput, new Vector2(1140, 10), Color.Orange);
            Text WeaponText2 = new Text(basicFont, Bor.WeaponName + " / Input: " + Bor.KeyboardInput, new Vector2(1140, 30), Color.Orange);

            staticBackground1 = new StaticBackground(new Vector2(0, 0), new Vector2(-50, 0), Content.Load<Texture2D>("City2d"), Color.White);
            staticBackground2 = new StaticBackground(new Vector2(1281, 0), new Vector2(-50, 0), Content.Load<Texture2D>("City2dRef"), Color.White);

            level1 = new Levels(10000, 100, 500, 1, 1, GameScene);

            meteor = new Spawner(level1, spawnList, Content.Load<Texture2D>("Meteor"), 50, Color.White, 2000, false, true, false, false);
            reloads = new Spawner(level1, spawnList, Content.Load<Texture2D>("Poweup"), 50, Color.White, 7150, false, false, true, false);
            shields = new Spawner(level1, spawnList, Content.Load<Texture2D>("Shield"), 50, Color.White, 11750, false, false, false, true);
            enemy = new Spawner(level1, spawnList, Content.Load<Texture2D>("Plane3"), 50, Color.White, 8660, true, true, false, false);

            player = new SpaceShip(new Vector2(200, 400), Content.Load<Texture2D>("Plane2"), Color.White, level1);

            BulletCountText = new Text(basicFont, "Bullets: " + player.BulletCount, new Vector2(10, 30), Color.White);
            playerHealthText = new Text(basicFont, "HP: " + player.HP, new Vector2(10, 600), Color.Red);
            playerShieldText = new Text(basicFont, "Shield: " + player.Shield, new Vector2(10, 620), Color.Red);
            playerScoreText = new Text(basicFont, "Score: " + score, new Vector2(10, 640), Color.White);
            LevelText = new Text(basicFont, "Level: " + level1.Level.ToString(), new Vector2(600, 10), Color.Red);

            GameControls = new Input(Keys.Up, Keys.Down, Keys.Right, Keys.Left, Keys.Space, player);

            splashScreenObject = new GameObjects();
            splashScreenObject.AddComponenet(splashScreen);

            mainMenuObject = new GameObjects();
            mainMenuObject.AddComponenet(mainMenu);
            mainMenuObject.AddComponenet(playButton);
            mainMenuObject.AddComponenet(playLabel);

            playerObject = new GameObjects();
            playerObject.AddComponenet(player);
            playerObject.AddComponenet(Missiles);
            playerObject.AddComponenet(MissileBullet);

            uiObject = new GameObjects();
            uiObject.AddComponenet(selectedWeaponText);
            uiObject.AddComponenet(LevelText);
            uiObject.AddComponenet(WeaponText1);
            uiObject.AddComponenet(WeaponText2);
            uiObject.AddComponenet(BulletCountText);
            uiObject.AddComponenet(playerHealthText);
            uiObject.AddComponenet(playerShieldText);
            uiObject.AddComponenet(playerScoreText);

            backgroundsObject = new GameObjects();
            backgroundsObject.AddComponenet(staticBackground1);
            backgroundsObject.AddComponenet(staticBackground2);

            GameManagerObj = new GameObjects();

            OpeningScene.AddGameObject(mainMenuObject);
            OpeningScene.AddGameObject(splashScreenObject);
            GameScene.AddGameObject(GameManagerObj);
            GameScene.AddGameObject(backgroundsObject);
            GameScene.AddGameObject(playerObject);
            GameScene.AddGameObject(uiObject);
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (!OpeningScene.IsSceneActive())
            {
                GameScene.ActivateScene();
            }

            foreach (SpawnerObject s in spawnList.ToArray())
            {
                s.Update(elapsed);

                if (s.IsFire)
                {
                    s.BulletImage = Content.Load<Texture2D>("Missile");
                    s.projectiles = enemyBullet;
                }
            }
            foreach (Projectile p in bullets.ToArray())
            {
                p.Update(elapsed);
            }

            meteor.Update(elapsed);
            reloads.Update(elapsed);
            shields.Update(elapsed);
            enemy.Update(elapsed);

            staticBackground1.Update(elapsed);
            staticBackground2.Update(elapsed);

            player.Update(elapsed);

            if (score < 0)
            {
                score = 0;
            }

            KeyHandler();
            UpdatedEntitied(elapsed);
            CheckCollisions();

            base.Update(gameTime);
        }
        void UpdatedEntitied(float elapsed)
        {
            splashScreen.Update();

            player.Transform.Velocity = new Vector2(0, 0);

            playButton.Update();

            foreach (SpawnerObject s in spawnList.ToArray())
            {
                s.Update(elapsed);
            }
            foreach (Projectile p in bullets.ToArray())
            {
                p.Update(elapsed);
            }
            foreach (Projectile h in enemyBullet.ToArray())
            {
                h.Update(elapsed);
            }

            BlankGame();

            if (playButton.IsClick())
            {
                OpeningScene.DeactivateScene();
            }

            selectedWeaponText.label = "Selected Weapon: " + GetWeaponSelected().WeaponName;
            selectedWeaponText.Update();

            LevelText.label = "Level: " + level1.Level.ToString();
            LevelText.Update();

            BulletCountText.label = "Bullets: " + player.BulletCount;
            BulletCountText.Update();

            playerHealthText.label = "HP: " + player.HP;
            playerHealthText.Update();

            playerShieldText.label = "Shield: " + player.Shield;
            playerShieldText.Update();

            playerScoreText.label = "Score: " + score;
            playerScoreText.Update();

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

            level1.Update();
            GameControls.Update();
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            foreach(var scenes in AllScenes.ToArray())
            {
                scenes.DrawAllObjects(_spriteBatch);
            }
            foreach(var bullet in bullets.ToArray())
            {
                bullet.Draw(_spriteBatch);
            }
            foreach(var spawn in spawnList.ToArray())
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
        void KeyHandler()
        {
            if (Keyboard.GetState().IsKeyDown(Missiles.KeyboardInput) && PrevState.IsKeyUp(Missiles.KeyboardInput))
            {
                foreach (Weapon w in weapons)
                {
                    w.IsSelected = false;
                }

                Missiles.IsSelected = true;
            }
            if (Keyboard.GetState().IsKeyDown(Bor.KeyboardInput) && PrevState.IsKeyUp(Bor.KeyboardInput))
            {
                foreach (Weapon w in weapons)
                {
                    w.IsSelected = false;
                }

                Bor.IsSelected = true;
            }
            if (Keyboard.GetState().IsKeyDown(GameControls.FireKey) && PrevState.IsKeyUp(GameControls.FireKey) && player.BulletCount > 0)
            {
                Projectile p = new Projectile(GetWeaponSelected().Bullet.Transform.Position, GetWeaponSelected().Bullet.Image, GetWeaponSelected().Bullet.Transform.Velocity, Color.White);
                bullets.Add(p);
                p.Enable();
                p.Transform.Position = new Vector2(player.Transform.Position.X, player.Transform.Position.Y + 15);
                GetWeaponSelected().ShotSound.Play();
                player.BulletCount--;
            }

            PrevState = Keyboard.GetState();
        }
        Weapon GetWeaponSelected()
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
        void BlankGame()
        {
            if (AllScenes.Count == 0)
            {
                Text t = new Text(basicFont, "Please Add Scene to the game", new Vector2(510, 300), Color.Red);
                GameObjects g = new GameObjects();
                Scenes s = new Scenes();
                s.ActivateScene();
                AllScenes.Add(s);
                s.AddGameObject(g);
                g.AddComponenet(t);
            }
        }
        void CheckCollisions()
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
                        HitSound.Play();
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
                        ReloadSound.Play();
                        player.BulletCount += 5;
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
                    HitSound.Play();
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

            if (player.HP <= 0)
            {
                AllScenes.Clear();
                bullets.Clear();
                spawnList.Clear();
                enemyBullet.Clear();
                player.Disable(); 
                Text t = new Text(basicFont, "Game Over", new Vector2(550, 300), Color.Red);
                Text t2 = new Text(basicFont, "Score: " + score , new Vector2(550, 320), Color.White);
                GameObjects g = new GameObjects();
                Scenes s = new Scenes();
                s.ActivateScene();
                AllScenes.Add(s);
                s.AddGameObject(g);
                g.AddComponenet(t);
                g.AddComponenet(t2);
            }
        }
    }
}
