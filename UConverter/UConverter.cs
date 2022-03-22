namespace Server.UConverter
{
    enum UCCategories
    {
        Currency,
        Temperature,
        NumberBase
    }

    public abstract class UConverterBase : Models.ConvertInfo
    {
        public abstract string Convert(string data, int from, int to);

        public abstract bool IsFormatted(Models.ConvertInfo ci);
    }

    public class UConverter
    {
        public static readonly List<int> subcategoriesCount = new()
        {
            CCurrency.categoryCurrency.Count,
            CTemperature.categoryTemperature.Count,
            35
        };

        public static readonly List<UConverterBase> uConverter = new()
        {
            new CCurrency(),
            new CTemperature(),
            new CNumber.Base()
        };
    }
}
