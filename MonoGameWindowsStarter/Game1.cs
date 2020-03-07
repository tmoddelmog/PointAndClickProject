using ContentPipelineLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace PointAndClickProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        const int SCREEN_WIDTH = 700, SCREEN_HEIGHT = 700;

        Texture2D pixel;
        SpriteFont font;
        Color background, square;
        RectangleSet level1, level2;
        List<Rectangle> current;
        bool gameOver;

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
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
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

            // TODO: use this.Content to load your game content here
            level1 = Content.Load<RectangleSet>("level1");
            level2 = Content.Load<RectangleSet>("level2");
            current = level1.Rectangles;

            pixel = Content.Load<Texture2D>("pixel");
            font = Content.Load<SpriteFont>("font");
            background = Color.Black;
            square = Color.Aquamarine;
            gameOver = false;
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
            if (current.Count == 0 && current == level1.Rectangles)
            {
                current = level2.Rectangles;
                background = Color.White;
                square = Color.DarkOliveGreen;
            }

            if (current.Count == 0 && current == level2.Rectangles)
                gameOver = true;

            MouseState mouse = Mouse.GetState();
            for (var i = 0; i < current.Count; i++)
            {
                if (current[i].X <= mouse.X && mouse.X <= current[i].X + current[i].Width
                    && current[i].Y <= mouse.Y && mouse.Y <= current[i].Y + current[i].Height)
                {
                    if (mouse.LeftButton == ButtonState.Pressed
                        || mouse.RightButton == ButtonState.Pressed)
                        current.RemoveAt(i);
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(background);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            if (!gameOver)
            {
                foreach (Rectangle r in current) spriteBatch.Draw(pixel, r, square);
            }
            else
            {
                spriteBatch.DrawString(
                    font,
                    "GAME OVER",
                    new Vector2((GraphicsDevice.Viewport.Width / 2) - 6*"GAME OVER".Length,
                                GraphicsDevice.Viewport.Height / 2),
                    Color.Crimson
                    );
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
