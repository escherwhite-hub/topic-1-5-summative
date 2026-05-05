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
        Rectangle window, lightningRect, chickHicksRect, fireRect;
        Texture2D trackTexture, lightningTexture, chickHicksTexture, introTexture, fireTexture;
        Vector2 lightningSpeed, chickHicksSpeed; 
        Screen screen;
        MouseState mouseState;
        float seconds;

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

            lightningRect = new Rectangle(320, 100, 120, 70);
            lightningSpeed = new Vector2(-1, 3);
            chickHicksRect = new Rectangle(445, 80, 120, 70);
            chickHicksSpeed = new Vector2(-1, 3);
            fireRect = new Rectangle(170,190,200,200);

            screen = Screen.intro;
            


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            lightningTexture = Content.Load<Texture2D>("Lightning_McQueen");
            trackTexture = Content.Load<Texture2D>("track");
            chickHicksTexture = Content.Load<Texture2D>("chickHicks");
            introTexture = Content.Load<Texture2D>("introScreen");
            fireTexture = Content.Load<Texture2D>("fire");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            this.Window.Title = mouseState.Position.ToString();
            mouseState = Mouse.GetState();

            if (screen == Screen.intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    screen = Screen.main;

            }

            else if (screen == Screen.main)
            {
                seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
                
                lightningRect.X += (int)lightningSpeed.X;
                lightningRect.Y += (int)lightningSpeed.Y;
                chickHicksRect.X += (int)chickHicksSpeed.X;
                chickHicksRect.Y += (int)chickHicksSpeed.Y;

                if (seconds >= 1)
                {
                    lightningSpeed.X = 0;
                    lightningSpeed.Y = 0;
                    chickHicksSpeed.X = 0;
                    chickHicksSpeed.Y = 0;
                    chickHicksSpeed.X = -1;

                }

                if (chickHicksRect.Left <= lightningRect.Right)
                {
                    lightningSpeed.X = -1;

                }

               if (lightningRect.Right <= 320)
                {
                    lightningSpeed.X = 0;
                    chickHicksSpeed.X = 0;
                }

            }
             

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            if (screen == Screen.intro)
            {
                _spriteBatch.Draw(introTexture,window, Color.White);
            }
            else if (screen == Screen.main)
            {
                _spriteBatch.Draw(trackTexture, window, Color.White);
                _spriteBatch.Draw(lightningTexture, lightningRect, Color.White);
                _spriteBatch.Draw(chickHicksTexture, chickHicksRect, Color.White);
                if (lightningRect.Right <= 320)
                {
                   _spriteBatch.Draw(fireTexture, fireRect, Color.White);
                }
                   
            }
            

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
