﻿using SharedCommonModel.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedCommonModel.Admin
{
    public class ProductAdmin : AuditableDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = "";
        public string Description { get; set; } = "";
        public double Price { get; set; }
        public int Quantity { get; set; } = 0;
        public int? CategoryId { get; set; }
        public string? ImageUrl { get; set; }

    }
}
