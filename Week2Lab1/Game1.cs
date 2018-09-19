using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Week2Lab1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        // Variables for the Dot
        Texture2D dot;
        Color dotColor;
        Rectangle dotRect;
        int dotSize;
        // Variables for the Background 
        Texture2D background;
        Rectangle backgroundRect;

        // Variables to hold the display properties
        int displayWidth;
        int displayHeight;

        // Variables to hold the color change
        byte redComponent = 0;
        byte blueComponent = 0;
        byte greenComponent = 0;
        byte alphaComponent = 0;

        // Vars to draw message
        SpriteFont font;
        string message = "";

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
            displayWidth = GraphicsDevice.Viewport.Width;
            displayHeight = GraphicsDevice.Viewport.Height;

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
            dot = Content.Load<Texture2D>("WhiteDot");
            dotColor = Color.White;
            dotSize = 40;

            dotRect = new Rectangle(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2, dotSize, dotSize);

            background = Content.Load<Texture2D>("background");
            backgroundRect = new Rectangle(0, 0, displayWidth, displayHeight);
            font = Content.Load<SpriteFont>("MessageFont");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed ||
                            Keyboard.GetState().IsKeyDown(Keys.B))
                blueComponent++;
            if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed ||
                            Keyboard.GetState().IsKeyDown(Keys.G))
                greenComponent++;
            if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed ||
                            Keyboard.GetState().IsKeyDown(Keys.R))
                redComponent++;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed ||
                            Keyboard.GetState().IsKeyDown(Keys.T))
                alphaComponent++;

            
            
            GamePadState gpState = GamePad.GetState(PlayerIndex.One);
            if (gpState.IsConnected)
            {
                if (gpState.ThumbSticks.Left.X != 0 ||
                    gpState.ThumbSticks.Left.Y != 0)
                {
                    if(dotRect.X + (int)(gpState.ThumbSticks.Left.X * 5) //*5 for scale pu factor and speed of movement.
                        < graphics.GraphicsDevice.Viewport.Width - dotRect.Width &&
                        dotRect.X + (int)(gpState.ThumbSticks.Left.X * 5) > 0)
                    {
                        dotRect.X += (int)(gpState.ThumbSticks.Left.X * 5);
                    }
                                
                    
                    if (dotRect.Y - (int)(gpState.ThumbSticks.Left.Y * 5) 
                        < graphics.GraphicsDevice.Viewport.Height - dotRect.Height &&
                        dotRect.Y - (int)(gpState.ThumbSticks.Left.Y * 5) > 0)
                    
                                dotRect.Y -= (int)(gpState.ThumbSticks.Left.Y * 5);
                }
            }
            // TODO: Add your update logic here
            dotColor = new Color(redComponent, greenComponent, blueComponent, alphaComponent);
            message = "Red: " + redComponent.ToString() +
                            " Green: " + greenComponent.ToString() +
                            " Blue: " + blueComponent.ToString() +
                            " Alpha: " + alphaComponent.ToString();

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

            spriteBatch.Draw(background, backgroundRect, Color.White);

            spriteBatch.Draw(dot, dotRect, dotColor);

            // Work out Centre the text to draw
            int stringWidth = (int)font.MeasureString(message).X;

            spriteBatch.DrawString(font, message, new Vector2((displayWidth - stringWidth) / 2, 0), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
