using System.Collections.Generic;

namespace ObjectMentor.Utilities
{
    interface IArgumentMarshaler
    {
        void Set(IIterator<string> currentArgument);
    }
}
