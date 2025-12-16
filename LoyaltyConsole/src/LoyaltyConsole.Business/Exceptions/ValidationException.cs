namespace LoyaltyConsole.Business.Exceptions
{
    public class ValidationException : Exception
    {
        public string PropName { get; set; }

        public ValidationException()
        {
        }

        public ValidationException(string? message) : base(message)
        {
        }

        public ValidationException(string propname, string? message) : base(message)
        {
            PropName = propname;
        }
    }
}
