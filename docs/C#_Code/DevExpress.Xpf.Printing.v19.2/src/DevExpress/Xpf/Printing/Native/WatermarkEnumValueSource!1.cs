namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Markup;

    public abstract class WatermarkEnumValueSource<T> : MarkupExtension
    {
        protected WatermarkEnumValueSource()
        {
        }

        private string GetDescription(T value) => 
            DataAnnotationsAttributeHelper.GetFieldDescription(typeof(T).GetField(value.ToString()));

        protected abstract string GetDisplayName(T value);
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            (from x in Enum.GetValues(typeof(T)).Cast<T>().Except<T>(this.ExcludedItems) select new EnumMemberInfo(this.GetDisplayName(x), base.GetDescription(x), x, null)).ToArray<EnumMemberInfo>();

        protected virtual IEnumerable<T> ExcludedItems =>
            Enumerable.Empty<T>();
    }
}

