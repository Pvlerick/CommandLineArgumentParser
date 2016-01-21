using System;
using System.Collections.Generic;

namespace ObjectMentor.Utilities
{
    internal class StringArrayArgumentMarshaler : IArgumentMarshaler
    {
        private string[] _value = Array.Empty<string>();

        public void Set(IIterator<string> currentArgument)
        {
            throw new NotImplementedException();
        }

        internal static string[] GetValue(IArgumentMarshaler argumentMarshaler)
        {
            if (argumentMarshaler != null && argumentMarshaler.GetType() == typeof(StringArrayArgumentMarshaler))
                return (argumentMarshaler as StringArrayArgumentMarshaler)._value;
            else
                return Array.Empty<string>();
        }
    }
}