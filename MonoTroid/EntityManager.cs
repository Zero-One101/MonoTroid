using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoTroid
{
    class EntityManager
    {
        public event KeyDownHandler KeyDown;
        public event KeyUpHandler KeyUp;
        private readonly List<GameObject> entities = new List<GameObject>();
        private readonly List<GameObject> entitiesToAdd = new List<GameObject>();
        private readonly List<Keys> downKeys = new List<Keys>();
        private readonly List<Keys> upKeys = new List<Keys>();
        private Viewport viewport;
        private InputManager inputManager;
        public ResourceManager ResourceManager { get; private set; }

        public EntityManager(InputManager inputManager, ResourceManager resourceManager, List<GameObject> entitiesToAdd)
        {
            ResourceManager = resourceManager;
            this.entitiesToAdd = entitiesToAdd;
            this.inputManager = inputManager;
            this.inputManager.KeyDown += InputManager_KeyDown;
            this.inputManager.KeyUp += InputManager_KeyUp;
        }

        private void InputManager_KeyUp(object sender, KeyUpEventArgs e)
        {
            upKeys.Add(e.Key);
        }

        private void InputManager_KeyDown(object sender, KeyDownEventArgs e)
        {
            downKeys.Add(e.Key);
        }

        public void Initialise(Viewport viewport)
        {
            this.viewport = viewport;
        }

        public void AddEntity(GameObject entity)
        {
            entitiesToAdd.Add(entity);
        }

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
