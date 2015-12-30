using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mastercard.Exceptions
{
    public class WrongUserPasswordException : Exception
    {
        public WrongUserPasswordException(string message) : base(message)
        {
            
        }
    }
}