using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoTroid
{
    public class Camera
    {
        private GameObject target;
        private Vector2 size;
        private Vector2 position = new Vector2(0, 0);
        private Vector2 levelBounds;
        public Rectangle ViewPlane { get; private set; }

        public Camera(Vector2 size, Vector2 levelBounds)
        {
            this.size = size;
            this.levelBounds = levelBounds;

            // The level bounds cannot be smaller than the camera
            if (this.levelBounds.X < this.size.X || this.levelBounds.Y < this.size.Y)
            {
                this.levelBounds = this.size;
            }

            ViewPlane = new Rectangle(position.ToPoint(), size.ToPoint());
        }

        public void TrackTarget(GameObject target)
        {
            this.target = target;
        }

        public void Update()
        {
            AdjustCamera();
            KeepCameraInBounds();
            ViewPlane = new Rectangle(position.ToPoint(), size.ToPoint());
        }

        private void AdjustCamera()
        {
            position.X = target.Position.X - (size.X / 2);
            position.Y = target.Position.Y - (size.Y / 2);
        }

        private void KeepCameraInBounds()
        {
            position.X = MathHelper.Clamp(position.X, 0, levelBounds.X);
            position.Y = MathHelper.Clamp(position.Y, 0, levelBounds.Y);

            if (position.X + size.X > levelBounds.X)
            {
                position.X = levelBounds.X - size.X;
            }

            if (position.Y + size.Y > levelBounds.Y)
            {
                position.Y = levelBounds.Y - size.Y;
            }
        }
    }
}
