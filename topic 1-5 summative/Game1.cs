using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.Versioning;

namespace topic_1_5_summative
{
    enum Screen
    {
        intro,
        main,
        podium,
        end
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Rectangle window, lightningRect, chickHicksRect;
        Texture2D trackTexture, lightningTexture, chickHicksTexture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            window = new Rectangle(0,0,800,600);
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.ApplyChanges();

            lightningRect = new Rectangle(350, 190, 190, 100);
            chickHicksRect = new Rectangle(500, 190, 190, 100);


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            lightningTexture = Content.Load<Texture2D>("Lightning_McQueen");
            trackTexture = Content.Load<Texture2D>("track");
            chickHicksTexture = Content.Load<Texture2D>("chickHicks"); 

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(trackTexture, window, Color.White);
            _spriteBatch.Draw(lightningTexture, lightningRect, Color.White);
            _spriteBatch.Draw(chickHicksTexture, chickHicksRect, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
