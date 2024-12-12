namespace DevExpress.XtraEditors
{
    using DevExpress.Utils.Editors;
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Runtime.CompilerServices;

    public class RangeControlRange
    {
        internal object rangeMinimum;
        internal object rangeMaximum;

        public RangeControlRange();
        public RangeControlRange(object minimum, object maximum);
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void InternalSetMaximum(object value);
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void InternalSetMinimum(object value);
        protected virtual void OnRangeMaximumChanged();
        protected virtual void OnRangeMinimumChanged();
        public void Reset();
        public bool ShouldSerialize();

        public IRangeControl Owner { get; set; }

        [DefaultValue((string) null), Editor(typeof(UIObjectEditor), typeof(UITypeEditor)), TypeConverter(typeof(ObjectEditorTypeConverter))]
        public object Minimum { get; set; }

        [DefaultValue((string) null), Editor(typeof(UIObjectEditor), typeof(UITypeEditor)), TypeConverter(typeof(ObjectEditorTypeConverter))]
        public object Maximum { get; set; }
    }
}

