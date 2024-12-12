namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class EventArgsToDataRowConverter : EventArgsConverterBase<EventArgs>
    {
        protected override object Convert(object sender, EventArgs args) => 
            GetDataRow(args);

        public static object GetDataRow(EventArgs args)
        {
            Func<IDataRowEventArgs, object> evaluator = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<IDataRowEventArgs, object> local1 = <>c.<>9__0_0;
                evaluator = <>c.<>9__0_0 = x => x.Row;
            }
            object obj2 = (args as IDataRowEventArgs).With<IDataRowEventArgs, object>(evaluator);
            if (obj2 != null)
            {
                return obj2;
            }
            IDataRowEventArgsConverter specificConverter = EventArgsConverterHelper.GetSpecificConverter<IDataRowEventArgsConverter>(args);
            RoutedEventArgs e = args as RoutedEventArgs;
            return (((specificConverter == null) || (e == null)) ? null : specificConverter.GetDataRow(e));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EventArgsToDataRowConverter.<>c <>9 = new EventArgsToDataRowConverter.<>c();
            public static Func<IDataRowEventArgs, object> <>9__0_0;

            internal object <GetDataRow>b__0_0(IDataRowEventArgs x) => 
                x.Row;
        }
    }
}

