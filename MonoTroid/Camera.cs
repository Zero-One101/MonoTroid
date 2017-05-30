using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoTroid.Managers;

namespace MonoTroid
{
    public class Camera
    {
        private float zoom;
        private float rotation;
        private Vector2 position;
        private Vector2 size;
        private Vector2 levelBounds;
        private Matrix transform = Matrix.Identity;
        private Matrix translationMatrix = Matrix.Identity;
        private Matrix rotationMatrix = Matrix.Identity;
        private Matrix scaleMatrix = Matrix.Identity;
        private Matrix resTranslationMatrix = Matrix.Identity;
        private GameObject targetObject;

        private readonly ResolutionManager resManager;
        private Vector3 translationVector = Vector3.Zero;
        private Vector3 scaleVector = Vector3.Zero;
        private Vector3 resTranslationVector = Vector3.Zero;

        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
            }
        }

        public float Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value;
                if (zoom < 0.1f)
                {
                    zoom = 0.1f;
                }
            }
        }

        public float Rotation
        {
            get { return rotation; }
            set
            {
                rotation = value;
            }
        }

        public Camera(ResolutionManager resManager, Vector2 size, Vector2 levelBounds)
        {
            this.resManager = resManager;
            zoom = 1;
            rotation = 0f;
            position = Vector2.Zero;

            this.size = size;
            this.levelBounds = levelBounds;

            // The level bounds cannot be smaller than the camera
            if (this.levelBounds.X < this.size.X || this.levelBounds.Y < this.size.Y)
            {
                this.levelBounds = this.size;
            }
        }

        public void TrackTarget(GameObject target)
        {
            targetObject = target;
        }

        public void Update()
        {
            AdjustCamera();
            KeepCameraInBounds();
        }

        private void AdjustCamera()
        {
            position.X = targetObject.Position.X;
            position.Y = targetObject.Position.Y;
        }

        private void KeepCameraInBounds()
        {
            position.X = MathHelper.Clamp(position.X, size.X / 2, levelBounds.X + size.X / 2);
            position.Y = MathHelper.Clamp(position.Y, size.Y / 2, levelBounds.Y + size.Y / 2);

            if (position.X + size.X / 2 > levelBounds.X)
            {
                position.X = levelBounds.X - size.X / 2;
            }

            if (position.Y + size.Y / 2 > levelBounds.Y)
            {
                position.Y = levelBounds.Y - size.Y / 2;
            }
        }

        public Matrix GetViewTransformMatrix()
        {
            translationVector.X = -position.X;
            translationVector.Y = -position.Y;

            Matrix.CreateTranslation(ref translationVector, out translationMatrix);
            Matrix.CreateRotationZ(rotation, out rotationMatrix);

            scaleVector.X = zoom;
            scaleVector.Y = zoom;
            scaleVector.Z = 1;

            Matrix.CreateScale(ref scaleVector, out scaleMatrix);
            resTranslationVector.X = resManager.VirtualResolution.X * 0.5f;
            resTranslationVector.Y = resManager.VirtualResolution.Y * 0.5f;
            resTranslationVector.Z = 0;

            Matrix.CreateTranslation(ref resTranslationVector, out resTranslationMatrix);

            transform = translationMatrix * rotationMatrix * scaleMatrix * resTranslationMatrix *
                        resManager.ScaleMatrix;

            return transform;
        }
    }
}
