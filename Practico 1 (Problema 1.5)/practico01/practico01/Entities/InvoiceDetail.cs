﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practico01.Domain
{
    public class InvoiceDetail
    {
        public int InvoiceDetailsID { get; set; }
        public Article Article { get; set; }
        public int Quantity { get; set; }
    }
}
