using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Earth20xx.Engine
{
    public class Camera2D
    {
        public Camera2D()
        {

        }
        public Rectangle ViewPort;
        private Vector2 _position = Vector2.Zero;
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

        public Vector2 GetMousePos(Vector2 MousePos)
        {
            int xpos = (int)(MousePos.X - MainClass.Instance.CenterX);
            int ypos = (int)(MousePos.Y - MainClass.Instance.CenterY);
            return new Vector2(_position.X + xpos, _position.Y + ypos);
        }
        private void CalculateViewPort()
        {
            int x = (int)(_position.X - MainClass.Instance.CenterX / _zoom);
            int y = (int)(_position.Y - MainClass.Instance.CenterY/_zoom);
            int xspan = (int)(MainClass.Instance.Device.Viewport.Width / _zoom);
            int yspan = (int)(MainClass.Instance.Device.Viewport.Height / _zoom);
            this.ViewPort = new Rectangle(x, y, xspan, yspan);

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
