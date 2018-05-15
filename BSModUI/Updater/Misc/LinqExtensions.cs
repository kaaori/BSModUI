using System;
using System.Collections.Generic;

namespace BSModUI.Updater.Misc
{
    public static class LinqExtensions
    {

        public static void ForEach<T1>(this IEnumerable<T1> collection, Action<T1> a)
        {
            foreach (var item in collection)
            {
                a(item);
            }

        }
    }
}
