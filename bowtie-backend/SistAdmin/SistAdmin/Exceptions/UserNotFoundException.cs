﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mastercard.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message) : base(message)
        {
            
        }
    }
}