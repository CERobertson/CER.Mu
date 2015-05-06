namespace CER.Graphs.SetExtensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class Set
    {
        public static string[] Empty { get { return new string[] { }; } }
    }

    public static class Extensions
    {
        public static void Assert_NoDifferences<T>(this T[] set1, T[] set0)
        {
            if (set1.Differences(set0).Count() > 0)
            {
                throw new Unexpected_difference_between_arrays_SetException("Differences detected between sets.");
            }
        }

        public static IEnumerable<SetContext<T>> Differences<T>(this T[] set1, T[] set0)
        {
            var duplicates0 = set0.Duplicates().Select(x => new SetContext<T> { Element = x, Set = 0 });
            var duplicates1 = set1.Duplicates().Select(x => new SetContext<T> { Element = x, Set = 1 });
            var duplicates = duplicates0.Union(duplicates1).OrderBy(x => x.Set);
            if (duplicates.Count() > 0)
            {
                foreach (var d in duplicates)
                {
                    yield return d;
                }
                throw new Duplicate_element_in_array_SetException("String arrarys should not have duplicate elements when used as sets in this extension.");
            }

            var unmatched_elements =
                set0.Select(x => new SetContext<T> { Element = x, Set = 0 }).Union(
                set1.Select(x => new SetContext<T> { Element = x, Set = 1 }))
                .GroupBy(x => x.Element)
                .Where(x => x.Count() < 2);
            if (unmatched_elements.Count() > 0)
            {
                foreach (var e in unmatched_elements.SelectMany(x => x))
                {
                    yield return e;
                }
            }
        }

        public static IEnumerable<T> Duplicates<T>(this T[] set)
        {
            foreach (var duplicate in set.GroupBy(x => x).Where(x => x.Count() > 1))
            {
                yield return duplicate.Key;
            }
        }

        /// <summary>
        /// public IEnumerable<Entity> { get; set; }
        /// </summary>
        public static IEnumerable<Entity> Entities(this object obj)
        {
            return obj.GetType().GetProperties()
                .Where(x => typeof(IEnumerable<Entity>).IsAssignableFrom(x.PropertyType))
                .SelectMany(x =>
                {
                    var enumeration = x.GetValue(obj);
                    if (enumeration != null)
                        return enumeration as IEnumerable<Entity>;
                    else
                        return new Entity[] { }.AsEnumerable();
                });
        }

        /// <summary>
        /// public static T
        /// </summary>
        public static IEnumerable<T> _Fields<T>(this Type t)
        {
            foreach (var f in t.GetFields().Where(x => x.FieldType == typeof(T)))
            {
                yield return (T)f.GetValue(null);
            }
        }

        /// <summary>
        /// public readonly T
        /// </summary>
        public static IEnumerable<T> _Fields<T>(this object obj)
        {
            foreach (var f in obj.GetType().GetFields().Where(x => x.FieldType == typeof(T) && x.IsInitOnly == true))
            {
                yield return (T)f.GetValue(obj);
            }
        }

        /// <summary>
        /// public T { get; }
        /// </summary>
        public static IEnumerable<T> _Properties<T>(this object obj)
        {
            foreach (var p in obj.GetType().GetProperties().Where(x => x.PropertyType == typeof(T)))
            {
                yield return (T)p.GetValue(obj);
            }
        }
    }

    public class SetContext<T>
    {
        public T Element { get; set; }
        public int Set { get; set; }
    }

    public class SetException : Exception { public SetException(string message) : base(message) { } }
    public class Duplicate_element_in_array_SetException : SetException { public Duplicate_element_in_array_SetException(string message) : base(message) { } }
    public class Unexpected_difference_between_arrays_SetException : SetException { public Unexpected_difference_between_arrays_SetException(string message) : base(message) { } }
}
