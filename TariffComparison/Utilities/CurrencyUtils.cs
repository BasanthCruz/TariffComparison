namespace TariffComparison.Utilities;

public static class CurrencyUtils {

    private const string _defaultCurrencyProfile = "default";

    private static readonly IDictionary<string, int> _currencyProfiles = new Dictionary<string, int>()
    {
        { "EUR", 2},
        { "CHF", 3},
        { "default", 2}
    };

    /// <summary>
    ///  Get rounding decimal points based on currency.
    /// </summary>
    /// <param name="currency"></param>
    /// <returns> return specific rounding decimal if found else returns default value</returns>
    public static int GetRoundingDecimalPoints(string currency) {
        return _currencyProfiles.TryGetValue(currency, out int value) is true ? value : _currencyProfiles[_defaultCurrencyProfile];
    }
}
