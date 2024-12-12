namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Validation.Native;
    using DevExpress.XtraReports.Parameters;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class DateRangeEditStrategy : PopupBaseEditStrategy
    {
        private bool isValueModified;

        public DateRangeEditStrategy(PopupBaseEdit editor) : base(editor)
        {
        }

        protected override object ConvertEditValueForFormatDisplayText(object convertedValue)
        {
            Func<Range<DateTime>?, string> evaluator = <>c.<>9__12_0;
            if (<>c.<>9__12_0 == null)
            {
                Func<Range<DateTime>?, string> local1 = <>c.<>9__12_0;
                evaluator = <>c.<>9__12_0 = x => $"{x.Value.Start:d} - {x.Value.End:d}";
            }
            return (base.ConvertEditValueForFormatDisplayText(convertedValue) as Range<DateTime>?).Return<Range<DateTime>, string>(evaluator, (<>c.<>9__12_1 ??= () => string.Empty));
        }

        public override void EditValueChanged(object oldValue, object newValue)
        {
            object obj2 = newValue;
            Range<DateTime>? nullable = newValue as Range<DateTime>?;
            if (nullable == null)
            {
                obj2 = new Range<DateTime>(DateTime.Now.Date, DateTime.Now.Date);
            }
            else if (nullable.Value.Start > nullable.Value.End)
            {
                obj2 = new Range<DateTime>(nullable.Value.End, nullable.Value.Start);
            }
            else
            {
                DateTime end = nullable.Value.End;
                end = new DateTime();
                if (end.Date == end.Date)
                {
                    obj2 = new Range<DateTime>(nullable.Value.Start, nullable.Value.End.Date);
                }
            }
            if (obj2.Equals(newValue))
            {
                base.EditValueChanged(oldValue, newValue);
            }
            else
            {
                this.Editor.EditValue = obj2;
            }
        }

        public void OnPopupOpened()
        {
            this.IsValueModified = false;
            if (this.Editor.DateRangeControl != null)
            {
                this.Editor.DateRangeControl.RangeChanged += new RoutedEventHandler(this.OnRangeChanged);
            }
        }

        private void OnRangeChanged(object sender, RoutedEventArgs e)
        {
            this.IsValueModified ??= true;
        }

        public void SyncWithRangeControl()
        {
            if (this.Editor.DateRangeControl != null)
            {
                base.ValueContainer.SetEditValue(this.Editor.DateRangeControl.Range, UpdateEditorSource.TextInput);
            }
        }

        protected DateRangeEdit Editor =>
            (DateRangeEdit) base.Editor;

        protected Range<DateTime> EditValue
        {
            get
            {
                Range<DateTime>? editValue = base.EditValue as Range<DateTime>?;
                return ((editValue != null) ? editValue.GetValueOrDefault() : new Range<DateTime>(DateTime.Now.Date, DateTime.Now.Date));
            }
        }

        private bool IsValueModified
        {
            get => 
                this.isValueModified;
            set
            {
                this.isValueModified = value;
                this.Editor.UpdateOkButtonIsEnabled(this.IsValueModified);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DateRangeEditStrategy.<>c <>9 = new DateRangeEditStrategy.<>c();
            public static Func<Range<DateTime>?, string> <>9__12_0;
            public static Func<string> <>9__12_1;

            internal string <ConvertEditValueForFormatDisplayText>b__12_0(Range<DateTime>? x) => 
                $"{x.Value.Start:d} - {x.Value.End:d}";

            internal string <ConvertEditValueForFormatDisplayText>b__12_1() => 
                string.Empty;
        }
    }
}

