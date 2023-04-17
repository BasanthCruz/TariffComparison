using TariffComparison.Models;

namespace TariffComparison.Interfaces;

public interface ICalculationModel {

    /// <summary>
    ///  Calculate annual costs for the consumption based on tariff plan.
    /// </summary>
    /// <param name="consumption"></param>
    /// <param name="tariffPlan"></param>
    /// <returns> calculated annual costs</returns>
    decimal CalculateAnnualCosts(decimal consumption, TariffPlan tariffPlan);
}
