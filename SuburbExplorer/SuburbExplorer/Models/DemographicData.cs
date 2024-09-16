using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuburbExplorer.Models
{
    public class DemographicData
    {
        //Data from ABS API and Walk Score API
        public AgeDistribution? AgeDistribution { get; set; }
        public IncomeLevel? IncomeLevel { get; set; }
        public RentalRate? RentalRate { get; set; }
        public WalkScore? WalkScore { get; set; }
        public RentalYield? RentalYield { get; set; }


        public decimal baseScore = 20;


        public decimal CalculateAgeDistributionScore(AgeDistribution ageDistribution)
        {

            if (ageDistribution.AgeDistributionPercentageSuburb >= ageDistribution.AgeDistributionPercentageState)
            {
                return baseScore;
            }
            else if (ageDistribution.AgeDistributionPercentageSuburb < ageDistribution.AgeDistributionPercentageState && 
                ageDistribution.AgeDistributionPercentageSuburb > 0.9m * ageDistribution.AgeDistributionPercentageState)
            {
                return baseScore * 0.5m;
            }
            else
            {
                return 0m;
            }
        }

        public decimal CalculateIncomeLevelScore(IncomeLevel incomeLevel)
        {
            decimal incomeLevelComparation = incomeLevel.IncomeHousehold / incomeLevel.IncomeHouseholdState;
            return incomeLevelComparation switch
            {
                decimal n when (n >= 1.2m) => baseScore,
                decimal n when (n >= 1.1m && n < 1.2m) => baseScore * 0.8m,
                decimal n when (n >= 1m && n < 1.1m) => baseScore * 0.6m,
                decimal n when (n >= 0.9m && n < 1m) => baseScore * 0.4m,
                decimal n when (n >= 0.8m && n < 0.9m) => baseScore * 0.2m,
                decimal n when (n < 0.8m) => 0m,
            };
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

        public decimal CalculateWalkScore (WalkScore walkScore)
        {
            if (walkScore.WalkScoreSuburb >= 70) 
            { 
                return baseScore;
            }
            else if (walkScore.WalkScoreSuburb >= 50 && walkScore.WalkScoreSuburb < 70)
            {
                return baseScore * 0.5m;
            }
            else
            {
                return 0m;
            }
        }

        public decimal CalculateOverallScore()
        {
            decimal ageDistributionScore = CalculateAgeDistributionScore(AgeDistribution);
            decimal incomeLevelScore = CalculateIncomeLevelScore(IncomeLevel);
            decimal rentalRateScore = CalculateRentalRateScore(RentalRate);
            decimal walkScoreScore = CalculateWalkScore(WalkScore);
            decimal rentalYielScore = CalculateRentalYieldScore(RentalYield);
            decimal overallScore = ageDistributionScore
                                 + incomeLevelScore
                                 + rentalRateScore
                                 + walkScoreScore
                                 + rentalYielScore;
            return overallScore;

        }
    }
}
