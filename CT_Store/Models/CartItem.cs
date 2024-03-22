using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CT_Store.Models
{
    public class CartItem
    {
        public int _product_id { get; set; }
        public string _product_name { get; set; }
        public string _image_main { get; set; }
        public decimal _price { get; set; }
        public int _quantity { get; set; }
        
    }
}