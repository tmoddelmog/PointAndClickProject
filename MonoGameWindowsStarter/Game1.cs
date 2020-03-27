using ContentPipelineLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameWindowsStarter;
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

        Texture2D pixel, particleTexture;
        SpriteFont font;
        Color background, square;
        RectangleSet level1;
        bool gameOver;

        ParticleSystem mouseSystem,
                       backgroundSystem,
                       endSystem;

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
            //this.IsMouseVisible = true;

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

            pixel = Content.Load<Texture2D>("pixel");
            particleTexture = Content.Load<Texture2D>("particle");
            font = Content.Load<SpriteFont>("font");
            background = Color.Black;
            square = Color.Aquamarine;
            gameOver = false;

            var r = new System.Random();

            // mouse system
            mouseSystem = new ParticleSystem(this.GraphicsDevice, 1000, particleTexture);
            mouseSystem.SpawnPerFrame = 4;
            mouseSystem.SpawnParticle = (ref Particle particle) =>
            {
                MouseState mouse = Mouse.GetState();
                particle.Position = new Vector2(mouse.X, mouse.Y);
                particle.Velocity = new Vector2(
                    MathHelper.Lerp(-50, 50, (float)r.NextDouble()),
                    MathHelper.Lerp(0, 100, (float)r.NextDouble())
                    );
                particle.Acceleration = 0.1f * new Vector2(0, (float) - r.NextDouble());
                particle.Color = Color.Crimson;
                particle.Scale = 1f;
                particle.Life = 1.0f;
            };
            mouseSystem.UpdateParticle = (float deltaT, ref Particle particle) =>
            {
                particle.Velocity += deltaT * particle.Acceleration;
                particle.Position += deltaT * particle.Velocity;
                particle.Scale -= deltaT;
                particle.Life -= deltaT;
            };

            // background system
            backgroundSystem = new ParticleSystem(this.GraphicsDevice, 500, pixel);
            backgroundSystem.SpawnPerFrame = 4;
            backgroundSystem.SpawnParticle = (ref Particle particle) =>
            {
                particle.Position = new Vector2(r.Next(SCREEN_WIDTH + 100), 0);
                particle.Velocity = new Vector2(0, SCREEN_HEIGHT);
                particle.Acceleration = Vector2.Zero;
                particle.Color = Color.Green;
                particle.Scale = 5f;
                particle.Life = 150.0f;
            };
            backgroundSystem.UpdateParticle = (float deltaT, ref Particle particle) =>
            {
                particle.Position += deltaT/2 * particle.Velocity;
                particle.Scale -= deltaT;
                particle.Life -= deltaT;
            };

            // end system
            endSystem = new ParticleSystem(this.GraphicsDevice, 100000, particleTexture);
            endSystem.SpawnPerFrame = 4;
            endSystem.SpawnParticle = (ref Particle particle) =>
            {
                particle.Position = new Vector2(SCREEN_WIDTH/2 + 25, SCREEN_HEIGHT/2 - 150);
                particle.Velocity = new Vector2(
                    MathHelper.Lerp(-100, 100, (float)r.NextDouble()),
                    MathHelper.Lerp(-100, 100, (float)r.NextDouble())
                    );
                particle.Acceleration = 0.1f * new Vector2(0, (float)-r.NextDouble());
                particle.Color = Color.Orange;
                particle.Scale = 3f;
                particle.Life = 500.0f;
            };
            endSystem.UpdateParticle = (float deltaT, ref Particle particle) =>
            {
                particle.Velocity += deltaT * particle.Acceleration;
                particle.Position += deltaT * particle.Velocity;
                particle.Scale -= deltaT;
                particle.Life -= deltaT;
            };
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
            mouseSystem.Update(gameTime);
            backgroundSystem.Update(gameTime);
            endSystem.Update(gameTime);

            if (level1.Rectangles.Count == 0) gameOver = true;

            var mouse = Mouse.GetState();
            var current = level1.Rectangles;
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
                mouseSystem.Draw();
                backgroundSystem.Draw();
                foreach (Rectangle r in level1.Rectangles) spriteBatch.Draw(pixel, r, square);
            }
            else
            {
                endSystem.Draw();

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
