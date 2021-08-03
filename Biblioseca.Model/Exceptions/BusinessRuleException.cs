using System;

namespace Biblioseca.Model.Exceptions
{
    public class BusinessRuleException : ApplicationException
    {
        public BusinessRuleException(string message) : base(message)
        {
        }
    }
}