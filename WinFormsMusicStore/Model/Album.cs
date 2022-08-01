using System;
using System.Collections.Generic;

#nullable disable

namespace WinFormsMusicStore.Model
{
    public partial class Album
    {

        public Album()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int AlbumId { get; set; }
        public int GenreId { get; set; }
        public int ArtisId { get; set; }
        public string Title { get; set; }
        public double? Price { get; set; }
        public string AlbumUrl { get; set; }

        public virtual Artist Artis { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
