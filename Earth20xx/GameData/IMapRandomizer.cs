using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Earth20xx.GameData
{
    interface IMapRandomizer
    {

        void Init(float prozwater, bool river, float prozforest, float prozhills, UInt16[,] tiles);
        bool Update(GameTime gameTime);
        void Unload();
    }
}
