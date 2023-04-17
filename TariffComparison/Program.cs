// See https://aka.ms/new-console-template for more information
using TariffComparison.Implementations;
using TariffComparison.Interfaces;
using TariffComparison.Models;

Console.WriteLine("Start Comparision!");

var totalAnnualComsumption = 6000;
var currency = "EUR";

List<TariffPlan> tariffPlans = new() {
    new TariffPlan() { Name = "Basic Electricity Tariff", BaseCost = 5, AdditionalCost = 0.22M, Currency = currency, CalculationModel =  new CalculationModelA() },
    new TariffPlan() { Name = "Packaged Tariff", BaseCost = 800, AdditionalCost = 0.30M, Currency = currency, CalculationModel =  new CalculationModelB() }
};

try {
    ITariffComparisonService tariffComparisonService = new TariffComparisonService(tariffPlans);

    Console.WriteLine($"Find the tariff plan comparison below in ascending order for annual cosumption of {totalAnnualComsumption} KWH/year");

    foreach (var tariffResult in tariffComparisonService.CompareTariffs(totalAnnualComsumption)) {

        Console.WriteLine($"Annual cost for cosumption is {tariffResult.AnnualCosts} {currency}/year for plan {tariffResult.Name}");
    }
}
catch (Exception ex) {

    Console.WriteLine($"Program execution failed with Exception {ex.Message}");
}


Console.WriteLine("End Comparision!");