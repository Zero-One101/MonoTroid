using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MonoTroid.States.Player
{
    class Spawning : SamusState
    {
        private const int waitTime = 7000;
        private int currentWaitTime;

        public override void Begin(Samus context)
        {
            context.Animation = new Animation(context.EntityManager, AppData.Spawn, false, 13, 50f, 0);
            var spawnSong = context.EntityManager.ResourceManager.LoadSong(AppData.SpawnMusic);
            MediaPlayer.Play(spawnSong);
        }

        public override void Update(Samus context, GameTime gameTime)
        {
            currentWaitTime += gameTime.ElapsedGameTime.Milliseconds;

            if (currentWaitTime >= waitTime)
            {
                HandleInput(context, gameTime);
            }

            context.Animation.Update(gameTime);
        }

        protected override void HandleInput(Samus context, GameTime gameTime)
        {
            if (context.downKeys.Contains(Keys.Left) || context.downKeys.Contains(Keys.Right))
            {
                context.Facing = context.downKeys.Contains(Keys.Left)
                    ? GameObject.EFacing.ELeft
                    : GameObject.EFacing.ERight;

                context.State = new Walking();
                context.State.Begin(context);
            }
        }
    }
}
