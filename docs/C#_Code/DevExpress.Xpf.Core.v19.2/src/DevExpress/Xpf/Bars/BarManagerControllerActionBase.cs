namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Core;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class BarManagerControllerActionBase : DXFrameworkContentElement, IBarManagerControllerAction, IControllerAction
    {
        object IBarManagerControllerAction.GetObject();
        void IControllerAction.Execute(DependencyObject context);
        protected virtual void ExecuteCore(DependencyObject context);
        public abstract object GetObjectCore();
        public virtual bool IsEqual(BarManagerControllerActionBase action);

        [Description("Gets the BarManager object that is customized by the current action.")]
        public virtual BarManager Manager { get; }

        public IActionContainer Container { get; set; }
    }
}

