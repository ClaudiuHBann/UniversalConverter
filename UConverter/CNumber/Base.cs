using System.Numerics;

namespace Server.UConverter.CNumber {
    public class Base : UConverterBase {
        public readonly string baseString = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public override bool IsFormatted(Models.ConvertInfo ci) {
            if (ci.Items is null) {
                return false;
            }

            foreach (var item in ci.Items) {
                if (string.IsNullOrWhiteSpace(item)) {
                    return false;
                }

                string newItem = item;
                if (item.StartsWith("0x") || item.StartsWith("0X")) {
                    newItem = item.Substring(2);
                }

                foreach (var c in newItem) {
                    Console.WriteLine((baseString.IndexOf(char.ToUpper(c)) + 1).ToString() + " > " + (ci.From + 2).ToString());

                    if (!char.IsLetterOrDigit(c) ||
                        baseString.IndexOf(char.ToUpper(c)) + 1 > ci.From + 2) {
                        return false;
                    }
                }
            }

            return true;
        }

        public override string Convert(string number, int from, int to) {
            from += 2;
            to += 2;

            if (number.StartsWith("0x") || number.StartsWith("0X")) {
                number = number.Substring(2);
            }

            if (number.StartsWith('0')) {
                number = number.TrimStart('0');

                if (number == "") {
                    return "0";
                }
            }

            BigInteger base10Number = new(0);
            if (from != 10) {
                for (int i = number.Length - 1; i >= 0; i--) {
                    if (number[i] == '0') {
                        continue;
                    }

                    base10Number += baseString.IndexOf(char.ToUpper(number[i])) * (BigInteger)Math.Pow(from, number.Length - i - 1);
                }
            } else {
                base10Number = BigInteger.Parse(number);
            }

            string baseNNumber = "";

            while (base10Number != 0) {
                baseNNumber = baseNNumber.Insert(0, baseString[(int)(base10Number % to)].ToString());
                base10Number /= to;
            }

            return baseNNumber;
        }
    }
}
