using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Earth20xx.GameData
{
    public enum SessionType { Offline, Online}
    public class Session
    {
        public Session()
        {
            AllPlayerColors = new List<Color>();
            AllPlayerColors.Add(Color.Blue);
            AllPlayerColors.Add(Color.Red);
            AllPlayerColors.Add(Color.Green);
            AllPlayerColors.Add(Color.Violet);

            AllPlayerColors.Add(Color.LightBlue);
            AllPlayerColors.Add(Color.Orange);
            AllPlayerColors.Add(Color.LightGreen);
            AllPlayerColors.Add(Color.PaleVioletRed);

            AllPlayerColors.Add(Color.DarkBlue);
            AllPlayerColors.Add(Color.DarkRed);
            AllPlayerColors.Add(Color.DarkGreen);
            AllPlayerColors.Add(Color.DarkViolet);

            AllPlayerColors.Add(Color.Fuchsia);
            AllPlayerColors.Add(Color.Bisque);
            AllPlayerColors.Add(Color.BurlyWood);
            AllPlayerColors.Add(Color.Chartreuse);

            AllPlayerColors.Add(Color.DeepSkyBlue);
            AllPlayerColors.Add(Color.DeepPink);
            AllPlayerColors.Add(Color.DarkSlateGray);
            AllPlayerColors.Add(Color.Olive);

            ResetPlayerColors();
            
        }
        public Guid SessionID { get;  set; }
        public SessionType SessionType { get;  set; }
        public List<Color> AllPlayerColors { get;  set; }
        public List<Color> AvailableColors { get;  set; }
        public List<Player> Players { get;  set; }
        public int CurrentGameType { get;  set; } = 0;
        public int MapType { get;  set; } = 0;
        public int MapSize { get;  set; } = 0;
        public int VictoryType { get;  set; }
        public int RoundLimit { get;  set; }
        public void ResetPlayerColors()
        {
            if (AvailableColors == null)
                AvailableColors = new List<Color>();
            AvailableColors.Clear();
            for (int i = 0; i < AllPlayerColors.Count;i++)
            {
                AvailableColors.Add(AllPlayerColors[i]);
            }
            if (Players != null)
            {
                for (int i = 0; i < Players.Count;i++)
                {
                    AvailableColors.Remove(Players[i].PlayerColor);
                }
            }
        }

        public void ResetSession(SessionType type)
        {
            this.SessionID = System.Guid.NewGuid();
            this.SessionType = type;
            switch(this.SessionType)
            {
                case SessionType.Online:
                    break;

                case SessionType.Offline:
                    break;
            }
        }

        public void SetPlayerCount(int players)
        {
            if (Players == null)
                Players = new List<Player>();
            while(Players.Count > players)
            {
                Players.RemoveAt(Players.Count - 1);
            }
            while (Players.Count < players)
            {
                Players.Add(new Player()
                {
                    PlayerID = System.Guid.NewGuid(),
                    PlayerType = PlayerType.PERSON,
                    PlayerColor = AvailableColors[0],
                    PlayerName = "Player " + Players.Count.ToString()
                });
                ResetPlayerColors();
            }
        }
    }

    public enum PlayerType { PERSON, KI}
    public class Player
    {
        public Player()
        {

        }
        public Guid PlayerID { get; set; }
        public PlayerType PlayerType { get; set; }
        public Color PlayerColor { get; set; }
        public string PlayerName { get; set; }
        public int Team { get; set; } = 0;
    }
}
