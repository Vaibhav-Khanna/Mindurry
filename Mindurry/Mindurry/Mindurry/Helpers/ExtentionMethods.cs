﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindurry
{
    public static class ExtentionMethods
    {
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            return items.GroupBy(property).Select(x => x.First());
        }
    }
}
