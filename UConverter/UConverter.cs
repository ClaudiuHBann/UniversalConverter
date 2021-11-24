namespace Server.UConverter
{
    enum UCCategories
    {
        Currency,
        Temperature,
        NumberBase
    }

    public class UConverter
    {
        public static List<Func<string, int, int, string>> methods = new()
        {
            (data, from, to) => CCurrency.ConvertCurrency(data, from, to),
            (data, from, to) => CTemperature.ConvertTemperature(data, from, to),
            (data, from, to) => CNumber.Base.ConvertNumberBase(data, from, to)
        };
    }
}
