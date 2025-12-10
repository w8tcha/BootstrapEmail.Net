using System;
using System.Collections.Generic;
using System.Linq;

namespace ExCSS;

internal static class CollectionExtensions
{
    extension<T>(IEnumerable<T> items)
    {
        public IEnumerable<T> Concat(T element)
        {
            foreach (var item in items) yield return item;

            yield return element;
        }

        public T GetItemByIndex(int index)
        {
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));

            var i = 0;

            foreach (var item in items)
            {   if (i++ == index)
                {
                    return item;
                }
            }

            throw new ArgumentOutOfRangeException(nameof(index));
        }

        public IEnumerable<object[]> ToObjectArray()
            => items.Select(i => new object[] { i });
    }
}