namespace Analyzer.lib
{
    //public sealed class IPv4Prefix
    //{
    //    public int Length { get; private set; }

    //    public IPv4Prefix(string number)
    //    {
    //        Length = this.Parse(number);
    //    }

    //    private int Parse(string number)
    //    {
    //        if (String.IsNullOrWhiteSpace(number))
    //            throw new ArgumentNullException("The prefix number cannot be null or empty");

    //        try
    //        {
    //            if (int.Parse(number) is < 0 or > 32)
    //                throw new ArgumentOutOfRangeException("IPv4 Prefix has to be in range 0 to 32.");

    //        }
    //        catch (FormatException)
    //        {
    //            throw new FormatException("IPv4 Prefix has to be a number");
    //        }

    //        return int.Parse(number);
    //    }
    //}

    public sealed class IPv4Prefix
    {
        public int Length { get; private set; }

        public IPv4Prefix(string prefix)
        {
            Length = Parse(prefix);
        }

        private int Parse(string prefix)
        {
            if (string.IsNullOrWhiteSpace(prefix))
                throw new ArgumentNullException(nameof(prefix), "Prefix cannot be null or empty.");

            if (!int.TryParse(prefix, out int length) || length < 0 || length > 32)
                throw new ArgumentOutOfRangeException(nameof(prefix), "Prefix must be a number between 0 and 32.");

            return length;
        }
    }
}
