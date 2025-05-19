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
    public class OfflineCreateScene : IScene
    {
        public void Draw(SpriteBatch spritebatch, GameTime gameTime)
        {
            
        }

        public string GetTag()
        {
            return "OFFLINECREATE";
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
                Text = "Create offline Game",
                TextAlign = FontStashSharp.RichText.TextHorizontalAlignment.Center
            };

            BackButton = new Myra.Graphics2D.UI.Button()
            {
                Top = 10,
                Left = 10,
                Width = 100,
                Content = new Myra.Graphics2D.UI.Label() { Text = "< BACK", Width = 100, TextAlign = FontStashSharp.RichText.TextHorizontalAlignment.Center }
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
            PlayerPanel.Widgets.Add(new Myra.Graphics2D.UI.Label() { Top = 0, Left = 2, Width = 100, Text = "Player" });
            PlayerPanel.Widgets.Add(new Myra.Graphics2D.UI.Label() { Top = 0, Left = 104, Width = 100, Text = "Color" });
            PlayerPanel.Widgets.Add(new Myra.Graphics2D.UI.Label() { Top = 0, Left = 206, Width = 100, Text = "Team" });
            PlayerPanel.Widgets.Add(new Myra.Graphics2D.UI.Label() { Top = 0, Left = 308, Width = 100, Text = "Fraction" });
            PlayerPanel.Widgets.Add(new Myra.Graphics2D.UI.Label() { Top = 0, Left = 410, Width = 100, Text = "Type" });
            int top = 30;
            Button minusbutton = new Button()
            {
                Top = 2,
                Left = 720,
                Width = 50,
                Height = 20,
                Content = new Label() { Text = "-" }
            };
            PlayerPanel.Widgets.Add(minusbutton);
            if (MainClass.Instance.CurrentSession.Players.Count <= 2)
                minusbutton.Enabled = true;
            else
                minusbutton.Enabled = true;
            Button plusbutton = new Button()
            {
                Top = 2,
                Left = 780,
                Width = 50,
                Height = 20,
                Content = new Label() { Text = "+" }
            };
            Button twoTeams = new Button()
            {
                Top = 40,
                Left = 720,
                Width = 100,
                Height = 20,
                Content = new Label() { Text = "2 Teams" }
            };
            PlayerPanel.Widgets.Add(twoTeams);
            Button NoTeams = new Button()
            {
                Top = 70,
                Left = 720,
                Width = 100,
                Height = 20,
                Content = new Label() { Text = "no Teams" }
            };
            PlayerPanel.Widgets.Add(NoTeams);

            PlayerPanel.Widgets.Add(plusbutton);
            if (MainClass.Instance.CurrentSession.Players.Count >= 12)
            {
                plusbutton.Enabled = true;
            }
            else
            {
                plusbutton.Enabled = true;
            }
            minusbutton.Click += new EventHandler(MinusButton_CLicked);
            plusbutton.Click += new EventHandler(PlusButton_Clicked);
            twoTeams.Click += new EventHandler(TwoTeams_Clicked);
            NoTeams.Click += new EventHandler(NoTeams_Clicked);
                foreach (var p in MainClass.Instance.CurrentSession.Players)
                {
                    Myra.Graphics2D.UI.TextBox tb = new Myra.Graphics2D.UI.TextBox()
                    {
                        Top = top,
                        Left = 2,
                        Width = 100,
                        Text = p.PlayerName,
                        Tag = p.PlayerID.ToString()
                    };
                    tb.TextChanged += new EventHandler<Myra.Events.ValueChangedEventArgs<string>>(PlayerNameChanged);

                    Myra.Graphics2D.UI.ComboView cb = new ComboView()
                    {
                        Top = top,
                        Left = 104,
                        Width = 100,
                        Tag = p.PlayerID.ToString()


                    };
                    cb.ListView.Tag = p.PlayerID.ToString();
                    cb.Widgets.Add(new Myra.Graphics2D.UI.Label() { Text = p.PlayerColor.GetColorName(), TextColor = p.PlayerColor });
                    foreach (var c in MainClass.Instance.CurrentSession.AvailableColors)
                    {
                        cb.Widgets.Add(new Myra.Graphics2D.UI.Label() { Text = c.GetColorName(), TextColor = c });
                    }
                    cb.SelectedIndex = 0;

                    Myra.Graphics2D.UI.ComboView teambox = new ComboView()
                    {
                        Top = top,
                        Left = 206,
                        Width = 100
                    };
                    teambox.ListView.Tag = p.PlayerID.ToString();
                    teambox.Widgets.Add(new Label() { Text = "NONE" });
                    teambox.Widgets.Add(new Label() { Text = "A" });
                    teambox.Widgets.Add(new Label() { Text = "B" });
                    teambox.Widgets.Add(new Label() { Text = "C" });
                    teambox.Widgets.Add(new Label() { Text = "D" });
                    teambox.SelectedIndex = p.Team;

                    Myra.Graphics2D.UI.ComboView FractionBox = new ComboView()
                    {
                        Top = top,
                        Left = 308,
                        Width = 100
                    };
                    FractionBox.ListView.Tag = p.PlayerID.ToString();
                FractionBox.Widgets.Add(new Label() { Text = "Random" });
                    FractionBox.Widgets.Add(new Label() { Text = "EDF" });
                    FractionBox.SelectedIndex = 0;

                    Myra.Graphics2D.UI.ComboView TypeBox = new ComboView()
                    {
                        Top = top,
                        Left = 410,
                        Width = 100
                    };
                    TypeBox.ListView.Tag = p.PlayerID.ToString();
                    TypeBox.Widgets.Add(new Label() { Text = "LOCAL" });
                    TypeBox.SelectedIndex = 0;
                    PlayerPanel.Widgets.Add(tb);
                    PlayerPanel.Widgets.Add(cb);

                    cb.SelectedIndexChanged += new EventHandler(ColorIndexChanged);

                    PlayerPanel.Widgets.Add(teambox);
                    teambox.SelectedIndexChanged += new EventHandler(TeamChanged);

                    PlayerPanel.Widgets.Add(FractionBox);

                    PlayerPanel.Widgets.Add(TypeBox);



                    top += 30;
                }
        }
        private void NoTeams_Clicked(object sender, EventArgs e)
        {
            for (int i = 0; i < MainClass.Instance.CurrentSession.Players.Count;i++)
            {
                MainClass.Instance.CurrentSession.Players[i].Team = 0;
            }
            DrawPlayers();
        }
        private void TwoTeams_Clicked(object sender, EventArgs e)
        {
            int center = MainClass.Instance.CurrentSession.Players.Count / 2;
            for (int i = 0; i < MainClass.Instance.CurrentSession.Players.Count;i++)
            {
                if (i < center)
                {
                    MainClass.Instance.CurrentSession.Players[i].Team = 1;
                }
                else
                {
                    MainClass.Instance.CurrentSession.Players[i].Team = 2;
                }
            }
            DrawPlayers();
        }
        private void PlusButton_Clicked(object sender, EventArgs e)
        {
            if (MainClass.Instance.CurrentSession.Players.Count < 12)
            {
                MainClass.Instance.CurrentSession.SetPlayerCount(MainClass.Instance.CurrentSession.Players.Count + 1);
                DrawPlayers();
            }
        }
        private void MinusButton_CLicked(object sender, EventArgs e)
        {
            if (MainClass.Instance.CurrentSession.Players.Count > 2)
            {
                MainClass.Instance.CurrentSession.SetPlayerCount(MainClass.Instance.CurrentSession.Players.Count - 1);
                DrawPlayers();
            }
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
                Text = "Map type"
            };
            Myra.Graphics2D.UI.ComboView maptypeview = new ComboView()
            {
                Top = 10,
                Left = 107,
                Width = 100
            };
            maptypeview.Widgets.Add(new Label() { Text = "Random" });
            maptypeview.SelectedIndex = 0;

            Myra.Graphics2D.UI.Label mapsizelabel = new Label()
            {
                Top = 10,
                Left = 300,
                Width = 100,
                Text = "Map Size"
            };
            Myra.Graphics2D.UI.ComboView mapsizeview = new ComboView()
            {
                Top = 10,
                Left = 405,
                Width = 100,

            };
            mapsizeview.Widgets.Add(new Label() { Text = "very small" });
            mapsizeview.Widgets.Add(new Label() { Text = "small" });
            mapsizeview.Widgets.Add(new Label() { Text = "standard" });
            mapsizeview.Widgets.Add(new Label() { Text = "large" });
            mapsizeview.Widgets.Add(new Label() { Text = "huge" });
            mapsizeview.Widgets.Add(new Label() { Text = "gigantic" });

            mapsizeview.SelectedIndex = MainClass.Instance.CurrentSession.MapSize;
            Myra.Graphics2D.UI.Label victorytypelabel = new Label()
            {
                Top = 40,
                Left = 2,
                Width = 100,
                Text = "Victory type"
            };
            Myra.Graphics2D.UI.ComboView victorytypeview = new ComboView()
            {
                Top = 40,
                Left = 107,
                Width = 100
            };
            victorytypeview.Widgets.Add(new Label() { Text = "Points" });
            victorytypeview.SelectedIndex = 0;


            Myra.Graphics2D.UI.Label roundlimitlabel = new Label()
            {
                Top = 70,
                Left = 2,
                Width = 100,
                Text = "Roundlimit: " + MainClass.Instance.CurrentSession.RoundLimit.ToString()
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
            l.Text = "Roundlimit: " + MainClass.Instance.CurrentSession.RoundLimit.ToString();
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
    }
}
