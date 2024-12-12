namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class BinaryFilterModel : CriteriaConverterFilterModelBase<ValueData>
    {
        internal BinaryFilterModel(FilterModelClient client, FilterModelValueItemInfo info, CriteriaConverter<ValueData> converter, EditSettingsInfo editSettings = null) : base(client, converter)
        {
            EditSettingsInfo info1 = editSettings;
            if (editSettings == null)
            {
                EditSettingsInfo local1 = editSettings;
                info1 = EditSettingsInfoFactory.Default(base.Column);
            }
            this.<ValueItem>k__BackingField = new FilterModelValueItem(client.GetColumn().Type, 0, info1, info, delegate {
                base.RaisePropertyChanged("Value");
                base.ApplyFilter();
            }, null);
        }

        protected internal override ValueData CreateConverterValue() => 
            this.ValueItem.ToValueData();

        protected override void UpdateFromConverterValue(ValueData value)
        {
            this.ValueItem.Update(value);
        }

        public FilterModelValueItem ValueItem { get; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public object Value
        {
            get => 
                this.ValueItem.Value;
            set
            {
                if (this.ValueItem.Value != value)
                {
                    this.ValueItem.Value = value;
                }
            }
        }
    }
}

