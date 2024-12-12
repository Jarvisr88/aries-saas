namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class Either
    {
        public static T Collapse<T>(this Either<T, T> value)
        {
            Func<T, T> func1 = <>c__11<T>.<>9__11_0;
            if (<>c__11<T>.<>9__11_0 == null)
            {
                Func<T, T> local1 = <>c__11<T>.<>9__11_0;
                func1 = <>c__11<T>.<>9__11_0 = left => left;
            }
            return value.Match<T>(func1, <>c__11<T>.<>9__11_1 ??= right => right);
        }

        public static Either<IEnumerable<TLeft>, TResult> Combine2<TLeft, T1, T2, TResult>(Either<TLeft, T1> x1, Either<TLeft, T2> x2, Func<T1, T2, TResult> combine)
        {
            IEnumerable<TLeft> source = Lefts<TLeft, T1, T2>(x1, x2);
            return (!source.Any<TLeft>() ? ((Either<IEnumerable<TLeft>, TResult>) combine(x1.ToRight<TLeft, T1>(), x2.ToRight<TLeft, T2>())) : Either<IEnumerable<TLeft>, TResult>.Left(source));
        }

        public static Either<IEnumerable<TLeft>, TResult> Combine3<TLeft, T1, T2, T3, TResult>(Either<TLeft, T1> x1, Either<TLeft, T2> x2, Either<TLeft, T3> x3, Func<T1, T2, T3, TResult> combine)
        {
            IEnumerable<TLeft> source = Lefts<TLeft, T1, T2, T3>(x1, x2, x3);
            return (!source.Any<TLeft>() ? ((Either<IEnumerable<TLeft>, TResult>) combine(x1.ToRight<TLeft, T1>(), x2.ToRight<TLeft, T2>(), x3.ToRight<TLeft, T3>())) : Either<IEnumerable<TLeft>, TResult>.Left(source));
        }

        public static Either<TLeftNew, TRightNew> CombineMany<TLeft, TRight, TLeftNew, TRightNew>(this IEnumerable<Either<TLeft, TRight>> values, Func<IEnumerable<TLeft>, TLeftNew> combineLefts, Func<IEnumerable<TRight>, TRightNew> combineRights)
        {
            Func<Either<TLeft, TRight>, bool> predicate = <>c__17<TLeft, TRight, TLeftNew, TRightNew>.<>9__17_0;
            if (<>c__17<TLeft, TRight, TLeftNew, TRightNew>.<>9__17_0 == null)
            {
                Func<Either<TLeft, TRight>, bool> local1 = <>c__17<TLeft, TRight, TLeftNew, TRightNew>.<>9__17_0;
                predicate = <>c__17<TLeft, TRight, TLeftNew, TRightNew>.<>9__17_0 = x => x.IsLeft();
            }
            IEnumerable<Either<TLeft, TRight>> source = values.Where<Either<TLeft, TRight>>(predicate);
            if (source.Any<Either<TLeft, TRight>>())
            {
                Func<Either<TLeft, TRight>, TLeft> func2 = <>c__17<TLeft, TRight, TLeftNew, TRightNew>.<>9__17_1;
                if (<>c__17<TLeft, TRight, TLeftNew, TRightNew>.<>9__17_1 == null)
                {
                    Func<Either<TLeft, TRight>, TLeft> local2 = <>c__17<TLeft, TRight, TLeftNew, TRightNew>.<>9__17_1;
                    func2 = <>c__17<TLeft, TRight, TLeftNew, TRightNew>.<>9__17_1 = x => x.ToLeft<TLeft, TRight>();
                }
                return combineLefts(source.Select<Either<TLeft, TRight>, TLeft>(func2));
            }
            Func<Either<TLeft, TRight>, TRight> selector = <>c__17<TLeft, TRight, TLeftNew, TRightNew>.<>9__17_2;
            if (<>c__17<TLeft, TRight, TLeftNew, TRightNew>.<>9__17_2 == null)
            {
                Func<Either<TLeft, TRight>, TRight> local3 = <>c__17<TLeft, TRight, TLeftNew, TRightNew>.<>9__17_2;
                selector = <>c__17<TLeft, TRight, TLeftNew, TRightNew>.<>9__17_2 = x => x.ToRight<TLeft, TRight>();
            }
            return combineRights(values.Select<Either<TLeft, TRight>, TRight>(selector));
        }

        public static void IgnoreLeft<TLeft, TRight>(this Either<TLeft, TRight> value, Action<TRight> right)
        {
            Action<TLeft> left = <>c__7<TLeft, TRight>.<>9__7_0;
            if (<>c__7<TLeft, TRight>.<>9__7_0 == null)
            {
                Action<TLeft> local1 = <>c__7<TLeft, TRight>.<>9__7_0;
                left = <>c__7<TLeft, TRight>.<>9__7_0 = delegate (TLeft e) {
                };
            }
            value.Match(left, right);
        }

        public static TLeft LeftOrDefault<TLeft, TRight>(this Either<TLeft, TRight> value)
        {
            Func<TLeft, TLeft> func1 = <>c__3<TLeft, TRight>.<>9__3_0;
            if (<>c__3<TLeft, TRight>.<>9__3_0 == null)
            {
                Func<TLeft, TLeft> local1 = <>c__3<TLeft, TRight>.<>9__3_0;
                func1 = <>c__3<TLeft, TRight>.<>9__3_0 = left => left;
            }
            return value.Match<TLeft>(func1, <>c__3<TLeft, TRight>.<>9__3_1 ??= right => default(TLeft));
        }

        public static IEnumerable<TLeft> Lefts<TLeft, TRight>(this IEnumerable<Either<TLeft, TRight>> source)
        {
            Func<Either<TLeft, TRight>, bool> predicate = <>c__16<TLeft, TRight>.<>9__16_0;
            if (<>c__16<TLeft, TRight>.<>9__16_0 == null)
            {
                Func<Either<TLeft, TRight>, bool> local1 = <>c__16<TLeft, TRight>.<>9__16_0;
                predicate = <>c__16<TLeft, TRight>.<>9__16_0 = x => x.IsLeft();
            }
            Func<Either<TLeft, TRight>, TLeft> selector = <>c__16<TLeft, TRight>.<>9__16_1;
            if (<>c__16<TLeft, TRight>.<>9__16_1 == null)
            {
                Func<Either<TLeft, TRight>, TLeft> local2 = <>c__16<TLeft, TRight>.<>9__16_1;
                selector = <>c__16<TLeft, TRight>.<>9__16_1 = x => x.ToLeft<TLeft, TRight>();
            }
            return source.Where<Either<TLeft, TRight>>(predicate).Select<Either<TLeft, TRight>, TLeft>(selector);
        }

        [IteratorStateMachine(typeof(<Lefts>d__19))]
        private static IEnumerable<TLeft> Lefts<TLeft, T1, T2>(Either<TLeft, T1> x1, Either<TLeft, T2> x2)
        {
            if (x1.IsLeft())
            {
                yield return x1.ToLeft<TLeft, T1>();
            }
            if (x2.IsLeft())
            {
                yield return x2.ToLeft<TLeft, T2>();
            }
        }

        [IteratorStateMachine(typeof(<Lefts>d__21))]
        private static IEnumerable<TLeft> Lefts<TLeft, T1, T2, T3>(Either<TLeft, T1> x1, Either<TLeft, T2> x2, Either<TLeft, T3> x3)
        {
            if (x1.IsLeft())
            {
                yield return x1.ToLeft<TLeft, T1>();
            }
            if (x2.IsLeft())
            {
                yield return x2.ToLeft<TLeft, T2>();
            }
            while (true)
            {
                if (x3.IsLeft())
                {
                    yield return x3.ToLeft<TLeft, T3>();
                }
            }
        }

        public static Either<TLeft, TRight> MaybeLeft<TLeft, TRight>(TLeft maybeLeft, TRight right) where TLeft: class
        {
            Func<TLeft, Either<TLeft, TRight>> evaluator = <>c__0<TLeft, TRight>.<>9__0_0;
            if (<>c__0<TLeft, TRight>.<>9__0_0 == null)
            {
                Func<TLeft, Either<TLeft, TRight>> local1 = <>c__0<TLeft, TRight>.<>9__0_0;
                evaluator = <>c__0<TLeft, TRight>.<>9__0_0 = x => Either<TLeft, TRight>.Left(x);
            }
            return maybeLeft.Return<TLeft, Either<TLeft, TRight>>(evaluator, () => right);
        }

        public static Either<TLeft, TRight> MaybeRight<TLeft, TRight>(TRight maybeRight, TLeft left) where TRight: class
        {
            Func<TRight, Either<TLeft, TRight>> evaluator = <>c__1<TLeft, TRight>.<>9__1_0;
            if (<>c__1<TLeft, TRight>.<>9__1_0 == null)
            {
                Func<TRight, Either<TLeft, TRight>> local1 = <>c__1<TLeft, TRight>.<>9__1_0;
                evaluator = <>c__1<TLeft, TRight>.<>9__1_0 = x => Either<TLeft, TRight>.Right(x);
            }
            return maybeRight.Return<TRight, Either<TLeft, TRight>>(evaluator, ((Func<Either<TLeft, TRight>>) (() => left)));
        }

        public static TResult Partition<TLeft, TRight, TAccumulate, TResult>(this IEnumerable<Either<TLeft, TRight>> source, TAccumulate seed, Func<TAccumulate, TLeft, TAccumulate> func, Func<TAccumulate, TRight[], TResult> resultSelector)
        {
            TRight[] localArray = source.SelectMany<Either<TLeft, TRight>, TRight>(delegate (Either<TLeft, TRight> item) {
                Func<TLeft, IEnumerable<TRight>> <>9__1;
                Func<TLeft, IEnumerable<TRight>> left = <>9__1;
                if (<>9__1 == null)
                {
                    Func<TLeft, IEnumerable<TRight>> local1 = <>9__1;
                    left = <>9__1 = delegate (TLeft x) {
                        seed = func(seed, x);
                        return Enumerable.Empty<TRight>();
                    };
                }
                return item.Match<IEnumerable<TRight>>(left, <>c__14<TLeft, TRight, TAccumulate, TResult>.<>9__14_2 ??= x => x.Yield<TRight>());
            }).ToArray<TRight>();
            return resultSelector(seed, localArray);
        }

        public static TRight RightOrDefault<TLeft, TRight>(this Either<TLeft, TRight> value)
        {
            Func<TLeft, TRight> func1 = <>c__4<TLeft, TRight>.<>9__4_0;
            if (<>c__4<TLeft, TRight>.<>9__4_0 == null)
            {
                Func<TLeft, TRight> local1 = <>c__4<TLeft, TRight>.<>9__4_0;
                func1 = <>c__4<TLeft, TRight>.<>9__4_0 = left => default(TRight);
            }
            return value.Match<TRight>(func1, <>c__4<TLeft, TRight>.<>9__4_1 ??= right => right);
        }

        public static IEnumerable<TRight> Rights<TLeft, TRight>(this IEnumerable<Either<TLeft, TRight>> source)
        {
            Func<Either<TLeft, TRight>, bool> predicate = <>c__15<TLeft, TRight>.<>9__15_0;
            if (<>c__15<TLeft, TRight>.<>9__15_0 == null)
            {
                Func<Either<TLeft, TRight>, bool> local1 = <>c__15<TLeft, TRight>.<>9__15_0;
                predicate = <>c__15<TLeft, TRight>.<>9__15_0 = x => x.IsRight();
            }
            Func<Either<TLeft, TRight>, TRight> selector = <>c__15<TLeft, TRight>.<>9__15_1;
            if (<>c__15<TLeft, TRight>.<>9__15_1 == null)
            {
                Func<Either<TLeft, TRight>, TRight> local2 = <>c__15<TLeft, TRight>.<>9__15_1;
                selector = <>c__15<TLeft, TRight>.<>9__15_1 = x => x.ToRight<TLeft, TRight>();
            }
            return source.Where<Either<TLeft, TRight>>(predicate).Select<Either<TLeft, TRight>, TRight>(selector);
        }

        public static Either<TLeft, TRightNew> Select<TLeft, TRight, TRightNew>(this Either<TLeft, TRight> value, Func<TRight, TRightNew> selector)
        {
            Func<TLeft, Either<TLeft, TRightNew>> func1 = <>c__8<TLeft, TRight, TRightNew>.<>9__8_0;
            if (<>c__8<TLeft, TRight, TRightNew>.<>9__8_0 == null)
            {
                Func<TLeft, Either<TLeft, TRightNew>> local1 = <>c__8<TLeft, TRight, TRightNew>.<>9__8_0;
                func1 = <>c__8<TLeft, TRight, TRightNew>.<>9__8_0 = left => Either<TLeft, TRightNew>.Left(left);
            }
            return value.Match<Either<TLeft, TRightNew>>(func1, right => Either<TLeft, TRightNew>.Right(selector(right)));
        }

        public static Either<TLeftNew, TRight> SelectLeft<TLeft, TRight, TLeftNew>(this Either<TLeft, TRight> value, Func<TLeft, TLeftNew> selector)
        {
            Func<TRight, Either<TLeftNew, TRight>> func1 = <>c__9<TLeft, TRight, TLeftNew>.<>9__9_1;
            if (<>c__9<TLeft, TRight, TLeftNew>.<>9__9_1 == null)
            {
                Func<TRight, Either<TLeftNew, TRight>> local1 = <>c__9<TLeft, TRight, TLeftNew>.<>9__9_1;
                func1 = <>c__9<TLeft, TRight, TLeftNew>.<>9__9_1 = right => Either<TLeftNew, TRight>.Right(right);
            }
            return value.Match<Either<TLeftNew, TRight>>(left => Either<TLeftNew, TRight>.Left(selector(left)), func1);
        }

        public static Either<TLeft, TRightNew> SelectMany<TLeft, TRight, TRightNew>(this Either<TLeft, TRight> value, Func<TRight, Either<TLeft, TRightNew>> selector)
        {
            Func<TLeft, Either<TLeft, TRightNew>> func1 = <>c__10<TLeft, TRight, TRightNew>.<>9__10_0;
            if (<>c__10<TLeft, TRight, TRightNew>.<>9__10_0 == null)
            {
                Func<TLeft, Either<TLeft, TRightNew>> local1 = <>c__10<TLeft, TRight, TRightNew>.<>9__10_0;
                func1 = <>c__10<TLeft, TRight, TRightNew>.<>9__10_0 = left => Either<TLeft, TRightNew>.Left(left);
            }
            return value.Match<Either<TLeft, TRightNew>>(func1, right => selector(right));
        }

        public static Either<TLeft, TRight[]> Sequence<TLeft, TRight>(this IEnumerable<Either<TLeft, TRight>> source)
        {
            Option<TLeft> left = Option<TLeft>.Empty;
            TRight[] right = source.SelectWhile<Either<TLeft, TRight>, TRight>(delegate (Either<TLeft, TRight> item) {
                Func<TLeft, Option<TRight>> <>9__1;
                Func<TLeft, Option<TRight>> func2 = <>9__1;
                if (<>9__1 == null)
                {
                    Func<TLeft, Option<TRight>> local1 = <>9__1;
                    func2 = <>9__1 = delegate (TLeft x) {
                        left = x.AsOption<TLeft>();
                        return Option<TRight>.Empty;
                    };
                }
                return item.Match<Option<TRight>>(func2, <>c__13<TLeft, TRight>.<>9__13_2 ??= x => x.AsOption<TRight>());
            }).ToArray<TRight>();
            Func<TLeft, Either<TLeft, TRight[]>> getValue = <>c__13<TLeft, TRight>.<>9__13_3;
            if (<>c__13<TLeft, TRight>.<>9__13_3 == null)
            {
                Func<TLeft, Either<TLeft, TRight[]>> local1 = <>c__13<TLeft, TRight>.<>9__13_3;
                getValue = <>c__13<TLeft, TRight>.<>9__13_3 = (Func<TLeft, Either<TLeft, TRight[]>>) (x => x);
            }
            return left.Match<Either<TLeft, TRight[]>>(getValue, (Func<Either<TLeft, TRight[]>>) (() => right));
        }

        public static TLeft ToLeft<TLeft, TRight>(this Either<TLeft, TRight> value)
        {
            Func<TLeft, TLeft> func1 = <>c__2<TLeft, TRight>.<>9__2_0;
            if (<>c__2<TLeft, TRight>.<>9__2_0 == null)
            {
                Func<TLeft, TLeft> local1 = <>c__2<TLeft, TRight>.<>9__2_0;
                func1 = <>c__2<TLeft, TRight>.<>9__2_0 = left => left;
            }
            return value.Match<TLeft>(func1, <>c__2<TLeft, TRight>.<>9__2_1 ??= delegate (TRight right) {
                throw new InvalidOperationException();
            });
        }

        public static TRight ToRight<TLeft, TRight>(this Either<TLeft, TRight> value)
        {
            Func<TLeft, TRight> left = <>c__5<TLeft, TRight>.<>9__5_0;
            if (<>c__5<TLeft, TRight>.<>9__5_0 == null)
            {
                Func<TLeft, TRight> local1 = <>c__5<TLeft, TRight>.<>9__5_0;
                left = <>c__5<TLeft, TRight>.<>9__5_0 = delegate (TLeft left) {
                    throw new InvalidOperationException();
                };
            }
            return value.Match<TRight>(left, <>c__5<TLeft, TRight>.<>9__5_1 ??= right => right);
        }

        public static Either<Exception, TResult> Try<TResult>(Func<Either<Exception, TResult>> func)
        {
            try
            {
                return func();
            }
            catch (Exception exception1)
            {
                return exception1;
            }
        }

        public static TRight UnwrapException<TException, TRight>(this Either<TException, TRight> value) where TException: Exception
        {
            Func<TException, TRight> left = <>c__6<TException, TRight>.<>9__6_0;
            if (<>c__6<TException, TRight>.<>9__6_0 == null)
            {
                Func<TException, TRight> local1 = <>c__6<TException, TRight>.<>9__6_0;
                left = <>c__6<TException, TRight>.<>9__6_0 = delegate (TException left) {
                    throw left;
                };
            }
            return value.Match<TRight>(left, <>c__6<TException, TRight>.<>9__6_1 ??= right => right);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__0<TLeft, TRight> where TLeft: class
        {
            public static readonly Either.<>c__0<TLeft, TRight> <>9;
            public static Func<TLeft, Either<TLeft, TRight>> <>9__0_0;

            static <>c__0()
            {
                Either.<>c__0<TLeft, TRight>.<>9 = new Either.<>c__0<TLeft, TRight>();
            }

            internal Either<TLeft, TRight> <MaybeLeft>b__0_0(TLeft x) => 
                Either<TLeft, TRight>.Left(x);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__1<TLeft, TRight> where TRight: class
        {
            public static readonly Either.<>c__1<TLeft, TRight> <>9;
            public static Func<TRight, Either<TLeft, TRight>> <>9__1_0;

            static <>c__1()
            {
                Either.<>c__1<TLeft, TRight>.<>9 = new Either.<>c__1<TLeft, TRight>();
            }

            internal Either<TLeft, TRight> <MaybeRight>b__1_0(TRight x) => 
                Either<TLeft, TRight>.Right(x);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__10<TLeft, TRight, TRightNew>
        {
            public static readonly Either.<>c__10<TLeft, TRight, TRightNew> <>9;
            public static Func<TLeft, Either<TLeft, TRightNew>> <>9__10_0;

            static <>c__10()
            {
                Either.<>c__10<TLeft, TRight, TRightNew>.<>9 = new Either.<>c__10<TLeft, TRight, TRightNew>();
            }

            internal Either<TLeft, TRightNew> <SelectMany>b__10_0(TLeft left) => 
                Either<TLeft, TRightNew>.Left(left);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__11<T>
        {
            public static readonly Either.<>c__11<T> <>9;
            public static Func<T, T> <>9__11_0;
            public static Func<T, T> <>9__11_1;

            static <>c__11()
            {
                Either.<>c__11<T>.<>9 = new Either.<>c__11<T>();
            }

            internal T <Collapse>b__11_0(T left) => 
                left;

            internal T <Collapse>b__11_1(T right) => 
                right;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__13<TLeft, TRight>
        {
            public static readonly Either.<>c__13<TLeft, TRight> <>9;
            public static Func<TRight, Option<TRight>> <>9__13_2;
            public static Func<TLeft, Either<TLeft, TRight[]>> <>9__13_3;

            static <>c__13()
            {
                Either.<>c__13<TLeft, TRight>.<>9 = new Either.<>c__13<TLeft, TRight>();
            }

            internal Option<TRight> <Sequence>b__13_2(TRight x) => 
                x.AsOption<TRight>();

            internal Either<TLeft, TRight[]> <Sequence>b__13_3(TLeft x) => 
                x;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__14<TLeft, TRight, TAccumulate, TResult>
        {
            public static readonly Either.<>c__14<TLeft, TRight, TAccumulate, TResult> <>9;
            public static Func<TRight, IEnumerable<TRight>> <>9__14_2;

            static <>c__14()
            {
                Either.<>c__14<TLeft, TRight, TAccumulate, TResult>.<>9 = new Either.<>c__14<TLeft, TRight, TAccumulate, TResult>();
            }

            internal IEnumerable<TRight> <Partition>b__14_2(TRight x) => 
                x.Yield<TRight>();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__15<TLeft, TRight>
        {
            public static readonly Either.<>c__15<TLeft, TRight> <>9;
            public static Func<Either<TLeft, TRight>, bool> <>9__15_0;
            public static Func<Either<TLeft, TRight>, TRight> <>9__15_1;

            static <>c__15()
            {
                Either.<>c__15<TLeft, TRight>.<>9 = new Either.<>c__15<TLeft, TRight>();
            }

            internal bool <Rights>b__15_0(Either<TLeft, TRight> x) => 
                x.IsRight();

            internal TRight <Rights>b__15_1(Either<TLeft, TRight> x) => 
                x.ToRight<TLeft, TRight>();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__16<TLeft, TRight>
        {
            public static readonly Either.<>c__16<TLeft, TRight> <>9;
            public static Func<Either<TLeft, TRight>, bool> <>9__16_0;
            public static Func<Either<TLeft, TRight>, TLeft> <>9__16_1;

            static <>c__16()
            {
                Either.<>c__16<TLeft, TRight>.<>9 = new Either.<>c__16<TLeft, TRight>();
            }

            internal bool <Lefts>b__16_0(Either<TLeft, TRight> x) => 
                x.IsLeft();

            internal TLeft <Lefts>b__16_1(Either<TLeft, TRight> x) => 
                x.ToLeft<TLeft, TRight>();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__17<TLeft, TRight, TLeftNew, TRightNew>
        {
            public static readonly Either.<>c__17<TLeft, TRight, TLeftNew, TRightNew> <>9;
            public static Func<Either<TLeft, TRight>, bool> <>9__17_0;
            public static Func<Either<TLeft, TRight>, TLeft> <>9__17_1;
            public static Func<Either<TLeft, TRight>, TRight> <>9__17_2;

            static <>c__17()
            {
                Either.<>c__17<TLeft, TRight, TLeftNew, TRightNew>.<>9 = new Either.<>c__17<TLeft, TRight, TLeftNew, TRightNew>();
            }

            internal bool <CombineMany>b__17_0(Either<TLeft, TRight> x) => 
                x.IsLeft();

            internal TLeft <CombineMany>b__17_1(Either<TLeft, TRight> x) => 
                x.ToLeft<TLeft, TRight>();

            internal TRight <CombineMany>b__17_2(Either<TLeft, TRight> x) => 
                x.ToRight<TLeft, TRight>();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__2<TLeft, TRight>
        {
            public static readonly Either.<>c__2<TLeft, TRight> <>9;
            public static Func<TLeft, TLeft> <>9__2_0;
            public static Func<TRight, TLeft> <>9__2_1;

            static <>c__2()
            {
                Either.<>c__2<TLeft, TRight>.<>9 = new Either.<>c__2<TLeft, TRight>();
            }

            internal TLeft <ToLeft>b__2_0(TLeft left) => 
                left;

            internal TLeft <ToLeft>b__2_1(TRight right)
            {
                throw new InvalidOperationException();
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__3<TLeft, TRight>
        {
            public static readonly Either.<>c__3<TLeft, TRight> <>9;
            public static Func<TLeft, TLeft> <>9__3_0;
            public static Func<TRight, TLeft> <>9__3_1;

            static <>c__3()
            {
                Either.<>c__3<TLeft, TRight>.<>9 = new Either.<>c__3<TLeft, TRight>();
            }

            internal TLeft <LeftOrDefault>b__3_0(TLeft left) => 
                left;

            internal TLeft <LeftOrDefault>b__3_1(TRight right) => 
                default(TLeft);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__4<TLeft, TRight>
        {
            public static readonly Either.<>c__4<TLeft, TRight> <>9;
            public static Func<TLeft, TRight> <>9__4_0;
            public static Func<TRight, TRight> <>9__4_1;

            static <>c__4()
            {
                Either.<>c__4<TLeft, TRight>.<>9 = new Either.<>c__4<TLeft, TRight>();
            }

            internal TRight <RightOrDefault>b__4_0(TLeft left) => 
                default(TRight);

            internal TRight <RightOrDefault>b__4_1(TRight right) => 
                right;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__5<TLeft, TRight>
        {
            public static readonly Either.<>c__5<TLeft, TRight> <>9;
            public static Func<TLeft, TRight> <>9__5_0;
            public static Func<TRight, TRight> <>9__5_1;

            static <>c__5()
            {
                Either.<>c__5<TLeft, TRight>.<>9 = new Either.<>c__5<TLeft, TRight>();
            }

            internal TRight <ToRight>b__5_0(TLeft left)
            {
                throw new InvalidOperationException();
            }

            internal TRight <ToRight>b__5_1(TRight right) => 
                right;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__6<TException, TRight> where TException: Exception
        {
            public static readonly Either.<>c__6<TException, TRight> <>9;
            public static Func<TException, TRight> <>9__6_0;
            public static Func<TRight, TRight> <>9__6_1;

            static <>c__6()
            {
                Either.<>c__6<TException, TRight>.<>9 = new Either.<>c__6<TException, TRight>();
            }

            internal TRight <UnwrapException>b__6_0(TException left)
            {
                throw left;
            }

            internal TRight <UnwrapException>b__6_1(TRight right) => 
                right;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__7<TLeft, TRight>
        {
            public static readonly Either.<>c__7<TLeft, TRight> <>9;
            public static Action<TLeft> <>9__7_0;

            static <>c__7()
            {
                Either.<>c__7<TLeft, TRight>.<>9 = new Either.<>c__7<TLeft, TRight>();
            }

            internal void <IgnoreLeft>b__7_0(TLeft e)
            {
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__8<TLeft, TRight, TRightNew>
        {
            public static readonly Either.<>c__8<TLeft, TRight, TRightNew> <>9;
            public static Func<TLeft, Either<TLeft, TRightNew>> <>9__8_0;

            static <>c__8()
            {
                Either.<>c__8<TLeft, TRight, TRightNew>.<>9 = new Either.<>c__8<TLeft, TRight, TRightNew>();
            }

            internal Either<TLeft, TRightNew> <Select>b__8_0(TLeft left) => 
                Either<TLeft, TRightNew>.Left(left);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__9<TLeft, TRight, TLeftNew>
        {
            public static readonly Either.<>c__9<TLeft, TRight, TLeftNew> <>9;
            public static Func<TRight, Either<TLeftNew, TRight>> <>9__9_1;

            static <>c__9()
            {
                Either.<>c__9<TLeft, TRight, TLeftNew>.<>9 = new Either.<>c__9<TLeft, TRight, TLeftNew>();
            }

            internal Either<TLeftNew, TRight> <SelectLeft>b__9_1(TRight right) => 
                Either<TLeftNew, TRight>.Right(right);
        }

        [CompilerGenerated]
        private sealed class <Lefts>d__19<TLeft, T1, T2> : IEnumerable<TLeft>, IEnumerable, IEnumerator<TLeft>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private TLeft <>2__current;
            private int <>l__initialThreadId;
            private Either<TLeft, T1> x1;
            public Either<TLeft, T1> <>3__x1;
            private Either<TLeft, T2> x2;
            public Either<TLeft, T2> <>3__x2;

            [DebuggerHidden]
            public <Lefts>d__19(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                switch (this.<>1__state)
                {
                    case 0:
                        this.<>1__state = -1;
                        if (!this.x1.IsLeft())
                        {
                            break;
                        }
                        this.<>2__current = this.x1.ToLeft<TLeft, T1>();
                        this.<>1__state = 1;
                        return true;

                    case 1:
                        this.<>1__state = -1;
                        break;

                    case 2:
                        this.<>1__state = -1;
                        goto TR_0003;

                    default:
                        return false;
                }
                if (this.x2.IsLeft())
                {
                    this.<>2__current = this.x2.ToLeft<TLeft, T2>();
                    this.<>1__state = 2;
                    return true;
                }
            TR_0003:
                return false;
            }

            [DebuggerHidden]
            IEnumerator<TLeft> IEnumerable<TLeft>.GetEnumerator()
            {
                Either.<Lefts>d__19<TLeft, T1, T2> d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new Either.<Lefts>d__19<TLeft, T1, T2>(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = (Either.<Lefts>d__19<TLeft, T1, T2>) this;
                }
                d__.x1 = this.<>3__x1;
                d__.x2 = this.<>3__x2;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<TLeft>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            TLeft IEnumerator<TLeft>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <Lefts>d__21<TLeft, T1, T2, T3> : IEnumerable<TLeft>, IEnumerable, IEnumerator<TLeft>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private TLeft <>2__current;
            private int <>l__initialThreadId;
            private Either<TLeft, T1> x1;
            public Either<TLeft, T1> <>3__x1;
            private Either<TLeft, T2> x2;
            public Either<TLeft, T2> <>3__x2;
            private Either<TLeft, T3> x3;
            public Either<TLeft, T3> <>3__x3;

            [DebuggerHidden]
            public <Lefts>d__21(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                switch (this.<>1__state)
                {
                    case 0:
                        this.<>1__state = -1;
                        if (!this.x1.IsLeft())
                        {
                            break;
                        }
                        this.<>2__current = this.x1.ToLeft<TLeft, T1>();
                        this.<>1__state = 1;
                        return true;

                    case 1:
                        this.<>1__state = -1;
                        break;

                    case 2:
                        this.<>1__state = -1;
                        goto TR_0005;

                    case 3:
                        this.<>1__state = -1;
                        goto TR_0004;

                    default:
                        return false;
                }
                if (this.x2.IsLeft())
                {
                    this.<>2__current = this.x2.ToLeft<TLeft, T2>();
                    this.<>1__state = 2;
                    return true;
                }
                goto TR_0005;
            TR_0004:
                return false;
            TR_0005:
                if (this.x3.IsLeft())
                {
                    this.<>2__current = this.x3.ToLeft<TLeft, T3>();
                    this.<>1__state = 3;
                    return true;
                }
                goto TR_0004;
            }

            [DebuggerHidden]
            IEnumerator<TLeft> IEnumerable<TLeft>.GetEnumerator()
            {
                Either.<Lefts>d__21<TLeft, T1, T2, T3> d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new Either.<Lefts>d__21<TLeft, T1, T2, T3>(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = (Either.<Lefts>d__21<TLeft, T1, T2, T3>) this;
                }
                d__.x1 = this.<>3__x1;
                d__.x2 = this.<>3__x2;
                d__.x3 = this.<>3__x3;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<TLeft>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            TLeft IEnumerator<TLeft>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

