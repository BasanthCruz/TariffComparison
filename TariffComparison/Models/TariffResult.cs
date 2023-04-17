namespace TariffComparison.Models;

public class TariffResult {
    /// <summary>
    /// The name of tariff plan.
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// Annual cost of the tariff plan based on Calculation Model.
    /// </summary>
    public required decimal AnnualCosts { get; set; }
}
