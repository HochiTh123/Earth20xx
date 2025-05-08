using Earth20xx.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Earth20xx.Engine
{
    public class StateMachine
    {
        public StateMachine()
        {

        }
       public Myra.Graphics2D.UI.Panel SceneDrawPanel { get; private set; }
        public List<IScene> AvailableScenes = new List<IScene>();
        public IScene CurrentScene = null;
        public void Init()
        {
            this.SceneDrawPanel = MainClass.Instance.SceneDrawPanel;
            AvailableScenes.Add(new LoadingScene());
            AvailableScenes.Add(new MenuScene());
            AvailableScenes.Add(new OfflineCreateScene());
        }

        public void SetState(int index)
        {
            if (index >= 0 && index < this.AvailableScenes.Count)
            {
                CurrentScene?.Unload();
                SceneDrawPanel.Widgets.Clear();
                CurrentScene = AvailableScenes[index];
                CurrentScene.Init();
            }
        }
        public int GetIndex(string tag)
        {
            for (int i = 0; i < AvailableScenes.Count;i++)
            {
                if (AvailableScenes[i].GetTag().ToUpper()==tag.ToUpper())
                {
                    return i;
                }
            }
            return -1;
        }
        public void SetState(string tag)
        {
            SetState(GetIndex(tag));
        }

        public void Update(GameTime gameTime)
        {
            CurrentScene?.Update(gameTime);
        }
        public void Draw(SpriteBatch spritebatch, GameTime gameTime)
        {
            CurrentScene?.Draw(spritebatch, gameTime);
        }


    }

    public interface IScene
    {
        public string GetTag();
        public void Init();
        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch spritebatch, GameTime gameTime);
        public void Unload();
    }
}
