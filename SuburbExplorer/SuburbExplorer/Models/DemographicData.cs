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

        public const int BASE_SCORE = 25;


        public int CalculateAgeScore(MedianAge medianAge)
        {
            int minAge = 35;
            int maxAge = 54;
            if (medianAge.Age >= minAge && medianAge.Age <= maxAge)
            {
                return BASE_SCORE;
            }
            else
            {
                return 0;
            }
        }

        public int CalculateIncomeLevelScore(IncomeLevel incomeLevel)
        {
            if (incomeLevel.IncomeHousehold >= incomeLevel.IncomeHouseholdState * 0.9)
            {
                return BASE_SCORE;
            }
            else 
            {
                return 0;
            }
        }

        public int CalculateRentalRateScore(RentalRate rentalRate)
        {
            decimal rentalRateComparation = rentalRate.RentalRateSuburb / rentalRate.RentalRateState;
            if (rentalRateComparation <= 1.2m && rentalRateComparation >= 0.8m)
            {
                return BASE_SCORE;
            }
            
            else { return 0; }
        }

        public int CalculateRentalYieldScore (RentalYield rentalYield)
        {
            if (rentalYield.MedianRentSuburb >= rentalYield.MedianRentState)
            {
                return BASE_SCORE;
            }
            else { return 0;}
        }


        public int CalculateOverallScore()
        {
            int ageScore = MedianAge != null ? CalculateAgeScore(MedianAge) : 0;
            int incomeLevelScore = IncomeLevel!= null? CalculateIncomeLevelScore(IncomeLevel) : 0;
            int rentalRateScore = RentalRate != null ? CalculateRentalRateScore(RentalRate) : 0;
            int rentalYieldScore = RentalYield != null ? CalculateRentalYieldScore(RentalYield) : 0;
            int overallScore = ageScore
                                 + incomeLevelScore
                                 + rentalRateScore                               
                                 + rentalYieldScore;
            return overallScore;

        }
    }
}
