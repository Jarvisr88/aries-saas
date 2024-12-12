namespace DevExpress.Xpf.Bars
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
    public class UpdateBarItemAction : BarItemActionBase
    {
        public static readonly DependencyProperty PropertyProperty;
        public static readonly DependencyProperty ValueProperty;

        static UpdateBarItemAction();
        protected override void ExecuteCore(DependencyObject context);
        public override bool IsEqual(BarManagerControllerActionBase action);

        [Description("This member supports the internal infrastructure, and is not intended to be used directly from your code.")]
        public DependencyProperty Property { get; set; }

        [Description("This member supports the internal infrastructure, and is not intended to be used directly from your code.")]
        public object Value { get; set; }
    }
}

