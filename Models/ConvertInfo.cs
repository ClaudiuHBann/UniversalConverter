namespace Server
{
    public class ConvertInfo
    {
        public byte Category { get; set; }

        public List<string>? Items { get; set; }

        public byte From { get; set; }

        public byte To { get; set; }
    }
}