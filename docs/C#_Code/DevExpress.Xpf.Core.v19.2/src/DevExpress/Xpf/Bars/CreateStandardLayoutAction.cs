namespace DevExpress.Xpf.Bars
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    public class CreateStandardLayoutAction : BarManagerControllerActionBase
    {
        public static readonly DependencyProperty AllowTopDockContainerProperty;
        public static readonly DependencyProperty AllowLeftDockContainerProperty;
        public static readonly DependencyProperty AllowRightDockContainerProperty;
        public static readonly DependencyProperty AllowBottomDockContainerProperty;
        internal bool isDefault;
        private BarManager internalManager;

        static CreateStandardLayoutAction();
        public CreateStandardLayoutAction();
        public CreateStandardLayoutAction(BarManager manager);
        protected override void ExecuteCore(DependencyObject context);
        public override object GetObjectCore();
        public override bool IsEqual(BarManagerControllerActionBase action);
        internal void SetManager(BarManager manager);

        [Description("Gets or sets whether a dock container at the top edge must be created.This is a dependency property.")]
        public bool AllowTopDockContainer { get; set; }

        [Description("Gets or sets whether a dock container at the bottom edge must be created.This is a dependency property.")]
        public bool AllowBottomDockContainer { get; set; }

        [Description("Gets or sets whether a dock container at the left edge must be created.This is a dependency property.")]
        public bool AllowLeftDockContainer { get; set; }

        [Description("Gets or sets whether a dock container at the right edge must be created.This is a dependency property.")]
        public bool AllowRightDockContainer { get; set; }

        [Description("Gets the current BarManager object.")]
        public override BarManager Manager { get; }
    }
}

