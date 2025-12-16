namespace LoyaltyConsole.Business.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public string PropName { get; set; }

        public UnauthorizedException()
        {
        }

        public UnauthorizedException(string? message) : base(message)
        {
        }

        public UnauthorizedException(string propname, string? message) : base(message)
        {
            PropName = propname;
        }
    }
}
