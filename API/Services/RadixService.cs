using Shared.Requests;
using Shared.Responses;
using Shared.Exceptions;

namespace API.Services
{
public class RadixService : BaseService<RadixRequest, RadixResponse>
{
    private const string Bases = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public override async Task<List<string>> FromTo() =>
        await Task.FromResult(Enumerable.Range(2, Bases.Length - 1).Select(number => number.ToString()).ToList());

    protected override async Task Validate(RadixRequest request)
    {
        var fromTo = await FromTo();

        var fromUpper = request.From.ToUpper();
        var from = fromUpper.StartsWith("0X") ? fromUpper[2..] : fromUpper;
        if (!fromTo.Contains(from))
        {
            throw new FromToException(this, true);
        }

        var toUpper = request.To.ToUpper();
        var to = toUpper.StartsWith("0X") ? toUpper[2..] : toUpper;
        if (!fromTo.Contains(to))
        {
            throw new FromToException(this, false);
        }
    }

    public override async Task<RadixResponse> Convert(RadixRequest request)
    {
        await Validate(request);

        var from = ulong.Parse(request.From);
        var to = ulong.Parse(request.To);

        return new(request.Numbers.Select(number => ToBase(number, from, to)).ToList());
    }

    private static bool WillOverflow(string number, ulong from)
    {
        var numberBaseFromMax = ToBase(ulong.MaxValue.ToString(), 10, from, false);
        if (number.Length > numberBaseFromMax.Length)
        {
            return true;
        }
        else if (number.Length < numberBaseFromMax.Length)
        {
            return false;
        }
        else
        {
            return string.CompareOrdinal(number, numberBaseFromMax) > 0;
        }
    }

    private static string ToBase(string number, ulong from, ulong to, bool checkForOverflow = true)
    {
        number = number.ToUpper();
        if (number.StartsWith("0X"))
        {
            number = number[2..];
        }

        number = number.TrimStart('0');
        if (number.Length == 0)
        {
            return "0";
        }

        if (from == to)
        {
            return number;
        }

        if (checkForOverflow && WillOverflow(number, from))
        {
            throw new ValueException($"{number} is too big!");
        }

        ulong numberBase10 = 0;
        if (from != 10)
        {
            for (int i = number.Length - 1; i >= 0; i--)
            {
                if (number[i] == '0')
                {
                    continue;
                }

                numberBase10 += (ulong)Bases.IndexOf(number[i]) * (ulong)Math.Pow(from, number.Length - i - 1);
            }
        }

        string numberBaseN = "";
        while (numberBase10 != 0)
        {
            numberBaseN = Bases[(int)(numberBase10 % to)] + numberBaseN;
            numberBase10 /= to;
        }

        return numberBaseN;
    }
}
}
