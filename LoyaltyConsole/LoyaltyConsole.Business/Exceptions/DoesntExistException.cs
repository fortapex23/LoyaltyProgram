namespace LoyaltyConsole.Business.Exceptions
{
    public class DoesntExistException : Exception
    {
        public string PropName { get; set; }

        public DoesntExistException()
        {
        }

        public DoesntExistException(string? message) : base(message)
        {
        }

        public DoesntExistException(string propname, string? message) : base(message)
        {
            PropName = propname;    
        }
    }
}
