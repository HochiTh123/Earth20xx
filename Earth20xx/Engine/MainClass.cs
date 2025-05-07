using Earth20xx.GameData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Earth20xx.Engine
{
    public sealed class MainClass
    {
        private static readonly MainClass _instance = new MainClass();
        private MainClass()
        {

        }
        static MainClass()
        {

        }
        public static MainClass Instance { get { return _instance; } }

        #region vars
        public ContentManager Content { get; private set; }
        public GraphicsDeviceManager Manager { get; private set; }
        public GraphicsDevice Device { get; private set; }
        public Game1 Game { get; private set; }
        public string Path { get; private set; }
        public string User { get; private set; }
        public Settings Settings { get; private set; }
        public Myra.Graphics2D.UI.Desktop Desktop { get; private set; }
        public Myra.Graphics2D.UI.Panel SceneDrawPanel { get; private set; }
        public StateMachine StateMachine { get; private set; }
        public Session CurrentSession { get; set; }
        public int CenterX;
        public int CenterY;

        public Controller.MouseController MouseController { get; private set; }
        #endregion

        #region publicMethods
        public void PreInit(Game1 game1, GraphicsDeviceManager manager)
        {
            this.Game = game1;
            this.Content = game1.Content;
            this.Device = game1.GraphicsDevice;
            this.Manager = manager;
            this.Path = this.Content.RootDirectory;
            this.MouseController = new Controller.MouseController();
            this.User = System.Environment.UserName;
            Myra.MyraEnvironment.Game = game1;
        }

        public void Init()
        {
            LoadGameSettings();
            ApplySettings();
            Desktop = new Myra.Graphics2D.UI.Desktop();
            SceneDrawPanel = new Myra.Graphics2D.UI.Panel();
            Desktop.Widgets.Add(SceneDrawPanel);
            this.StateMachine = new StateMachine();
            this.StateMachine.Init();

        }
        public void PostInit()
        {
            this.StateMachine.SetState(0);
        }

        public void Update(GameTime gameTime)
        {
            this.StateMachine?.Update(gameTime);
            this.MouseController?.Update(gameTime);
        }
        public void Draw(SpriteBatch spritebatch, GameTime gameTime)
        {
            
            this.StateMachine?.Draw(spritebatch, gameTime);
            this.Desktop.Render();
        }

        public void ApplySettings()
        {
            if (Manager.PreferredBackBufferWidth != this.Settings.ResolutionX)
            {
                Manager.PreferredBackBufferWidth = this.Settings.ResolutionX;
            }
            if (Manager.PreferredBackBufferHeight != this.Settings.ResolutionY)
            {
                Manager.PreferredBackBufferHeight = this.Settings.ResolutionY;
            }
            if (Manager.IsFullScreen != this.Settings.FullScreen)
            {
                Manager.IsFullScreen = this.Settings.FullScreen;
            }
            this.Manager.ApplyChanges();
            CenterX = Manager.PreferredBackBufferWidth / 2;
            CenterY = Manager.PreferredBackBufferWidth / 2;
        }

        public void LoadGameSettings()
        {
            string folder = this.Path + "/Settings/";
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(folder);
            if (!di.Exists)
                di.Create();
            folder += this.User + "/";
            di = new System.IO.DirectoryInfo(folder);
            if (!di.Exists)
                di.Create();
            string file = folder + "settings.ini";
            System.IO.FileInfo fi = new System.IO.FileInfo(file);
            if (fi.Exists)
            {
                this.Settings = new Settings();
                using (var sr = new System.IO.StreamReader(fi.FullName, System.Text.Encoding.UTF8))
                {
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] tmp = line.Split('=');
                        switch(tmp[0].ToUpper())
                        {
                            case "DEBUG":
                                this.Settings.Debug = Convert.ToBoolean(tmp[1]);
                                break;
                            case "RESX":
                                this.Settings.ResolutionX = Convert.ToInt32(tmp[1]);
                                break;
                            case "RESY":
                                this.Settings.ResolutionY = Convert.ToInt32(tmp[1]);
                                break;
                            case "SCALE":
                                this.Settings.GameScale = (float)Convert.ToDecimal(tmp[1]);
                                break;
                            case "FULLSCREEN":
                                this.Settings.FullScreen = Convert.ToBoolean(tmp[1]);
                                break;
                        }
                    }
                }
            }
            else
            {
                this.Settings = new Settings();
                this.Settings.ResolutionX = this.Device.DisplayMode.Width;
                this.Settings.ResolutionY = this.Device.DisplayMode.Height;
                SaveGameSettings();
            }
        }

        public void SaveGameSettings()
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(this.Path + "/Settings/" + this.User + "/settings.ini");
            using (var sw = new System.IO.StreamWriter(fi.FullName, false, System.Text.Encoding.UTF8))
            {
                sw.WriteLine("DEBUG=" + Settings.Debug.ToString());
                sw.WriteLine("RESX=" + Settings.ResolutionX.ToString());
                sw.WriteLine("RESY=" + Settings.ResolutionY.ToString());
                sw.WriteLine("SCALE=" + Settings.GameScale.ToString());
                sw.WriteLine("FULLSCREEN=" + Settings.FullScreen.ToString());
            }
        }

        #endregion



    }
}
