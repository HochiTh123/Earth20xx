using Controller;
using Earth20xx.States;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Earth20xx.Engine
{
    public class StateMachine
    {
        public StateMachine()
        {

        }
       public Myra.Graphics2D.UI.Panel SceneDrawPanel { get; private set; }
        public List<IScene> AvailableScenes = new List<IScene>();
        public IScene CurrentScene = null;
        public void Init()
        {
            this.SceneDrawPanel = MainClass.Instance.SceneDrawPanel;
            AvailableScenes.Add(new LoadingScene());
            AvailableScenes.Add(new MenuScene());
            AvailableScenes.Add(new OfflineCreateScene());
            MainClass.Instance.MouseController.MouseMove += new EventHandler<Controller.MouseEventArgs>(Mouse_Move);
            MainClass.Instance.MouseController.MouseDown += new EventHandler<Controller.MouseEventArgs>(Mouse_Down);
            MainClass.Instance.MouseController.MouseUp += new EventHandler<Controller.MouseEventArgs>(Mouse_Up);
            MainClass.Instance.MouseController.MouseScroll += new EventHandler<Controller.MouseEventArgs>(Mouse_Scroll);
            MainClass.Instance.KeyboardController.KeyDown += new EventHandler<Controller.KeyboardEventArgs>(Keyboard_Down);
            MainClass.Instance.KeyboardController.KeyUp += new EventHandler<Controller.KeyboardEventArgs>(Keyboard_Up);
        }

        public void SetState(int index)
        {
            if (index >= 0 && index < this.AvailableScenes.Count)
            {
                CurrentScene?.Unload();
                SceneDrawPanel.Widgets.Clear();
                CurrentScene = AvailableScenes[index];
                CurrentScene.Init();
            }
        }
        public int GetIndex(string tag)
        {
            for (int i = 0; i < AvailableScenes.Count;i++)
            {
                if (AvailableScenes[i].GetTag().ToUpper()==tag.ToUpper())
                {
                    return i;
                }
            }
            return -1;
        }
        public void SetState(string tag)
        {
            SetState(GetIndex(tag));
        }

        public void Update(GameTime gameTime)
        {
            CurrentScene?.Update(gameTime);
        }
        public void Draw(SpriteBatch spritebatch, GameTime gameTime)
        {
            CurrentScene?.Draw(spritebatch, gameTime);
        }

        private void Mouse_Move(object sender, MouseEventArgs e)
        {
            CurrentScene?.Mouse_Move(e);
        }
        private void Mouse_Up(object sender, MouseEventArgs e)
        {
            CurrentScene?.Mouse_Up(e);
        }
        private void Mouse_Down(object sender, MouseEventArgs e)
        {
            CurrentScene?.Mouse_Down(e);
        }
        private void Mouse_Scroll(object sender, MouseEventArgs e)
        {
            CurrentScene?.Mouse_Scroll(e);
        }
        private void Keyboard_Up(object sender, KeyboardEventArgs e)
        {
            CurrentScene?.Keyboard_Up(e);
        }
        private void Keyboard_Down(object sender, KeyboardEventArgs e)
        {
            CurrentScene?.Keyboard_Down(e);
        }

    }

    public interface IScene
    {
        public string GetTag();
        public void Init();
        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch spritebatch, GameTime gameTime);
        public void Unload();
        public void Mouse_Move(MouseEventArgs e);
        public void Mouse_Down(MouseEventArgs e);
        public void Mouse_Up(MouseEventArgs e);
        public void Mouse_Scroll(MouseEventArgs e);
        public void Keyboard_Up(KeyboardEventArgs e);
        public void Keyboard_Down(KeyboardEventArgs e);

    }
}
