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

        public virtual bool IsFormatted()
        {
            foreach (string item in Items)
            {
                foreach (char c in item)
                {
                    if (char.IsDigit(c) is false && " ,.\n".IndexOf(c) == -1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
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
