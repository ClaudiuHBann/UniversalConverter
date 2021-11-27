namespace Server.Models
{
    public class ConvertInfo
    {
        public int Category { get; set; }

        public List<string>? Items { get; set; }

        public int From { get; set; }

        public int To { get; set; }
    }
}