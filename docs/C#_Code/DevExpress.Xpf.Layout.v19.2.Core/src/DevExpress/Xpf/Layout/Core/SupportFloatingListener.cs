namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class SupportFloatingListener
    {
        private IFloatingHelper floatingHelper;

        protected SupportFloatingListener()
        {
        }

        protected virtual IFloatingHelper CreateFloatingHelper() => 
            null;

        protected virtual ILayoutElement EnsureElementForFloating(ILayoutElement element) => 
            element;

        protected virtual void EnsureFloatingView(IView floatingView)
        {
        }

        protected virtual IView GetFloatingView(ILayoutElement element)
        {
            element = this.EnsureElementForFloating(element);
            if (!element.IsReady)
            {
                element.EnsureBounds();
            }
            IView floatingView = this.FloatingHelper.GetFloatingView(element);
            if (floatingView != null)
            {
                floatingView.EnsureLayoutRoot();
                this.EnsureFloatingView(floatingView);
            }
            return floatingView;
        }

        private IFloatingHelper FloatingHelper
        {
            get
            {
                IFloatingHelper floatingHelper = this.floatingHelper;
                if (this.floatingHelper == null)
                {
                    IFloatingHelper local1 = this.floatingHelper;
                    floatingHelper = this.floatingHelper = this.CreateFloatingHelper();
                }
                return floatingHelper;
            }
        }

        public IUIServiceProvider ServiceProvider { get; set; }
    }
}

