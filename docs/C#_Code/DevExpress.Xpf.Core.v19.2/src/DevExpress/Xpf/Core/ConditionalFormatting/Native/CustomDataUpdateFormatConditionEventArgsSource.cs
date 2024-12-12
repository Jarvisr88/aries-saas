namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct CustomDataUpdateFormatConditionEventArgsSource
    {
        private readonly FormatValueProvider oldValueProviderCore;
        private readonly FormatValueProvider newValueProviderCore;
        private readonly FormatConditionBaseInfo conditionInfoCore;
        public FormatValueProvider OldValueProvider =>
            this.oldValueProviderCore;
        public FormatValueProvider NewValueProvider =>
            this.newValueProviderCore;
        public FormatConditionBaseInfo ConditionInfo =>
            this.conditionInfoCore;
        public CustomDataUpdateFormatConditionEventArgsSource(FormatValueProvider oldValueProvider, FormatValueProvider newValueProvider, FormatConditionBaseInfo conditionInfo)
        {
            this.oldValueProviderCore = oldValueProvider;
            this.newValueProviderCore = newValueProvider;
            this.conditionInfoCore = conditionInfo;
        }

        public CustomDataUpdateFormatConditionEventArgsSource(FormatConditionBaseInfo conditionInfo) : this(provider, provider, conditionInfo)
        {
            FormatValueProvider provider = new FormatValueProvider();
            provider = new FormatValueProvider();
        }
    }
}

