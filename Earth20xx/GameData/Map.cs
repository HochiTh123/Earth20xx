using Controller;
using Earth20xx.Engine;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ObjectiveC;
using System.Security.Cryptography;
using System.Transactions;
using System.Xml.Serialization;

namespace Earth20xx.GameData
{
    public class Map
    {
        public bool Grid { get; set; } = false;
        public List<TilePrototype> Prototypes { get; set; }
        public List<WallPrototype> WallPrototypes { get; set; }

        public Myra.Graphics2D.UI.Window MapWindow = new Myra.Graphics2D.UI.Window();
        public Myra.Graphics2D.UI.Panel MapPanel = new Myra.Graphics2D.UI.Panel();
        public Myra.Graphics2D.UI.Button GridButton = new Myra.Graphics2D.UI.Button();
        public Myra.Graphics2D.UI.Label ZoomLabel = new Myra.Graphics2D.UI.Label();
        public Myra.Graphics2D.UI.HorizontalSlider ZoomSlider = new Myra.Graphics2D.UI.HorizontalSlider();
        public List<Engine.Characters.Character> Characters { get; set; }
        public Tile? MouseOverTile;
        public Texture2D Select;
        public Random Random { get; set; } = new Random();
        public void Init()
        {
            Select = MainClass.Instance.TextureController.LoadTileSheet(MainClass.Instance.Content.RootDirectory + "/Textures/cursor.png",
                MainClass.Instance.Device);
            if (Prototypes == null)
                Prototypes = new List<TilePrototype>();
            Prototypes.Clear();
            Prototypes.Add(new TilePrototype("dirt", "dirt_0"));
            Prototypes.Add(new TilePrototype("Sand", "sand_0"));
            if (WallPrototypes == null)
            {
                WallPrototypes = new List<WallPrototype>();
            }
            WallPrototypes.Clear();
            WallPrototypes.Add(new WallPrototype("wall", "wall_0_0"));
            WallPrototypes.Add(new WallPrototype("wall", "wall_0_1"));
            WallPrototypes.Add(new WallPrototype("wall", "wall_0_2"));
            WallPrototypes.Add(new WallPrototype("wall", "wall_0_3"));


            MapWindow = new Myra.Graphics2D.UI.Window()
            {
                Top = 0,
                Left = MainClass.Instance.Device.Viewport.Width - 210,
                Width = 210,
                Height = 110,
                Title = "Map Orders"
            };
            MapPanel = new Myra.Graphics2D.UI.Panel()
            {
                Top = 0,
                Left =0,
                Width = 200,
                Height = 100,

            };
            GridButton = new Myra.Graphics2D.UI.Button()
            {
                Top = 5,
                Left = 5,
                Width = 20,
                Height = 20,
                Content = new Myra.Graphics2D.UI.Label() { Text = "G" }
            };
            ZoomLabel = new Myra.Graphics2D.UI.Label()
            {
                Top = 30,
                Left = 5,
                Text = "ZOOM",
                Width = 50,
                Height = 20
            };
            ZoomSlider = new Myra.Graphics2D.UI.HorizontalSlider()
            {
                Top = 30,
                Left = 90,
                Width = 100,
                Height = 20,
                Minimum = 0.1f,
                Maximum = 2f,
                Value = MainClass.Instance.Camera2D.Zoom
            };

            MapPanel.Widgets.Add(GridButton);
            MapPanel.Widgets.Add(ZoomLabel);
            MapPanel.Widgets.Add(ZoomSlider);
            MapWindow.Content = MapPanel;
            MapWindow.CloseButton.Visible = false;
            ZoomSlider.ValueChanged += ZoomSlider_ValueChanged;
            GridButton.Click += new System.EventHandler(Grid_Clicked);
            MainClass.Instance.SceneDrawPanel.Widgets.Add(MapWindow);

        }
        private Vector2 scroll = Vector2.Zero;
        private bool scrollleft;
        private bool scrollright;
        private bool scrolldown;
        private bool scrollup;
        public void Update(GameTime gameTime)
        {
            scroll = Vector2.Zero;
            if (scrollleft)
            {
                scroll.X -= MainClass.Instance.Settings.ScrollSpeed;
            }
            if (scrollright)
            {
                scroll.X += MainClass.Instance.Settings.ScrollSpeed;
            }
            if (scrollup)
            {
                scroll.Y -= MainClass.Instance.Settings.ScrollSpeed;
            }
            if (scrolldown)
            {
                scroll.Y += MainClass.Instance.Settings.ScrollSpeed;
            }

         
            MainClass.Instance.Camera2D.Position += scroll;
        }
        public void MouseMove(MouseEventArgs e)
        {
            float CenterCamX = MainClass.Instance.Camera2D.Position.X;
            float CenterCamY = MainClass.Instance.Camera2D.Position.Y;
           
            float mousex = (e.Position.X  / MainClass.Instance.Camera2D.Zoom) + MainClass.Instance.Camera2D.ViewPort.X;
            float mousey = (e.Position.Y / MainClass.Instance.Camera2D.Zoom) + MainClass.Instance.Camera2D.ViewPort.Y;
           

            
            if (MainClass.Instance.Camera2D.LeftScroll.Contains(e.Position))
            {
                scrollleft = true;
            }
            else
            {
                scrollleft = false;
            }
            if (MainClass.Instance.Camera2D.RightScroll.Contains(e.Position))
            {
                scrollright = true;
            }
            else
            {
                scrollright = false;
            }
            if (MainClass.Instance.Camera2D.TopScroll.Contains(e.Position))
            {
                scrollup = true;
            }
            else
            {
                scrollup = false;
            }
            if (MainClass.Instance.Camera2D.DownScroll.Contains(e.Position))
            {
                scrolldown = true;
            }
            else
            {
                scrolldown = false;
            }





                int tiley = (int)(Math.Round(mousey / 16f));
            int tilex = (int)(Math.Round(mousex / 64f));
            if (tiley % 2 != 0)
            {
                tilex = (int)(Math.Round(mousex / 64f + 32f));
            }
            if (tilex >= 0 && tiley >= 0 && tilex <= Tiles.GetUpperBound(0) && tiley <= Tiles.GetUpperBound(1))
            {
               // MouseOverTile = Tiles[tilex, tiley];
            }
            float diff = 999;
            int startx = tilex - 1;
            int starty = tiley - 1;
            int endx = tilex + 1;
            int endy = tiley + 1;
            if (startx < 0)
                startx = 0;
            if (starty < 0)
                starty = 0;
            if (endx >= Tiles.GetUpperBound(0))
            {
                endx = Tiles.GetUpperBound(0);
            }
            if (endy >= Tiles.GetUpperBound(1))
            {
                endy = Tiles.GetUpperBound(1);
            }
            for (int x = startx; x <= endx; x++)
            {
                for (int y = starty; y <= endy; y++)
                {
                    float neuediff = Math.Abs(Tiles[x,y].Position.X - mousex) + Math.Abs(Tiles[x,y].Position.Y - mousey);
                    if (neuediff < diff)
                    {
                        diff = neuediff;
                        MouseOverTile = Tiles[x, y];
                    }
                }
            }
        }
        private void ZoomSlider_ValueChanged(object sender, Myra.Events.ValueChangedEventArgs<float> e)
        {
            MainClass.Instance.Camera2D.Zoom = e.NewValue;
        }

        private void Grid_Clicked(object sender, EventArgs e)
        {
            MainClass.Instance.Map.Grid = !MainClass.Instance.Map.Grid;
        }
        public Tile[,] Tiles { get; set; }
        public void Generate(int xtile, int ytile)
        {
            Tiles = new Tile[xtile, ytile];
            for(int x = 0; x <= Tiles.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= Tiles.GetUpperBound(1);y++)
                {
                    Tiles[x, y] = new Tile(Prototypes[Random.Next(Prototypes.Count)], new Vector2(x, y));
                    if (Random.Next(100) <= 2)
                    {
                        Tiles[x, y].LeftWall = WallPrototypes[0];
                    }
                    if (Random.Next(100) <= 2)
                    {
                        Tiles[x, y].UpWall = WallPrototypes[1];
                    }
                  
                }
            }
            if (Characters == null)
                Characters = new List<Engine.Characters.Character>();
            Characters.Clear();
            for (int i = 0; i < 5; i++)
            {
                Characters.Add(new Engine.Characters.Character() { TilePosition = new Vector2(0 + i, 0 + i), Player = 0 });
            }
            for (int i = 0; i < 5; i++)
            {
                Characters.Add(new Engine.Characters.Character() { TilePosition = new Vector2(Tiles.GetUpperBound(0) - i, Tiles.GetUpperBound(1) - 1), Player = 1 });
            }
            MainClass.Instance.Camera2D.CameraBounds = new Rectangle(0, 0, Tiles.GetUpperBound(0) * 64, Tiles.GetUpperBound(1) * 16);

        }
        public void Draw(SpriteBatch spritebatch, GameTime gameTime)
        {
            for (int x = 0; x <= Tiles.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= Tiles.GetUpperBound(1); y++)
                {
                    if (MainClass.Instance.Camera2D.ViewPort.Contains(Tiles[x, y].Position))
                    {
                        Tiles[x, y].Draw(spritebatch, gameTime);
                    }
                }
            }
            for (int i = 0; i < Characters.Count; i++)
            {
                if (MainClass.Instance.Camera2D.ViewPort.Contains(Characters[i].DrawPosition))
                {
                    Characters[i].Draw(spritebatch, gameTime);
                }
            }

            if (MouseOverTile != null)
            {
                spritebatch.Draw(Select, MouseOverTile.Position, null, Color.White, 0f, MouseOverTile.TP.DrawPoint, 1f, SpriteEffects.None, 0.1f);
            }
        }
    }
    public class Tile
    {
        public Tile(TilePrototype tp, Vector2 tilepos)
        {
            this.TP = tp;
            this.TilePos = tilepos;

            this.layer -= (tilepos.Y * 0.0001f);
        }
        public TilePrototype TP { get; set; }
        private Vector2 _tilepos = new Vector2(-1, -1);
        public Vector2 Position { get; set; }
        public WallPrototype LeftWall;
       
        public float layer = 1f;
        public WallPrototype UpWall;
        public Vector2 TilePos
        {
            get
            {
                return _tilepos;
            }
            set
            {
                if (value != _tilepos)
                {
                    _tilepos = value;
                    int y = (int)_tilepos.Y * 16;
                    int x = 0;
                    if (_tilepos.Y % 2 == 0)
                    {
                        x = (int)_tilepos.X * 64;
                    }
                    else
                    {
                        x = (int)(_tilepos.X * 64)+ 32;
                    }
                    Position = new Vector2(x, y);
                  

                }
            }
        }
        public Vector2 Center;


        public void Draw(SpriteBatch spritebatch, GameTime gameTime)
        {
            TP.Draw(spritebatch, Position, gameTime, layer);
            LeftWall?.Draw(spritebatch, Position, gameTime,layer);
         
            UpWall?.Draw(spritebatch, Position, gameTime,layer);
        }
    }
    public class WallPrototype
    {
        public WallPrototype(string ressourcetag, string filename)
        {
            this.Ressourcetag = ressourcetag;
            this.Texturename = filename;
            this.DrawPoint = new Vector2(32, 80);
        }
        private string _ressourcetag = "";
        public string Ressourcetag
        {
            get
            {
                return _ressourcetag;
            }
            set
            {
                if (value != _ressourcetag)
                {
                    _ressourcetag = value;
                    TileName = MainClass.Instance.LanguageManager.GetString(_ressourcetag + "_name");
                    TileDescription = MainClass.Instance.LanguageManager.GetString(_ressourcetag + "_description");

                }
            }
        }
        private string _texturename = "";
       

        public string Texturename
        {
            get
            {
                return _texturename;
            }
            set
            {
                if (value != _texturename)
                {
                    _texturename = value;
                    Texture = MainClass.Instance.TextureController.LoadTileSheet(MainClass.Instance.Content.RootDirectory + "/Textures/Walls/" + _texturename + ".png",
                        MainClass.Instance.Device);
                 

                }
            }
        }
        private Vector2 DrawPoint;
        public void Draw(SpriteBatch spritebatch, Vector2 position, GameTime gameTime,float layer)
        {
            spritebatch.Draw(Texture, position, null, Color.White, 0f, DrawPoint, 1f, SpriteEffects.None, layer -0.0001f);
        }

        [XmlIgnore]
        public string TileName;
        public string TileDescription;
        public Texture2D Texture;
       
        
    }

    public class TilePrototype
    {
        public TilePrototype(string ressourcetag, string filename)
        {
            Ressourcetag = ressourcetag;
            Texturename = filename;
            GridTexture = MainClass.Instance.TextureController.LoadTileSheet(MainClass.Instance.Content.RootDirectory + "/Textures/grid.png",
                MainClass.Instance.Device);
            DrawPoint = new Vector2(32, 80);

        }
        private string _ressourcetag = "";
        public string Ressourcetag
        {
            get
            {
                return _ressourcetag;
            }
            set
            {
                if (value != _ressourcetag)
                {
                    _ressourcetag = value;
                    TileName = MainClass.Instance.LanguageManager.GetString(_ressourcetag + "_name");
                    TileDescription = MainClass.Instance.LanguageManager.GetString(_ressourcetag + "_description");

                }
            }
        }
        private string _texturename = "";
        public string Texturename
        {
            get
            {
                return _texturename;
            }
            set
            {
                if (value != _texturename)
                {
                    _texturename = value;
                    TileTexture = MainClass.Instance.TextureController.LoadTileSheet(MainClass.Instance.Content.RootDirectory + "/Textures/Tiles/" + _texturename + ".png",
                        MainClass.Instance.Device);
                  
                }
            }
        }
        public Vector2 DrawPoint;
        public void Draw(SpriteBatch spritebatch,Vector2 position, GameTime gameTime, float layer   )
        {
            spritebatch.Draw(TileTexture, position, null, Color.White, 0f, DrawPoint, 1f, SpriteEffects.None, layer);
            if (MainClass.Instance.Map.Grid)
            {
               spritebatch.Draw(GridTexture, position, null, Color.White, 0f, DrawPoint, 1f, SpriteEffects.None, layer -= 0.1f);
            }
        }

        [XmlIgnore]
        public string TileName;
        public string TileDescription;
        public Texture2D TileTexture;
        public Texture2D GridTexture;
    }
}

