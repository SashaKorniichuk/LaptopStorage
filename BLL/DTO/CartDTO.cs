﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class CartDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string State { get; set; }
        public bool MakeOrder { get; set; }
    }
}