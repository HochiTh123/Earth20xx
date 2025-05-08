using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Controller
{
    public class TextureController
    {
        public Texture2D LoadTileSheet(string pfad, GraphicsDevice device)
        {
            System.IO.FileInfo fi = new FileInfo(pfad);
            if (fi.Exists)
            {
                using (var fs = new System.IO.FileStream(pfad, FileMode.Open, FileAccess.Read))
                {
                    var texture2d = Texture2D.FromStream(device, fs);
                    ChangeColor(ref texture2d, Microsoft.Xna.Framework.Color.Fuchsia, Microsoft.Xna.Framework.Color.Transparent);
                    return texture2d;
                }
            }
            else
            {
                return null;
            }
        }

        public void ChangeColor(ref Texture2D texture, Microsoft.Xna.Framework.Color color, Microsoft.Xna.Framework.Color newcolor)
        {
            Microsoft.Xna.Framework.Color[] data = new Microsoft.Xna.Framework.Color[texture.Width * texture.Height];
            texture.GetData<Microsoft.Xna.Framework.Color>(data);
            for (int i = 0; i <= data.GetUpperBound(0); i++)
            {
                if (data[i] == color)
                    data[i] = newcolor;
            }
            texture.SetData<Microsoft.Xna.Framework.Color>(data);
            data = null;
            GC.Collect();

        }
    }
}
