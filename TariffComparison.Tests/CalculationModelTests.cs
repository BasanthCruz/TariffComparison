using FluentAssertions;
using TariffComparison.Implementations;
using TariffComparison.Interfaces;
using TariffComparison.Models;

namespace TariffComparison.Tests;

public class CalculationModelTests {

    [Fact]
    public void CalculateAnnualCosts_When_Run_Based_On_CalculationModelA_Should_Return_Expected_Value() {

        string currency = "EUR";
        decimal totalAnnualComsumption = 4500;
        ICalculationModel calculationModel = new CalculationModelA();
        TariffPlan tariffPlan = new() { Name = "Basic Electricity Tariff", BaseCost = 10, AdditionalCost = 0.28M, Currency = currency, CalculationModel = calculationModel };

        decimal expectedResult = 1380.00M;

        var result = calculationModel.CalculateAnnualCosts(totalAnnualComsumption, tariffPlan);

        result.Should().Be(expectedResult);
    }

    [Fact]
    public void CalculateAnnualCosts_When_Run_Based_On_CalculationModelB_And_Annual_Consumption_Is_Greater_Than_Plans_Consumption_Limit_Should_Return_Expected_Value() {

        string currency = "EUR";
        decimal totalAnnualComsumption = 4500;
        ICalculationModel calculationModel = new CalculationModelB();
        TariffPlan tariffPlan = new() { Name = "Basic Electricity Tariff", BaseCost = 800, AdditionalCost = 0.28M, Currency = currency, CalculationModel = calculationModel };

        decimal expectedResult = 940.00M;

        var result = calculationModel.CalculateAnnualCosts(totalAnnualComsumption, tariffPlan);

        result.Should().Be(expectedResult);
    }

    [Fact]
    public void CalculateAnnualCosts_When_Run_Based_On_CalculationModelB_And_Annual_Consumption_Is_Less_Than_Plans_Consumption_Limit_Should_Return_Expected_Value() {

        string currency = "EUR";
        decimal totalAnnualComsumption = 3800;
        ICalculationModel calculationModel = new CalculationModelB();
        TariffPlan tariffPlan = new() { Name = "Basic Electricity Tariff", BaseCost = 800, AdditionalCost = 0.28M, Currency = currency, CalculationModel = calculationModel };

        decimal expectedResult = 800.00M;

        var result = calculationModel.CalculateAnnualCosts(totalAnnualComsumption, tariffPlan);

        result.Should().Be(expectedResult);
    }

    [Fact]
    public void CalculateAnnualCosts_When_Run_Should_Round_Based_On_Default_Currency_Profile_If_The_Provided_Profile_No_Found() {

        string currency = "test";
        decimal totalAnnualComsumption = 4500;
        ICalculationModel calculationModel = new CalculationModelA();
        TariffPlan tariffPlan = new() { Name = "Basic Electricity Tariff", BaseCost = 20.36M, AdditionalCost = 0.28M, Currency = currency, CalculationModel = calculationModel };

        decimal expectedResult = 1504.32M;

        var result = calculationModel.CalculateAnnualCosts(totalAnnualComsumption, tariffPlan);

        result.Should().Be(expectedResult);
    }

    [Fact]
    public void CalculateAnnualCosts_When_Provided_With_Negative_Consumpution_Value_Should_Throw_Exception() {

        string currency = "EUR";
        decimal totalAnnualComsumption = -4500;
        ICalculationModel calculationModel = new CalculationModelA();
        TariffPlan tariffPlan = new() { Name = "Basic Electricity Tariff", BaseCost = 10, AdditionalCost = 0.28M, Currency = currency, CalculationModel = calculationModel };

        var act = () => calculationModel.CalculateAnnualCosts(totalAnnualComsumption, tariffPlan);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }
}
