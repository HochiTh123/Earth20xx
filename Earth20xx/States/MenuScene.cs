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
using System.Runtime.InteropServices.ObjectiveC;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Earth20xx.States
{
    public class MenuScene : Engine.IScene
    {
        public void Draw(SpriteBatch spritebatch, GameTime gameTime)
        {
            spritebatch.Draw(Background, new Rectangle(0, 0, MainClass.Instance.Device.Viewport.Width, MainClass.Instance.Device.Viewport.Height), Color.White);
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
        public Myra.Graphics2D.UI.ComboView Language;
        public Myra.Graphics2D.UI.Button SinglePlayer;
        public Myra.Graphics2D.UI.Button DEV_MAPTEST;
        public Texture2D Background;
        public void Init()
        {
            Background = MainClass.Instance.TextureController.LoadTileSheet(MainClass.Instance.Content.RootDirectory + "/textures/background.png", MainClass.Instance.Device);
            MainClass.Instance.Camera2D.Position = new Vector2(MainClass.Instance.Device.Viewport.Width / 2, MainClass.Instance.Device.Viewport.Height / 2);
            VersionsLabel = new Myra.Graphics2D.UI.Label()
            {
                Top = 10,
                Left = 0,
                Width = 100,
                Height = 20,
                Text = MainClass.Instance.LanguageManager.GetString("str_version") + MainClass.Instance.Version
            };
           

            HeaderLabel = new Myra.Graphics2D.UI.Label()
            {
                Top = 10,
                Left = MainClass.Instance.CenterY - 100,
                Width = 200,
                Height = 20,
                Text = MainClass.Instance.LanguageManager.GetString("str_gamename"),
                TextAlign = FontStashSharp.RichText.TextHorizontalAlignment.Center
            };
            SinglePlayer = new Myra.Graphics2D.UI.Button()
            {
                Top = 40,
                Left = MainClass.Instance.CenterX - 100,
                Width = 200,
                Height = 20,
                Content = new Myra.Graphics2D.UI.Label() { Width = 200, Text = MainClass.Instance.LanguageManager.GetString("str_singleplayer"), TextAlign = FontStashSharp.RichText.TextHorizontalAlignment.Center }
            };
            DEV_MAPTEST = new Myra.Graphics2D.UI.Button()
            {
                Top = 300,
                Left = 10,
                Width = 200,
                Height = 20,
                Content = new Myra.Graphics2D.UI.Label() { Width = 200, Text = "MAPTEST", TextAlign = FontStashSharp.RichText.TextHorizontalAlignment.Center }
            };

            OfflineGameButton = new Myra.Graphics2D.UI.Button()
            {
                Top = 70,
                Left = MainClass.Instance.CenterY - 100,
                Width = 200,
                Height = 20,
                Content = new Myra.Graphics2D.UI.Label() { Width = 200, Text = MainClass.Instance.LanguageManager.GetString("str_offline"), TextAlign = FontStashSharp.RichText.TextHorizontalAlignment.Center }

            };
            SettingsButton = new Myra.Graphics2D.UI.Button()
            {
                Top = 100,
                Left = MainClass.Instance.CenterY - 100,
                Width = 200,
                Height = 20,
                Content = new Myra.Graphics2D.UI.Label() { Width = 200, Text = MainClass.Instance.LanguageManager.GetString("str_settings"), TextAlign = FontStashSharp.RichText.TextHorizontalAlignment.Center }
            };
            ExitButton = new Myra.Graphics2D.UI.Button()
            {
                Top = 130,
                Left = MainClass.Instance.CenterY - 100,
                Width = 200,
                Height = 20,
                Content = new Myra.Graphics2D.UI.Label() { Width = 200, Text = MainClass.Instance.LanguageManager.GetString("str_exit"), TextAlign = FontStashSharp.RichText.TextHorizontalAlignment.Center }
            };
            Language = new Myra.Graphics2D.UI.ComboView()
            {
                Top = 20,
                Left = MainClass.Instance.Device.Viewport.Width - 200,
                Width = 150,
                Height = 20,

            };
           for (int i = 0; i < MainClass.Instance.LanguageManager.Languages.Count;i++)
            {
                Language.Widgets.Add(new Myra.Graphics2D.UI.Label() { Text = MainClass.Instance.LanguageManager.Languages[i].Name });
            }
            Language.SelectedIndex = MainClass.Instance.LanguageManager.LangIndex;

            MainClass.Instance.SceneDrawPanel.Widgets.Add(VersionsLabel);
            MainClass.Instance.SceneDrawPanel.Widgets.Add(HeaderLabel);
            MainClass.Instance.SceneDrawPanel.Widgets.Add(OfflineGameButton);
            MainClass.Instance.SceneDrawPanel.Widgets.Add(SinglePlayer);
            MainClass.Instance.SceneDrawPanel.Widgets.Add(SettingsButton);
            MainClass.Instance.SceneDrawPanel.Widgets.Add(ExitButton);
            MainClass.Instance.SceneDrawPanel.Widgets.Add(Language);
            MainClass.Instance.SceneDrawPanel.Widgets.Add(DEV_MAPTEST);
            Language.SelectedIndexChanged += new EventHandler(LanguageChanged);
            OfflineGameButton.Click += new EventHandler(OfflineCreateButton);
            SinglePlayer.Click += new EventHandler(SinglePlayerClicked);
            ExitButton.Click += new EventHandler(Exit_Clicked);
            SettingsButton.Click += new EventHandler(Settings_Clicked);
            DEV_MAPTEST.Click += new EventHandler(DEV_MAPTEST_CLICKED);

        }
        private void DEV_MAPTEST_CLICKED(object sender, EventArgs e)
        {
            MainClass.Instance.StateMachine.SetState("MAPTEST");
        }
        private void Settings_Clicked(object sender, EventArgs e)
        {
            MainClass.Instance.StateMachine.SetState("Settings");
        }
        private void Exit_Clicked(object sender, EventArgs e)
        {
            MainClass.Instance.Game.Exit();
        }
        private void SinglePlayerClicked(object sender, EventArgs e)
        {
            MainClass.Instance.StateMachine.SetState("SINGLEPLAYERCREATE");
        }
        private void LanguageChanged(object sender, EventArgs e)
        {
            MainClass.Instance.LanguageManager.LangIndex = (int)Language.SelectedIndex;
            VersionsLabel.Text = MainClass.Instance.LanguageManager.GetString("str_version") +" "+ MainClass.Instance.Version;
            HeaderLabel.Text = MainClass.Instance.LanguageManager.GetString("str_gamename");
            var label = (Myra.Graphics2D.UI.Label)OfflineGameButton.Content;
            label.Text = MainClass.Instance.LanguageManager.GetString("str_offline");
            label = (Myra.Graphics2D.UI.Label)SettingsButton.Content;
            label.Text = MainClass.Instance.LanguageManager.GetString("str_settings");
            label = (Myra.Graphics2D.UI.Label)ExitButton.Content;
            label.Text = MainClass.Instance.LanguageManager.GetString("str_exit");
            label = (Myra.Graphics2D.UI.Label)SinglePlayer.Content;
            label.Text = MainClass.Instance.LanguageManager.GetString("str_singleplayer");
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
