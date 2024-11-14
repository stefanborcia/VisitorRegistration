using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorBusinessLogic.Exceptions
{
    public class VisitorAlreadySignedInException : Exception
    {
        public VisitorAlreadySignedInException(string message) : base(message) { }
    }
}
