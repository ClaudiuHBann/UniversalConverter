using Shared.Requests;
using Shared.Responses;
using Shared.Exceptions;

using API.Entities;

namespace API.Services
{
public sealed class RadixService : BaseService<RadixRequest, RadixResponse>
{
    private const string Bases = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    private static readonly List<string> _numberBaseFromMax =
        Enumerable.Range(2, Bases.Length - 1)
            .Select(from => ToBase(ulong.MaxValue.ToString(), 10, (ulong)from, false))
            .ToList();

    private static readonly List<string> _fromTo =
        Enumerable.Range(2, Bases.Length - 1).Select(number => number.ToString()).ToList();

    public RadixService(UCContext context) : base(context)
    {
    }

    public override async Task<List<string>> FromTo() => await Task.FromResult(_fromTo);

    protected override async Task ConvertValidate(RadixRequest request)
    {
        await base.ConvertValidate(request);

        // check if the number contains invalid characters for the specific base
        var maxBaseIndex = ulong.Parse(request.From);
        var bases = Bases[..(int)maxBaseIndex];
        if (request.Numbers.Any(number => TrimPrefix(number, maxBaseIndex).Any(c => !bases.Contains(char.ToUpper(c)))))
        {
            throw new ValueException(
                $"The value converted from {request.From} to {request.To} contains invalid characters!");
        }
    }

    protected override async Task<RadixResponse> ConvertInternal(RadixRequest request)
    {
        var from = ulong.Parse(request.From);
        var to = ulong.Parse(request.To);

        RadixResponse response = new(request.Numbers.Select(number => ToBase(number, from, to)).ToList());
        return await Task.FromResult(response);
    }

    private static string TrimPrefix(string number, ulong from)
    {
        if (from == 16 && number.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
        {
            number = number[2..];
        }
        else if (from == 2 && number.StartsWith("0b", StringComparison.OrdinalIgnoreCase))
        {
            number = number[2..];
        }

        return number.TrimStart('0');
    }

    private static bool WillOverflow(string number, ulong from)
    {
        var numberBaseFromMax = _numberBaseFromMax[(int)from - 2];
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
        number = TrimPrefix(number.ToUpper(), from);
        if (number.Length == 0)
        {
            return "0";
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
        else
        {
            numberBase10 = ulong.Parse(number);
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
