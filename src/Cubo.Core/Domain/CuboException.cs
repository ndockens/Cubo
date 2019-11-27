using System;

namespace Cubo.Core.Domain
{
    public class CuboException : Exception
    {
        public string ErrorCode { get; }

        public CuboException()
        {

        }

        public CuboException(string errorCode)
         : this(errorCode, null, null, null)
        {

        }

        public CuboException(string errorCode, string message)
         : this(errorCode, null, message, null)
        {

        }

        public CuboException(string message, params object[] args)
         : this(null, null, message, args)
        {

        }

        public CuboException(string errorCode, string message, params object[] args)
         : this(errorCode, null, message, args)
        {

        }

        public CuboException(Exception innerException, string message, params object[] args)
         : this(null, innerException, message, args)
        {

        }

        public CuboException(string errorCode, Exception innerException, string message, params object[] args)
         : base(string.Format(message ?? "", args ?? Array.Empty<object>()), innerException)
        {
            ErrorCode = errorCode;
        }
    }
}