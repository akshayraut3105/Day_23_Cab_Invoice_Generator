using System;
using System.Collections.Generic;
using System.Text;

namespace Day_23_Cab_Invoice_Generator
{
    [Serializable]
    public class CabInvoiceException : Exception
    {
        // Custom Exception Class to denote the exception  internally generated
        public ExceptionType exceptionType;
        // Exception enum class to initialize the error messages
        public enum ExceptionType
        {
            INVALID_DISTANCE,
            INVALID_TIME,
            NULL_RIDES,
            INVALID_USER_ID,
            INVALID_RIDETYPE
        }
        // Parameterised constructor to override the base class message
        public CabInvoiceException(ExceptionType innerException, string message) : base(message)
        {
            this.exceptionType = innerException;
        }
    }
}