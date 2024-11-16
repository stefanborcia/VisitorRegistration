using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorBusinessLogic.Exceptions
{
    public class ValidationException : Exception
    {
        public List<string> ValidationErrors { get; }

        public ValidationException(List<string> validationErrors)
        {
            ValidationErrors = validationErrors;
        }
    }
}
