using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mastercard.Exceptions
{
    public class ServiceException : Exception
    {

		private List<string> details = new List<string>();

        public ServiceException(string message) : base(message)
        {
            
        }
		public ServiceException(string message, List<string> details)
			: base(message)
		{
			this.details = details;
		}

		public List<string> Details {
			get { return this.details; }
		}
    }
}