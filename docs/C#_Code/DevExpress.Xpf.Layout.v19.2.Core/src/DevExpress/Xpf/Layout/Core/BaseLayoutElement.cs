namespace DevExpress.Xpf.Layout.Core
{
    using DevExpress.Xpf.Layout.Core.Base;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class BaseLayoutElement : BaseObject, ILayoutElement, IBaseObject, IDisposable, ISupportHierarchy<ILayoutElement>, ISupportVisitor<ILayoutElement>
    {
        protected static readonly ILayoutElement[] EmptyNodes = new ILayoutElement[0];
        internal ILayoutContainer parentCore;
        private ILayoutElement[] nodesCore;
        private bool isVisualStateInitialized;
        private object pressedHitResult;
        private object hotHitResult;
        private bool isDraggingCore;

        protected BaseLayoutElement()
        {
        }

        public void Accept(IVisitor<ILayoutElement> visitor)
        {
            if (visitor != null)
            {
                this.AcceptCore(new VisitDelegate<ILayoutElement>(visitor.Visit));
            }
        }

        public void Accept(VisitDelegate<ILayoutElement> visit)
        {
            this.AcceptCore(visit);
        }

        private void AcceptCore(VisitDelegate<ILayoutElement> visit)
        {
            if (visit != null)
            {
                visit(this);
                this.AcceptNodes(visit);
            }
        }

        private void AcceptNodes(VisitDelegate<ILayoutElement> visit)
        {
            ILayoutElement[] nodes = this.Nodes;
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i].Accept(visit);
            }
        }

        public LayoutElementHitInfo CalcHitInfo(Point pt)
        {
            LayoutElementHitInfo info = null;
            foreach (BaseLayoutElement element in this)
            {
                if (element.HitTestingEnabled && element.HitTest(pt))
                {
                    element.CalcHitInfoCore(element.CreateHitInfo(pt));
                }
            }
            return ((info == null) ? LayoutElementHitInfo.Empty : info);
        }

        protected virtual void CalcHitInfoCore(LayoutElementHitInfo hitInfo)
        {
        }

        protected virtual LayoutElementHitInfo CreateHitInfo(Point pt) => 
            new LayoutElementHitInfo(pt, this);

        public bool EnsureBounds()
        {
            if (this.IsReady)
            {
                return true;
            }
            this.EnsureBoundsCore();
            this.IsReady = true;
            return this.IsReady;
        }

        protected abstract void EnsureBoundsCore();
        private void EnsureInitialState()
        {
            if (!this.isVisualStateInitialized)
            {
                this.isVisualStateInitialized = true;
                this.pressedHitResult = this.InitPressedState();
                this.hotHitResult = this.InitHotState();
            }
        }

        public IEnumerator<ILayoutElement> GetEnumerator() => 
            new LayoutElementEnumerator(this);

        protected virtual ILayoutElement[] GetNodesCore() => 
            EmptyNodes;

        public DevExpress.Xpf.Layout.Core.State GetState(object hitResult)
        {
            if (hitResult == null)
            {
                return DevExpress.Xpf.Layout.Core.State.Normal;
            }
            this.EnsureInitialState();
            DevExpress.Xpf.Layout.Core.State normal = DevExpress.Xpf.Layout.Core.State.Normal;
            if (this.HitEquals(hitResult, this.pressedHitResult))
            {
                normal |= DevExpress.Xpf.Layout.Core.State.Normal | DevExpress.Xpf.Layout.Core.State.Pressed;
            }
            if (this.HitEquals(hitResult, this.hotHitResult))
            {
                normal |= DevExpress.Xpf.Layout.Core.State.Hot;
            }
            return normal;
        }

        protected virtual bool HitEquals(object prevHitResult, object hitResult) => 
            Equals(prevHitResult, hitResult);

        public bool HitTest(Point pt) => 
            this.HitTestCore(pt);

        protected virtual bool HitTestBounds(Point hitPoint, Rect bounds) => 
            bounds.Contains(hitPoint);

        protected virtual bool HitTestCore(Point pt) => 
            this.EnsureBounds() && this.HitTestBounds(pt, this.Bounds);

        protected virtual object InitHotState() => 
            null;

        protected virtual object InitPressedState() => 
            null;

        public void Invalidate()
        {
            this.IsReady = false;
        }

        protected override void OnDispose()
        {
            this.nodesCore = null;
            if (!this.IsDragging)
            {
                this.parentCore = null;
            }
            base.OnDispose();
        }

        protected virtual void OnResetIsDragging()
        {
            if (base.IsDisposing)
            {
                this.parentCore = null;
            }
        }

        protected internal virtual void OnStateChanged(object hitResult, DevExpress.Xpf.Layout.Core.State state)
        {
        }

        public virtual void ResetState()
        {
            this.SetStateCore(ref this.pressedHitResult, null, DevExpress.Xpf.Layout.Core.State.Normal);
            this.SetStateCore(ref this.hotHitResult, null, DevExpress.Xpf.Layout.Core.State.Normal);
        }

        public void SetState(object hitResult, DevExpress.Xpf.Layout.Core.State state)
        {
            if (!base.IsDisposing)
            {
                this.EnsureInitialState();
                if ((state & (DevExpress.Xpf.Layout.Core.State.Normal | DevExpress.Xpf.Layout.Core.State.Pressed)) != DevExpress.Xpf.Layout.Core.State.Normal)
                {
                    this.SetStateCore(ref this.pressedHitResult, hitResult, state);
                }
                if ((state & DevExpress.Xpf.Layout.Core.State.Hot) != DevExpress.Xpf.Layout.Core.State.Normal)
                {
                    this.SetStateCore(ref this.hotHitResult, hitResult, state);
                }
            }
        }

        private void SetStateCore(ref object prevHitResult, object hitResult, DevExpress.Xpf.Layout.Core.State state)
        {
            if (!this.HitEquals(prevHitResult, hitResult))
            {
                object obj2 = prevHitResult;
                prevHitResult = hitResult;
                if (obj2 != null)
                {
                    this.OnStateChanged(obj2, this.GetState(obj2));
                }
                if (hitResult != null)
                {
                    this.OnStateChanged(hitResult, state);
                }
            }
        }

        public bool IsDragging
        {
            get => 
                this.isDraggingCore;
            set
            {
                if (this.isDraggingCore != value)
                {
                    this.isDraggingCore = value;
                    if (!this.isDraggingCore)
                    {
                        this.OnResetIsDragging();
                    }
                }
            }
        }

        public virtual bool HitTestingEnabled =>
            true;

        public virtual bool IsActive =>
            true;

        public virtual bool IsEnabled =>
            true;

        public bool IsReady { get; private set; }

        public virtual bool IsTabHeader =>
            false;

        public Rect Bounds =>
            ElementHelper.GetRect(this);

        public ILayoutContainer Container =>
            this.parentCore;

        public ILayoutElement Parent =>
            this.parentCore;

        public ILayoutElement[] Nodes
        {
            get
            {
                this.nodesCore ??= this.GetNodesCore();
                return this.nodesCore;
            }
        }

        public Point Location { get; protected internal set; }

        public System.Windows.Size Size { get; protected internal set; }
    }
}

