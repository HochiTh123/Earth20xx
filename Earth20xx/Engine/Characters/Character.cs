using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Earth20xx.Engine.Characters
{
    public class Character
    {
        public Character()
        {
            CharacterBody = MainClass.Instance.TextureController.LoadTileSheet(MainClass.Instance.Content.RootDirectory + "/Textures/Units/unit_test.png",
                MainClass.Instance.Device);

        }
        public Texture2D CharacterBody { get; set; }
        public Color PlayerColor { get; set; }
        private Vector2 _tileposition = new Vector2(0, 0);
        public Vector2 DrawPosition;
        public Vector2 Center = new Vector2(32, 80);
        public float layer = 1f;
        public Vector2 TilePosition
        {
            get
            {
                return _tileposition;
            }
            set
            {
                if (value != _tileposition)
                {
                    _tileposition = value;
                    int x = (int)_tileposition.X * 64;
                    int y = (int)_tileposition.Y * 16;
                    if (_tileposition.Y % 2 != 0)
                    {
                        x = x + 32;

                    }
                    DrawPosition = new Vector2(x, y);
                    layer = 1f;
                    this.layer -= (_tileposition.Y * 0.0001f) + 0.0001f;


                }
            }
        }
        private int _player = -1;
        public int Player
        {
            get
            {
                return _player;
            }
            set
            {
                if (value != _player)
                {
                    _player = value;
                    this.PlayerColor = MainClass.Instance.CurrentSession.Players[_player].PlayerColor;
                }
            }
        }
        public void Draw(SpriteBatch spritebatch, GameTime gameTime)
        {
            spritebatch.Draw(CharacterBody, DrawPosition, null, PlayerColor, 0f, Center, 1f, SpriteEffects.None, this.layer);
        }

    }


}
