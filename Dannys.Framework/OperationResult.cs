using System;
using System.Collections.Generic;
using System.Text;

namespace Dannys.Framework
{
    public class OperationResult<T> : OperationResult
    {
        public OperationResult(T returnObject) : base()
        {
            ReturnObject = returnObject;
        }

        public OperationResult(string message) : base(message) { }

        public OperationResult(ValidationResult validation) : base(validation) { }

        public T ReturnObject { get; set; }
    }
    public class OperationResult
    {
        public OperationResult()
        {
            Status = OperationResultStatus.OK;
        }

        public OperationResult(string message)
        {
            Status = OperationResultStatus.Failed;
            Message = message;
        }

        public OperationResult(ValidationResult validation)
        {
            Status = validation.Passed ? OperationResultStatus.OK : OperationResultStatus.Failed;
            Validation = validation;
        }
        public OperationResultStatus Status { get; set; }

        public ValidationResult Validation { get; set; }

        public string Message { get; set; }
    }

    public enum OperationResultStatus
    {
        OK, Failed
    }
}
