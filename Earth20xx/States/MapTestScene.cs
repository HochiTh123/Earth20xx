using Controller;
using Earth20xx.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Earth20xx.States
{
    public class MapTestScene : Engine.IScene
    {
        public void Draw(SpriteBatch spritebatch, GameTime gameTime)
        {
            MainClass.Instance.Map?.Draw(spritebatch, gameTime);
        }

        public string GetTag()
        {
            return "MAPTEST";
        }
        public Myra.Graphics2D.UI.Button BackButton;
       
        public void Init()
        {
            if (MainClass.Instance.Map == null)
            {
                MainClass.Instance.Map = new GameData.Map();
            }
            MainClass.Instance.Map.Init();
            MainClass.Instance.Map.Generate(100, 100);
            BackButton = new Myra.Graphics2D.UI.Button()
            {
                Top = 0,
                Left = 0,
                Width = 100,
                Height = 20,
                Content = new Myra.Graphics2D.UI.Label() { Text = MainClass.Instance.LanguageManager.GetString("str_back"), Width = 100, HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center }
            };
            
            MainClass.Instance.SceneDrawPanel.Widgets.Add(BackButton);
            BackButton.Click += new EventHandler(Back_Clicked);
        }
        private void Back_Clicked(object sender, EventArgs e)
        {
            MainClass.Instance.StateMachine.SetState("MENU");
        }
        public void Unload()
        {
            

            MainClass.Instance.SceneDrawPanel.Widgets.Remove(BackButton);
            
        }

        public void Update(GameTime gameTime)
        {
            MainClass.Instance.Map?.Update(gameTime);
        }

        public void Mouse_Move(MouseEventArgs e)
        {
            MainClass.Instance.Map?.MouseMove(e);
        }

        public void Mouse_Down(MouseEventArgs e)
        {
           
        }

        public void Mouse_Up(MouseEventArgs e)
        {
            
        }

        public void Mouse_Scroll(MouseEventArgs e)
        {
            
        }

        public void Keyboard_Up(KeyboardEventArgs e)
        {
            
        }

        public void Keyboard_Down(KeyboardEventArgs e)
        {
           
        }
    }
}
