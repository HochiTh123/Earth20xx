using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Earth20xx.Engine
{
    public enum Type { Land, Water, Other}
    public class TilePrototype
    {
        public TilePrototype()
        {
           
        }
        public string Name { get; set; }
        public UInt16 ID { get; set; }
        public bool MoveAble { get; set; }
        public Type TileType { get; set; }
        private string _texture = "";
        public string Texture
        {
            get
            {
                return _texture;
            }
            set
            {
                if (value != _texture)
                {
                    _texture = value;
                    this.TileSheet = MainClass.Instance.TextureController.LoadTileSheet(this.Texture, MainClass.Instance.Device);
                }
            }
        }
        [XmlIgnore]
        public Texture2D TileSheet;
    }
}
