using System;
using System.Collections.Generic;

namespace ObjectMentor.Utilities
{
    internal class DoubleArgumentMarshaler : IArgumentMarshaler
    {
        private double _value = 0.0;

        public void Set(IIterator<string> currentArgument)
        {
            string parameter = null;

            try
            {
                parameter = currentArgument.Next();
                _value = double.Parse(parameter);
            }
            catch (InvalidOperationException)
            {
                throw new ArgsException(ErrorCode.MissingDouble);
            }
            catch (FormatException)
            {
                throw new ArgsException(ErrorCode.MissingDouble);
            }
        }

        public static double GetValue(IArgumentMarshaler argumentMarshaler)
        {
            if (argumentMarshaler != null && argumentMarshaler.GetType() == typeof(BooleanArgumentMarshaler))
                return (argumentMarshaler as DoubleArgumentMarshaler)._value;
            else
                return 0.0;
        }
    }
}