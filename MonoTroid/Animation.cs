using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTroid
{
    class Animation
    {
        /// <summary>
        /// The size of each frame
        /// </summary>
        private Point FrameSize { get; set; }

        /// <summary>
        /// The texture of the strip of animation
        /// </summary>
        private Texture2D AnimStrip { get; set; }

        /// <summary>
        /// Whether or not the animation loops. If not, it will finish on the final frame.
        /// </summary>
        private bool Looping { get; set; }

        /// <summary>
        /// The time each frame will be displayed for
        /// </summary>
        private float FrameTime { get; set; }

        /// <summary>
        /// The total time the current frame has been displayed
        /// </summary>
        private float CurrentFrameTime { get; set; }

        /// <summary>
        /// The number of frames in the animation
        /// </summary>
        private int FrameCount { get; set; } = 0;

        /// <summary>
        /// The current frame of animation
        /// </summary>
        public int CurrentFrame { get; private set; } = 0;

        /// <summary>
        /// The target frame of the animation strip
        /// </summary>
        private Rectangle SourceFrame { get; set; }

        /// <summary>
        /// Creates a new Animation
        /// </summary>
        /// <param name="entityManager">The world's entity manager</param>
        /// <param name="animStrip">The filename of the animation strip</param>
        /// <param name="looping">Whether or not the animation loops at the end</param>
        /// <param name="frameCount">The number of frames in the animation</param>
        /// <param name="frameTime">The time each frame will be displayed for</param>
        /// <param name="frameOffset">Which frame of the animation to start at</param>
        public Animation(EntityManager entityManager, string animStrip, bool looping, int frameCount, float frameTime, int frameOffset)
        {
            AnimStrip = entityManager.ResourceManager.LoadTexture(animStrip);
            Looping = looping;
            FrameCount = frameCount;
            FrameTime = frameTime;
            CurrentFrame = frameOffset;
            FrameSize = new Point(AnimStrip.Width / frameCount, AnimStrip.Height);
            UpdateSourceFrame();
        }

        /// <summary>
        /// Updates the animation
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            CurrentFrameTime += gameTime.ElapsedGameTime.Milliseconds;

            if (CurrentFrameTime >= FrameTime)
            {
                CurrentFrameTime = 0;
                CurrentFrame++;

                if (CurrentFrame == FrameCount)
                {
                    if (Looping)
                    {
                        CurrentFrame = 0;
                    }
                    else
                    {
                        CurrentFrame--;
                    }
                }
            }

            UpdateSourceFrame();
        }

        private void UpdateSourceFrame()
        {
            SourceFrame = new Rectangle(new Point(CurrentFrame * FrameSize.X, 0), FrameSize);
        }

        /// <summary>
        /// Draws the current frame of animation at the specified position
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch of the graphics device</param>
        /// <param name="drawPos">The position at which to draw the frame</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 drawPos)
        {
            spriteBatch.Draw(AnimStrip, drawPos, SourceFrame, Color.White);
        }
    }
}
