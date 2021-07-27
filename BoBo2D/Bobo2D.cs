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

        Button playButton;

        SpriteFont basicFont;
        SpaceShip player;
        SoundEffect MissileShot;

        KeyboardState PrevState;
        List<Weapon> weapons = new List<Weapon>();
        List<Projectile> bullets = new List<Projectile>();

        Scenes OpeningScene;

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
            OpeningScene = new Scenes();
            OpeningScene.ActivateScene();

            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            MissileShot = Content.Load<SoundEffect>("ShotSound");
            basicFont = Content.Load<SpriteFont>("Font");
            SplashScreen splashScreen = new SplashScreen(new Vector2(0, 0), Content.Load<Texture2D>("SkyIsYours"), Color.White, 6f, 6f, 1000);
            Text playLabel = new Text(basicFont, "Play!", new Vector2(0, 0), Color.White);
            MainMenu mainMenu = new MainMenu(new Vector2(0, 0), new Rectangle(1280, 720, 0, 0), Content.Load<Texture2D>("Sky"), Color.White);
            player = new SpaceShip(new Vector2(200, 400), Content.Load<Texture2D>("Plane2"), Color.White);
            playButton = new Button(new Rectangle(0, 0, 380, 160), playLabel, Content.Load<Texture2D>("Button2"), Color.White);
            playButton.Invoke();

            splashScreenObject = new GameObjects();
            splashScreenObject.AddComponenet(splashScreen);

            mainMenuObject = new GameObjects();
            mainMenuObject.AddComponenet(mainMenu);
            mainMenuObject.AddComponenet(playButton);
            mainMenuObject.AddComponenet(playLabel);

            OpeningScene.AddGameObject(mainMenuObject);
            OpeningScene.AddGameObject(splashScreenObject);
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyHandler();
            UpdatedEntitied(elapsed);
            base.Update(gameTime);
        }
        void UpdatedEntitied(float elapsed)
        {
            player.Velocity = new Vector2(0, 0);
            player.Update(elapsed);
            foreach (Projectile p in bullets)
            {
                p.Update(elapsed);
            }
            playButton.Update();
            if (playButton.IsClick())
            {
                OpeningScene.DeactivateScene();
            }
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            OpeningScene.DrawAllObjects(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        void KeyHandler()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                player.Velocity.Y = 500;
                //background.Velocity.Y = -20;
                //background2.Velocity.Y = -20;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                player.Velocity.Y = -500;
                //background.Velocity.Y = +20;
                //background2.Velocity.Y = +20;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                player.Velocity.X = 500;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                player.Velocity.X = -500;
            }
            //if (Keyboard.GetState().IsKeyDown(Missiles.KeyboardInput) && PrevState.IsKeyUp(Missiles.KeyboardInput))
            //{
            //    foreach (Weapon w in weapons)
            //    {
            //        w.IsSelected = false;
            //    }

            //    Missiles.IsSelected = true;
            //}
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
                Projectile p = new Projectile(GetWeaponSelected().Bullet.Transform.Position, GetWeaponSelected().Bullet.Image, GetWeaponSelected().Bullet.Velocity, Color.White);
                bullets.Add(p);
                p.Transform.Position = new Vector2(player.Position.X, player.Position.Y + 15);
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
    }
}
