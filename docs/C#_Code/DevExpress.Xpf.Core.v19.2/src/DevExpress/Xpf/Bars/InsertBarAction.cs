namespace DevExpress.Xpf.Bars
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Markup;

    [Browsable(false), ContentProperty("Bar"), EditorBrowsable(EditorBrowsableState.Never)]
    public class InsertBarAction : BarManagerControllerActionBase
    {
        public static readonly DependencyProperty BarProperty;
        public static readonly DependencyProperty BarIndexProperty;

        static InsertBarAction();
        protected override void ExecuteCore(DependencyObject context);
        public static int GetBarIndex(DependencyObject d);
        public override object GetObjectCore();
        public override bool IsEqual(BarManagerControllerActionBase action);
        public static void SetBarIndex(DependencyObject d, int value);

        [Description("Gets or sets the bar inserted in the BarManager.Bars collection.")]
        public DevExpress.Xpf.Bars.Bar Bar { get; set; }

        [Description("Gets or sets the index at which a bar is inserted in the BarManager.Bars collection. This is an attached property.")]
        public virtual int BarIndex { get; set; }
    }
}

