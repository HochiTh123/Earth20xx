using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Earth20xx.GameData
{
    public class FractialGenerator : IMapRandomizer
    {
        private float prozwater;
        private bool river;
        private float prozforest;
        private float prozhills;
       public ushort[,] Tiles;
      
        int step = 0;
        public Random Randomizer = new Random();
        public void Init(float prozwater, bool river, float prozforest, float prozhills,  ushort[,] tiles)
        {
            this.prozwater = prozwater;
            this.river = river;
            this.prozforest = prozforest;
            this.prozhills = prozhills;
            this.Tiles = tiles;
        }

        public void Unload()
        {
            this.Tiles = null;
            GC.Collect();
        }

        public bool Update(GameTime gameTime)
        {
            switch (step)
            {
                case 0: // Tiles
                    GenerateTiles();
                    return false;
                case 1: // Rivers

                    return false;
                case 2: // Forest + Hills + DeepWater

                    return false;
                case 3: // Factories, Cities

                    return false;
                case 4: // Startpoints

                    return true;
            }
            return false;



        }

        public void GenerateTiles()
        {
            Int32 tilecount = Tiles.GetUpperBound(0) * Tiles.GetUpperBound(1);
            Int32 Points = 1000;
            List<FacPoints> fpoints = new List<FacPoints>();
            for (int i = 0; i < Points; i++)
            {
                int x = Randomizer.Next(Tiles.GetUpperBound(0));
                int y = Randomizer.Next(Tiles.GetUpperBound(1));
                int w = Randomizer.Next(100);
                UInt16 tile = 0;
                if (w > (100 - (int)prozwater))
                {
                    tile = 1;
                }
                bool found = false;
                for (int j = 0; j < fpoints.Count;j++)
                {
                    if (fpoints[j].X == x && fpoints[j].Y == y)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    fpoints.Add(new FacPoints(x, y, tile));
                }
                else // Maybe doing something but for small maps there is no possible to set 1000 points (Small are only 1024 tiles)
                {
                   
                }
            }
         
        }
    }
    public class FacPoints
    {
        public FacPoints(int x, int y, UInt16 tile)
        {
            this.X = x;
            this.Y = y;
            this.TIle = tile;
        }
        public int X;
        public int Y;
        public UInt16 TIle;
    }
}
