using System.CodeDom;

namespace LoyaltyConsole.Business.Exceptions
{
    public class InvalidDataException : Exception
    {
        public string PropName { get; set; }

        public InvalidDataException()
        {
        }

        public InvalidDataException(string? message) : base(message)
        {
        }

        public InvalidDataException(string propname, string? message) : base(message)
        {
            PropName = propname;
        }
    }
}
