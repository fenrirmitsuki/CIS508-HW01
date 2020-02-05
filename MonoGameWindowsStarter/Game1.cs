using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random random = new Random();
        Texture2D ball;
        Rectangle ballRect;
        Vector2 ballPosition;
        Vector2 ballVelocity;
        Texture2D player;
        Rectangle playerRect;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 1500;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();

            //ball physics initialization
            ballPosition = Vector2.Zero;
            ballVelocity = new Vector2((float)random.NextDouble(),(float)random.NextDouble());
            ballVelocity.Normalize();

            //ball bounding rectangle initialization
            ballRect.X = 0;
            ballRect.Y = 0;
            ballRect.Width = 100;
            ballRect.Height = 100;

            //player sprite location initialization
            playerRect.Width = 100;
            playerRect.Height = 150;
            playerRect.X = (graphics.PreferredBackBufferWidth / 2);
            playerRect.Y = graphics.PreferredBackBufferHeight - playerRect.Height;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ball = Content.Load<Texture2D>("ball");
            player = Content.Load<Texture2D>("player");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //update ball position
            ballPosition += (float)gameTime.ElapsedGameTime.TotalMilliseconds * ballVelocity;

            //player controls
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                playerRect.X -= 15;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                playerRect.X += 15;
            }

            //check for player wall collision
            if(playerRect.X < 0)
            {
                playerRect.X = 0;
            }
            if (playerRect.X > (GraphicsDevice.Viewport.Width - playerRect.Width))
            {
                playerRect.X = (GraphicsDevice.Viewport.Width - playerRect.Width);
            }


            //check for ball wall collision
            if (ballPosition.Y < 0)
            {
                ballVelocity.Y *= -1;
                float delta = 0 - ballPosition.Y;
                ballPosition.Y += 2 * delta;
            }
            if (ballPosition.Y > graphics.PreferredBackBufferHeight - 100)
            {
                ballVelocity.Y *= -1;
                float delta = graphics.PreferredBackBufferHeight - 100 - ballPosition.Y;
                ballPosition.Y += 2 * delta;
            }
            if (ballPosition.X < 0)
            {
                ballVelocity.X *= -1;
                float delta = 0 - ballPosition.X;
                ballPosition.X += 2 * delta;
            }
            if (ballPosition.X > graphics.PreferredBackBufferWidth - 100)
            {
                ballVelocity.X *= -1;
                float delta = graphics.PreferredBackBufferWidth - 100 - ballPosition.X;
                ballPosition.X += 2 * delta;
            }

            //check for ball collision with player
            if (!((ballPosition.X > playerRect.X + playerRect.Width) || (ballPosition.X + 100 < playerRect.X) || (ballPosition.Y > playerRect.Y + playerRect.Height) || (ballPosition.Y + 100 < playerRect.Y)))
            {
                if(!((ballPosition.X > playerRect.X + playerRect.Width) || (ballPosition.X + 100 < playerRect.X)))
                {
                    ballVelocity.X *= -1;
                    float delta = 0 - ballPosition.X;
                    //ballPosition.X += 2 * delta;
                }
                if(!((ballPosition.Y > playerRect.Y + playerRect.Height) || (ballPosition.Y + 100 < playerRect.Y)))
                {
                    ballVelocity.Y *= -1;
                    float delta = 0 - ballPosition.Y;
                    //ballPosition.Y += 2 * delta;
                }
            }

            ballRect.X = (int)ballPosition.X;
            ballRect.Y = (int)ballPosition.Y;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(ball, ballRect, Color.White);
            spriteBatch.Draw(player, playerRect, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
