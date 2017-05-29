using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoTroid.Managers
{
    public class ResolutionManager
    {
        private readonly GraphicsDeviceManager graphics;
        private readonly Vector2 virtualResolution = new Vector2(256, 224);
        public Matrix ScaleMatrix { get; private set; }

        public ResolutionManager(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
            UpdateScale();
        }

        public void ChangeResolution(int backBufferWidth, int backBufferHeight)
        {
            graphics.PreferredBackBufferWidth = backBufferWidth;
            graphics.PreferredBackBufferHeight = backBufferHeight;
            graphics.ApplyChanges();
            UpdateScale();
        }

        private void UpdateScale()
        {
            var widthScale = graphics.PreferredBackBufferWidth / virtualResolution.X;
            var heightScale = graphics.PreferredBackBufferHeight / virtualResolution.Y;
            var scaleFactor = new Vector3(widthScale, heightScale, 1);
            ScaleMatrix = Matrix.CreateScale(scaleFactor);
        }
    }
}
