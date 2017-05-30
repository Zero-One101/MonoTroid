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
        public Matrix ScaleMatrix { get; private set; }

        public Vector2 VirtualResolution { get; } = new Vector2(256, 224);

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
            var widthScale = graphics.PreferredBackBufferWidth / VirtualResolution.X;
            var heightScale = graphics.PreferredBackBufferHeight / VirtualResolution.Y;
            var scaleFactor = new Vector3(widthScale, heightScale, 1);
            ScaleMatrix = Matrix.CreateScale(scaleFactor);
        }
    }
}
