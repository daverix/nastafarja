using System;
using System.Collections.Generic;

namespace TrafikverketFarjor
{
    public class OrderBy<T,TKey> : IComparer<T>
    {
        private readonly Func<T, TKey> _keySelector;
        private readonly bool _ascending;
        private readonly Comparer<TKey> _keyComparer;

        private OrderBy(Func<T, TKey> keySelector, bool ascending)
        {
            _keySelector = keySelector;
            _ascending = ascending;
            _keyComparer = Comparer<TKey>.Default;
        }

        public int Compare(T x, T y)
        {
            var ascendingResult = InnerCompare(x, y);
            return RevertReturnValueIfDescending(ascendingResult);
        }

        private int InnerCompare(T x, T y)
        {
            return BasicCompare(x, y, () => CompareKeys(_keySelector(x), _keySelector(y)));
        }

        private int CompareKeys(TKey x, TKey y)
        {
            return BasicCompare(x, y, () => _keyComparer.Compare(x, y));
        }

        private int RevertReturnValueIfDescending(int ascendingResult)
        {
            if (_ascending) return ascendingResult;
            if (ascendingResult == 0) return 0;
            if (ascendingResult == 1) return -1;
            if (ascendingResult == -1) return 1;

            return ascendingResult;
        }

        private static int BasicCompare<TN>(TN x, TN y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (Equals(x, y)) return 0;
            if (Equals(x, default(T))) return 1;
            if (Equals(y, default(T))) return -1;
            return 0;
        }

        private static int BasicCompare<TN>(TN x, TN y, Func<int> comparer)
        {
            var result = BasicCompare(x, y);
            return result != 0 ? result : comparer();
        }

        public static OrderBy<T, TKey> Ascending(Func<T, TKey> keySelector)
        {
            return new OrderBy<T, TKey>(keySelector, true);
        }

        public static OrderBy<T, TKey> Descending(Func<T, TKey> keySelector)
        {
            return new OrderBy<T, TKey>(keySelector, false);
        }
    }

    public static class OrderBy
    {
        public static IComparer<T> Ascending<T, TKey>(Func<T, TKey> keySelector)
        {
            return OrderBy<T, TKey>.Ascending(keySelector);
        }

        public static IComparer<T> Descending<T, TKey>(Func<T, TKey> keySelector)
        {
            return OrderBy<T, TKey>.Descending(keySelector);
        }

        public static void SortBy<T, TKey>(this List<T> collection, Func<T, TKey> keySelector)
        {
            collection.Sort(Ascending(keySelector));
        }

        public static void SortByDescending<T, TKey>(this List<T> collection, Func<T, TKey> keySelector)
        {
            collection.Sort(Descending(keySelector));
        }
    }
}