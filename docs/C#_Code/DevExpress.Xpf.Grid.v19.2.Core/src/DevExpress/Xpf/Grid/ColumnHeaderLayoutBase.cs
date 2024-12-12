namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    public class ColumnHeaderLayoutBase : Control
    {
        private const string HeaderPresenterName = "PART_Content";
        private const string HeaderGripperName = "PART_Thumb";
        private const string SortIndicatorName = "PART_SortIndicator";
        private const string FilterName = "PART_Filter";
        private BaseGridHeader headerCore;

        private void InitParentLayoutElements()
        {
            Action<BaseGridHeader> action = <>c.<>9__28_0;
            if (<>c.<>9__28_0 == null)
            {
                Action<BaseGridHeader> local1 = <>c.<>9__28_0;
                action = <>c.<>9__28_0 = x => x.InitInnerLayoutElements();
            }
            this.Header.Do<BaseGridHeader>(action);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.LayoutPanel = base.GetTemplateChild("PART_LayoutPanel") as ColumnHeaderDockPanel;
            this.HeaderPresenter = base.GetTemplateChild("PART_Content") as ContentControl;
            this.HeaderGripper = base.GetTemplateChild("PART_Thumb") as Thumb;
            this.SortIndicator = base.GetTemplateChild("PART_SortIndicator") as FrameworkElement;
            this.Filter = base.GetTemplateChild("PART_Filter") as ContentControl;
            this.InitParentLayoutElements();
        }

        internal BaseGridHeader Header
        {
            get => 
                this.headerCore;
            set
            {
                if (!ReferenceEquals(this.headerCore, value))
                {
                    this.headerCore = value;
                    this.InitParentLayoutElements();
                }
            }
        }

        internal ColumnHeaderDockPanel LayoutPanel { get; private set; }

        internal ContentControl HeaderPresenter { get; private set; }

        internal Thumb HeaderGripper { get; private set; }

        internal FrameworkElement SortIndicator { get; private set; }

        internal ContentControl Filter { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ColumnHeaderLayoutBase.<>c <>9 = new ColumnHeaderLayoutBase.<>c();
            public static Action<BaseGridHeader> <>9__28_0;

            internal void <InitParentLayoutElements>b__28_0(BaseGridHeader x)
            {
                x.InitInnerLayoutElements();
            }
        }
    }
}

