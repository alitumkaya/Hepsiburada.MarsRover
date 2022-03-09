using System;
using System.Collections.Generic;
using System.Text;

namespace Hepsiburada.MarsRover.Domain.Common
{
    public static class Check
    {
        public static void NotNullOrNegative(int value,string paramName)
        {
            if (value < 0)
                throw new NegativeOrNullException(paramName);
        }
        public static void NotNullOrWhiteSpace(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new NegativeOrNullException(paramName);
        }
    }

    public class NegativeOrNullException : ArgumentOutOfRangeException
    {
        public NegativeOrNullException(string paramName):base(paramName)
        {
        }
        public override string Message { get { return $"{ParamName} could not be null or negative!"; } }
    }
    public class NullOrWhiteSpaceException : ArgumentNullException
    {
        public NullOrWhiteSpaceException(string paramName) : base(paramName)
        {
        }
        public override string Message { get { return $"{ParamName} could not be null or white space!"; } }
    }
}
