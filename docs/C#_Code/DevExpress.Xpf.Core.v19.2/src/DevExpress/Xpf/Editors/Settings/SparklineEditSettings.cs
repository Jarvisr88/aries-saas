namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    public class SparklineEditSettings : BaseEditSettings
    {
        public static readonly DependencyProperty PointValueMemberProperty;
        public static readonly DependencyProperty PointArgumentMemberProperty;
        public static readonly DependencyProperty PointArgumentSortOrderProperty;
        public static readonly DependencyProperty FilterCriteriaProperty;
        public static readonly DependencyProperty PointArgumentRangeProperty;
        public static readonly DependencyProperty PointValueRangeProperty;

        static SparklineEditSettings()
        {
            Type ownerType = typeof(SparklineEditSettings);
            PointValueMemberProperty = DependencyPropertyManager.Register("PointValueMember", typeof(string), ownerType, new FrameworkPropertyMetadata(string.Empty));
            PointArgumentMemberProperty = DependencyPropertyManager.Register("PointArgumentMember", typeof(string), ownerType, new FrameworkPropertyMetadata(string.Empty));
            PointArgumentSortOrderProperty = DependencyProperty.Register("PointArgumentSortOrder", typeof(SparklineSortOrder), ownerType, new FrameworkPropertyMetadata(SparklineSortOrder.Ascending));
            FilterCriteriaProperty = DependencyProperty.Register("FilterCriteria", typeof(CriteriaOperator), ownerType, new FrameworkPropertyMetadata(null));
            PointArgumentRangeProperty = DependencyProperty.Register("PointArgumentRange", typeof(Range), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (o, args) => ((SparklineEditSettings) o).OnPointArgumentRangeChanged((Range) args.NewValue)));
            PointValueRangeProperty = DependencyProperty.Register("PointValueRange", typeof(Range), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (o, args) => ((SparklineEditSettings) o).OnPointValueRangeChanged((Range) args.NewValue)));
        }

        protected override void AssignToEditCore(IBaseEdit edit)
        {
            base.AssignToEditCore(edit);
            SparklineEdit editor = edit as SparklineEdit;
            if (editor != null)
            {
                base.SetValueFromSettings(PointValueMemberProperty, () => editor.PointValueMember = this.PointValueMember);
                base.SetValueFromSettings(PointArgumentMemberProperty, () => editor.PointArgumentMember = this.PointArgumentMember);
                base.SetValueFromSettings(PointArgumentSortOrderProperty, () => editor.PointArgumentSortOrder = this.PointArgumentSortOrder);
                base.SetValueFromSettings(FilterCriteriaProperty, () => editor.FilterCriteria = this.FilterCriteria);
                this.bindToArgumentRange(editor);
                this.bindToValueRange(editor);
            }
        }

        private void bindToArgumentRange(SparklineEdit editor)
        {
            editor.PointArgumentRange = new Range();
            Binding binding1 = new Binding("Limit1");
            binding1.Source = this.PointArgumentRange;
            Binding binding = binding1;
            binding.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(editor.PointArgumentRange, Range.Limit1Property, binding);
            Binding binding4 = new Binding("Limit2");
            binding4.Source = this.PointArgumentRange;
            Binding binding2 = binding4;
            binding2.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(editor.PointArgumentRange, Range.Limit2Property, binding2);
            Binding binding5 = new Binding("Auto");
            binding5.Source = this.PointArgumentRange;
            Binding binding3 = binding5;
            binding3.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(editor.PointArgumentRange, Range.AutoProperty, binding3);
        }

        private void bindToValueRange(SparklineEdit editor)
        {
            editor.PointValueRange = new Range();
            Binding binding1 = new Binding("Limit1");
            binding1.Source = this.PointValueRange;
            Binding binding = binding1;
            binding.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(editor.PointValueRange, Range.Limit1Property, binding);
            Binding binding4 = new Binding("Limit2");
            binding4.Source = this.PointValueRange;
            Binding binding2 = binding4;
            binding2.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(editor.PointValueRange, Range.Limit2Property, binding2);
            Binding binding5 = new Binding("Auto");
            binding5.Source = this.PointValueRange;
            Binding binding3 = binding5;
            binding3.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(editor.PointValueRange, Range.AutoProperty, binding3);
        }

        private void OnPointArgumentRangeChanged(Range range)
        {
            base.AddLogicalChild(range);
        }

        private void OnPointValueRangeChanged(Range range)
        {
            base.AddLogicalChild(range);
        }

        public string PointArgumentMember
        {
            get => 
                (string) base.GetValue(PointArgumentMemberProperty);
            set => 
                base.SetValue(PointArgumentMemberProperty, value);
        }

        public string PointValueMember
        {
            get => 
                (string) base.GetValue(PointValueMemberProperty);
            set => 
                base.SetValue(PointValueMemberProperty, value);
        }

        public SparklineSortOrder PointArgumentSortOrder
        {
            get => 
                (SparklineSortOrder) base.GetValue(PointArgumentSortOrderProperty);
            set => 
                base.SetValue(PointArgumentSortOrderProperty, value);
        }

        public CriteriaOperator FilterCriteria
        {
            get => 
                (CriteriaOperator) base.GetValue(FilterCriteriaProperty);
            set => 
                base.SetValue(FilterCriteriaProperty, value);
        }

        public Range PointArgumentRange
        {
            get => 
                (Range) base.GetValue(PointArgumentRangeProperty);
            set => 
                base.SetValue(PointArgumentRangeProperty, value);
        }

        public Range PointValueRange
        {
            get => 
                (Range) base.GetValue(PointValueRangeProperty);
            set => 
                base.SetValue(PointValueRangeProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SparklineEditSettings.<>c <>9 = new SparklineEditSettings.<>c();

            internal void <.cctor>b__6_0(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((SparklineEditSettings) o).OnPointArgumentRangeChanged((Range) args.NewValue);
            }

            internal void <.cctor>b__6_1(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((SparklineEditSettings) o).OnPointValueRangeChanged((Range) args.NewValue);
            }
        }
    }
}

