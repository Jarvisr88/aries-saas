namespace DevExpress.Xpf.Layout.Core.UIInteraction
{
    using DevExpress.Xpf.Layout.Core;
    using DevExpress.Xpf.Layout.Core.Platform;
    using System;
    using System.Windows;

    internal class UIInteractionService : UIService, IUIInteractionService, IUIService, IDisposable
    {
        private const long tickLength = 0x2710L;
        private bool fWaitForSecondClick;
        private WeakReference lastClickView = new WeakReference(null);
        private Point lastClickPoint;
        private long lastClickTime;
        private ILayoutElement activeItemCore;
        private LayoutElementHitInfo prevHotInfo;
        private LayoutElementHitInfo prevPressedInfo;

        public void Activate(IView view)
        {
            if (view != null)
            {
                IViewAdapter adapter = view.Adapter;
                if (adapter != null)
                {
                    IView[] viewArray = adapter.Views.ToArray();
                    this.DoActivate(view);
                    for (int i = 0; i < viewArray.Length; i++)
                    {
                        IView objA = viewArray[i];
                        objA.InvalidateZOrder();
                        if (!ReferenceEquals(objA, view))
                        {
                            if (objA.Type == HostType.AutoHide)
                            {
                                adapter.ActionService.Hide(objA, false);
                            }
                            this.DoDeactivate(objA);
                        }
                    }
                }
            }
        }

        protected bool CanProcessUIInteraction(ILayoutElement element) => 
            (element != null) && !element.IsDisposing;

        private bool CheckClick(LayoutElementHitInfo hi)
        {
            if ((base.Sender != null) && base.Sender.Adapter.GetIsDragging())
            {
                this.fWaitForSecondClick = false;
                return false;
            }
            if (!this.fWaitForSecondClick)
            {
                this.SaveFirstClickInfo(hi);
            }
            else
            {
                if (hi.InDoubleClickBounds && this.IsDoubleClick(hi))
                {
                    this.DoDoubleClickAction(hi);
                    this.fWaitForSecondClick = false;
                    return true;
                }
                this.SaveFirstClickInfo(hi);
            }
            if (((hi.Element != null) && ((hi.Element.GetState(hi.HitResult) & (DevExpress.Xpf.Layout.Core.State.Normal | DevExpress.Xpf.Layout.Core.State.Pressed)) != DevExpress.Xpf.Layout.Core.State.Normal)) && hi.InClickBounds)
            {
                if (hi.Element != null)
                {
                    hi.Element.ResetState();
                }
                if (this.DoClickAction(hi) || !hi.InDoubleClickBounds)
                {
                    this.fWaitForSecondClick = false;
                    return true;
                }
            }
            return false;
        }

        private bool CheckLocation(Point point) => 
            (Math.Abs((double) (point.X - this.lastClickPoint.X)) <= SystemInformation.DoubleClickWidth) & (Math.Abs((double) (point.Y - this.lastClickPoint.Y)) <= SystemInformation.DoubleClickHeight);

        private void CheckResetPrevHotElement(LayoutElementHitInfo hitInfo)
        {
            if ((this.prevHotInfo != null) && !ReferenceEquals(this.prevHotInfo.Element, hitInfo.Element))
            {
                this.SetElementHotTrackedInfo(this.prevHotInfo.Element, LayoutElementHitInfo.Empty);
            }
            this.prevHotInfo = hitInfo.IsHot ? hitInfo : null;
        }

        private void CheckResetPrevPressedElement(LayoutElementHitInfo hitInfo)
        {
            if ((this.prevPressedInfo != null) && !ReferenceEquals(this.prevPressedInfo.Element, hitInfo.Element))
            {
                this.SetElementPressedInfo(this.prevPressedInfo.Element, LayoutElementHitInfo.Empty);
            }
            this.prevPressedInfo = hitInfo.IsPressed ? hitInfo : null;
            if ((this.prevPressedInfo != null) && ((this.ActiveItem == null) || !ReferenceEquals(this.prevPressedInfo.Element, this.ActiveItem)))
            {
                ILayoutElement element = this.prevPressedInfo.Element;
                if (element != null)
                {
                    element.Disposed += new EventHandler(this.OnActiveItemDisposed);
                }
            }
        }

        private bool CheckTime(long time) => 
            (Math.Abs((long) (this.lastClickTime - time)) / 0x2710L) <= SystemInformation.DoubleClickTime;

        public void Deactivate(IView view)
        {
            if (view != null)
            {
                this.DoDeactivate(view);
            }
        }

        protected void DoActivate(IView view)
        {
            IUIInteractionServiceListener uIServiceListener = view.GetUIServiceListener<IUIInteractionServiceListener>();
            if (uIServiceListener != null)
            {
                uIServiceListener.OnActivate();
            }
        }

        protected void DoChangeActiveItem(IView view, ILayoutElement element)
        {
            IUIInteractionServiceListener uIServiceListener = view.GetUIServiceListener<IUIInteractionServiceListener>();
            if (((uIServiceListener != null) && (!ReferenceEquals(this.ActiveItem, element) || !this.ActiveItem.IsActive)) && uIServiceListener.OnActiveItemChanging(element))
            {
                this.UnSubscribe(this.ActiveItem);
                if (uIServiceListener.OnActiveItemChanged(element))
                {
                    if ((element != null) && element.IsDisposing)
                    {
                        element = null;
                    }
                    this.activeItemCore = element;
                    this.Subscribe(this.ActiveItem);
                }
                else
                {
                    this.activeItemCore = null;
                }
            }
        }

        protected bool DoClickAction(LayoutElementHitInfo clickInfo)
        {
            IUIInteractionServiceListener uIServiceListener = base.Sender.GetUIServiceListener<IUIInteractionServiceListener>();
            return ((uIServiceListener != null) && uIServiceListener.OnClickAction(clickInfo));
        }

        protected bool DoClickPreviewAction(LayoutElementHitInfo clickInfo)
        {
            IUIInteractionServiceListener uIServiceListener = base.Sender.GetUIServiceListener<IUIInteractionServiceListener>();
            return ((uIServiceListener != null) && uIServiceListener.OnClickPreviewAction(clickInfo));
        }

        protected void DoDeactivate(IView view)
        {
            IUIInteractionServiceListener uIServiceListener = view.GetUIServiceListener<IUIInteractionServiceListener>();
            if (uIServiceListener != null)
            {
                uIServiceListener.OnDeactivate();
            }
        }

        protected bool DoDoubleClickAction(LayoutElementHitInfo clickInfo)
        {
            IUIInteractionServiceListener uIServiceListener = base.Sender.GetUIServiceListener<IUIInteractionServiceListener>();
            return ((uIServiceListener != null) && uIServiceListener.OnDoubleClickAction(clickInfo));
        }

        protected bool DoMenuAction(LayoutElementHitInfo clickInfo)
        {
            IUIInteractionServiceListener uIServiceListener = base.Sender.GetUIServiceListener<IUIInteractionServiceListener>();
            return ((uIServiceListener != null) && uIServiceListener.OnMenuAction(clickInfo));
        }

        protected bool DoMiddleButtonClickAction(LayoutElementHitInfo clickInfo)
        {
            IUIInteractionServiceListener uIServiceListener = base.Sender.GetUIServiceListener<IUIInteractionServiceListener>();
            return ((uIServiceListener != null) && uIServiceListener.OnMiddleButtonClickAction(clickInfo));
        }

        private bool IsDoubleClick(LayoutElementHitInfo hi) => 
            (ReferenceEquals(base.Sender, this.lastClickView.Target) && this.CheckLocation(hi.HitPoint)) && this.CheckTime(DateTime.Now.Ticks);

        private void OnActiveItemDisposed(object sender, EventArgs e)
        {
            ILayoutElement item = sender as ILayoutElement;
            item ??= this.ActiveItem;
            this.UnSubscribe(item);
            if ((this.prevHotInfo != null) && ReferenceEquals(this.prevHotInfo.Element, item))
            {
                this.prevHotInfo = null;
            }
            if ((this.prevPressedInfo != null) && ReferenceEquals(this.prevPressedInfo.Element, item))
            {
                this.prevPressedInfo = null;
            }
            this.activeItemCore = null;
        }

        protected override void OnDispose()
        {
            this.UnSubscribe(this.ActiveItem);
            this.activeItemCore = null;
            this.lastClickView = null;
            this.prevHotInfo = null;
            this.prevPressedInfo = null;
            base.OnDispose();
        }

        protected bool OnMouseDown(LayoutElementHitInfo clickInfo)
        {
            IUIInteractionServiceListener uIServiceListener = base.Sender.GetUIServiceListener<IUIInteractionServiceListener>();
            return ((uIServiceListener != null) && uIServiceListener.OnMouseDown(clickInfo));
        }

        protected bool OnMouseUp(LayoutElementHitInfo clickInfo)
        {
            IUIInteractionServiceListener uIServiceListener = base.Sender.GetUIServiceListener<IUIInteractionServiceListener>();
            return ((uIServiceListener != null) && uIServiceListener.OnMouseUp(clickInfo));
        }

        protected bool ProcessMouseDown(LayoutElementHitInfo downHitInfo, MouseEventArgs ea)
        {
            this.CheckResetPrevPressedElement(downHitInfo);
            if (ea.Buttons == MouseButtons.Left)
            {
                this.SetElementPressedInfo(downHitInfo.Element, downHitInfo.IsPressed ? downHitInfo : LayoutElementHitInfo.Empty);
            }
            if (ea.Buttons == MouseButtons.Middle)
            {
                this.DoMiddleButtonClickAction(downHitInfo);
            }
            this.OnMouseDown(downHitInfo);
            return false;
        }

        protected bool ProcessMouseLeave()
        {
            this.CheckResetPrevHotElement(LayoutElementHitInfo.Empty);
            return false;
        }

        protected bool ProcessMouseMove(LayoutElementHitInfo moveInfo, MouseEventArgs ea)
        {
            this.CheckResetPrevHotElement(moveInfo);
            this.SetElementHotTrackedInfo(moveInfo.Element, moveInfo.IsHot ? moveInfo : LayoutElementHitInfo.Empty);
            return false;
        }

        protected override bool ProcessMouseOverride(IView view, MouseEventType eventType, MouseEventArgs ea)
        {
            IViewAdapter adapter = view.Adapter;
            if (adapter != null)
            {
                if (eventType == MouseEventType.MouseLeave)
                {
                    return this.ProcessMouseLeave();
                }
                LayoutElementHitInfo downHitInfo = adapter.CalcHitInfo(view, ea.Point);
                if ((eventType == MouseEventType.MouseDown) && !downHitInfo.InControlBox)
                {
                    this.Activate(base.Sender);
                    this.SetActiveItem(base.Sender, downHitInfo.Element);
                    downHitInfo = adapter.CalcHitInfo(view, ea.Point);
                }
                ILayoutElement element = downHitInfo.Element;
                if (this.CanProcessUIInteraction(element))
                {
                    bool flag = false;
                    switch (eventType)
                    {
                        case MouseEventType.MouseDown:
                            flag = this.ProcessMouseDown(downHitInfo, ea);
                            break;

                        case MouseEventType.MouseUp:
                            flag = this.ProcessMouseUp(downHitInfo, ea);
                            break;

                        case MouseEventType.MouseMove:
                            flag = this.ProcessMouseMove(downHitInfo, ea);
                            break;

                        default:
                            break;
                    }
                    if (flag)
                    {
                        adapter.DragService.Reset();
                    }
                    return flag;
                }
            }
            return false;
        }

        protected bool ProcessMouseUp(LayoutElementHitInfo upHitInfo, MouseEventArgs ea)
        {
            this.CheckResetPrevPressedElement(upHitInfo);
            bool flag = false;
            bool isDisposing = false;
            if ((ea.Buttons != MouseButtons.None) || (upHitInfo.Element == null))
            {
                this.fWaitForSecondClick = false;
            }
            else
            {
                if (ea.ChangedButtons == MouseButtons.Left)
                {
                    if (upHitInfo.InClickPreviewBounds)
                    {
                        this.DoClickPreviewAction(upHitInfo);
                    }
                    if (upHitInfo.InClickBounds || upHitInfo.InDoubleClickBounds)
                    {
                        flag = this.CheckClick(upHitInfo);
                    }
                }
                if ((ea.ChangedButtons == (MouseButtons.None | MouseButtons.Right)) && upHitInfo.InMenuBounds)
                {
                    flag = this.DoMenuAction(upHitInfo);
                    if (flag)
                    {
                        ea.Handled = true;
                    }
                }
                isDisposing = upHitInfo.Element.IsDisposing;
                if (flag)
                {
                    LayoutElementHitInfo info;
                    this.prevPressedInfo = (LayoutElementHitInfo) (info = null);
                    this.prevHotInfo = info;
                }
            }
            if (!isDisposing)
            {
                this.SetElementPressedInfo(upHitInfo.Element, LayoutElementHitInfo.Empty);
                if (flag && upHitInfo.InControlBox)
                {
                    this.SetActiveItem(base.Sender, upHitInfo.Element);
                }
            }
            this.OnMouseUp(upHitInfo);
            return flag;
        }

        private void SaveFirstClickInfo(LayoutElementHitInfo hi)
        {
            this.lastClickPoint = hi.HitPoint;
            this.lastClickView = new WeakReference(base.Sender);
            this.lastClickTime = DateTime.Now.Ticks;
            this.fWaitForSecondClick = true;
        }

        public void SetActiveItem(IView view, ILayoutElement element)
        {
            if ((view != null) && (element != null))
            {
                this.DoChangeActiveItem(view, element);
            }
        }

        private void SetElementHotTrackedInfo(ILayoutElement element, LayoutElementHitInfo hitInfo)
        {
            if (element != null)
            {
                element.SetState(hitInfo.HitResult, DevExpress.Xpf.Layout.Core.State.Hot);
            }
        }

        private void SetElementPressedInfo(ILayoutElement element, LayoutElementHitInfo hitInfo)
        {
            if (element != null)
            {
                element.SetState(hitInfo.HitResult, DevExpress.Xpf.Layout.Core.State.Normal | DevExpress.Xpf.Layout.Core.State.Pressed);
            }
        }

        private void Subscribe(ILayoutElement item)
        {
            if (item != null)
            {
                item.Disposed += new EventHandler(this.OnActiveItemDisposed);
            }
        }

        private void UnSubscribe(ILayoutElement item)
        {
            if (item != null)
            {
                item.Disposed -= new EventHandler(this.OnActiveItemDisposed);
            }
        }

        public ILayoutElement ActiveItem =>
            this.activeItemCore;
    }
}

