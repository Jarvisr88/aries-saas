namespace DevExpress.Xpf.Layout.Core
{
    using DevExpress.Xpf.Layout.Core.Base;
    using System;
    using System.Diagnostics;
    using System.Windows;

    public class BaseLayoutElementHost : BaseObject, ILayoutElementHost, IBaseObject, IDisposable
    {
        private ILayoutElement layoutRootCore;
        private ILayoutElementFactory defautFactoryCore;
        private ILayoutElementBehavior defaultBehaviorCore;

        protected BaseLayoutElementHost() : this(null, null)
        {
        }

        protected BaseLayoutElementHost(ILayoutElementFactory factory, ILayoutElementBehavior behavior)
        {
            this.defautFactoryCore = factory;
            this.defaultBehaviorCore = behavior;
        }

        public virtual Point ClientToScreen(Point clientPoint) => 
            clientPoint;

        public void EnsureLayoutRoot()
        {
            this.layoutRootCore = this.EnsureLayoutRootCore();
        }

        protected virtual ILayoutElement EnsureLayoutRootCore() => 
            this.GetLayoutElementFactory().CreateLayoutHierarchy(this.RootKey);

        public ILayoutElement GetDragItem(ILayoutElement element) => 
            (element != null) ? this.GetDragItemCore(element) : null;

        protected virtual ILayoutElement GetDragItemCore(ILayoutElement element) => 
            element;

        public ILayoutElement GetElement(object key)
        {
            if (this.LayoutRoot == null)
            {
                this.EnsureLayoutRoot();
            }
            return this.GetLayoutElementFactory().GetElement(key);
        }

        public ILayoutElementBehavior GetElementBehavior(ILayoutElement element) => 
            this.GetElementBehaviorCore(element) ?? this.DefaultBehavior;

        protected virtual ILayoutElementBehavior GetElementBehaviorCore(ILayoutElement element) => 
            null;

        public ILayoutElementFactory GetLayoutElementFactory() => 
            this.GetLayoutElementFactoryCore() ?? this.DefaultFactory;

        protected virtual ILayoutElementFactory GetLayoutElementFactoryCore() => 
            null;

        public void Invalidate()
        {
            Ref.Dispose<ILayoutElement>(ref this.layoutRootCore);
        }

        protected override void OnDispose()
        {
            this.defautFactoryCore = null;
            this.defaultBehaviorCore = null;
            Ref.Dispose<ILayoutElement>(ref this.layoutRootCore);
            base.OnDispose();
        }

        public void ReleaseCapture()
        {
            this.ReleaseCaptureCore();
        }

        protected virtual void ReleaseCaptureCore()
        {
        }

        protected virtual ILayoutElementBehavior ResolveDefaultBehavior() => 
            ServiceLocator<ILayoutElementBehavior>.Resolve();

        protected virtual ILayoutElementFactory ResolveDefaultFactory() => 
            ServiceLocator<ILayoutElementFactory>.Resolve();

        public virtual Point ScreenToClient(Point screenPoint) => 
            screenPoint;

        public void SetCapture()
        {
            this.SetCaptureCore();
        }

        protected virtual void SetCaptureCore()
        {
        }

        public virtual HostType Type =>
            HostType.Layout;

        protected ILayoutElementFactory DefaultFactory
        {
            [DebuggerStepThrough]
            get
            {
                this.defautFactoryCore ??= this.ResolveDefaultFactory();
                return this.defautFactoryCore;
            }
        }

        protected ILayoutElementBehavior DefaultBehavior
        {
            [DebuggerStepThrough]
            get
            {
                this.defaultBehaviorCore ??= this.ResolveDefaultBehavior();
                return this.defaultBehaviorCore;
            }
        }

        public ILayoutElement LayoutRoot =>
            this.layoutRootCore;

        public virtual object RootKey =>
            null;

        public virtual bool IsActiveAndCanProcessEvent =>
            (this.layoutRootCore != null) && !base.IsDisposing;
    }
}

