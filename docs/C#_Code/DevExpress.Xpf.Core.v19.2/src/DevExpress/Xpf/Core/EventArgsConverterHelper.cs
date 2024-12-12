namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class EventArgsConverterHelper
    {
        public static T GetSpecificConverter<T>(EventArgs e) where T: class
        {
            Func<EventArgs, RoutedEventArgs> evaluator = <>c__0<T>.<>9__0_0;
            if (<>c__0<T>.<>9__0_0 == null)
            {
                Func<EventArgs, RoutedEventArgs> local1 = <>c__0<T>.<>9__0_0;
                evaluator = <>c__0<T>.<>9__0_0 = x => x as RoutedEventArgs;
            }
            Func<RoutedEventArgs, DependencyObject> func2 = <>c__0<T>.<>9__0_1;
            if (<>c__0<T>.<>9__0_1 == null)
            {
                Func<RoutedEventArgs, DependencyObject> local2 = <>c__0<T>.<>9__0_1;
                func2 = <>c__0<T>.<>9__0_1 = x => x.OriginalSource as DependencyObject;
            }
            Func<IEventArgsConverterSource, T> func3 = <>c__0<T>.<>9__0_2;
            if (<>c__0<T>.<>9__0_2 == null)
            {
                Func<IEventArgsConverterSource, T> local3 = <>c__0<T>.<>9__0_2;
                func3 = <>c__0<T>.<>9__0_2 = x => x.EventArgsConverter as T;
            }
            return LayoutHelper.FindLayoutOrVisualParentObject<IEventArgsConverterSource>(e.With<EventArgs, RoutedEventArgs>(evaluator).With<RoutedEventArgs, DependencyObject>(func2), true, null).With<IEventArgsConverterSource, T>(func3);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__0<T> where T: class
        {
            public static readonly EventArgsConverterHelper.<>c__0<T> <>9;
            public static Func<EventArgs, RoutedEventArgs> <>9__0_0;
            public static Func<RoutedEventArgs, DependencyObject> <>9__0_1;
            public static Func<IEventArgsConverterSource, T> <>9__0_2;

            static <>c__0()
            {
                EventArgsConverterHelper.<>c__0<T>.<>9 = new EventArgsConverterHelper.<>c__0<T>();
            }

            internal RoutedEventArgs <GetSpecificConverter>b__0_0(EventArgs x) => 
                x as RoutedEventArgs;

            internal DependencyObject <GetSpecificConverter>b__0_1(RoutedEventArgs x) => 
                x.OriginalSource as DependencyObject;

            internal T <GetSpecificConverter>b__0_2(IEventArgsConverterSource x) => 
                x.EventArgsConverter as T;
        }
    }
}

