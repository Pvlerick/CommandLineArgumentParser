using System;
using System.Collections.Generic;

namespace ObjectMentor.Utilities
{
    internal class IntegerArgumentMarshaler : IArgumentMarshaler
    {
        private int _value = 0;

        public void Set(IIterator<string> currentArgument)
        {
            string parameter = null;

            try
            {
                parameter = currentArgument.Next();
                _value = int.Parse(parameter);
            }
            catch (InvalidOperationException)
            {
                throw new ArgsException(ErrorCode.MissingInteger);
            }
            catch (FormatException)
            {
                throw new ArgsException(ErrorCode.InvalidInteger);
            }
        }

        internal static int GetValue(IArgumentMarshaler argumentMarshaler)
        {
            if (argumentMarshaler != null && argumentMarshaler.GetType() == typeof(IntegerArgumentMarshaler))
                return (argumentMarshaler as IntegerArgumentMarshaler)._value;
            else
                return 0;
        }
    }
}