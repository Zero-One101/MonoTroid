using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Editor
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class EditorGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private readonly IntPtr drawSurface;
        private MainForm form;

        public EditorGame(MainForm form)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.form = form;
            drawSurface = form.GetEditorSurface();
            graphics.PreparingDeviceSettings += Graphics_PreparingDeviceSettings;
            System.Windows.Forms.Control.FromHandle((this.Window.Handle)).VisibleChanged += Game1_VisibleChanged;
        }

        /// <summary>
        /// Occurs when the original game window's visibility changes and makes sure it stays invisible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Game1_VisibleChanged(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Control.FromHandle(this.Window.Handle).Visible)
            {
                System.Windows.Forms.Control.FromHandle(this.Window.Handle).Visible = false;
            }
        }

        /// <summary>
        /// Captures the construction of a draw surface and makes sure it gets redirected to a
        /// predesignated draw surface marked by pointer drawSurfacea
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.DeviceWindowHandle = drawSurface;
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

            base.Draw(gameTime);
        }
    }
}
