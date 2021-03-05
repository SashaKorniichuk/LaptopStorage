using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Entities
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
        
        public double? Price { get; set; }
        public string Description { get; set; }    
        public string Image { get; set; }
        [Required]
        public LaptopType LaptopType { get; set; }
        [Required]
        public Developer Developer { get; set; }
    }
}
