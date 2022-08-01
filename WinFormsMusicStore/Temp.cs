using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsMusicStore
{
    public class Temp
    {
       

        public int AlbumId { get; set; }
        public int GenreId { get; set; }
        public int ArtisId { get; set; }
        public string Title { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }

        public Temp(int albumId, int genreId, int artisId, string title, float price, int quantity)
        {
            AlbumId = albumId;
            GenreId = genreId;
            ArtisId = artisId;
            Title = title;
            Price = price;
            Quantity = quantity;
        }

        public Temp()
        {
        }
    }
}
