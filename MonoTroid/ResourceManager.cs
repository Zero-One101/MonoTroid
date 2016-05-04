using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace MonoTroid
{
    /// <summary>
    /// Handles the loading and unloading of assets
    /// </summary>
    public class ResourceManager
    {
        private readonly ContentManager content;
        private readonly Dictionary<string, Texture2D> textures;
        private readonly Dictionary<string, SpriteFont> fonts;
        private readonly Dictionary<string, Song> songs; 

        public ResourceManager(ContentManager content)
        {
            this.content = content;
            textures = new Dictionary<string, Texture2D>();
            fonts = new Dictionary<string, SpriteFont>();
            songs = new Dictionary<string, Song>();
        }

        /// <summary>
        /// Loads the Texture2D into memory
        /// </summary>
        /// <param name="filename">The filename of the Texture2D to be loaded</param>
        /// <returns>The loaded Texture2D</returns>
        public Texture2D LoadTexture(string filename)
        {
            if (textures.ContainsKey(filename))
            {
                return textures[filename];
            }

            var texture = content.Load<Texture2D>(string.Format(@"images\{0}", filename));
            textures.Add(filename, texture);
            return texture;
        }

        /// <summary>
        /// Loads the SpriteFont into memory
        /// </summary>
        /// <param name="filename">The filename of the SpriteFont to be loaded</param>
        /// <returns>The loaded SpriteFont</returns>
        public SpriteFont LoadFont(string filename)
        {
            if (fonts.ContainsKey(filename))
            {
                return fonts[filename];
            }

            var font = content.Load<SpriteFont>(string.Format(@"Fonts\{0}", filename));
            fonts.Add(filename, font);
            return font;
        }

        public Song LoadSong(string filename)
        {
            if (songs.ContainsKey(filename))
            {
                return songs[filename];
            }

            var song = content.Load<Song>(string.Format(@"Audio\Music\{0}", filename));
            songs.Add(filename, song);
            return song;
        }

        public byte[] LoadLevelFile(string filename)
        {
            return File.ReadAllBytes(string.Format(@"Levels\{0}.map", filename));
        }

        /// <summary>
        /// Clears the assets loaded in memory.
        /// </summary>
        public void Clear()
        {
            textures.Clear();
            fonts.Clear();
            songs.Clear();
        }
    }
}
