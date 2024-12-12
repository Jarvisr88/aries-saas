namespace DevExpress.Mvvm.POCO
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public static class ViewModelSource<T>
    {
        public static T Create() => 
            ViewModelSource<T>.GetFactoryByTypes<Func<T>>(<>c<T>.<>9__1_0 ??= () => Type.EmptyTypes)();

        public static T Create<T1>(T1 param1) => 
            ViewModelSource<T>.GetFactoryByTypes<Func<T1, T>>(<>c__2<T, T1>.<>9__2_0 ??= () => new Type[] { typeof(T1) })(param1);

        public static T Create<T1, T2>(T1 param1, T2 param2) => 
            ViewModelSource<T>.GetFactoryByTypes<Func<T1, T2, T>>(<>c__3<T, T1, T2>.<>9__3_0 ??= () => new Type[] { typeof(T1), typeof(T2) })(param1, param2);

        public static T Create<T1, T2, T3>(T1 param1, T2 param2, T3 param3) => 
            ViewModelSource<T>.GetFactoryByTypes<Func<T1, T2, T3, T>>(<>c__4<T, T1, T2, T3>.<>9__4_0 ??= () => new Type[] { typeof(T1), typeof(T2), typeof(T3) })(param1, param2, param3);

        public static T Create<T1, T2, T3, T4>(T1 param1, T2 param2, T3 param3, T4 param4) => 
            ViewModelSource<T>.GetFactoryByTypes<Func<T1, T2, T3, T4, T>>(<>c__5<T, T1, T2, T3, T4>.<>9__5_0 ??= () => new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4) })(param1, param2, param3, param4);

        public static T Create<T1, T2, T3, T4, T5>(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) => 
            ViewModelSource<T>.GetFactoryByTypes<Func<T1, T2, T3, T4, T5, T>>(<>c__6<T, T1, T2, T3, T4, T5>.<>9__6_0 ??= () => new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5) })(param1, param2, param3, param4, param5);

        public static T Create<T1, T2, T3, T4, T5, T6>(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) => 
            ViewModelSource<T>.GetFactoryByTypes<Func<T1, T2, T3, T4, T5, T6, T>>(<>c__7<T, T1, T2, T3, T4, T5, T6>.<>9__7_0 ??= () => new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6) })(param1, param2, param3, param4, param5, param6);

        public static T Create<T1, T2, T3, T4, T5, T6, T7>(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) => 
            ViewModelSource<T>.GetFactoryByTypes<Func<T1, T2, T3, T4, T5, T6, T7, T>>(<>c__8<T, T1, T2, T3, T4, T5, T6, T7>.<>9__8_0 ??= () => new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7) })(param1, param2, param3, param4, param5, param6, param7);

        public static T Create<T1, T2, T3, T4, T5, T6, T7, T8>(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8) => 
            ViewModelSource<T>.GetFactoryByTypes<Func<T1, T2, T3, T4, T5, T6, T7, T8, T>>(<>c__9<T, T1, T2, T3, T4, T5, T6, T7, T8>.<>9__9_0 ??= () => new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8) })(param1, param2, param3, param4, param5, param6, param7, param8);

        public static T Create<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9) => 
            ViewModelSource<T>.GetFactoryByTypes<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T>>(<>c__10<T, T1, T2, T3, T4, T5, T6, T7, T8, T9>.<>9__10_0 ??= delegate {
                Type[] typeArray1 = new Type[9];
                typeArray1[0] = typeof(T1);
                typeArray1[1] = typeof(T2);
                typeArray1[2] = typeof(T3);
                typeArray1[3] = typeof(T4);
                typeArray1[4] = typeof(T5);
                typeArray1[5] = typeof(T6);
                typeArray1[6] = typeof(T7);
                typeArray1[7] = typeof(T8);
                typeArray1[8] = typeof(T9);
                return typeArray1;
            })(param1, param2, param3, param4, param5, param6, param7, param8, param9);

        public static T Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9, T10 param10) => 
            ViewModelSource<T>.GetFactoryByTypes<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T>>(<>c__11<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.<>9__11_0 ??= delegate {
                Type[] typeArray1 = new Type[10];
                typeArray1[0] = typeof(T1);
                typeArray1[1] = typeof(T2);
                typeArray1[2] = typeof(T3);
                typeArray1[3] = typeof(T4);
                typeArray1[4] = typeof(T5);
                typeArray1[5] = typeof(T6);
                typeArray1[6] = typeof(T7);
                typeArray1[7] = typeof(T8);
                typeArray1[8] = typeof(T9);
                typeArray1[9] = typeof(T10);
                return typeArray1;
            })(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10);

        private static TDelegate GetFactoryByTypes<TDelegate>(Func<Type[]> getTypesDelegate) => 
            ViewModelSource.GetFactoryCore<TDelegate>(delegate {
                Type[] argsTypes = getTypesDelegate();
                ConstructorInfo constructor = ViewModelSource.GetConstructor(ViewModelSource.GetPOCOType(typeof(T), null), argsTypes);
                Func<Type, ParameterExpression> selector = <>c__0<T, TDelegate>.<>9__0_1;
                if (<>c__0<T, TDelegate>.<>9__0_1 == null)
                {
                    Func<Type, ParameterExpression> local1 = <>c__0<T, TDelegate>.<>9__0_1;
                    selector = <>c__0<T, TDelegate>.<>9__0_1 = x => Expression.Parameter(x);
                }
                ParameterExpression[] parameters = argsTypes.Select<Type, ParameterExpression>(selector).ToArray<ParameterExpression>();
                return Expression.Lambda<TDelegate>(Expression.New(constructor, parameters), parameters).Compile();
            });

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ViewModelSource<T>.<>c <>9;
            public static Func<Type[]> <>9__1_0;

            static <>c()
            {
                ViewModelSource<T>.<>c.<>9 = new ViewModelSource<T>.<>c();
            }

            internal Type[] <Create>b__1_0() => 
                Type.EmptyTypes;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__0<TDelegate>
        {
            public static readonly ViewModelSource<T>.<>c__0<TDelegate> <>9;
            public static Func<Type, ParameterExpression> <>9__0_1;

            static <>c__0()
            {
                ViewModelSource<T>.<>c__0<TDelegate>.<>9 = new ViewModelSource<T>.<>c__0<TDelegate>();
            }

            internal ParameterExpression <GetFactoryByTypes>b__0_1(Type x) => 
                Expression.Parameter(x);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__10<T1, T2, T3, T4, T5, T6, T7, T8, T9>
        {
            public static readonly ViewModelSource<T>.<>c__10<T1, T2, T3, T4, T5, T6, T7, T8, T9> <>9;
            public static Func<Type[]> <>9__10_0;

            static <>c__10()
            {
                ViewModelSource<T>.<>c__10<T1, T2, T3, T4, T5, T6, T7, T8, T9>.<>9 = new ViewModelSource<T>.<>c__10<T1, T2, T3, T4, T5, T6, T7, T8, T9>();
            }

            internal Type[] <Create>b__10_0()
            {
                Type[] typeArray1 = new Type[9];
                typeArray1[0] = typeof(T1);
                typeArray1[1] = typeof(T2);
                typeArray1[2] = typeof(T3);
                typeArray1[3] = typeof(T4);
                typeArray1[4] = typeof(T5);
                typeArray1[5] = typeof(T6);
                typeArray1[6] = typeof(T7);
                typeArray1[7] = typeof(T8);
                typeArray1[8] = typeof(T9);
                return typeArray1;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__11<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
        {
            public static readonly ViewModelSource<T>.<>c__11<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> <>9;
            public static Func<Type[]> <>9__11_0;

            static <>c__11()
            {
                ViewModelSource<T>.<>c__11<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.<>9 = new ViewModelSource<T>.<>c__11<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>();
            }

            internal Type[] <Create>b__11_0()
            {
                Type[] typeArray1 = new Type[10];
                typeArray1[0] = typeof(T1);
                typeArray1[1] = typeof(T2);
                typeArray1[2] = typeof(T3);
                typeArray1[3] = typeof(T4);
                typeArray1[4] = typeof(T5);
                typeArray1[5] = typeof(T6);
                typeArray1[6] = typeof(T7);
                typeArray1[7] = typeof(T8);
                typeArray1[8] = typeof(T9);
                typeArray1[9] = typeof(T10);
                return typeArray1;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__2<T1>
        {
            public static readonly ViewModelSource<T>.<>c__2<T1> <>9;
            public static Func<Type[]> <>9__2_0;

            static <>c__2()
            {
                ViewModelSource<T>.<>c__2<T1>.<>9 = new ViewModelSource<T>.<>c__2<T1>();
            }

            internal Type[] <Create>b__2_0() => 
                new Type[] { typeof(T1) };
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__3<T1, T2>
        {
            public static readonly ViewModelSource<T>.<>c__3<T1, T2> <>9;
            public static Func<Type[]> <>9__3_0;

            static <>c__3()
            {
                ViewModelSource<T>.<>c__3<T1, T2>.<>9 = new ViewModelSource<T>.<>c__3<T1, T2>();
            }

            internal Type[] <Create>b__3_0() => 
                new Type[] { typeof(T1), typeof(T2) };
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__4<T1, T2, T3>
        {
            public static readonly ViewModelSource<T>.<>c__4<T1, T2, T3> <>9;
            public static Func<Type[]> <>9__4_0;

            static <>c__4()
            {
                ViewModelSource<T>.<>c__4<T1, T2, T3>.<>9 = new ViewModelSource<T>.<>c__4<T1, T2, T3>();
            }

            internal Type[] <Create>b__4_0() => 
                new Type[] { typeof(T1), typeof(T2), typeof(T3) };
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__5<T1, T2, T3, T4>
        {
            public static readonly ViewModelSource<T>.<>c__5<T1, T2, T3, T4> <>9;
            public static Func<Type[]> <>9__5_0;

            static <>c__5()
            {
                ViewModelSource<T>.<>c__5<T1, T2, T3, T4>.<>9 = new ViewModelSource<T>.<>c__5<T1, T2, T3, T4>();
            }

            internal Type[] <Create>b__5_0() => 
                new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4) };
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__6<T1, T2, T3, T4, T5>
        {
            public static readonly ViewModelSource<T>.<>c__6<T1, T2, T3, T4, T5> <>9;
            public static Func<Type[]> <>9__6_0;

            static <>c__6()
            {
                ViewModelSource<T>.<>c__6<T1, T2, T3, T4, T5>.<>9 = new ViewModelSource<T>.<>c__6<T1, T2, T3, T4, T5>();
            }

            internal Type[] <Create>b__6_0() => 
                new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5) };
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__7<T1, T2, T3, T4, T5, T6>
        {
            public static readonly ViewModelSource<T>.<>c__7<T1, T2, T3, T4, T5, T6> <>9;
            public static Func<Type[]> <>9__7_0;

            static <>c__7()
            {
                ViewModelSource<T>.<>c__7<T1, T2, T3, T4, T5, T6>.<>9 = new ViewModelSource<T>.<>c__7<T1, T2, T3, T4, T5, T6>();
            }

            internal Type[] <Create>b__7_0() => 
                new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6) };
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__8<T1, T2, T3, T4, T5, T6, T7>
        {
            public static readonly ViewModelSource<T>.<>c__8<T1, T2, T3, T4, T5, T6, T7> <>9;
            public static Func<Type[]> <>9__8_0;

            static <>c__8()
            {
                ViewModelSource<T>.<>c__8<T1, T2, T3, T4, T5, T6, T7>.<>9 = new ViewModelSource<T>.<>c__8<T1, T2, T3, T4, T5, T6, T7>();
            }

            internal Type[] <Create>b__8_0() => 
                new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7) };
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__9<T1, T2, T3, T4, T5, T6, T7, T8>
        {
            public static readonly ViewModelSource<T>.<>c__9<T1, T2, T3, T4, T5, T6, T7, T8> <>9;
            public static Func<Type[]> <>9__9_0;

            static <>c__9()
            {
                ViewModelSource<T>.<>c__9<T1, T2, T3, T4, T5, T6, T7, T8>.<>9 = new ViewModelSource<T>.<>c__9<T1, T2, T3, T4, T5, T6, T7, T8>();
            }

            internal Type[] <Create>b__9_0() => 
                new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8) };
        }
    }
}

