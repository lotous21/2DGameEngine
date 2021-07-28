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

        GameObjects splashScreenObject;
        GameObjects mainMenuObject;
        GameObjects playerObject;
        GameObjects uiObject;
        GameObjects backgroundsObject;

        SplashScreen splashScreen;
        Button playButton;

        StaticBackground staticBackground1;
        StaticBackground staticBackground2;

        SpriteFont basicFont;
        SpaceShip player;
        Weapon Missiles;

        KeyboardState PrevState;
        List<Weapon> weapons = new List<Weapon>();
        List<Projectile> bullets = new List<Projectile>();


        Scenes OpeningScene;
        Scenes GameScene;

        List<Scenes> AllScenes;

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
            splashScreen = new SplashScreen(new Vector2(0, 0), Content.Load<Texture2D>("SkyIsYours"), Color.White, 3f, 3f, 6000);
            Text playLabel = new Text(basicFont, "Play!", new Vector2(0, 0), Color.White);
            Background mainMenu = new Background(new Vector2(0, 0), new Rectangle(1280, 720, 0, 0), Content.Load<Texture2D>("Sky"), Color.White);
            player = new SpaceShip(new Vector2(200, 400), Content.Load<Texture2D>("Plane2"), Color.White);
            playButton = new Button(new Rectangle(0, 0, 380, 160), playLabel, Content.Load<Texture2D>("Button2"), Color.White);
            playButton.Invoke();

            Projectile MissileBullet = new Projectile(new Vector2(-50, -50), Content.Load<Texture2D>("Missile"), new Vector2(500, 0), Color.White);
            MissileBullet.Disable();
            SoundEffect MissileShot = Content.Load<SoundEffect>("ShotSound");
            Missiles = new Weapon("Missiles", MissileBullet, Keys.D1, MissileShot);
            Missiles.Disable();
            Missiles.IsSelected = true;
            weapons.Add(Missiles);
            bullets.Add(MissileBullet);
            Text SelectedWeaponText = new Text(basicFont, "Selected Weapon: " + GetWeaponSelected().WeaponName, new Vector2(10, 10), Color.Green);
            Text LevelText = new Text(basicFont, "Level: ", new Vector2(600, 10), Color.Red);
            Text WeaponText1 = new Text(basicFont, Missiles.WeaponName + " / Input: "+ Missiles.KeyboardInput, new Vector2(1140, 10), Color.Orange);
            Text WeaponText2 = new Text(basicFont, Missiles.WeaponName + " / Input: " + Missiles.KeyboardInput, new Vector2(1140, 30), Color.Red);
            staticBackground1 = new StaticBackground(new Vector2(0, 0), new Vector2(-50, 0), Content.Load<Texture2D>("City2d"), Color.White);
            staticBackground2 = new StaticBackground(new Vector2(1281, 0), new Vector2(-50, 0), Content.Load<Texture2D>("City2dRef"), Color.White);

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
            uiObject.AddComponenet(SelectedWeaponText);
            uiObject.AddComponenet(LevelText);
            uiObject.AddComponenet(WeaponText1);
            uiObject.AddComponenet(WeaponText2);

            backgroundsObject = new GameObjects();
            backgroundsObject.AddComponenet(staticBackground1);
            backgroundsObject.AddComponenet(staticBackground2);

            OpeningScene.AddGameObject(mainMenuObject);
            OpeningScene.AddGameObject(splashScreenObject);
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
            KeyHandler();
            UpdatedEntitied(elapsed);
            base.Update(gameTime);
        }
        void UpdatedEntitied(float elapsed)
        {
            staticBackground1.Update(elapsed);
            staticBackground2.Update(elapsed);
            splashScreen.Update();
            player.Update(elapsed);
            player.Transform.Velocity = new Vector2(0, 0);
            foreach (Projectile p in bullets)
            {
                p.Update(elapsed);
            }
            playButton.Update();
            BlankGame();
            if (playButton.IsClick())
            {
                OpeningScene.DeactivateScene();
            }
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            foreach(var scenes in AllScenes)
            {
                scenes.DrawAllObjects(_spriteBatch);
            }
            foreach(var bullet in bullets)
            {
                bullet.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
        void KeyHandler()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                player.Transform.Velocity.Y = 500;
                //background.Velocity.Y = -20;
                //background2.Velocity.Y = -20;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                player.Transform.Velocity.Y = -500;
                //background.Velocity.Y = +20;
                //background2.Velocity.Y = +20;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                player.Transform.Velocity.X = 500;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                player.Transform.Velocity.X = -500;
            }
            if (Keyboard.GetState().IsKeyDown(Missiles.KeyboardInput) && PrevState.IsKeyUp(Missiles.KeyboardInput))
            {
                foreach (Weapon w in weapons)
                {
                    w.IsSelected = false;
                }

                Missiles.IsSelected = true;
            }
            //if (Keyboard.GetState().IsKeyDown(Bor.KeyboardInput) && PrevState.IsKeyUp(Bor.KeyboardInput))
            //{
            //    foreach (Weapon w in weapons)
            //    {
            //        w.IsSelected = false;
            //    }

            //    Bor.IsSelected = true;
            //}
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && PrevState.IsKeyUp(Keys.Space))
            {
                Projectile p = new Projectile(GetWeaponSelected().Bullet.Transform.Position, GetWeaponSelected().Bullet.Image, GetWeaponSelected().Bullet.Transform.Velocity, Color.White);
                bullets.Add(p);
                p.Enable();
                p.Transform.Position = new Vector2(player.Transform.Position.X, player.Transform.Position.Y + 15);
                GetWeaponSelected().ShotSound.Play();
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
    }
}
