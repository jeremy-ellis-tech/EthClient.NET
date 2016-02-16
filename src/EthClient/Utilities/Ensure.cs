using System;
using System.Collections.Generic;

namespace Eth.Utilities
{
    /// <summary>
    /// Utility class for checking parameters fall within requirements.
    /// </summary>
    public static class Ensure
    {
        public static void EnsureParameterIsNotNull(object value, string paramName)
        {
            if(value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void EnsureStringIsNotNullOrEmpty(string value, string paramName)
        {
            EnsureParameterIsNotNull(value, paramName);

            if(Equals(value, String.Empty))
            {
                throw new ArgumentOutOfRangeException(paramName);
            }
        }

        public static void EnsureCountIsCorrect<T>(ICollection<T> value, int expectedLength, string paramName)
        {
            if(value.Count != expectedLength)
            {
                throw new ArgumentOutOfRangeException(paramName);
            }
        }
    }
}
