using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoTroid;

namespace Editor
{
    public interface ILevelSerialiser
    {
        /// <summary>
        /// Loads a Level from a level name
        /// </summary>
        /// <param name="levelName">The name of the level to be loaded</param>
        /// <returns>The loaded Level object</returns>
        Level LoadLevel(EntityManager entityManager, string levelName);

        /// <summary>
        /// Saves a Level to a file of a given name
        /// </summary>
        /// <param name="levelName">The name of the level to be saved</param>
        /// <returns>True for a successful save, else false</returns>
        bool SaveLevel(string levelName);
    }
}
