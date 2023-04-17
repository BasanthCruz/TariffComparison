using TariffComparison.Interfaces;
using TariffComparison.Models;
using TariffComparison.Utilities;

namespace TariffComparison.Implementations;

public class CalculationModelA : ICalculationModel {
    private readonly int _oneYearInMonths = 12;

    /// <summary>
    ///  Calculate annual costs for the consumption based on tariff plan.
    /// </summary>
    /// <param name="consumption"></param>
    /// <param name="tariffPlan"></param>
    /// <returns> calculated annual costs</returns>
    public decimal CalculateAnnualCosts(decimal consumption, TariffPlan tariffPlan) {

        if (consumption < 0) throw new ArgumentOutOfRangeException(nameof(consumption), "consumption value should not be less than zero");

        return Math.Round((tariffPlan.BaseCost * _oneYearInMonths) + (consumption * tariffPlan.AdditionalCost), CurrencyUtils.GetRoundingDecimalPoints(tariffPlan.Currency));
    }
}
