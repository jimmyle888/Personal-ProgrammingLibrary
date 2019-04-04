using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace DodgerMonoGamePort
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;  //Default font
        SpriteFont gameOver; //Game Over Font

        Texture2D baddieImg;
        Texture2D playerImg;
        KeyboardState kb = Keyboard.GetState();

        int screenWidth = 400;
        int screenHeight = 1000;

        Player p1;

        List<Enemy> enemies = new List<Enemy>();

        int curTime;
        int bestTime;
        int frames;
       
        const int PLAY = 1;
        const int GAMEOVER = 0;
        const int MENU = 2;
        int gamestate = MENU;

        Vector2 centerBegin = new Vector2(112, 450);
        Vector2 centerOver = new Vector2(22, 10);

        Texture2D rect;
        Vector2 coor;


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

            spriteBatch = new SpriteBatch(GraphicsDevice);

            baddieImg = Content.Load<Texture2D>("images/baddie");
            playerImg = Content.Load<Texture2D>("images/player");

            font = Content.Load<SpriteFont>("fonts/font");
            gameOver = Content.Load<SpriteFont>("fonts/gameOver");

            p1 = new Player(playerImg);
            //enemies.Add(new Enemy(baddieImg, 2, screenWidth, screenHeight));
            //enemies[0].dest.X = 200;
            //enemies.Add(new Enemy(baddieImg, 1, screenWidth, screenHeight));
            //enemies[1].dest.X = 300;

            //enemies.Add(new Enemy(baddieImg, 3, screenWidth, screenHeight));

            CreateEnemies(10);

            curTime = 0;
            bestTime = 0;
            frames = 0;

            rect = new Texture2D(graphics.GraphicsDevice, 80, 30);
            coor = new Vector2(10, 20);
            Color[] data = new Color[80*30];

            for (int i = 0; i < data.Length; ++i) data[i] = Color.GreenYellow;

            rect.SetData(data);


            //enemies[0].dest.X = 100;
            //enemies[0].dest.Y = 100;

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
            if (frames == 60)
            {
                curTime++;
                frames = 0;
            }


            switch (gamestate)
            {
                case MENU:
                    p1.Update();
                    if (Keyboard.GetState().IsKeyDown(Keys.E))
                    {
                        gamestate = PLAY;
                    }
                    break;
                case PLAY:
                    p1.Update();
                    UpdateEnemies();
                    if (CheckGameOver())
                    {
                        gamestate = GAMEOVER;
                    }
                    break;
                case GAMEOVER:
                    break;
            }

            frames++;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            spriteBatch.Draw(rect, coor, Color.Green);


            switch (gamestate)
            {
                case MENU:
                    spriteBatch.DrawString(font, "Press 'E' TO BEGIN", centerBegin, Color.Green);
                    break;
                case PLAY:
                    DrawEnemies(spriteBatch);
                    break;
                case GAMEOVER:
                    DrawEnemies(spriteBatch);
                    spriteBatch.DrawString(gameOver, "GAME OVER", centerOver, Color.Red);
                    break;
            }

            p1.Draw(spriteBatch);
            //enemies[0].Draw(spriteBatch);

            //spriteBatch.DrawString()

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void UpdateEnemies()
        {
            foreach (Enemy e in enemies)
            {
                e.Update();
            }
        }

        private void DrawEnemies(SpriteBatch s)
        {
            foreach (Enemy e in enemies)
            {
                e.Draw(s);
            }
        }

        private void CreateEnemies(int n)
        {
            Enemy prevE = new Enemy(null, 1, screenWidth, screenHeight);
            prevE.dest.X = -1000;   
            Enemy curE = new Enemy(baddieImg, 1, screenWidth, screenHeight);

            for (int i = 1; i < n + 1; i++)
            {
                curE = new Enemy(baddieImg, i, screenWidth, screenHeight);
                while ((curE.dest.X > prevE.dest.X -50 && curE.dest.X < prevE.dest.X + 50))
                {
                    curE = new Enemy(baddieImg, i, screenWidth, screenHeight);
                }
                enemies.Add(curE);
                prevE = curE;
            }
        }

        private bool CheckGameOver()
        {
            foreach (Enemy en in enemies)
            {
                if (p1.CheckCollsion(en.dest))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
