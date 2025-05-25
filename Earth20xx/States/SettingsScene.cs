using Controller;
using Earth20xx.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Earth20xx.States
{
    public class SettingsScene : Engine.IScene
    {
        public void Draw(SpriteBatch spritebatch, GameTime gameTime)
        {
        }

        public string GetTag()
        {
            return "SETTINGS";
        }
        public Myra.Graphics2D.UI.Label HeaderLabel;
        public Myra.Graphics2D.UI.Button BackButton;
       
        public void Init()
        {
          
           

            HeaderLabel = new Myra.Graphics2D.UI.Label()
            {
                Top = 10,
                Left = MainClass.Instance.CenterY - 100,
                Width = 200,
                Height = 20,
                Text = MainClass.Instance.LanguageManager.GetString("str_settings"),
                TextAlign = FontStashSharp.RichText.TextHorizontalAlignment.Center
            };
            BackButton = new Myra.Graphics2D.UI.Button()
            {
                Top = 10,
                Left = 10,
                Width = 100,
                Content = new Myra.Graphics2D.UI.Label() { Text = MainClass.Instance.LanguageManager.GetString("str_back"), Width = 100, TextAlign = FontStashSharp.RichText.TextHorizontalAlignment.Center }
            };
            MainClass.Instance.SceneDrawPanel.Widgets.Add(HeaderLabel);
            MainClass.Instance.SceneDrawPanel.Widgets.Add(BackButton);
            BackButton.Click += new EventHandler(BackButton_Clicked);
          
        }
       private void BackButton_Clicked(object sender, EventArgs e)
        {
            MainClass.Instance.StateMachine.SetState("MENU");
        }
        public void Unload()
        {
         

            MainClass.Instance.SceneDrawPanel.Widgets.Remove(HeaderLabel);
        
        }

        public void Update(GameTime gameTime)
        {
           
        }

        public void Mouse_Move(MouseEventArgs e)
        {
            
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
