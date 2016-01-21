using System.Collections.Generic;

namespace ObjectMentor.Utilities
{
    public interface IIterator<T>
    {
        T Next();
        bool HasNext { get; }
        T Previous();
    }

    public static class IListExtensions
    {
        public static IIterator<T> GetIterator<T>(this IList<T> source) => new Iterator<T>(source);

        class Iterator<T> : IIterator<T>
        {
            private IList<T> _list;
            private int _currentIndex;

            public Iterator(IList<T> list)
            {
                _list = list;
                _currentIndex = -1;
            }

            public T Next() => _list[++_currentIndex];

            public bool HasNext => _currentIndex + 1 < _list.Count;

            public T Previous() => _list[--_currentIndex];
        }
    }
}
