using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoTroid.Managers;

namespace MonoTroid
{
    public delegate void KeyDownHandler(object sender, KeyDownEventArgs e);
    public delegate void KeyUpHandler(object sender, KeyUpEventArgs e);

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private InputManager inputManager;
        private ResourceManager resourceManager;
        private EntityManager entityManager;
        private LevelManager levelManager;
        private ResolutionManager resManager;
        private RenderTarget2D renderTarget;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 256;
            graphics.PreferredBackBufferHeight = 224;
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
            // TODO: Need a matrix-based camera because this will NOT work for long
            renderTarget = new RenderTarget2D(GraphicsDevice, 2560,
                2240);

            resManager = new ResolutionManager(graphics);
            inputManager = new InputManager();
            resourceManager = new ResourceManager(Content);
            entityManager = new EntityManager(inputManager, resourceManager);
            levelManager = new LevelManager(entityManager);
            entityManager.Initialise(GraphicsDevice.Viewport, levelManager);
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
            spriteBatch.CreateWhiteTexture();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                resManager.ChangeResolution(1024, 896);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.G))
            {
                resManager.ChangeResolution(256, 224);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.L))
            {
                graphics.ToggleFullScreen();
            }
            
            inputManager.Update();
            entityManager.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            entityManager.Draw(spriteBatch);
            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: resManager.ScaleMatrix);
            spriteBatch.Draw(renderTarget, Vector2.Zero, entityManager.camera.ViewPlane, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
