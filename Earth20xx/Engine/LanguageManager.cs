using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Earth20xx.Engine
{
    public class LanguageManager
    {
        public LanguageManager()
        {
            Languages.Add(new Lang("English", "%CONTENT%/Ressources/en.data"));
            Languages.Add(new Lang("Deutsch", "%CONTENT%/Ressources/de.data"));
        }
        public List<Lang> Languages = new List<Lang>();

        private int langindex = -1;
        public List<StringValue> Strings = new List<StringValue>();
        public int LangIndex
        {
            get
            {
                return langindex;
            }
            set
            {
                if (value != langindex)
                {
                    langindex = value;
                    LoadStrings();
                }
            }
        }
        public void LoadStrings()
        {
            Strings.Clear();
            System.IO.FileInfo fi = new System.IO.FileInfo(Languages[langindex].File.Replace("%CONTENT%", MainClass.Instance.Content.RootDirectory));
            if (fi.Exists)
            {
                using (var sr = new System.IO.StreamReader(fi.FullName, Encoding.UTF8))
                {
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!line.Contains("//"))
                        {
                            string[] tmp = line.Split('=');
                            StringValue sv = new StringValue(tmp[0], tmp[1]);
                            Strings.Add(sv);
                        }
                    }
                }
            }
        }

        public string GetString(string key)
        {
            for (int i = 0; i < Strings.Count;i++)
            {
                if (Strings[i].Tag.ToUpper() == key.ToUpper())
                {
                    return Strings[i].Value;
                }
            }
            using (var sw = new System.IO.StreamWriter(MainClass.Instance.Content.RootDirectory + "/missingstrings.txt", true, Encoding.UTF8))
            {
                Strings.Add(new StringValue(key, "[" + key + "]"));
                sw.WriteLine(key.ToString());
                return "[" + key + "]";
            }
        }
    }
    public class StringValue
    {
        public StringValue(string tag, string value)
        {
            this.Tag = tag;
            this.Value = value;
        }
        public string Tag { get; private set; }
        public string Value { get; private set; }
    }
    public class Lang
    {
        public Lang(string name, string file)
        {
            this.Name = name;
            this.File = file;
        }
        public string Name;
        public string File;
    }
}
