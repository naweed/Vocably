using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace XGENO.Mobile.Framework.Extensions
{
    public static partial class ArrayExtensions
    {
        public static Array ToArray(this IEnumerable source, Type type)
        {
            var param = Expression.Parameter(typeof(IEnumerable), "source");
            var cast = Expression.Call(typeof(Enumerable), "Cast", new[] { type }, param);
            var toArray = Expression.Call(typeof(Enumerable), "ToArray", new[] { type }, cast);
            var lambda = Expression.Lambda<Func<IEnumerable, Array>>(toArray, param).Compile();

            return lambda(source);
        }
    }
}
