using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuburbExplorer.Models
{
    public class AgeDistribution
    {
        public decimal Age35To39Percentage { get; set; }
        public decimal Age35To39StatePercentage { get; set; }
        public decimal Age40To44Percentage { get; set; }
        public decimal Age40To44StatePercentage { get; set; }
        public decimal Age45To49Percentage { get; set; }
        public decimal Age45To49StatePercentage { get; set; }
        public decimal Age50To54Percentage { get; set; }
        public decimal Age50To54StatePercentage { get; set; }
        public decimal Age55To59Percentage { get; set; }
        public decimal Age55To59StatePercentage { get; set; }

        public decimal AgeDistributionPercentageSuburb => Age35To39Percentage
                                                        + Age40To44Percentage
                                                        + Age45To49Percentage
                                                        + Age50To54Percentage
                                                        + Age55To59Percentage;
        public decimal AgeDistributionPercentageState => Age35To39StatePercentage
                                                        + Age40To44StatePercentage
                                                        + Age45To49StatePercentage
                                                        + Age50To54StatePercentage
                                                        + Age55To59StatePercentage;
    }
}
