namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class CustomDataUpdateFormatConditionEventArgs : EventArgs
    {
        internal static CustomDataUpdateFormatConditionEventArgs Create(CustomDataUpdateFormatConditionEventArgsSource argsSource, ITableView view)
        {
            CustomDataUpdateFormatConditionEventArgs args1 = new CustomDataUpdateFormatConditionEventArgs();
            args1.OldValue = GetProviderValue(argsSource.OldValueProvider);
            args1.NewValue = GetProviderValue(argsSource.NewValueProvider);
            args1.Condition = GetCondition(argsSource.ConditionInfo, view);
            return args1;
        }

        internal static FormatConditionBase GetCondition(FormatConditionBaseInfo conditionInfo, ITableView view) => 
            view.FormatConditions.FirstOrDefault<FormatConditionBase>(x => ReferenceEquals(x.Info, conditionInfo));

        internal static object GetProviderValue(FormatValueProvider provider) => 
            provider.Value;

        public bool Allow { get; set; }

        public object OldValue { get; private set; }

        public object NewValue { get; private set; }

        public FormatConditionBase Condition { get; private set; }
    }
}

