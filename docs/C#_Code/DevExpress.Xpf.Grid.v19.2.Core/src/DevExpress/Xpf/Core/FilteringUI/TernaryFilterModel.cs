namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class TernaryFilterModel : CriteriaConverterFilterModelBase<Tuple<ValueData, ValueData>>
    {
        internal TernaryFilterModel(FilterModelClient client, FilterModelValueItemInfo info, CriteriaConverter<Tuple<ValueData, ValueData>> converter, EditSettingsInfo leftEditSettings = null, EditSettingsInfo rightEditSettings = null) : base(client, converter)
        {
            EditSettingsInfo editSettings = leftEditSettings;
            if (leftEditSettings == null)
            {
                EditSettingsInfo local1 = leftEditSettings;
                editSettings = EditSettingsInfoFactory.Default(base.Column);
            }
            this.<LeftValueItem>k__BackingField = new FilterModelValueItem(client.GetColumn().Type, 0, editSettings, info, delegate {
                base.RaisePropertyChanged("Left");
                base.ApplyFilter();
            }, null);
            EditSettingsInfo info2 = rightEditSettings;
            if (rightEditSettings == null)
            {
                EditSettingsInfo local2 = rightEditSettings;
                info2 = EditSettingsInfoFactory.Default(base.Column);
            }
            this.<RightValueItem>k__BackingField = new FilterModelValueItem(client.GetColumn().Type, 1, info2, info, delegate {
                base.RaisePropertyChanged("Right");
                base.ApplyFilter();
            }, null);
        }

        protected internal override Tuple<ValueData, ValueData> CreateConverterValue() => 
            new Tuple<ValueData, ValueData>(this.LeftValueItem.ToValueData(), this.RightValueItem.ToValueData());

        protected override void UpdateFromConverterValue(Tuple<ValueData, ValueData> value)
        {
            this.LeftValueItem.Update(value?.Item1);
            this.RightValueItem.Update(value?.Item2);
        }

        public FilterModelValueItem LeftValueItem { get; }

        public FilterModelValueItem RightValueItem { get; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public object Left
        {
            get => 
                this.LeftValueItem.Value;
            set
            {
                if (this.LeftValueItem.Value != value)
                {
                    this.LeftValueItem.Value = value;
                }
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public object Right
        {
            get => 
                this.RightValueItem.Value;
            set
            {
                if (this.RightValueItem.Value != value)
                {
                    this.RightValueItem.Value = value;
                }
            }
        }
    }
}

