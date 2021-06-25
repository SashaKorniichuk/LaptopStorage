using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
     public class CartItem
    {
        public int Id { get; set; }
        [ForeignKey("Laptop")]
        public int LaptopId { get; set; }
        public virtual Laptop Laptop { get; set; }
        [ForeignKey("Cart")]
        public int CartId { get; set; }
        public virtual Cart Cart { get; set; }

    }
}
