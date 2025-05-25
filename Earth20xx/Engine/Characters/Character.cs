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

        }
        public string Name { get; set; }
        public string Description { get; set; }
        public CharacterValues CharacterValues { get; set; }
        private string _profiletexturename;
        private string _profileshoestexture;
        private string _profilelegtexture;
        private string _profilebodytexture;
        private string _profileheadtexture;
        private string _profilehairtexture;
        private string _profilehandtexture;
        public string ProfileTextureName
        {
            get
            {
                return _profiletexturename;
            }
            set
            {
                if (value != _profiletexturename)
                {
                    _profiletexturename = value;
                    ProfilePicture = MainClass.Instance.TextureController.LoadTileSheet(_profiletexturename, MainClass.Instance.Device);
                }
            }
        }
        public string ProfileShoesTexture
        {
            get
            {
                return _profileshoestexture;
            }
            set
            {
                if (value != _profileshoestexture)
                {
                    _profileshoestexture = value;
                    ProfileShoes = MainClass.Instance.TextureController.LoadTileSheet(_profileshoestexture, MainClass.Instance.Device);
                }
            }
        }
        public string ProfileLegTexture
        {
            get
            {
                return _profilelegtexture;
            }
            set
            {
                if (value != _profilelegtexture)
                {
                    ProfileLeg = MainClass.Instance.TextureController.LoadTileSheet(_profilelegtexture, MainClass.Instance.Device);
                }
            }
        }
        public string ProfileBodyTexture
        {
            get
            {
                return _profilebodytexture;
            }
            set
            {
                if (value != _profilebodytexture)
                {
                    _profilebodytexture = value;
                    ProfileBody = MainClass.Instance.TextureController.LoadTileSheet(_profilebodytexture, MainClass.Instance.Device);

                }
            }
        }
        public string ProfileHeadTexture
        {
            get
            {
                return _profileheadtexture;
            }
            set
            {
                _profileheadtexture = value;
                ProfileHead = MainClass.Instance.TextureController.LoadTileSheet(_profileheadtexture, MainClass.Instance.Device);

            }
        }
        public string ProfileHairTexture
        {
            get
            {
                return _profilehairtexture;

            }
            set
            {
                if (value != _profilehairtexture)
                {
                    _profilehairtexture = value;
                    ProfileHair = MainClass.Instance.TextureController.LoadTileSheet(_profilehairtexture, MainClass.Instance.Device);

                }
            }
        }
        public string ProfileHandTexture
        {
            get
            {
                return _profilehandtexture;
            }
            set
            {
                if (value != _profilehandtexture)
                {
                    _profilehandtexture = value;
                    ProfileHand = MainClass.Instance.TextureController.LoadTileSheet(_profilehandtexture, MainClass.Instance.Device);
                }
            }
        }
        [XmlIgnore]
        public Texture2D ProfilePicture;
        public Texture2D ProfileShoes;
        public Texture2D ProfileLeg;
        public Texture2D ProfileBody;
        public Texture2D ProfileHead;
        public Texture2D ProfileHair;
        public Texture2D ProfileHand;


    }

    public  class CharacterValues
    {
        public CharacterValues()
        {

        
        }
        public int HitPoints { get; set; }
        public int MaxHitPoints { get; set; }
        public float HungerRate { get; set; }
        public float ThirstRate { get; set; }
        public float EnergyRate { get; set; }
        public float Energy { get; set; }
        public float EnergyMax { get; set; }
        public float Hunger { get; set; }
        public float Thirst { get; set; }
        public  float MaxCarryingKg { get; set; }
    }
}
