using TariffComparison.Interfaces;

namespace TariffComparison.Models;

public class TariffPlan {
    /// <summary>
    /// The name of tariff plan.
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// The base cost of tariff plan.
    /// Can be cost per month or cost based on consumption limit
    /// </summary>
    public required decimal BaseCost { get; set; }
    /// <summary>
    /// The additional cost of tariff plan.
    /// Cost per KWH
    /// </summary>
    public required decimal AdditionalCost { get; set; }
    /// <summary>
    /// The ISO 4217 currency code by country.
    /// </summary>
    public required string Currency { get; set; }
    /// <summary>
    /// Type of Calculation Model for tariff plan 
    /// </summary>
    public required ICalculationModel CalculationModel { get; set; }
}
