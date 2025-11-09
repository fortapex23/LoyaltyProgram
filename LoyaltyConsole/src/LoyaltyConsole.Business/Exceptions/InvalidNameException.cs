namespace LoyaltyConsole.Business.Exceptions
{
    public class InvalidNameException : Exception
    {
        public string PropName { get; set; }

        public InvalidNameException()
        {
        }

        public InvalidNameException(string? message) : base(message)
        {
        }

        public InvalidNameException(string propname, string? message) : base(message)
        {
            PropName = propname;
        }
    }
}
