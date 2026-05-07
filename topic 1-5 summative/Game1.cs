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
        Rectangle window, lightningRect, chickHicksRect, fireRect, deadLightningRect, chickHicksRectPodium;
        Texture2D trackTexture, lightningTexture, chickHicksTexture, introTexture, fireTexture, podiumTexture, deadLightningTexture;
        Vector2 lightningSpeed, chickHicksSpeed, deadLightningSpeed, chickHicksSpeedPodium; 
        Screen screen;
        MouseState mouseState;
        float seconds;
        SpriteFont talkingFont;
        float chickRotation = 0f;
        bool isspinning = false;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            window = new Rectangle(0,0,800,600);
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.ApplyChanges();

            lightningRect = new Rectangle(320, 100, 120, 70);
            lightningSpeed = new Vector2(-1, 3);
            chickHicksRect = new Rectangle(445, 80, 120, 70);
            chickHicksSpeed = new Vector2(-1, 3);
            fireRect = new Rectangle(170,190,200,200);
            deadLightningRect = new Rectangle(800, 400, 120, 70);
            deadLightningSpeed = new Vector2(0, 0);
            chickHicksRectPodium = new Rectangle(360, 420, 120, 70);
            chickHicksSpeedPodium = new Vector2(0, 0);

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
            podiumTexture = Content.Load<Texture2D>("podium");
            deadLightningTexture = Content.Load<Texture2D>("deadLightning");
            talkingFont = Content.Load<SpriteFont>("talking");
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
                    chickHicksSpeed.Y = 3;
                }

                if (chickHicksRect.Top >= 600)
                {
                    screen = Screen.podium;
                }
            }
            else if (screen == Screen.podium)
            {
                deadLightningRect.X += (int)deadLightningSpeed.X;
                chickHicksRectPodium.X += (int)chickHicksSpeedPodium.X;
                chickHicksRectPodium.Y += (int)chickHicksSpeedPodium.Y;
                deadLightningSpeed.X = -2;
                
               if (deadLightningRect.X <= chickHicksRectPodium.Right)
                {

                    chickHicksSpeedPodium.X = -5;
                    chickHicksSpeedPodium.Y = -2;
                    isspinning = true;
                    
                }
               if (isspinning)
                {
                    chickRotation -= 0.3f;
                    if(chickRotation <= 600 * -MathHelper.TwoPi)
                    {
                        chickRotation = 0f;
                        isspinning = false;
                    }
                }

               if (deadLightningRect.X <= 360)
                {
                    deadLightningSpeed.X = 0;
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
                _spriteBatch.Draw(introTexture, window, Color.White);
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
            else if (screen == Screen.podium)
            {
                _spriteBatch.Draw(podiumTexture, window, Color.White);
                _spriteBatch.Draw(chickHicksTexture, new Vector2(chickHicksRectPodium.X + chickHicksRectPodium.Width / 2, chickHicksRectPodium.Y + chickHicksRectPodium.Height / 2), null, Color.White, chickRotation, new Vector2(chickHicksTexture.Width / 2, chickHicksTexture.Height / 2), new Vector2((float)chickHicksRectPodium.Width / chickHicksTexture.Width, (float)chickHicksRectPodium.Height / chickHicksTexture.Height), SpriteEffects.None, 0f);
                _spriteBatch.Draw(deadLightningTexture, deadLightningRect, Color.SlateGray * 0.7f);
                if (deadLightningRect.X <= 360)
                {
                    _spriteBatch.DrawString(talkingFont, "Im Backkk!", new Vector2(444, 411), Color.White);
                }
            }
                _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
