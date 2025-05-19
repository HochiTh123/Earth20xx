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
    public class LoadingScene : Engine.IScene
    {
        public void Draw(SpriteBatch spritebatch, GameTime gameTime)
        {
          
        }

        public string GetTag()
        {
            return "Loading";
        }
        public Myra.Graphics2D.UI.Label HeaderLabel;
        public Myra.Graphics2D.UI.HorizontalProgressBar Progressbar;
        string [] Files;
        int findex;
        public void Init()
        {
            MainClass.Instance.Camera2D.Position = new Vector2(MainClass.Instance.Device.Viewport.Width / 2, MainClass.Instance.Device.Viewport.Height / 2);
            var di = new System.IO.DirectoryInfo(MainClass.Instance.Content.RootDirectory + "/DataCache/");
            if (!di.Exists)
                di.Create();
             di = new System.IO.DirectoryInfo(MainClass.Instance.Content.RootDirectory + "/DataCache/TilePrototypes/");
            if (!di.Exists)
                di.Create();
            Files = System.IO.Directory.GetFileSystemEntries(MainClass.Instance.Content.RootDirectory + "/DataCache/TilePrototypes/");
            findex = -1;
            
            HeaderLabel = new Myra.Graphics2D.UI.Label()
            {
                Top = 10,
                Left = MainClass.Instance.CenterY - 100,
                Width = 200,
                Height = 20,
                Text = "Loading",
                TextAlign = FontStashSharp.RichText.TextHorizontalAlignment.Center
            };
            Progressbar = new Myra.Graphics2D.UI.HorizontalProgressBar()
            {
                Top = 50,
                Left = MainClass.Instance.CenterX - 300,
                Width = 600,
                Height = 50,
                Minimum = 0f,
                Maximum = 1f,
                Value = 0f
            };
            if (MainClass.Instance.Prototypes == null)
            {
                MainClass.Instance.Prototypes = new List<TilePrototype>();
            }
            MainClass.Instance.Prototypes.Clear();

            MainClass.Instance.SceneDrawPanel.Widgets.Add(HeaderLabel);
            MainClass.Instance.SceneDrawPanel.Widgets.Add(Progressbar);
            
        }
        private void OfflineCreateButton(object sender, EventArgs e)
        {
            MainClass.Instance.StateMachine.SetState("OFFLINECREATE");
        }
        public void Unload()
        {
            

            MainClass.Instance.SceneDrawPanel.Widgets.Remove(HeaderLabel);
            
        }

        public void Update(GameTime gameTime)
        {
            findex++;
            if (findex >= Files.GetUpperBound(0))
            {
                if (Files.GetUpperBound(0) < 1)
                {
                    TilePrototype tp = new TilePrototype()
                    {
                        ID = 0,
                        Name = "Dirt",
                        MoveAble = true,
                        TileType = Engine.Type.Land,
                        Texture = MainClass.Instance.Content.RootDirectory + "/Textures/dirt.png"

                    };
                    MainClass.Instance.Prototypes.Add(tp);

                    for (int i = 0; i < MainClass.Instance.Prototypes.Count;i++)
                    {
                        string file = MainClass.Instance.Content.RootDirectory + "/DataCache/TilePrototypes/" + MainClass.Instance.Prototypes[i].ID.ToString() + ".xml";
                        using (var fs = new System.IO.FileStream(file, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite))
                        {
                            System.Xml.Serialization.XmlSerializer xmlser = new System.Xml.Serialization.XmlSerializer(typeof(TilePrototype));
                            xmlser.Serialize(fs, MainClass.Instance.Prototypes[i]);
                        }
                    }
                }
                
                MainClass.Instance.StateMachine.SetState(1);
            }
            else
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(Files[findex]);
                if (fi.Exists)
                {
                    using (var fs = new System.IO.FileStream(fi.FullName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite))
                    {
                        System.Xml.Serialization.XmlSerializer xmlser = new System.Xml.Serialization.XmlSerializer(typeof(TilePrototype));
                        var tp = (TilePrototype)xmlser.Deserialize(fs);
                        MainClass.Instance.Prototypes.Add(tp);
                    }
                }
            }

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
