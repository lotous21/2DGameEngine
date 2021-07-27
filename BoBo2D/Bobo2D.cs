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
        SpriteFont basicFont;
        Projectile Bullet;
        SplashScreen splashScreen;
        Weapon Missiles;
        Weapon Bor;
        SpaceShip player;
        SoundEffect MissileShot;
        KeyboardState PrevState;
        List<Weapon> weapons = new List<Weapon>();
        List<Projectile> bullets = new List<Projectile>();

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

            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            splashScreen = new SplashScreen(new Vector2(0, 0), Content.Load<Texture2D>("SkyIsYours"), Color.White, 6f, 6f, 1000);
            basicFont = Content.Load<SpriteFont>("Font");
            MissileShot = Content.Load<SoundEffect>("ShotSound");
            player = new SpaceShip(new Vector2(200, 400), Content.Load<Texture2D>("Plane2"), Color.White);
            Bullet = new Projectile(new Vector2(50, 50), Content.Load<Texture2D>("Missile"), new Vector2(500, 0), Color.White);
            Bullet.Enable();
            Missiles = new Weapon("Missiles", Bullet, Keys.Q, MissileShot);
            Missiles.IsSelected = true;
            Bor = new Weapon("Bor", Bullet, Keys.W, MissileShot);
            weapons.Add(Missiles);
            weapons.Add(Bor);
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            player.Velocity = new Vector2(0, 0);

            foreach (Projectile p in bullets)
            {
                p.Update(elapsed);
            }


            KeyHandler();
            player.Update(elapsed);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            splashScreen.DrawScreen(_spriteBatch);

            if (!splashScreen.IsEnable())
            {
                _spriteBatch.DrawString(basicFont, "Selected Weapon: " + GetWeaponSelected().WeaponName, new Vector2(0, 60), Color.WhiteSmoke);
                foreach (var p in bullets)
                {
                    p.Draw(_spriteBatch);
                }
                player.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
        void CheckCollisions()
        {

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
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && PrevState.IsKeyUp(Keys.Space))
            {
                Projectile p = new Projectile(GetWeaponSelected().Bullet.Position, GetWeaponSelected().Bullet.Image, GetWeaponSelected().Bullet.Velocity, Color.White);
                bullets.Add(p);
                p.Position = new Vector2(player.Position.X, player.Position.Y + 15);
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
