using Earth20xx.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Earth20xx.States
{
    public class MenuScene : Engine.IScene
    {
        public void Draw(SpriteBatch spritebatch, GameTime gameTime)
        {
          
        }

        public string GetTag()
        {
            return "MENU";
        }
        public Myra.Graphics2D.UI.Label HeaderLabel;
        public Myra.Graphics2D.UI.Button OfflineGameButton;
        public Myra.Graphics2D.UI.Button SettingsButton;
        public Myra.Graphics2D.UI.Button ExitButton;
        public Myra.Graphics2D.UI.Label VersionsLabel;
        public void Init()
        {
            MainClass.Instance.Camera2D.Position = new Vector2(MainClass.Instance.Device.Viewport.Width / 2, MainClass.Instance.Device.Viewport.Height / 2);
            VersionsLabel = new Myra.Graphics2D.UI.Label()
            {
                Top = 10,
                Left = 0,
                Width = 100,
                Height = 20,
                Text = "Version: " + MainClass.Instance.Version
            };
           

            HeaderLabel = new Myra.Graphics2D.UI.Label()
            {
                Top = 10,
                Left = MainClass.Instance.CenterY - 100,
                Width = 200,
                Height = 20,
                Text = "Earth 20xx",
                TextAlign = FontStashSharp.RichText.TextHorizontalAlignment.Center
            };
            OfflineGameButton = new Myra.Graphics2D.UI.Button()
            {
                Top = 40,
                Left = MainClass.Instance.CenterY - 100,
                Width = 200,
                Height = 20,
                Content = new Myra.Graphics2D.UI.Label() { Width = 200, Text = "Offline Game", TextAlign = FontStashSharp.RichText.TextHorizontalAlignment.Center }

            };
            SettingsButton = new Myra.Graphics2D.UI.Button()
            {
                Top = 70,
                Left = MainClass.Instance.CenterY - 100,
                Width = 200,
                Height = 20,
                Content = new Myra.Graphics2D.UI.Label() { Width = 200, Text = "Settings", TextAlign = FontStashSharp.RichText.TextHorizontalAlignment.Center }
            };
            ExitButton = new Myra.Graphics2D.UI.Button()
            {
                Top = 100,
                Left = MainClass.Instance.CenterY - 100,
                Width = 200,
                Height = 20,
                Content = new Myra.Graphics2D.UI.Label() { Width = 200, Text = "Exit", TextAlign = FontStashSharp.RichText.TextHorizontalAlignment.Center }
            };


            MainClass.Instance.SceneDrawPanel.Widgets.Add(VersionsLabel);
            MainClass.Instance.SceneDrawPanel.Widgets.Add(HeaderLabel);
            MainClass.Instance.SceneDrawPanel.Widgets.Add(OfflineGameButton);
            MainClass.Instance.SceneDrawPanel.Widgets.Add(SettingsButton);
            MainClass.Instance.SceneDrawPanel.Widgets.Add(ExitButton);
            OfflineGameButton.Click += new EventHandler(OfflineCreateButton);
        }
        private void OfflineCreateButton(object sender, EventArgs e)
        {
            MainClass.Instance.StateMachine.SetState("OFFLINECREATE");
        }
        public void Unload()
        {
            OfflineGameButton.Click -= new EventHandler(OfflineCreateButton);

            MainClass.Instance.SceneDrawPanel.Widgets.Remove(HeaderLabel);
            MainClass.Instance.SceneDrawPanel.Widgets.Remove(OfflineGameButton);
            MainClass.Instance.SceneDrawPanel.Widgets.Remove(SettingsButton);
            MainClass.Instance.SceneDrawPanel.Widgets.Remove(ExitButton);
        }

        public void Update(GameTime gameTime)
        {
           
        }
    }
}
