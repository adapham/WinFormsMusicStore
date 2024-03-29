﻿using System;
using System.Collections.Generic;

#nullable disable

namespace WinFormsMusicStore.Model
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int AlbumId { get; set; }
        public int? Quantity { get; set; }
        public double? UnitPrice { get; set; }

        public virtual Album Album { get; set; }
        public virtual Order Order { get; set; }
    }
}
