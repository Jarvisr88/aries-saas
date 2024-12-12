namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    internal class PageLayoutCommandButton : CommandToggleButton
    {
        private DevExpress.Xpf.Printing.PageLayoutSettings pageLayoutSettings;

        public DevExpress.Xpf.Printing.PageLayoutSettings PageLayoutSettings
        {
            get => 
                this.pageLayoutSettings;
            set => 
                base.SetProperty<DevExpress.Xpf.Printing.PageLayoutSettings>(ref this.pageLayoutSettings, value, Expression.Lambda<Func<DevExpress.Xpf.Printing.PageLayoutSettings>>(Expression.Property(Expression.Constant(this, typeof(PageLayoutCommandButton)), (MethodInfo) methodof(PageLayoutCommandButton.get_PageLayoutSettings)), new ParameterExpression[0]));
        }
    }
}

