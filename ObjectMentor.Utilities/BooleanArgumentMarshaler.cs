namespace ObjectMentor.Utilities
{
    internal class BooleanArgumentMarshaler : IArgumentMarshaler
    {
        private bool _value = false;

        public void Set(IIterator<string> currentArgument)
        {
            _value = true;
        }

        public static bool GetValue(IArgumentMarshaler argumentMarshaler)
        {
            if (argumentMarshaler != null && argumentMarshaler.GetType() == typeof(BooleanArgumentMarshaler))
                return (argumentMarshaler as BooleanArgumentMarshaler)._value;
            else
                return false;
        }
    }
}