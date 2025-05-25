using info.lundin.math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Earth20xx.Engine
{
    public class Camera2D
    {
        public Camera2D()
        {
            this.ViewPort = new Rectangle(0, 0, MainClass.Instance.Device.Viewport.Width, MainClass.Instance.Device.Viewport.Height);
            LeftScroll = new Rectangle(this.ViewPort.X, this.ViewPort.Y, 20, this.ViewPort.Height);
            RightScroll = new Rectangle(this.ViewPort.Width - 40, this.ViewPort.Y, 40, this.ViewPort.Height);
            TopScroll = new Rectangle(this.ViewPort.X, this.ViewPort.Y, this.ViewPort.Width, 40);
            DownScroll = new Rectangle(this.ViewPort.X, this.ViewPort.Height - 40, this.ViewPort.Width, 40);
        }
        public Rectangle ViewPort;
        private Vector2 _position = Vector2.Zero;
        public Rectangle LeftScroll;
        public Rectangle RightScroll;
        public Rectangle TopScroll;
        public Rectangle DownScroll;
        public Rectangle? CameraBounds;
        public Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                if (value != _position)
                {
                    if (CameraBounds != null)
                    {
                        if (value.X < CameraBounds.Value.X)
                            value.X = CameraBounds.Value.X;
                        if (value.X > CameraBounds.Value.Width)
                            value.X = CameraBounds.Value.Width;
                        if (value.Y < CameraBounds.Value.Y)
                            value.Y = CameraBounds.Value.Y;
                        if (value.Y > CameraBounds.Value.Height)
                            value.Y = CameraBounds.Value.Height;
                    }
                    _position = value;
                    CalculateViewPort();
                }
            }
        }
        private float _zoom = 1f;
        public float Zoom
        {
            get
            {
                return _zoom;
            }
            set
            {
                if (value != _zoom)
                {
                    _zoom = value;
                    CalculateViewPort();
                    
                }
            }
        }
        private float _rotation = 0f;
        public float Rotation
        {
            get
            {
                return _rotation;
            }
        }

        
           
        private void CalculateViewPort()
        {
            /*
            int x = (int)(_position.X - MainClass.Instance.Device.Viewport.Width /2 / _zoom);
            int y = (int)(_position.Y - MainClass.Instance.Device.Viewport.Height/2 /_zoom);
            int xspan = (int)(MainClass.Instance.Device.Viewport.Width / _zoom);
            int yspan = (int)(MainClass.Instance.Device.Viewport.Height / _zoom);
            this.ViewPort = new Rectangle(x, y, xspan, yspan);
            */
            float xlen = MainClass.Instance.Device.Viewport.Width / Zoom;
            float ylen = MainClass.Instance.Device.Viewport.Height / Zoom;
            this.ViewPort = new Rectangle((int)(_position.X - xlen / 2), (int)(_position.Y - ylen / 2), (int)xlen, (int)ylen);
         
        }

        /*
         * public Matrix get_transformation(GraphicsDevice graphicsDevice)
{
	_transform =       // Thanks to o KB o for this solution
	  Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
								 Matrix.CreateRotationZ(Rotation) *
								 Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
								 Matrix.CreateTranslation(new Vector3(ViewportWidth * 0.5f, ViewportHeight * 0.5f, 0));
	return _transform;
}
          */

        public Matrix GetTransformation()
        {
            return Matrix.CreateTranslation(new Vector3(-_position.X, -_position.Y, 0)) *
                Matrix.CreateRotationZ(_rotation) *
                Matrix.CreateScale(new Vector3(_zoom, _zoom, 1f)) *
                Matrix.CreateTranslation(new Vector3(MainClass.Instance.Device.Viewport.Width * 0.5f, MainClass.Instance.Device.Viewport.Height * 0.5f, 0f));

        }

    }
}
