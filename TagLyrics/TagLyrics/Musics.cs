using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagLyrics
{
    class Music
    {

        public Music(string name, string title, string album, string lyrics, string filepath)
        {
            Name = name;
            Title = title;
            Album = album;
            Lyrics = lyrics;
            Location = filepath;
            IsChanged = false;
            NewLyrics = "";
            Save = false;
        }

        public string Name { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public string Lyrics { get; set; }
        public string Location { get; set; }
        public string NewLyrics { get; set; }
        public bool IsChanged { get; set; }
        public bool Save { get; set; }
    }
}
