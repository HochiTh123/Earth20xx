using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class KeyboardController
    {
        public KeyboardController()
        {
            okeys = new bool[255];
            nkeys = new bool[255];
            for (int i = 0; i <= okeys.GetUpperBound(0); i++)
            {
                okeys[i] = false;
                nkeys[i] = false;
            }
        }

        public event EventHandler<KeyboardEventArgs> KeyUp;
        public event EventHandler<KeyboardEventArgs> KeyDown;
        private void OnKeyUp(KeyboardEventArgs e)
        {
            KeyUp?.Invoke(this, e);
        }
        private void OnKeyDown(KeyboardEventArgs e)
        {
            KeyDown?.Invoke(this, e);
        }

        private readonly bool[] okeys;
        private readonly bool[] nkeys;

        bool shift = false;
        bool ctrl = false;
        bool alt = false;

        public void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            shift = ks.IsKeyDown(Keys.LeftShift) || ks.IsKeyDown(Keys.RightShift);
            alt = ks.IsKeyDown(Keys.LeftAlt) || ks.IsKeyDown(Keys.RightAlt);
            ctrl = ks.IsKeyDown(Keys.LeftControl) || ks.IsKeyDown(Keys.RightControl);

            var pressedkeys = ks.GetPressedKeys();
            for (int i = 0; i <= pressedkeys.GetUpperBound(0); i++)
            {
                nkeys[(int)pressedkeys[i]] = true;
            }
            for (int i = 0; i <= nkeys.GetUpperBound(0); i++)
            {
                if ((i < 160) || (i > 165))
                {
                    if (nkeys[i] == true && okeys[i] == false) // KEYDOWN
                    {
                        KeyboardEventArgs e = new((Keys)i, shift, alt, ctrl);
                        OnKeyDown(e);
                    }
                    if (nkeys[i] == false && okeys[i] == true) // KEyUp
                    {
                        KeyboardEventArgs e = new((Keys)i, shift, alt, ctrl);
                        OnKeyUp(e);
                    }
                }
                okeys[i] = nkeys[i];
                nkeys[i] = false;
            }
        }
    }


    public class KeyboardEventArgs : EventArgs
    {
        public KeyboardEventArgs(Keys key, bool shift, bool alt, bool ctrl) : base()
        {
            this.Keys = key;
            this.Alt = alt;
            this.Shift = shift;
            this.Ctrl = ctrl;

        }
        public Keys Keys { get; private set; }
        public bool Shift { get; private set; }
        public bool Alt { get; private set; }
        public bool Ctrl { get; private set; }
    }
}
