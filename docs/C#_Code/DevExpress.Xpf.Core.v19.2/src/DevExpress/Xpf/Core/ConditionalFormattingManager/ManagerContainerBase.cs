namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;

    public abstract class ManagerContainerBase : Control
    {
        protected ManagerContainerBase()
        {
        }

        protected abstract object CreateContent();
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.Container = base.GetTemplateChild("PART_Container") as ContentPresenter;
            this.Container.Content = this.CreateContent();
        }

        protected ContentPresenter Container { get; private set; }
    }
}

