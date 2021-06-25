using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Laptop
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Processor { get; set; }
        [Required]
        public string RAM { get; set; }
        [Required]
        public string VideoCard { get; set; }
        [Required]
        public string Disc { get; set; }
        [Required]
        public double? Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
    
        public int LaptopTypeId { get; set; }
      
        public int DeveloperId { get; set; }
        public virtual LaptopType LaptopType { get; set; }
        public virtual Developer Developer { get; set; }
    }
}
