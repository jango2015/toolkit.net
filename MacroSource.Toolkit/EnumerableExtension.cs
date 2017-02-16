using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MacroSource.Toolkit
{
    public static class EnumerableExtensions
    {

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            return source.Where(element => seenKeys.Add(keySelector(element)));
        }

        /// <summary>
        /// 打乱
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> Shuffle<TSource>(this IEnumerable<TSource> source)
        {
            return source.OrderBy(_ => Guid.NewGuid());
        }

        /// <summary>
        /// 取样，随机取出i个
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="i">要取出的个数</param>
        /// <returns></returns>
        public static IEnumerable<TSource> Sample<TSource>(this IEnumerable<TSource> source, int i)
        {
            return source.Shuffle().Take(i);
        }

        
    }
}
