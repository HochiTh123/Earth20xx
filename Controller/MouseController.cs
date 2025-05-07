
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Controller
{
    public enum Button { None, Left, Right, Middle, Xbutton1, Xbutton2 }

    public class MouseController
    {
        public MouseController()
        {

        }

        public event EventHandler<MouseEventArgs> MouseMove;
        public event EventHandler<MouseEventArgs> MouseUp;
        public event EventHandler<MouseEventArgs> MouseDown;
        public event EventHandler<MouseEventArgs> MouseScroll;

        private void OnMouseMove(MouseEventArgs e)
        {
            MouseMove?.Invoke(this, e);
        }
        private void OnMouseUp(MouseEventArgs e)
        {
            MouseUp?.Invoke(this, e);
        }
        private void OnMouseDown(MouseEventArgs e)
        {
            MouseDown?.Invoke(this, e);
        }
        private void OnMouseScroll(MouseEventArgs e)
        {
            MouseScroll?.Invoke(this, e);
        }

        private Vector2 _position;
        private int _scrollwheelvalue;
        private int scrollwheeldelta;
        private ButtonState _left = ButtonState.Released;
        private ButtonState _right = ButtonState.Released;
        private ButtonState _middle = ButtonState.Released;
        private ButtonState _xbutton1  = ButtonState.Released;
        private ButtonState _xbutton2 = ButtonState.Released;

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
                    MouseEventArgs e = new MouseEventArgs(Button.None, _position, scrollwheeldelta);
                    OnMouseMove(e);
                }
            }
        }
        public int ScrollWheelValue
        {
            get
            {
                return _scrollwheelvalue;
            }
            set
            {
                if (value != _scrollwheelvalue)
                {
                    scrollwheeldelta = value - _scrollwheelvalue;
                    _scrollwheelvalue = value;
                    MouseEventArgs e = new MouseEventArgs(Button.None, _position, scrollwheeldelta);
                    OnMouseScroll(e);
                }
            }
        }
        public ButtonState Left
        {
            get
            {
                return _left;
            }
            set
            {
                if (value != _left)
                {
                    value = _left;
                    MouseEventArgs e = new MouseEventArgs(Button.Left, _position, ScrollWheelValue);
                    if (value == ButtonState.Released)
                        OnMouseUp(e);
                    else
                        OnMouseDown(e);
                }
            }
        }
        public ButtonState Right
        {
            get
            {
                 return _right;
            }
            set
            {
                if (value != _right)
                {
                    value = _right;
                    MouseEventArgs e = new MouseEventArgs(Button.Right, _position, ScrollWheelValue);
                    if (value == ButtonState.Released)
                        OnMouseUp(e);
                    else
                        OnMouseDown(e);
                }
            }
        }
        public ButtonState Middle
        {
            get
            {
                return _middle;
            }
            set
            {
                if (value != _middle)
                {
                    value = _middle;
                    MouseEventArgs e = new MouseEventArgs(Button.Middle, _position, ScrollWheelValue);
                    if (value == ButtonState.Released)
                        OnMouseUp(e);
                    else
                        OnMouseDown(e);
                }
            }
        }
        public ButtonState XButton1
        {
            get
            {
                return _xbutton1;
            }
            set
            {
                if (value != _xbutton1)
                {
                    value = _xbutton1;
                    MouseEventArgs e = new MouseEventArgs(Button.Xbutton1, _position, ScrollWheelValue);
                    if (value == ButtonState.Released)
                        OnMouseUp(e);
                    else
                        OnMouseDown(e);
                }
            }
        }
        public ButtonState XButton2
        {
            get
            {
                return _xbutton2;
            }
            set
            {
                if (value != _xbutton2)
                {
                    value = _xbutton2;
                    MouseEventArgs e = new MouseEventArgs(Button.Xbutton2, _position, ScrollWheelValue);
                    if (value == ButtonState.Released)
                        OnMouseUp(e);
                    else
                        OnMouseDown(e);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            this.Position = new Vector2(ms.X, ms.Y);
            this.ScrollWheelValue = ms.ScrollWheelValue;
            this.Left = ms.LeftButton;
            this.Right = ms.RightButton;
            this.Middle = ms.MiddleButton;
            this.XButton1 = ms.XButton1;
            this.XButton2 = ms.XButton2;
        }
    }

    public class MouseEventArgs : EventArgs
    {
        public MouseEventArgs(Button button, Vector2 position, int scrollwheeldelta)
        {
            this.Button = button;
            this.Position = position;
            this.ScrollWheelDelta = scrollwheeldelta;
        }
        public Button Button { get; private set; }
        public Vector2 Position { get; private set; }
        public int ScrollWheelDelta { get; private set; }
    }
}
