using Earth20xx.Engine;
using FontStashSharp.RichText;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;

namespace Earth20xx.States
{
    public class SinglePlayerCreateScene : IScene
    {
        public void Draw(SpriteBatch spritebatch, GameTime gameTime)
        {
            
        }

        public string GetTag()
        {
            return "SINGLEPLAYERCREATE";
        }
        public Myra.Graphics2D.UI.Label HeaderLabel;
        public Myra.Graphics2D.UI.Button BackButton;
        public Myra.Graphics2D.UI.ScrollViewer PlayerScrollView;
        public Myra.Graphics2D.UI.Panel PlayerPanel;
        public Myra.Graphics2D.UI.ScrollViewer GameSettingsView;
        public Myra.Graphics2D.UI.Panel GameSettingsPanel;
        public void Init()
        {
            MainClass.Instance.CurrentSession = new GameData.Session();
            MainClass.Instance.CurrentSession.ResetSession(GameData.SessionType.Offline);
            MainClass.Instance.CurrentSession.SetPlayerCount(2);
            MainClass.Instance.Camera2D.Position = new Vector2(MainClass.Instance.Device.Viewport.Width / 2, MainClass.Instance.Device.Viewport.Width / 2);

            HeaderLabel = new Myra.Graphics2D.UI.Label()
            {
                Top = 10,
                Left = MainClass.Instance.CenterX - 100,
                Width = 200,
                Text = MainClass.Instance.LanguageManager.GetString("Str_createofflinegame"),
                TextAlign = FontStashSharp.RichText.TextHorizontalAlignment.Center
            };

            BackButton = new Myra.Graphics2D.UI.Button()
            {
                Top = 10,
                Left = 10,
                Width = 100,
                Content = new Myra.Graphics2D.UI.Label() { Text = MainClass.Instance.LanguageManager.GetString("str_back"), Width = 100, TextAlign = FontStashSharp.RichText.TextHorizontalAlignment.Center }
            };
            PlayerScrollView = new Myra.Graphics2D.UI.ScrollViewer()
            {
                Top = 50,
                Left = 0,
                Width = MainClass.Instance.CenterX - 2,
                Height = MainClass.Instance.CenterY - 50,
                Border = new Myra.Graphics2D.Brushes.SolidBrush(Color.White),
                BorderThickness = new Myra.Graphics2D.Thickness(1)
            };
            PlayerPanel = new Myra.Graphics2D.UI.Panel()
            {
                Top = 0,
                Width = 820,
                Height = 500,
                Left = 0
            };
            GameSettingsView = new Myra.Graphics2D.UI.ScrollViewer()
            {
                Top = 50,
                Left = MainClass.Instance.CenterX + 2,
                Width = MainClass.Instance.CenterX - 10,
                Height = MainClass.Instance.CenterY - 100,
                Border = new Myra.Graphics2D.Brushes.SolidBrush(Color.White),
                BorderThickness = new Myra.Graphics2D.Thickness(1)
            };
            PlayerScrollView.Content = PlayerPanel;
            MainClass.Instance.SceneDrawPanel.Widgets.Add(HeaderLabel);
            BackButton.Click += new EventHandler(BackClicked);
            MainClass.Instance.SceneDrawPanel.Widgets.Add(BackButton);
            MainClass.Instance.SceneDrawPanel.Widgets.Add(PlayerScrollView);
            MainClass.Instance.SceneDrawPanel.Widgets.Add(GameSettingsView);
            DrawPlayers();
            DrawGame();
        }
        public void DrawPlayers()
        {
            PlayerPanel.Widgets.Clear();
        }
      
      
        public void DrawGame()
        {
            if (GameSettingsPanel == null)
                GameSettingsPanel = new Panel();
            GameSettingsPanel.Widgets.Clear();
            GameSettingsView.Content = GameSettingsPanel;

            Myra.Graphics2D.UI.Label maptypelabel = new Label()
            {
                Top = 10,
                Left = 2,
                Width = 100,
                Text = MainClass.Instance.LanguageManager.GetString("str_maptype")
            };
            Myra.Graphics2D.UI.ComboView maptypeview = new ComboView()
            {
                Top = 10,
                Left = 107,
                Width = 100
            };
            maptypeview.Widgets.Add(new Label() { Text = MainClass.Instance.LanguageManager.GetString("str_random") });
            maptypeview.SelectedIndex = 0;

            Myra.Graphics2D.UI.Label mapsizelabel = new Label()
            {
                Top = 10,
                Left = 300,
                Width = 100,
                Text = MainClass.Instance.LanguageManager.GetString("str_mapsize")
            };
            Myra.Graphics2D.UI.ComboView mapsizeview = new ComboView()
            {
                Top = 10,
                Left = 405,
                Width = 100,

            };
            mapsizeview.Widgets.Add(new Label() { Text = MainClass.Instance.LanguageManager.GetString("str_verysmall") });
            mapsizeview.Widgets.Add(new Label() { Text = MainClass.Instance.LanguageManager.GetString("str_small") });
            mapsizeview.Widgets.Add(new Label() { Text = MainClass.Instance.LanguageManager.GetString("str_standard") });
            mapsizeview.Widgets.Add(new Label() { Text = MainClass.Instance.LanguageManager.GetString("str_big") });
            mapsizeview.Widgets.Add(new Label() { Text = MainClass.Instance.LanguageManager.GetString("str_huge") });
            mapsizeview.Widgets.Add(new Label() { Text = MainClass.Instance.LanguageManager.GetString("str_gigantic") });

            mapsizeview.SelectedIndex = MainClass.Instance.CurrentSession.MapSize;
            Myra.Graphics2D.UI.Label victorytypelabel = new Label()
            {
                Top = 40,
                Left = 2,
                Width = 100,
                Text = MainClass.Instance.LanguageManager.GetString("str_victorytype")
            };
            Myra.Graphics2D.UI.ComboView victorytypeview = new ComboView()
            {
                Top = 40,
                Left = 107,
                Width = 100
            };
            victorytypeview.Widgets.Add(new Label() { Text = MainClass.Instance.LanguageManager.GetString("str_points")});
            victorytypeview.SelectedIndex = 0;


            Myra.Graphics2D.UI.Label roundlimitlabel = new Label()
            {
                Top = 70,
                Left = 2,
                Width = 100,
                Text = MainClass.Instance.LanguageManager.GetString("str_roundlimit")+":" + MainClass.Instance.CurrentSession.RoundLimit.ToString()
            };
            Myra.Graphics2D.UI.HorizontalSlider roundlimitslider = new HorizontalSlider()
            {
                Top = 70,
                Left = 202,
                Width = 200,
                Minimum = 0,
                Maximum = 1000,
                Value = 0
            };
             


            GameSettingsPanel.Widgets.Add(maptypelabel);
            GameSettingsPanel.Widgets.Add(maptypeview);
            GameSettingsPanel.Widgets.Add(mapsizeview);
            GameSettingsPanel.Widgets.Add(mapsizelabel);
            GameSettingsPanel.Widgets.Add(victorytypelabel);
            GameSettingsPanel.Widgets.Add(victorytypeview);
            GameSettingsPanel.Widgets.Add(roundlimitslider);
            GameSettingsPanel.Widgets.Add(roundlimitlabel);
            roundlimitslider.ValueChanged += new EventHandler<Myra.Events.ValueChangedEventArgs<float>>(roundlimitchanged);
        }
        private void roundlimitchanged(object sender, Myra.Events.ValueChangedEventArgs<float> e)
        {
            MainClass.Instance.CurrentSession.RoundLimit = (int)e.NewValue;
            var l = (Label)GameSettingsPanel.Widgets[7];
            l.Text = MainClass.Instance.LanguageManager.GetString("str_roundlimit")+":" + MainClass.Instance.CurrentSession.RoundLimit.ToString();
        }
        private void TeamChanged(object sender, EventArgs e)
        {
            var cb = (ListView)sender;
            foreach (var p in MainClass.Instance.CurrentSession.Players)
            {
                if (p.PlayerID.ToString() == cb.Tag.ToString())
                {
                    p.Team = (int)cb.SelectedIndex;
                }
            }
            DrawPlayers();
        }
        private void ColorIndexChanged(object sender, EventArgs e)
        {
            var cb = (ListView)sender;
            foreach (var p in MainClass.Instance.CurrentSession.Players)
            {
                if (p.PlayerID.ToString() == cb.Tag.ToString())
                {
                    p.PlayerColor = MainClass.Instance.CurrentSession.AvailableColors[(int)cb.SelectedIndex-1];
                  


                }
            }
            MainClass.Instance.CurrentSession.ResetPlayerColors();
            DrawPlayers();
        }
        private void PlayerNameChanged(object sender, Myra.Events.ValueChangedEventArgs<string> e)
        {
            TextBox tb = (TextBox)sender;
            foreach(var p in MainClass.Instance.CurrentSession.Players)
            {
                if (p.PlayerID.ToString()==tb.Tag.ToString())
                {
                    p.PlayerName = e.NewValue;
                }
            }
        }


        private void BackClicked(object sender, EventArgs e)
        {
            MainClass.Instance.StateMachine.SetState(0);
        }
        public void Unload()
        {
            MainClass.Instance.SceneDrawPanel.Widgets.Remove(HeaderLabel);
            BackButton.Click -= new EventHandler(BackClicked);
            MainClass.Instance.SceneDrawPanel.Widgets.Remove(BackButton);
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Mouse_Move(Controller.MouseEventArgs e)
        {
            
        }

        public void Mouse_Down(Controller.MouseEventArgs e)
        {
            
        }

        public void Mouse_Up(Controller.MouseEventArgs e)
        {
            
        }

        public void Mouse_Scroll(Controller.MouseEventArgs e)
        {
            
        }

        public void Keyboard_Up(Controller.KeyboardEventArgs e)
        {
           
        }

        public void Keyboard_Down(Controller.KeyboardEventArgs e)
        {
            
        }
    }
}
