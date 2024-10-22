using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuburbExplorer.Models
{
    public class DemographicData
    {
        //Data from ABS API 
        public MedianAge? MedianAge { get; set; }
        public IncomeLevel? IncomeLevel { get; set; }
        public RentalRate? RentalRate { get; set; }
        public RentalYield? RentalYield { get; set; }


        public decimal baseScore = 20;


        public decimal CalculateAgeScore(MedianAge medianAge)
        {
            int minAge = 35;
            int maxAge = 54;
            if (medianAge.Age >= minAge && medianAge.Age <= maxAge)
            {
                return baseScore;
            }
            else
            {
                return 0m;
            }
        }

        public decimal CalculateIncomeLevelScore(IncomeLevel incomeLevel)
        {
            //Calculate the percentage difference between suburb income level and the state income level
            decimal incomeLevelComparation = (incomeLevel.IncomeHousehold - incomeLevel.IncomeHouseholdState)/ incomeLevel.IncomeHouseholdState;
            if (incomeLevelComparation >= -0.1m)
            {
                return baseScore;
            }
            else 
            {
                return 0m;
            }
        }

        public decimal CalculateRentalRateScore(RentalRate rentalRate)
        {
            decimal rentalRateComparation = rentalRate.RentalRateSuburb / rentalRate.RentalRateState;
            if (rentalRateComparation <= 1.1m && rentalRateComparation >= 0.9m)
            {
                return baseScore;
            }
            else if ((rentalRateComparation > 1.1m && rentalRateComparation <= 1.2m) || 
                     (rentalRateComparation < 0.9m && rentalRateComparation >= 0.8m))
            {
                return baseScore * 0.5m;
            }
            else { return 0m; }
        }

        public decimal CalculateRentalYieldScore (RentalYield rentalYield)
        {
            decimal rentalScore = rentalYield.MedianRentSuburb / rentalYield.MedianRentState;
            decimal affordablityScore = rentalYield.PercentageOfRenterPayingLessThan30Percent / rentalYield.PercentageOfRenterPayingLessThan30PercentState;
            decimal rentalYieldComparation = (rentalScore + affordablityScore) / 2 * 100;
            if (rentalYieldComparation >= 100)
            {
                return baseScore;
            }
            else if (rentalYieldComparation <100 && rentalYieldComparation >= 90)
            {
                return baseScore * 0.8m;
            }
            else { return 0m;}
        }


        public decimal CalculateOverallScore(MedianAge medianAge, IncomeLevel incomeLevel, RentalRate rentalRate, RentalYield rentalYield)
        {
            decimal ageScore = CalculateAgeScore(medianAge);
            decimal incomeLevelScore = CalculateIncomeLevelScore(incomeLevel);
            decimal rentalRateScore = CalculateRentalRateScore(rentalRate);
            decimal rentalYielScore = CalculateRentalYieldScore(rentalYield);
            decimal overallScore = ageScore
                                 + incomeLevelScore
                                 + rentalRateScore                               
                                 + rentalYielScore;
            return overallScore;

        }
    }
}
