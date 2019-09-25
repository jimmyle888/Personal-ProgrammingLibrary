using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Simon
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int screenWidth = 1280;
        int screenHeight = 720;

        Texture2D bg;
        Rectangle bgBounds;

        Rectangle green;
        Rectangle red;
        Rectangle yellow;
        Rectangle blue;

        Texture2D highlight;

        Random rng = new Random();

        List<int> seq = new List<int>();

        int timer = 0;

        const int MENU = 0;
        const int SHOW = 1;
        const int SELECT = 2;
        int gameState = MENU;

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
            graphics.PreferredBackBufferWidth = screenWidth;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = screenHeight;   // set this value to the desired height of your window
            graphics.ApplyChanges();

            this.IsMouseVisible = true;

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

            bg = Content.Load<Texture2D>("images/imadethismyself");
            bgBounds = new Rectangle(0, 0, screenWidth, screenHeight);

            green = new Rectangle(0, 0, screenWidth / 2, screenHeight / 2);
            red = new Rectangle(screenWidth / 2, 0, screenWidth / 2, screenHeight / 2);
            yellow = new Rectangle(0, screenHeight / 2, screenWidth / 2, screenHeight / 2);
            blue = new Rectangle(screenWidth / 2, screenHeight / 2, screenWidth / 2, screenHeight / 2);

            highlight = Content.Load<Texture2D>("images/jefff");

            seq = GenerateSeq(4);

            // TODO: use this.Content to load your game content here
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

            // TODO: Add your update logic here
            switch (gameState)
            {
                case MENU:
                    break;
                case SHOW:
                    break;
                case SELECT:
                    break;
                default:
                    break;

            }
            

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

            spriteBatch.Draw(bg, bgBounds, Color.White);
            //spriteBatch.Draw(highlight, red, Color.Red);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private List<int> GenerateSeq(int length)
        {
            List<int> newSeq = new List<int>();

            for (int i = 0; i < length; i++)
            {
                newSeq.Add(rng.Next(1, 5));
            }

            return newSeq;

        }

        private void PlaySeq()
        {

        }
        
        private void DrawSeq()
        {
            
        }
    }
}
