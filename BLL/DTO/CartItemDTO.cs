using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class CartItemDTO
    {
        public int Id { get; set; }
        public int LaptopId { get; set; }
        public int CartId { get; set; }
    }
}
