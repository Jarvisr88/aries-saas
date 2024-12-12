namespace DevExpress
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class GCTestHelper
    {
        public static bool? HardOptional;
        private static Random rnd = new Random();

        private static void AssertCollectedCore(IEnumerable<WeakReference> references, int alreadyCollectedGen)
        {
            int num;
            List<WeakReference> nextIterationHolder = CollectExistingData(references, out num);
            if (nextIterationHolder.Count != 0)
            {
                if (num <= alreadyCollectedGen)
                {
                    SlowButSureAssertCollected(nextIterationHolder);
                }
                else
                {
                    GC.Collect(num, GCCollectionMode.Forced);
                    AssertCollectedCore(nextIterationHolder, num);
                }
            }
        }

        private static List<WeakReference> CollectExistingData(IEnumerable<WeakReference> references, out int maxGeneration)
        {
            maxGeneration = -1;
            List<WeakReference> list = new List<WeakReference>();
            foreach (WeakReference reference in references)
            {
                object target = reference.Target;
                if (target != null)
                {
                    list.Add(reference);
                    int generation = GC.GetGeneration(target);
                    if (generation > maxGeneration)
                    {
                        maxGeneration = generation;
                    }
                }
            }
            return list;
        }

        public static void CollectOptional(IEnumerable<WeakReference> references)
        {
            if (IsHardOptional())
            {
                GC.Collect();
                GC.GetTotalMemory(true);
            }
            else
            {
                Func<WeakReference, object> selector = <>c.<>9__13_0;
                if (<>c.<>9__13_0 == null)
                {
                    Func<WeakReference, object> local1 = <>c.<>9__13_0;
                    selector = <>c.<>9__13_0 = wr => wr.Target;
                }
                Func<object, bool> predicate = <>c.<>9__13_1;
                if (<>c.<>9__13_1 == null)
                {
                    Func<object, bool> local2 = <>c.<>9__13_1;
                    predicate = <>c.<>9__13_1 = t => t != null;
                }
                Func<object, int> func3 = <>c.<>9__13_2;
                if (<>c.<>9__13_2 == null)
                {
                    Func<object, int> local3 = <>c.<>9__13_2;
                    func3 = <>c.<>9__13_2 = t => GC.GetGeneration(t);
                }
                Func<int, int?> func4 = <>c.<>9__13_3;
                if (<>c.<>9__13_3 == null)
                {
                    Func<int, int?> local4 = <>c.<>9__13_3;
                    func4 = <>c.<>9__13_3 = gen => new int?(gen);
                }
                int? nullable = references.Select<WeakReference, object>(selector).Where<object>(predicate).Select<object, int>(func3).Max<int>(func4);
                if (nullable != null)
                {
                    GC.Collect(nullable.Value, GCCollectionMode.Forced);
                }
            }
        }

        public static void CollectOptional(params WeakReference[] references)
        {
            CollectOptional(references.AsEnumerable<WeakReference>());
        }

        public static void EnsureCollected(IEnumerable<WeakReference> references)
        {
            AssertCollectedCore(references, -1);
        }

        public static void EnsureCollected(Func<object> obtainer)
        {
            WeakReference[] references = new WeakReference[] { Obtain(obtainer) };
            EnsureCollected(references);
        }

        public static void EnsureCollected(Func<object[]> obtainer)
        {
            EnsureCollected(Obtain(obtainer));
        }

        public static void EnsureCollected(params WeakReference[] references)
        {
            EnsureCollected(references.AsEnumerable<WeakReference>());
        }

        private static bool IsHardOptional()
        {
            if (HardOptional != null)
            {
                return HardOptional.Value;
            }
            Random rnd = GCTestHelper.rnd;
            lock (rnd)
            {
                return (GCTestHelper.rnd.Next(100) < 5);
            }
        }

        private static WeakReference Obtain(Func<object> obtainer) => 
            new WeakReference(obtainer());

        private static WeakReference[] Obtain(Func<object[]> obtainer)
        {
            Func<object, WeakReference> selector = <>c.<>9__1_0;
            if (<>c.<>9__1_0 == null)
            {
                Func<object, WeakReference> local1 = <>c.<>9__1_0;
                selector = <>c.<>9__1_0 = o => new WeakReference(o);
            }
            return obtainer().Select<object, WeakReference>(selector).ToArray<WeakReference>();
        }

        private static void SlowButSureAssertCollected(IList<WeakReference> nextIterationHolder)
        {
            GC.GetTotalMemory(true);
            Func<WeakReference, bool> predicate = <>c.<>9__8_0;
            if (<>c.<>9__8_0 == null)
            {
                Func<WeakReference, bool> local1 = <>c.<>9__8_0;
                predicate = <>c.<>9__8_0 = wr => !wr.IsAlive;
            }
            if (!nextIterationHolder.All<WeakReference>(predicate))
            {
                GC.Collect();
                Func<WeakReference, bool> func2 = <>c.<>9__8_1;
                if (<>c.<>9__8_1 == null)
                {
                    Func<WeakReference, bool> local2 = <>c.<>9__8_1;
                    func2 = <>c.<>9__8_1 = wr => !wr.IsAlive;
                }
                if (!nextIterationHolder.All<WeakReference>(func2))
                {
                    GC.GetTotalMemory(true);
                    Func<WeakReference, object> selector = <>c.<>9__8_2;
                    if (<>c.<>9__8_2 == null)
                    {
                        Func<WeakReference, object> local3 = <>c.<>9__8_2;
                        selector = <>c.<>9__8_2 = wr => wr.Target;
                    }
                    Func<object, bool> func4 = <>c.<>9__8_3;
                    if (<>c.<>9__8_3 == null)
                    {
                        Func<object, bool> local4 = <>c.<>9__8_3;
                        func4 = <>c.<>9__8_3 = t => t != null;
                    }
                    object[] source = nextIterationHolder.Select<WeakReference, object>(selector).Where<object>(func4).ToArray<object>();
                    if (source.Length != 0)
                    {
                        Func<object, Type> keySelector = <>c.<>9__8_4;
                        if (<>c.<>9__8_4 == null)
                        {
                            Func<object, Type> local5 = <>c.<>9__8_4;
                            keySelector = <>c.<>9__8_4 = o => o.GetType();
                        }
                        Func<IGrouping<Type, object>, string> func6 = <>c.<>9__8_5;
                        if (<>c.<>9__8_5 == null)
                        {
                            Func<IGrouping<Type, object>, string> local6 = <>c.<>9__8_5;
                            func6 = <>c.<>9__8_5 = gr => gr.Key.FullName;
                        }
                        Func<IGrouping<Type, object>, string> func7 = <>c.<>9__8_6;
                        if (<>c.<>9__8_6 == null)
                        {
                            Func<IGrouping<Type, object>, string> local7 = <>c.<>9__8_6;
                            func7 = <>c.<>9__8_6 = delegate (IGrouping<Type, object> gr) {
                                Func<object, string> func1 = <>c.<>9__8_7;
                                if (<>c.<>9__8_7 == null)
                                {
                                    Func<object, string> local1 = <>c.<>9__8_7;
                                    func1 = <>c.<>9__8_7 = o => o.ToString();
                                }
                                Func<string, string> func2 = <>c.<>9__8_8;
                                if (<>c.<>9__8_8 == null)
                                {
                                    Func<string, string> local2 = <>c.<>9__8_8;
                                    func2 = <>c.<>9__8_8 = s => s;
                                }
                                Func<string, string> func3 = <>c.<>9__8_9;
                                if (<>c.<>9__8_9 == null)
                                {
                                    Func<string, string> local3 = <>c.<>9__8_9;
                                    func3 = <>c.<>9__8_9 = s => $"		{s}";
                                }
                                return $"	{gr.Count<object>()} object(s) of type {gr.Key.FullName}:
{string.Join("\n", gr.Select<object, string>(func1).OrderBy<string, string>(func2).Select<string, string>(func3))}";
                            };
                        }
                        string str = string.Join("\n", source.GroupBy<object, Type>(keySelector).OrderBy<IGrouping<Type, object>, string>(func6).Select<IGrouping<Type, object>, string>(func7));
                        throw new GCTestHelperException($"{source.Length} garbage object(s) not collected:
{str}");
                    }
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GCTestHelper.<>c <>9 = new GCTestHelper.<>c();
            public static Func<object, WeakReference> <>9__1_0;
            public static Func<WeakReference, bool> <>9__8_0;
            public static Func<WeakReference, bool> <>9__8_1;
            public static Func<WeakReference, object> <>9__8_2;
            public static Func<object, bool> <>9__8_3;
            public static Func<object, Type> <>9__8_4;
            public static Func<IGrouping<Type, object>, string> <>9__8_5;
            public static Func<object, string> <>9__8_7;
            public static Func<string, string> <>9__8_8;
            public static Func<string, string> <>9__8_9;
            public static Func<IGrouping<Type, object>, string> <>9__8_6;
            public static Func<WeakReference, object> <>9__13_0;
            public static Func<object, bool> <>9__13_1;
            public static Func<object, int> <>9__13_2;
            public static Func<int, int?> <>9__13_3;

            internal object <CollectOptional>b__13_0(WeakReference wr) => 
                wr.Target;

            internal bool <CollectOptional>b__13_1(object t) => 
                t != null;

            internal int <CollectOptional>b__13_2(object t) => 
                GC.GetGeneration(t);

            internal int? <CollectOptional>b__13_3(int gen) => 
                new int?(gen);

            internal WeakReference <Obtain>b__1_0(object o) => 
                new WeakReference(o);

            internal bool <SlowButSureAssertCollected>b__8_0(WeakReference wr) => 
                !wr.IsAlive;

            internal bool <SlowButSureAssertCollected>b__8_1(WeakReference wr) => 
                !wr.IsAlive;

            internal object <SlowButSureAssertCollected>b__8_2(WeakReference wr) => 
                wr.Target;

            internal bool <SlowButSureAssertCollected>b__8_3(object t) => 
                t != null;

            internal Type <SlowButSureAssertCollected>b__8_4(object o) => 
                o.GetType();

            internal string <SlowButSureAssertCollected>b__8_5(IGrouping<Type, object> gr) => 
                gr.Key.FullName;

            internal string <SlowButSureAssertCollected>b__8_6(IGrouping<Type, object> gr)
            {
                Func<object, string> selector = <>9__8_7;
                if (<>9__8_7 == null)
                {
                    Func<object, string> local1 = <>9__8_7;
                    selector = <>9__8_7 = o => o.ToString();
                }
                Func<string, string> keySelector = <>9__8_8;
                if (<>9__8_8 == null)
                {
                    Func<string, string> local2 = <>9__8_8;
                    keySelector = <>9__8_8 = s => s;
                }
                Func<string, string> func3 = <>9__8_9;
                if (<>9__8_9 == null)
                {
                    Func<string, string> local3 = <>9__8_9;
                    func3 = <>9__8_9 = s => $"		{s}";
                }
                return $"	{gr.Count<object>()} object(s) of type {gr.Key.FullName}:
{string.Join("\n", gr.Select<object, string>(selector).OrderBy<string, string>(keySelector).Select<string, string>(func3))}";
            }

            internal string <SlowButSureAssertCollected>b__8_7(object o) => 
                o.ToString();

            internal string <SlowButSureAssertCollected>b__8_8(string s) => 
                s;

            internal string <SlowButSureAssertCollected>b__8_9(string s) => 
                $"		{s}";
        }
    }
}

