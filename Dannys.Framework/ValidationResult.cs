using System;
using System.Collections.Generic;
using System.Text;

namespace Dannys.Framework
{
    public class ValidationResult
    {
        public bool Passed => Errors.Count == 0;

        public IList<ValidationError> Errors { get; } = new List<ValidationError>();

        public void AddError(string message)
        {
            Errors.Add(new ValidationError(message));
        }

        public void AddError(string property, string message)
        {
            Errors.Add(new ValidationError(property, message));
        }
    }

    public class ValidationError
    {
        public ValidationError(string message)
        {
            Property = "";
            Message = message;
        }
        public ValidationError(string property, string message)
        {
            Property = property;
            Message = message;
        }

        public string Message { get; set; }

        public string Property { get; set; }
    }
}
