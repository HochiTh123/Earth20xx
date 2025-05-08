using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace Controller
{
    public class SoundController
    {
        public SoundController()
        {


        }
        List<MySong> Songs = new List<MySong>();
        private int index = 0;
        private bool loaded = false;
        public void Init(string contentpath) // LOAD SONGS
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(contentpath + "/Songs/");
            if (di.Exists)
            {
                var files = System.IO.Directory.GetFileSystemEntries(di.FullName);
                foreach (var f in files)
                {
                    try
                    {
                        System.IO.FileInfo fi = new FileInfo(f);
                        if (fi.Extension == ".OGG")
                        {
                            MySong ms = new MySong(fi.FullName, fi.Name);
                            Songs.Add(ms);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            for (int i = 0; i < Songs.Count;i++)
            {
                Songs[i].Load();
            }
            index = 0;
            loaded = true;
        }

        public void NextSong()
        {
            index++;
            if (index >= Songs.Count)
            {
                index = 0;
            }
            MediaPlayer.Play(Songs[index].Song);
        }

        public void Update(GameTime gameTime)
        {
            if (MediaPlayer.State != MediaState.Playing && loaded)
            {
                NextSong();
            }
        }
    }

    public class MySong
    {
        public MySong(string pfad, string name)
        {
            this.Pfad = pfad;
            this.Name = name;
            this.SongUri = new Uri(pfad, UriKind.RelativeOrAbsolute);
        }
        public string Pfad { get; private set; }
        public string Name { get; private set; }
        public Uri SongUri { get; private set; }
        public Song Song { get; private set; }

        public void Load()
        {
            Song = Song.FromUri(Name, this.SongUri) ;
        }


    }
}
