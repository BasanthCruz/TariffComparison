using FluentAssertions;
using NSubstitute;
using TariffComparison.Implementations;
using TariffComparison.Interfaces;
using TariffComparison.Models;

namespace TariffComparison.Tests;

public class TariffComparisonServiceTests {

    private readonly ICalculationModel _calculationModelA;
    private readonly ICalculationModel _calculationModelB;
    private const string _currency = "EUR";

    public TariffComparisonServiceTests() {

        _calculationModelA = Substitute.For<ICalculationModel>();
        _calculationModelB = Substitute.For<ICalculationModel>();
    }

    [Fact]
    public void TariffComparisonService_When_Instantiated_With_Null_List_Should_Throw_Exception() {

        List<TariffPlan> tariffPlans = null;

        var act = () => new TariffComparisonService(tariffPlans);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void TariffComparisonService_When_Instantiated_With_Empty_List_Should_Throw_Exception() {

        List<TariffPlan> tariffPlans = new();

        var act = () => new TariffComparisonService(tariffPlans);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void CompareTariffs_When_Ran_Successfully_Should_Return_Expected_Values() {

        var totalAnnualComsumption = 3500;
        _calculationModelA.CalculateAnnualCosts(Arg.Any<decimal>(), Arg.Any<TariffPlan>()).Returns(10);
        _calculationModelB.CalculateAnnualCosts(Arg.Any<decimal>(), Arg.Any<TariffPlan>()).Returns(20);
        List<TariffPlan> tariffPlans = GetTestTariffPlanData();

        var expectedResult = new List<TariffResult>() {
            new TariffResult() { Name = "Basic Electricity Tariff", AnnualCosts = 10  },
            new TariffResult() { Name = "Packaged Tariff", AnnualCosts = 20  }
        };

        ITariffComparisonService tariffComparisonService = new TariffComparisonService(tariffPlans);

        var result = tariffComparisonService.CompareTariffs(totalAnnualComsumption);

        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void CompareTariffs_When_Ran_Should_Return_Result_In_AscendingOrder_Ordered_By_AnnualCosts() {

        var totalAnnualComsumption = 3500;
        _calculationModelA.CalculateAnnualCosts(Arg.Any<decimal>(), Arg.Any<TariffPlan>()).Returns(10);
        _calculationModelB.CalculateAnnualCosts(Arg.Any<decimal>(), Arg.Any<TariffPlan>()).Returns(20);
        List<TariffPlan> tariffPlans = GetTestTariffPlanData();

        ITariffComparisonService tariffComparisonService = new TariffComparisonService(tariffPlans);

        var result = tariffComparisonService.CompareTariffs(totalAnnualComsumption);

        result.Should().BeInAscendingOrder(x => x.AnnualCosts);
    }

    [Fact]
    public void CompareTariffs_When_Ran_With_Different_Calculation_Values_Should_Return_Result_In_AscendingOrder_Ordered_By_AnnualCosts() {

        var totalAnnualComsumption = 3500;
        _calculationModelA.CalculateAnnualCosts(Arg.Any<decimal>(), Arg.Any<TariffPlan>()).Returns(20);
        _calculationModelB.CalculateAnnualCosts(Arg.Any<decimal>(), Arg.Any<TariffPlan>()).Returns(10);
        List<TariffPlan> tariffPlans = GetTestTariffPlanData();

        ITariffComparisonService tariffComparisonService = new TariffComparisonService(tariffPlans);

        var result = tariffComparisonService.CompareTariffs(totalAnnualComsumption);

        result.Should().BeInAscendingOrder(x => x.AnnualCosts);
    }

    private List<TariffPlan> GetTestTariffPlanData() {

        return new() {
            new TariffPlan() { Name = "Basic Electricity Tariff", BaseCost = 5, AdditionalCost = 0.22M, Currency = _currency, CalculationModel =  _calculationModelA },
            new TariffPlan() { Name = "Packaged Tariff", BaseCost = 800, AdditionalCost = 0.30M, Currency = _currency, CalculationModel =  _calculationModelB }
        };
    }
}
