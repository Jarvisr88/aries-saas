namespace DevExpress.Xpf.Layout.Core.Platform
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;

    public class BaseView : BaseLayoutElementHost, IView, ILayoutElementHost, IBaseObject, IDisposable, IUIServiceProvider
    {
        private IDictionary<object, IUIServiceListener> defaultListenersCore;
        private IDictionary<object, IUIServiceListener> customListenersCore;
        internal IViewAdapter adapterCore;
        private int? zOrderCore;

        protected BaseView(ILayoutElementFactory factory, ILayoutElementBehavior behavior) : base(factory, behavior)
        {
        }

        protected virtual void BeginProcessEvent(object ea)
        {
        }

        protected virtual int CalcZOrder() => 
            0;

        protected virtual bool CanHandleMouseCaptureLost() => 
            true;

        public bool CanHandleMouseDown() => 
            this.CanHandleMouseDownCore();

        protected virtual bool CanHandleMouseDownCore() => 
            this.Adapter.DragService.DragItem != null;

        protected internal virtual bool CanSuspendBehindDragging(ILayoutElement dragItem) => 
            false;

        protected internal virtual bool CanSuspendClientDragging(ILayoutElement dragItem) => 
            false;

        protected internal virtual bool CanSuspendDocking(ILayoutElement dragItem) => 
            false;

        protected internal virtual bool CanSuspendFloating(ILayoutElement dragItem) => 
            false;

        protected internal virtual bool CanSuspendFloatingMoving(ILayoutElement dragItem) => 
            false;

        protected internal virtual bool CanSuspendReordering(ILayoutElement dragItem) => 
            false;

        protected internal virtual bool CanSuspendResizing(ILayoutElement dragItem) => 
            false;

        protected virtual bool CanUseCustomServiceListener(object key) => 
            false;

        protected internal virtual bool CheckReordering(Point point) => 
            true;

        protected virtual void EndProcessEvent(object ea)
        {
        }

        protected virtual ServiceListener GetCustomUIServiceListener<ServiceListener>(object key) where ServiceListener: class, IUIServiceListener => 
            default(ServiceListener);

        protected virtual ServiceListener GetDefaultUIServiceListener<ServiceListener>(object key) where ServiceListener: class, IUIServiceListener
        {
            IUIServiceListener listener;
            return (this.defaultListenersCore.TryGetValue(key, out listener) ? ((ServiceListener) listener) : null);
        }

        public ServiceListener GetUIServiceListener<ServiceListener>() where ServiceListener: class, IUIServiceListener => 
            this.GetUIServiceListenerCore<ServiceListener>(typeof(ServiceListener));

        public ServiceListener GetUIServiceListener<ServiceListener>(object key) where ServiceListener: class, IUIServiceListener => 
            this.GetUIServiceListenerCore<ServiceListener>(key);

        private ServiceListener GetUIServiceListenerCore<ServiceListener>(object key) where ServiceListener: class, IUIServiceListener
        {
            if (base.IsDisposing)
            {
                return default(ServiceListener);
            }
            ServiceListener local = default(ServiceListener);
            if (this.CanUseCustomServiceListener(key))
            {
                IUIServiceListener customUIServiceListener = null;
                if (!this.customListenersCore.TryGetValue(key, out customUIServiceListener))
                {
                    customUIServiceListener = this.GetCustomUIServiceListener<ServiceListener>(key);
                    customUIServiceListener.ServiceProvider = this;
                    if (customUIServiceListener is ServiceListener)
                    {
                        this.customListenersCore.Add(key, customUIServiceListener);
                    }
                }
                local = customUIServiceListener as ServiceListener;
            }
            ServiceListener defaultUIServiceListener = local;
            if (local == null)
            {
                ServiceListener local1 = local;
                defaultUIServiceListener = this.GetDefaultUIServiceListener<ServiceListener>(key);
            }
            return defaultUIServiceListener;
        }

        public void InvalidateZOrder()
        {
            this.zOrderCore = null;
        }

        protected override void OnCreate()
        {
            this.defaultListenersCore = new Dictionary<object, IUIServiceListener>();
            this.customListenersCore = new Dictionary<object, IUIServiceListener>();
            base.OnCreate();
        }

        protected override void OnDispose()
        {
            Ref.Clear<object, IUIServiceListener>(ref this.defaultListenersCore);
            Ref.Clear<object, IUIServiceListener>(ref this.customListenersCore);
            this.adapterCore = null;
            base.OnDispose();
        }

        public void OnKeyDown(Key key)
        {
            this.Adapter.ProcessKey(this, KeyEventType.KeyDown, key);
        }

        public void OnKeyUp(Key key)
        {
            this.Adapter.ProcessKey(this, KeyEventType.KeyUp, key);
        }

        public void OnMouseCaptureLost()
        {
            if (this.CanHandleMouseCaptureLost())
            {
                this.Adapter.ProcessMouseEvent(this, MouseEventType.MouseCaptureLost, null);
            }
        }

        public void OnMouseDown(DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs ea)
        {
            this.Adapter.ProcessMouseEvent(this, MouseEventType.MouseDown, ea);
        }

        public void OnMouseEvent(MouseEventType eventType, DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs ea = null)
        {
            this.BeginProcessEvent(ea);
            try
            {
                switch (eventType)
                {
                    case MouseEventType.MouseDown:
                        this.OnMouseDown(ea);
                        break;

                    case MouseEventType.MouseUp:
                        this.OnMouseUp(ea);
                        break;

                    case MouseEventType.MouseMove:
                        this.OnMouseMove(ea);
                        break;

                    case MouseEventType.MouseLeave:
                        this.OnMouseLeave();
                        break;

                    case MouseEventType.MouseCaptureLost:
                        this.OnMouseCaptureLost();
                        break;

                    default:
                        break;
                }
            }
            finally
            {
                this.EndProcessEvent(ea);
            }
        }

        public void OnMouseLeave()
        {
            this.Adapter.ProcessMouseEvent(this, MouseEventType.MouseLeave, null);
        }

        public void OnMouseMove(DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs ea)
        {
            this.Adapter.ProcessMouseEvent(this, MouseEventType.MouseMove, ea);
        }

        public void OnMouseUp(DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs ea)
        {
            this.Adapter.ProcessMouseEvent(this, MouseEventType.MouseUp, ea);
        }

        public void RegisterUIServiceListener(IUIServiceListener listener)
        {
            if (listener != null)
            {
                this.defaultListenersCore[listener.Key] = listener;
                listener.ServiceProvider = this;
            }
        }

        public IViewAdapter Adapter =>
            this.adapterCore;

        public int ZOrder
        {
            get
            {
                if (this.zOrderCore == null)
                {
                    this.zOrderCore = new int?(this.CalcZOrder());
                }
                return this.zOrderCore.Value;
            }
        }
    }
}

