using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoTroid.Managers
{
    /// <summary>
    /// Handles adding, updating, drawing and removing entities. The gateway through which all entities access the world, other managers and other entities.
    /// </summary>
    public class EntityManager
    {
        public event KeyDownHandler KeyDown;
        public event KeyUpHandler KeyUp;
        private readonly List<GameObject> entities = new List<GameObject>();
        private readonly List<GameObject> entitiesToAdd = new List<GameObject>();
        private readonly List<Keys> downKeys = new List<Keys>();
        private readonly List<Keys> upKeys = new List<Keys>();
        private Viewport viewport;
        private LevelManager levelManager;
        public Camera camera;
        public ResourceManager ResourceManager { get; private set; }

        public EntityManager(InputManager inputManager, ResourceManager resourceManager)
        {
            ResourceManager = resourceManager;
            inputManager.KeyDown += InputManager_KeyDown;
            inputManager.KeyUp += InputManager_KeyUp;
        }

        private void InputManager_KeyUp(object sender, KeyUpEventArgs e)
        {
            upKeys.Add(e.Key);
        }

        private void InputManager_KeyDown(object sender, KeyDownEventArgs e)
        {
            downKeys.Add(e.Key);
        }

        public void Initialise(Viewport viewport, LevelManager levelManager)
        {
            this.viewport = viewport;
            this.levelManager = levelManager;
            var samus = new Samus();
            samus.Initialise(this, new Vector2(50, 50));
            AddEntity(samus);

            levelManager.LoadLevel("testLevel2");
            var levelBounds = new Vector2(levelManager.Level.Header.ScreenXY.X * 16 * 16,
                levelManager.Level.Header.ScreenXY.Y * 14 * 16); // ewwwww

            camera = new Camera(new Vector2(256, 224), levelBounds);
            camera.TrackTarget(samus);
        }

        /// <summary>
        /// Adds the specified GameObject to the list of entities.
        /// </summary>
        /// <param name="entity"></param>
        public void AddEntity(GameObject entity)
        {
            entitiesToAdd.Add(entity);
        }

        /// <summary>
        /// Iterates through each entity and updates them. Removes dead entities from the list, then performs collision checking.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            entities.AddRange(entitiesToAdd);
            entitiesToAdd.Clear();

            UpdateKeys();

            foreach (var entity in entities.Where(entity => !entity.IsDead))
            {
                entity.Update(gameTime);
            }
            entities.RemoveAll(x => x.IsDead);

            downKeys.Clear();
            upKeys.Clear();

            CheckTileCollisions();
            CheckCollisions();

            camera.Update();
        }

        private void CheckTileCollisions()
        {
            foreach (var entity in entities)
            {
                levelManager.CheckCollisions(entity);
            }
        }

        private void CheckCollisions()
        {
            // Godawful O(n^2) checking. Will be ripped out later
            foreach (var first in entities)
            {
                foreach (var second in entities.Where(second => first.HitRect.Intersects(second.HitRect)))
                {
                    first.Collide(second);
                    second.Collide(first);
                }
            }
        }

        /// <summary>
        /// Iterates through each entity, draws them, then draws the level
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entity in entities)
            {
                entity.Draw(spriteBatch);
            }

            // TODO: This will need to be done one layer at a time, to allow for a foreground layer
            levelManager.Draw(spriteBatch);
        }

        private void UpdateKeys()
        {
            foreach (var key in downKeys)
            {
                FireKeyDown(key);
            }

            foreach (var key in upKeys)
            {
                FireKeyUp(key);
            }
        }

        private void FireKeyUp(Keys key)
        {
            KeyUp?.Invoke(this, new KeyUpEventArgs(key));
        }

        private void FireKeyDown(Keys key)
        {
            KeyDown?.Invoke(this, new KeyDownEventArgs(key));
        }
    }
}
