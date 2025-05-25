using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Earth20xx.Engine
{
    public class Settings
    {
        public bool Debug { get; set; } = false;
        public float GameScale { get; set; } = 1.0f;
        public int ResolutionX { get; set; } = 1024;
        public int ResolutionY { get; set; } = 768;
        public bool FullScreen { get; set; } = false;
        public bool ShowGrid { get; set; } = true;
        public int ScrollSpeed { get; set; } = 32;
    }
}
