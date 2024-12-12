namespace DevExpress.Xpf.Core.Internal
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class BadgesTypeConverter : TypeConverter
    {
        private static readonly object olock = new object();
        private static readonly List<Tuple<Type, Func<object, Badge>>> factories = new List<Tuple<Type, Func<object, Badge>>>();

        static BadgesTypeConverter()
        {
            RegisterNoLock<bool, Badge>(delegate (bool b) {
                Badge badge1 = new Badge();
                Badge badge2 = new Badge();
                badge2.Visibility = b ? Visibility.Visible : Visibility.Collapsed;
                return badge2;
            }, false);
            RegisterNoLock<string, Badge>(delegate (string s) {
                Badge badge1 = new Badge();
                badge1.Content = s;
                return badge1;
            }, false);
            RegisterNoLock<int, Badge>(delegate (int s) {
                Badge badge1 = new Badge();
                badge1.Content = s;
                return badge1;
            }, false);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            this.GetFactoryFor(sourceType) != null;

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null)
            {
                return null;
            }
            Func<object, Badge> factoryFor = this.GetFactoryFor(value.GetType());
            return ((factoryFor == null) ? base.ConvertFrom(context, culture, value) : factoryFor(value));
        }

        private Func<object, Badge> GetFactoryFor(Type type)
        {
            Func<object, Badge> func;
            using (List<Tuple<Type, Func<object, Badge>>>.Enumerator enumerator = factories.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Tuple<Type, Func<object, Badge>> current = enumerator.Current;
                        if (!current.Item1.IsAssignableFrom(type))
                        {
                            continue;
                        }
                        func = current.Item2;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return func;
        }

        public static void Register<TSourceType, TBadge>(Func<TSourceType, TBadge> factory) where TBadge: Badge
        {
            object olock = BadgesTypeConverter.olock;
            lock (olock)
            {
                RegisterNoLock<TSourceType, TBadge>(factory, true);
            }
        }

        private static void RegisterNoLock<TSourceType, TBadge>(Func<TSourceType, TBadge> factory, bool overridesStandard) where TBadge: Badge
        {
            Tuple<Type, Func<object, Badge>> item = Tuple.Create<Type, Func<object, Badge>>(typeof(TSourceType), (Func<object, Badge>) (o => factory((TSourceType) o)));
            if (overridesStandard)
            {
                factories.Insert(0, item);
            }
            else
            {
                factories.Add(item);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BadgesTypeConverter.<>c <>9 = new BadgesTypeConverter.<>c();

            internal Badge <.cctor>b__2_0(bool b)
            {
                Badge badge1 = new Badge();
                Badge badge2 = new Badge();
                badge2.Visibility = b ? Visibility.Visible : Visibility.Collapsed;
                return badge2;
            }

            internal Badge <.cctor>b__2_1(string s)
            {
                Badge badge1 = new Badge();
                badge1.Content = s;
                return badge1;
            }

            internal Badge <.cctor>b__2_2(int s)
            {
                Badge badge1 = new Badge();
                badge1.Content = s;
                return badge1;
            }
        }
    }
}

