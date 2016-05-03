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
        private Point FrameSize { get; set; }
        private Texture2D AnimStrip { get; set; }
        private bool Looping { get; set; }
        private float FrameTime { get; set; }
        private float CurrentFrameTime { get; set; }
        private int FrameCount { get; set; } = 0;
        private int CurrentFrame { get; set; } = 0;
        private Rectangle SourceFrame { get; set; }

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

        public void Update(GameTime gameTime)
        {
            CurrentFrameTime += gameTime.ElapsedGameTime.Milliseconds;

            if (CurrentFrameTime >= FrameTime)
            {
                CurrentFrameTime = 0;
                CurrentFrame++;

                CurrentFrame = CurrentFrame == FrameCount ? 0 : CurrentFrame;
            }

            UpdateSourceFrame();
        }

        private void UpdateSourceFrame()
        {
            SourceFrame = new Rectangle(new Point(CurrentFrame * FrameSize.X, 0), FrameSize);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 drawPos)
        {
            spriteBatch.Draw(AnimStrip, drawPos, SourceFrame, Color.White);
        }
    }
}
