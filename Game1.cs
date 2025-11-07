using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Projectile_motion_simulator
{
    public class Game1 : Game
    {
        //60 pixels per meter
        private const float EARTHGRAVITY = 9.81f*60f;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _projectileTexture;
        private Texture2D _rulerTexture;
        private Projectile _projectile;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.ApplyChanges();
            // TODO: Add your initialization logic here
            _projectileTexture = Content.Load<Texture2D>("Ball");
            _rulerTexture = Content.Load<Texture2D>("MeterRule");
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _projectile = new Projectile(_projectileTexture, new Vector2(0, _graphics.PreferredBackBufferHeight-100), Color.Black, 40, 900, 40, new Vector2(0f, EARTHGRAVITY), 40, 45);
            _projectile.Launch();

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // TODO: Add your update logic here
            _projectile.Movement(deltaTime,EARTHGRAVITY,_graphics.PreferredBackBufferWidth,_graphics.PreferredBackBufferHeight);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();
            //_projectile.Draw(_spriteBatch);
            _spriteBatch.Draw(_rulerTexture, new Vector2(0, _graphics.PreferredBackBufferHeight-50), Color.White);
            _spriteBatch.End();


            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
