using System.Numerics;

namespace Server.UConverter.CNumber
{
    public class Base
    {
        public static readonly string baseString = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string ConvertNumberBase(string number, int from, int to)
        {
            BigInteger base10Number = new(0);
            if (from != 10)
            {
                for (int i = number.Length - 1; i >= 0; i--)
                {
                    if (number[i] == '0')
                    {
                        continue;
                    }

                    base10Number += baseString.IndexOf(number[i]) * (BigInteger)Math.Pow(from, number.Length - i - 1);
                }
            }
            else
            {
                base10Number = BigInteger.Parse(number);
            }

            string baseNNumber = "";

            while (base10Number != 0)
            {
                baseNNumber = baseNNumber.Insert(0, baseString[(int)(base10Number % to)].ToString());
                base10Number /= to;
            }

            return baseNNumber;
        }
    }
}
