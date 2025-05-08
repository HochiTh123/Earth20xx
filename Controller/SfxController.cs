using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class SfxController
    {
        public SfxController()
        {

        }
        List<MySfx> Effects = new List<MySfx>();
        public void Load(string file, string tag)
        {
            MySfx ms = new MySfx(file, tag);
            ms.Load();
            Effects.Add(ms);
        }

        public int GetIndex(string tag)
        {
            for (int i = 0; i < Effects.Count;i++)
            {
                if (Effects[i].Tag == tag.ToUpper())
                {
                    return i;
                }
            }
            return -1;
        }
        public void Play(int index, float volume, float pitch = 0f, float pan = 0f)
        {
            if (index >= 0 && index < Effects.Count)
            {
                Effects[index].Play(volume, pitch, pan);
            }
        }
        public void Play(string tag, float volume, float pitch =0f, float pan = 0f)
        {
            Play(GetIndex(tag), volume, pitch, pan);
        }
    }

    public class MySfx
    {
        public MySfx(string pfad, string tag)
        {
            this.Pfad = pfad;
            this.Tag = tag;
        }
        public string Pfad { get; private set; }
        public string Tag { get; private set; }
        public SoundEffect Effect { get; private set; }

        public void Load()
        {
            this.Effect = SoundEffect.FromFile(this.Pfad);
        }

        public void Play(float volume, float pitch = 0f, float pan = 0f)
        {
            Effect.Play(volume, pitch, pan);
        }
    }
}
