﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuburbExplorer.Models
{
    public class RentalYield
    {
        public int MedianRentSuburb { get; set; }
        public int MedianRentState { get; set; }
        public decimal PercentageOfRenterPayingLessThan30Percent { get; set; }
        public decimal PercentageOfRenterPayingLessThan30PercentState { get; set; }

    }
}