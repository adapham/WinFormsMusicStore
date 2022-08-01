using System;
using System.Collections.Generic;

#nullable disable

namespace WinFormsMusicStore.Model
{
    public partial class Genre
    {
        public Genre()
        {
            Albums = new HashSet<Album>();
        }

        public int GenreId { get; set; }
        public string Name { get; set; }
        public string Desctription { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
