namespace DevExpress.Xpf.Layout.Core.Platform
{
    using DevExpress.Xpf.Layout.Core;
    using DevExpress.Xpf.Layout.Core.Base;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class LayoutElementHostAdapter : BaseObject, ILayoutElementHostAdapter, IDisposable
    {
        internal IDictionary<ILayoutElementHost, LayoutElementHitInfo> Context;
        private int lockEventProcessing;

        public void BeginEvent(ILayoutElementHost host)
        {
            this.BeginEventCore(host);
            this.Context.Clear();
        }

        private void BeginEventCore(ILayoutElementHost host)
        {
            this.lockEventProcessing++;
            this.ActiveHost = host;
            this.Ensure(this.ActiveHost);
        }

        public LayoutElementHitInfo CalcHitInfo(ILayoutElementHost host, Point hitPoint)
        {
            if (hitPoint == UIService.InvalidPoint)
            {
                return LayoutElementHitInfo.Empty;
            }
            LayoutElementHitInfo info = null;
            bool flag = this.Context.TryGetValue(host, out info);
            if (!flag || ((info.HitPoint != hitPoint) || info.Element.IsDisposing))
            {
                this.Ensure(host);
                info = this.CalcHitInfoCore(host, hitPoint);
                if (flag)
                {
                    this.Context[host] = info;
                }
                else
                {
                    this.Context.Add(host, info);
                }
            }
            return info;
        }

        protected virtual LayoutElementHitInfo CalcHitInfoCore(ILayoutElementHost host, Point point)
        {
            ILayoutElement layoutRoot = host?.LayoutRoot;
            return ((layoutRoot != null) ? layoutRoot.CalcHitInfo(point) : LayoutElementHitInfo.Empty);
        }

        protected virtual IDictionary<ILayoutElementHost, LayoutElementHitInfo> CreateContext() => 
            new Dictionary<ILayoutElementHost, LayoutElementHitInfo>();

        public void EndEvent()
        {
            foreach (KeyValuePair<ILayoutElementHost, LayoutElementHitInfo> pair in this.Context)
            {
                this.Invalidate(pair.Key);
            }
            this.Context.Clear();
            this.EndEventCore();
        }

        private void EndEventCore()
        {
            this.ActiveHost = null;
            this.lockEventProcessing--;
        }

        protected void Ensure(ILayoutElementHost host)
        {
            if ((host != null) && (host.LayoutRoot == null))
            {
                host.EnsureLayoutRoot();
            }
        }

        public bool HitTest(ILayoutElementHost host, Point hitPoint) => 
            this.HitTestCore(host, hitPoint);

        protected virtual bool HitTestCore(ILayoutElementHost host, Point point)
        {
            ILayoutElement layoutRoot = host?.LayoutRoot;
            return ((layoutRoot != null) && HitTestRootElements(host, layoutRoot, point));
        }

        private static bool HitTestRootElements(ILayoutElementHost host, ILayoutElement root, Point point)
        {
            ILayoutElement element = (root.Nodes.Length != 0) ? root.Nodes[0] : null;
            return (root.HitTest(point) || ((host.Type == HostType.AutoHide) && ((element != null) && element.HitTest(point))));
        }

        protected void Invalidate(ILayoutElementHost host)
        {
            ILayoutElement layoutRoot = host?.LayoutRoot;
            if (layoutRoot != null)
            {
                using (IEnumerator<ILayoutElement> enumerator = layoutRoot.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        enumerator.Current.Invalidate();
                    }
                }
            }
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            this.Context = this.CreateContext();
        }

        protected override void OnDispose()
        {
            Ref.Clear<ILayoutElementHost, LayoutElementHitInfo>(ref this.Context);
            base.OnDispose();
        }

        public ILayoutElementHost ActiveHost { get; private set; }

        public bool IsInEvent =>
            this.lockEventProcessing > 0;
    }
}

