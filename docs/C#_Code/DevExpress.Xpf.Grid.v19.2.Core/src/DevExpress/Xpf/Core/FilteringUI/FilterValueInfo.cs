namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Xpf.Editors.Settings;
    using System;

    public class FilterValueInfo : ValueInfo<object>
    {
        private object row;
        private Lazy<BaseEditSettings> editSettingsLazy;

        public FilterValueInfo(object value, int? count, Func<object, string> getDisplayText, object row, Lazy<BaseEditSettings> editSettingsLazy) : base(value, count, getDisplayText)
        {
            this.row = row;
            this.editSettingsLazy = editSettingsLazy;
        }

        public object Row
        {
            get => 
                this.row;
            internal set => 
                base.SetValue<object>(ref this.row, value, "Row");
        }

        public BaseEditSettings EditSettings =>
            this.EditSettingsLazy.Value;

        public Lazy<BaseEditSettings> EditSettingsLazy
        {
            get => 
                this.editSettingsLazy;
            internal set => 
                base.SetValue<Lazy<BaseEditSettings>>(ref this.editSettingsLazy, value, "EditSettingsLazy");
        }
    }
}

