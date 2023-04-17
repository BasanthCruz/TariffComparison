using TariffComparison.Interfaces;
using TariffComparison.Models;
using TariffComparison.Utilities;

namespace TariffComparison.Implementations;

public class CalculationModelB : ICalculationModel {
    private readonly decimal _fixedConsumptionLimit = 4000;

    /// <summary>
    ///  Calculate annual costs for the consumption based on tariff plan.
    /// </summary>
    /// <param name="consumption"></param>
    /// <param name="tariffPlan"></param>
    /// <returns> calculated annual costs</returns>
    public decimal CalculateAnnualCosts(decimal consumption, TariffPlan tariffPlan) {
        return consumption switch {
            var _ when consumption < 0 => throw new ArgumentOutOfRangeException(nameof(consumption), "consumption value should not be less than zero"),
            var _ when consumption <= _fixedConsumptionLimit => tariffPlan.BaseCost,
            var _ when consumption > _fixedConsumptionLimit => Math.Round(tariffPlan.BaseCost + ((consumption - _fixedConsumptionLimit) * tariffPlan.AdditionalCost), CurrencyUtils.GetRoundingDecimalPoints(tariffPlan.Currency)),
        };
    }
}
