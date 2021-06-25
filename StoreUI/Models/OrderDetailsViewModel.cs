using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreUI.Models
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }
        public string LaptopName {get;set;}
        public string LaptopImage { get; set; }
        public int LaptopPrice { get; set; }
        public int LaptopId { get; set; }
    }
}