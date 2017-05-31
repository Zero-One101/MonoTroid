using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoTroid.Managers
{
    public class LevelManager
    {
        public Level Level { get; private set; }
        private readonly EntityManager entityManager;

        public LevelManager(EntityManager entityManager)
        {
            this.entityManager = entityManager;
        }

        /// <summary>
        /// Attempts to load the specified level
        /// </summary>
        /// <param name="levelName">The name of hte level to load</param>
        /// <returns>True if loaded successfully, else false</returns>
        public bool LoadLevel(string levelName)
        {
            ILevelSerialiser levelSerialiser = new BinLevelSerialiser();
            try
            {
                Level = levelSerialiser.LoadLevel(entityManager, levelName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Level.Draw(spriteBatch);
        }

        /// <summary>
        /// Checks for collisions between GameObjects and level tiles
        /// </summary>
        /// <param name="entity"></param>
        public void CheckCollisions(GameObject entity)
        {
            foreach (var tile in Level.Tiles)
            {
                if (tile != null)
                {
                    var hitResult = tile.Hit.CheckCollision(entity.Hit, entity.MoveSpeed);
                    if (hitResult.Intersecting)
                    {
                        entity.ResolveTileCollision(hitResult.MinimumTranslationVector);
                    }
                }
            }
        }
    }
}
