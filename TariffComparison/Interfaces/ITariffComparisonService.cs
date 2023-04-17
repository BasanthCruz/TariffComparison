using TariffComparison.Models;

namespace TariffComparison.Interfaces;

public interface ITariffComparisonService {

    /// <summary>
    ///  Compare available list of tariff plans.
    /// </summary>
    /// <param name="consumptionPerYear"> consumption per year</param>
    /// <returns> Ordered lists of tariff results</returns>
    IList<TariffResult> CompareTariffs(int consumptionPerYear);
}
