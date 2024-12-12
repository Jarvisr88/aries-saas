namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class EventArgsToDataCellConverter : EventArgsConverterBase<EventArgs>
    {
        protected override object Convert(object sender, EventArgs args)
        {
            Func<IDataCellEventArgs, CellValue> evaluator = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<IDataCellEventArgs, CellValue> local1 = <>c.<>9__0_0;
                evaluator = <>c.<>9__0_0 = x => x.Cell;
            }
            object obj2 = (args as IDataCellEventArgs).With<IDataCellEventArgs, CellValue>(evaluator);
            if (obj2 != null)
            {
                return obj2;
            }
            IDataCellEventArgsConverter specificConverter = EventArgsConverterHelper.GetSpecificConverter<IDataCellEventArgsConverter>(args);
            RoutedEventArgs e = args as RoutedEventArgs;
            return (((specificConverter == null) || (e == null)) ? null : specificConverter.GetDataCell(e));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EventArgsToDataCellConverter.<>c <>9 = new EventArgsToDataCellConverter.<>c();
            public static Func<IDataCellEventArgs, CellValue> <>9__0_0;

            internal CellValue <Convert>b__0_0(IDataCellEventArgs x) => 
                x.Cell;
        }
    }
}

