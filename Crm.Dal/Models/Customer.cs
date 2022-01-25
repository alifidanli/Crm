﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Dal.Models
{
    public class Customer
    {
        public int CustomerNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public DateTime DateofBirth { get; set; }
        public bool Enabled { get; set; }

    }
}