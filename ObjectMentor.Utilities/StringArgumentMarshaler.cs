using System;
using System.Collections.Generic;

namespace ObjectMentor.Utilities
{
    internal class StringArgumentMarshaler : IArgumentMarshaler
    {
        private string _value = string.Empty;

        public void Set(IIterator<string> currentArgument)
        {
            try
            {
                _value = currentArgument.Next();
            }
            catch (InvalidOperationException)
            {
                throw new ArgsException(ErrorCode.MissingString);
            }
        }

        internal static string GetValue(IArgumentMarshaler argumentMarshaler)
        {
            if (argumentMarshaler != null && argumentMarshaler.GetType() == typeof(StringArgumentMarshaler))
                return (argumentMarshaler as StringArgumentMarshaler)._value;
            else
                return string.Empty;
        }
    }
}