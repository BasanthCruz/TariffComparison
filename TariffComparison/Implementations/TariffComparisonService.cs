using TariffComparison.Interfaces;
using TariffComparison.Models;

namespace TariffComparison.Implementations;

public class TariffComparisonService : ITariffComparisonService {
    public IList<TariffPlan> TariffPlans { get; }

    public TariffComparisonService(IList<TariffPlan> tariffPlans) {

        if (tariffPlans is null || !tariffPlans.Any())
            throw new ArgumentNullException(nameof(tariffPlans));

        TariffPlans = tariffPlans;
    }

    /// <summary>
    ///  Compare available list of tariff plans.
    /// </summary>
    /// <param name="consumptionPerYear">consumption per year</param>
    /// <returns> Ordered lists of tariff results</returns>
    public IList<TariffResult> CompareTariffs(int consumptionPerYear) {
        return TariffPlans.Select(plan =>
            new TariffResult() {
                Name = plan.Name,
                AnnualCosts = plan.CalculationModel.CalculateAnnualCosts(consumptionPerYear, plan)
            }).OrderBy(result => result.AnnualCosts).ToList();
    }
}
