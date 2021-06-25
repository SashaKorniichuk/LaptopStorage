using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
   public class LaptopDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Processor { get; set; }
        public string RAM { get; set; }
        public string VideoCard { get; set; }
        public string Disc { get; set; }
        public double? Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int LaptopTypeId { get; set; }
        public int DeveloperId { get; set; }
        public string LaptopType { get; set; }
        public string Developer { get; set; }


    }
}
