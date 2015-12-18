using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;

namespace J.Common
{
    public static class LambdaHelper
    {
        public static T If<T>(this T t, Predicate<T> predicate, Action<T> action) where T : class
        {
            if (t == null) throw new ArgumentNullException();
            if (predicate(t)) action(t);
            return t;
        }
        public static T If<T>(this T t, Predicate<T> predicate, params Action<T>[] actions) where T : class
        {
            if (t == null) throw new ArgumentNullException();
            if (predicate(t))
            {
                foreach (var action in actions)
                    action(t);
            }
            return t;
        }

        public static T If<T>(this T t, Predicate<T> predicate, Func<T, T> func) where T : struct
        {
            return predicate(t) ? func(t) : t;
        }


        //string englishName = "apple";
        //string chineseName = englishName.Switch(
        //    new string[] { "apple", "orange", "banana", "pear" },
        //    new string[] { "苹果", "桔子", "香蕉", "梨" },
        //    "未知"
        //    );

        public static TOutput Switch<TOutput, TInput>(this TInput input, IEnumerable<TInput> inputSource, IEnumerable<TOutput> outputSource, TOutput defaultOutput)
        {
            IEnumerator<TInput> inputIterator = inputSource.GetEnumerator();
            IEnumerator<TOutput> outputIterator = outputSource.GetEnumerator();

            TOutput result = defaultOutput;
            while (inputIterator.MoveNext())
            {
                if (outputIterator.MoveNext())
                {
                    if (input.Equals(inputIterator.Current))
                    {
                        result = outputIterator.Current;
                        break;
                    }
                }
                else break;
            }
            return result;
        }

        public static void While<T>(this T t, Predicate<T> predicate, Action<T> action) where T : class
        {
            while (predicate(t)) action(t);
        }

        //people.While(
        //      p => p.WorkCount < 7,
        //      p => p.Work(),
        //      p => p.Eat(),
        //      p => p.Drink(),
        //      p => p.Rest()
        //          );

        public static void While<T>(this T t, Predicate<T> predicate, params Action<T>[] actions) where T : class
        {
            while (predicate(t))
            {
                foreach (var action in actions)
                    action(t);
            }
        }

        //含有
        public static bool InLike(this string t, string[] c)
        {
            bool inThis = false;
            foreach (var v in c)
            {
                if (t.Contains(v))
                {
                    inThis = true;
                    break;
                }
            }
            return inThis;
        }

        /**/
        /// <summary>
        /// 先执行操作，再返回自身
        /// </summary>
        public static T Do<T>(this T t, Action<T> action)
        {
            action(t);
            return t;
        }
    }
}
