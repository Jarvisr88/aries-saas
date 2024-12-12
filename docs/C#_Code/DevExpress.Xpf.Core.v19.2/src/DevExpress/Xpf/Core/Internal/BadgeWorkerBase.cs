namespace DevExpress.Xpf.Core.Internal
{
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Threading;

    public abstract class BadgeWorkerBase : DispatcherObject
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty WorkerProperty = DependencyProperty.RegisterAttached("Worker", typeof(BadgeWorkerBase), typeof(BadgeWorkerBase), new PropertyMetadata(null));
        private DevExpress.Xpf.Core.Badge badge;
        private int cachedDuration;
        private readonly Locker animationLocker = new Locker();
        private DevExpress.Xpf.Core.Native.BadgeControl badgeControl;
        private int showHideOperationId;
        private DispatcherOperation refreshOperation;
        private Rect badgeBounds;

        protected BadgeWorkerBase(System.Windows.FrameworkElement target)
        {
            this.<Target>k__BackingField = target;
            this.AttachTargetEvents(target);
        }

        protected virtual void AttachTargetEvents(System.Windows.FrameworkElement target)
        {
            target.SizeChanged += new SizeChangedEventHandler(this.OnTargetSizeChanged);
            target.IsVisibleChanged += new DependencyPropertyChangedEventHandler(this.OnTargetIsVisibleChanged);
        }

        protected virtual void BadgeChanged(DevExpress.Xpf.Core.Badge oldValue, DevExpress.Xpf.Core.Badge newValue)
        {
            bool hideFast = newValue != null;
            bool showFast = oldValue != null;
            if (hideFast | showFast)
            {
                bool flag3;
                bool flag4;
                this.CalculateIsVisibleEx(false, out flag3, out flag4);
                if (flag3)
                {
                    if (this.IsVisible && !flag4)
                    {
                        hideFast = false;
                    }
                    if (!this.IsVisible & flag4)
                    {
                        showFast = false;
                    }
                }
            }
            this.Refresh(hideFast, showFast);
        }

        public virtual void CalculateBadgePlacement(Size badgesize, ref Rect targetRect, ref Rect precomputed)
        {
            if (this.BadgeHost != null)
            {
                HorizontalAlignment alignment;
                VerticalAlignment alignment2;
                HorizontalAlignment alignment3;
                VerticalAlignment alignment4;
                DevExpress.Xpf.Core.Native.BadgeControl.CalculateAlignment(this.BadgeControl, out alignment, out alignment2, out alignment3, out alignment4);
                bool flag = this.BadgeHost.CalculateBounds(badgesize, alignment, alignment3, alignment2, alignment4, ref targetRect, ref precomputed);
                this.BadgeBounds = precomputed;
            }
        }

        protected virtual bool CalculateIsVisible()
        {
            bool flag;
            bool flag2;
            this.CalculateIsVisibleEx(true, out flag, out flag2);
            return (flag & flag2);
        }

        protected virtual void CalculateIsVisibleEx(bool fast, out bool canShowBadge, out bool badgeIsVisible)
        {
            canShowBadge = true;
            badgeIsVisible = true;
            if ((this.Badge == null) || !BadgeProperties.GetIsVisible(this.Badge))
            {
                canShowBadge = false;
            }
            else
            {
                if (this.Badge.Visibility != Visibility.Visible)
                {
                    badgeIsVisible = false;
                    if (fast)
                    {
                        return;
                    }
                }
                if ((this.FrameworkElement == null) || (!this.FrameworkElement.IsVisible || ((this.FrameworkElement.ActualWidth < 1.0) || (this.FrameworkElement.ActualHeight < 1.0))))
                {
                    canShowBadge = false;
                }
                else if (!BadgeProperties.GetIsVisible(this.FrameworkElement))
                {
                    canShowBadge = false;
                }
                else if (!this.CalculateIsVisibleOverride())
                {
                    canShowBadge = false;
                }
            }
        }

        protected abstract bool CalculateIsVisibleOverride();
        protected virtual void Destroy()
        {
            this.Hide();
            this.DetachTargetEvents(this.FrameworkElement);
        }

        protected virtual void DetachTargetEvents(System.Windows.FrameworkElement target)
        {
            target.SizeChanged -= new SizeChangedEventHandler(this.OnTargetSizeChanged);
            target.IsVisibleChanged -= new DependencyPropertyChangedEventHandler(this.OnTargetIsVisibleChanged);
        }

        protected internal static BadgeWorkerBase GetWorker(DependencyObject element) => 
            (BadgeWorkerBase) element.GetValue(WorkerProperty);

        [AsyncStateMachine(typeof(<Hide>d__28))]
        protected virtual void Hide()
        {
            <Hide>d__28 d__;
            d__.<>4__this = this;
            d__.<>t__builder = AsyncVoidMethodBuilder.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<Hide>d__28>(ref d__);
        }

        [AsyncStateMachine(typeof(<HideAsync>d__29))]
        private Task HideAsync(int duration, int operationId)
        {
            <HideAsync>d__29 d__;
            d__.<>4__this = this;
            d__.duration = duration;
            d__.operationId = operationId;
            d__.<>t__builder = AsyncTaskMethodBuilder.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<HideAsync>d__29>(ref d__);
            return d__.<>t__builder.Task;
        }

        protected abstract void HideOverride();
        protected virtual void OnBadgeControlChanged(DevExpress.Xpf.Core.Native.BadgeControl oldValue, DevExpress.Xpf.Core.Native.BadgeControl value)
        {
            if (oldValue != null)
            {
                oldValue.Worker = null;
                LogicalTreeWrapper.RemoveLogicalChild(this.Target, oldValue, null, false);
            }
            if (value != null)
            {
                LogicalTreeWrapper.AddLogicalChild(this.Target, value, null, false);
                value.Worker = this;
                if (this.animationLocker.IsLocked)
                {
                    value.PrepareForAnimation(true, this.cachedDuration);
                }
            }
        }

        protected virtual void OnTargetIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.Refresh(true, false);
        }

        protected virtual void OnTargetSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (((e.NewSize.Width < 1.0) || (e.NewSize.Height < 1.0)) || !this.IsVisible)
            {
                this.Refresh(true, false);
            }
        }

        public virtual void Refresh(bool hideFast = false, bool showFast = false)
        {
            if (this.refreshOperation == null)
            {
                this.refreshOperation = base.Dispatcher.BeginInvoke(delegate {
                    int animationDuration;
                    DevExpress.Xpf.Core.Badge badge = this.Badge;
                    if (badge != null)
                    {
                        animationDuration = badge.AnimationDuration;
                    }
                    else
                    {
                        DevExpress.Xpf.Core.Badge local1 = badge;
                        animationDuration = 0;
                    }
                    int num = animationDuration;
                    this.ShowHide(hideFast ? 0 : num, showFast ? 0 : num);
                    this.refreshOperation = null;
                }, DispatcherPriority.Normal, new object[0]);
            }
        }

        protected static void SetWorker(DependencyObject element, BadgeWorkerBase value)
        {
            element.SetValue(WorkerProperty, value);
        }

        [AsyncStateMachine(typeof(<Show>d__30))]
        protected virtual void Show()
        {
            <Show>d__30 d__;
            d__.<>4__this = this;
            d__.<>t__builder = AsyncVoidMethodBuilder.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<Show>d__30>(ref d__);
        }

        [AsyncStateMachine(typeof(<ShowAsync>d__31))]
        private Task ShowAsync(int duration, int operationId)
        {
            <ShowAsync>d__31 d__;
            d__.<>4__this = this;
            d__.duration = duration;
            d__.operationId = operationId;
            d__.<>t__builder = AsyncTaskMethodBuilder.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ShowAsync>d__31>(ref d__);
            return d__.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<ShowHide>d__33))]
        private void ShowHide(int hideDuration, int showDuration)
        {
            <ShowHide>d__33 d__;
            d__.<>4__this = this;
            d__.hideDuration = hideDuration;
            d__.showDuration = showDuration;
            d__.<>t__builder = AsyncVoidMethodBuilder.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ShowHide>d__33>(ref d__);
        }

        protected abstract void ShowOverride();
        public static void Update(DependencyObject target, Tuple<DevExpress.Xpf.Core.Badge, BadgeBlendingMode> oldValue, Tuple<DevExpress.Xpf.Core.Badge, BadgeBlendingMode> newValue)
        {
            if (target is System.Windows.FrameworkElement)
            {
                System.Windows.FrameworkElement element = target as System.Windows.FrameworkElement;
                DevExpress.Xpf.Core.Badge badge = oldValue?.Item1;
                DevExpress.Xpf.Core.Badge badge2 = newValue?.Item1;
                BadgeBlendingMode mode = (oldValue != null) ? oldValue.Item2 : ((newValue != null) ? newValue.Item2 : BadgeBlendingMode.Shader);
                BadgeBlendingMode mode2 = (newValue != null) ? newValue.Item2 : ((oldValue != null) ? oldValue.Item2 : BadgeBlendingMode.Shader);
                if ((badge != null) || (badge2 != null))
                {
                    BadgeWorkerBase worker = GetWorker(target);
                    if (((mode != mode2) || (badge2 == null)) && (worker != null))
                    {
                        worker.Destroy();
                        worker = null;
                    }
                    if (badge2 != null)
                    {
                        if (worker == null)
                        {
                            if (mode2 == BadgeBlendingMode.Shader)
                            {
                                worker = new BadgeShaderWorker(element);
                            }
                            else
                            {
                                if (mode2 != BadgeBlendingMode.Adorner)
                                {
                                    throw new ArgumentOutOfRangeException();
                                }
                                worker = new BadgeAdornerWorker(element);
                            }
                            SetWorker(target, worker);
                        }
                        worker.Badge = newValue.Item1;
                    }
                }
            }
        }

        protected DevExpress.Xpf.Core.Native.BadgeControl BadgeControl
        {
            get => 
                this.badgeControl;
            set
            {
                if (!ReferenceEquals(this.badgeControl, value))
                {
                    DevExpress.Xpf.Core.Native.BadgeControl badgeControl = this.badgeControl;
                    this.badgeControl = value;
                    this.OnBadgeControlChanged(badgeControl, value);
                }
            }
        }

        protected DependencyObject Target { get; }

        protected System.Windows.FrameworkElement FrameworkElement =>
            this.Target as System.Windows.FrameworkElement;

        protected IBadgeHost BadgeHost =>
            this.Target as IBadgeHost;

        public DevExpress.Xpf.Core.Badge Badge
        {
            get => 
                this.badge;
            set
            {
                DevExpress.Xpf.Core.Badge oldValue = this.badge;
                this.badge = value;
                this.BadgeChanged(oldValue, value);
            }
        }

        protected bool IsVisible { get; private set; }

        protected Rect BadgeBounds
        {
            get => 
                this.badgeBounds;
            private set => 
                this.badgeBounds = value;
        }

        [CompilerGenerated]
        private struct <Hide>d__28 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncVoidMethodBuilder <>t__builder;
            public BadgeWorkerBase <>4__this;
            private TaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter awaiter;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new TaskAwaiter();
                        this.<>1__state = num = -1;
                        goto TR_0004;
                    }
                    else if (this.<>4__this.IsVisible)
                    {
                        int num3 = this.<>4__this.showHideOperationId + 1;
                        this.<>4__this.showHideOperationId = num3;
                        int operationId = num3;
                        awaiter = this.<>4__this.HideAsync(0, operationId).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, BadgeWorkerBase.<Hide>d__28>(ref awaiter, ref this);
                        }
                        return;
                    }
                    goto TR_0002;
                TR_0004:
                    awaiter.GetResult();
                    awaiter = new TaskAwaiter();
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
            TR_0002:
                this.<>1__state = -2;
                this.<>t__builder.SetResult();
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <HideAsync>d__29 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder <>t__builder;
            public BadgeWorkerBase <>4__this;
            public int duration;
            public int operationId;
            private BadgeWorkerBase.<>c__DisplayClass29_0 <>8__1;
            private TaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter awaiter;
                    TaskAwaiter awaiter2;
                    TaskAwaiter awaiter3;
                    switch (num)
                    {
                        case 0:
                            awaiter = this.<>u__1;
                            this.<>u__1 = new TaskAwaiter();
                            this.<>1__state = num = -1;
                            break;

                        case 1:
                            awaiter2 = this.<>u__1;
                            this.<>u__1 = new TaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_0007;

                        case 2:
                            awaiter3 = this.<>u__1;
                            this.<>u__1 = new TaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_0006;

                        default:
                            this.<>8__1 = new BadgeWorkerBase.<>c__DisplayClass29_0();
                            this.<>8__1.<>4__this = this.<>4__this;
                            this.<>8__1.duration = this.duration;
                            if (this.<>4__this.IsVisible && (this.operationId == this.<>4__this.showHideOperationId))
                            {
                                this.<>4__this.cachedDuration = this.<>8__1.duration;
                                if (this.<>4__this.BadgeControl != null)
                                {
                                    awaiter = this.<>4__this.Dispatcher.InvokeAsync(new Action(this.<>8__1.<HideAsync>b__0)).GetAwaiter();
                                    if (awaiter.IsCompleted)
                                    {
                                        break;
                                    }
                                    this.<>1__state = num = 0;
                                    this.<>u__1 = awaiter;
                                    this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, BadgeWorkerBase.<HideAsync>d__29>(ref awaiter, ref this);
                                    return;
                                }
                            }
                            goto TR_0002;
                    }
                    awaiter.GetResult();
                    awaiter = new TaskAwaiter();
                    awaiter2 = Task.Delay(this.<>8__1.duration).GetAwaiter();
                    if (!awaiter2.IsCompleted)
                    {
                        this.<>1__state = num = 1;
                        this.<>u__1 = awaiter2;
                        this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, BadgeWorkerBase.<HideAsync>d__29>(ref awaiter2, ref this);
                        return;
                    }
                    goto TR_0007;
                TR_0006:
                    awaiter3.GetResult();
                    awaiter3 = new TaskAwaiter();
                    goto TR_0002;
                TR_0007:
                    awaiter2.GetResult();
                    awaiter2 = new TaskAwaiter();
                    awaiter3 = this.<>4__this.Dispatcher.InvokeAsync(new Action(this.<>4__this.<HideAsync>b__29_1)).GetAwaiter();
                    if (awaiter3.IsCompleted)
                    {
                        goto TR_0006;
                    }
                    else
                    {
                        this.<>1__state = num = 2;
                        this.<>u__1 = awaiter3;
                        this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, BadgeWorkerBase.<HideAsync>d__29>(ref awaiter3, ref this);
                    }
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
                return;
            TR_0002:
                this.<>1__state = -2;
                this.<>t__builder.SetResult();
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <Show>d__30 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncVoidMethodBuilder <>t__builder;
            public BadgeWorkerBase <>4__this;
            private TaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter awaiter;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new TaskAwaiter();
                        this.<>1__state = num = -1;
                        goto TR_0004;
                    }
                    else if (!this.<>4__this.IsVisible)
                    {
                        int num3 = this.<>4__this.showHideOperationId + 1;
                        this.<>4__this.showHideOperationId = num3;
                        int operationId = num3;
                        awaiter = this.<>4__this.ShowAsync(0, operationId).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, BadgeWorkerBase.<Show>d__30>(ref awaiter, ref this);
                        }
                        return;
                    }
                    goto TR_0002;
                TR_0004:
                    awaiter.GetResult();
                    awaiter = new TaskAwaiter();
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
            TR_0002:
                this.<>1__state = -2;
                this.<>t__builder.SetResult();
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <ShowAsync>d__31 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder <>t__builder;
            public BadgeWorkerBase <>4__this;
            public int duration;
            public int operationId;
            private BadgeWorkerBase.<>c__DisplayClass31_0 <>8__1;
            private Locker <>7__wrap1;
            private TaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter awaiter2;
                    TaskAwaiter awaiter3;
                    switch (num)
                    {
                        case 0:
                            goto TR_0015;

                        case 1:
                            awaiter2 = this.<>u__1;
                            this.<>u__1 = new TaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_000A;

                        case 2:
                            awaiter3 = this.<>u__1;
                            this.<>u__1 = new TaskAwaiter();
                            this.<>1__state = num = -1;
                            break;

                        default:
                            this.<>8__1 = new BadgeWorkerBase.<>c__DisplayClass31_0();
                            this.<>8__1.<>4__this = this.<>4__this;
                            this.<>8__1.duration = this.duration;
                            if (!this.<>4__this.IsVisible && (this.operationId == this.<>4__this.showHideOperationId))
                            {
                                this.<>4__this.cachedDuration = this.<>8__1.duration;
                                if (this.<>4__this.CalculateIsVisible())
                                {
                                    this.<>7__wrap1 = this.<>4__this.animationLocker.Lock();
                                    goto TR_0015;
                                }
                            }
                            goto TR_0002;
                    }
                TR_0009:
                    awaiter3.GetResult();
                    awaiter3 = new TaskAwaiter();
                    goto TR_0002;
                TR_000A:
                    awaiter2.GetResult();
                    awaiter2 = new TaskAwaiter();
                    awaiter3 = Task.Delay(this.<>8__1.duration).GetAwaiter();
                    if (awaiter3.IsCompleted)
                    {
                        goto TR_0009;
                    }
                    else
                    {
                        this.<>1__state = num = 2;
                        this.<>u__1 = awaiter3;
                        this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, BadgeWorkerBase.<ShowAsync>d__31>(ref awaiter3, ref this);
                    }
                    return;
                TR_000E:
                    this.<>7__wrap1 = null;
                    if (this.<>4__this.BadgeControl != null)
                    {
                        awaiter2 = this.<>4__this.Dispatcher.InvokeAsync(new Action(this.<>8__1.<ShowAsync>b__1)).GetAwaiter();
                        if (!awaiter2.IsCompleted)
                        {
                            this.<>1__state = num = 1;
                            this.<>u__1 = awaiter2;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, BadgeWorkerBase.<ShowAsync>d__31>(ref awaiter2, ref this);
                            return;
                        }
                        goto TR_000A;
                    }
                    goto TR_0002;
                TR_0015:
                    try
                    {
                        TaskAwaiter awaiter;
                        if (num == 0)
                        {
                            awaiter = this.<>u__1;
                            this.<>u__1 = new TaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_0010;
                        }
                        else
                        {
                            awaiter = this.<>4__this.Dispatcher.InvokeAsync(new Action(this.<>4__this.<ShowAsync>b__31_0)).GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto TR_0010;
                            }
                            else
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, BadgeWorkerBase.<ShowAsync>d__31>(ref awaiter, ref this);
                            }
                        }
                        return;
                    TR_0010:
                        awaiter.GetResult();
                        awaiter = new TaskAwaiter();
                        goto TR_000E;
                    }
                    finally
                    {
                        if ((num < 0) && (this.<>7__wrap1 != null))
                        {
                            ((IDisposable) this.<>7__wrap1).Dispose();
                        }
                    }
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
                return;
            TR_0002:
                this.<>1__state = -2;
                this.<>t__builder.SetResult();
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <ShowHide>d__33 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncVoidMethodBuilder <>t__builder;
            public BadgeWorkerBase <>4__this;
            public int hideDuration;
            public int showDuration;
            private bool <newIsVisible>5__1;
            private int <operationId>5__2;
            private TaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter awaiter;
                    TaskAwaiter awaiter2;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new TaskAwaiter();
                        this.<>1__state = num = -1;
                        goto TR_0009;
                    }
                    else
                    {
                        if (num == 1)
                        {
                            awaiter2 = this.<>u__1;
                            this.<>u__1 = new TaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_0005;
                        }
                        else
                        {
                            int num2 = this.<>4__this.showHideOperationId + 1;
                            this.<>4__this.showHideOperationId = num2;
                            this.<operationId>5__2 = num2;
                            bool isVisible = this.<>4__this.IsVisible;
                            this.<newIsVisible>5__1 = this.<>4__this.CalculateIsVisible();
                            if (isVisible & this.<newIsVisible>5__1)
                            {
                                this.hideDuration = 0;
                                this.showDuration = 0;
                            }
                            if (!isVisible)
                            {
                                goto TR_0008;
                            }
                            else
                            {
                                awaiter = this.<>4__this.HideAsync(this.hideDuration, this.<operationId>5__2).GetAwaiter();
                                if (awaiter.IsCompleted)
                                {
                                    goto TR_0009;
                                }
                                else
                                {
                                    this.<>1__state = num = 0;
                                    this.<>u__1 = awaiter;
                                    this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, BadgeWorkerBase.<ShowHide>d__33>(ref awaiter, ref this);
                                }
                            }
                            return;
                        }
                        goto TR_0009;
                    }
                    goto TR_0008;
                TR_0005:
                    awaiter2.GetResult();
                    awaiter2 = new TaskAwaiter();
                    goto TR_0004;
                TR_0008:
                    if (!this.<newIsVisible>5__1)
                    {
                        goto TR_0004;
                    }
                    else
                    {
                        awaiter2 = this.<>4__this.ShowAsync(this.showDuration, this.<operationId>5__2).GetAwaiter();
                        if (awaiter2.IsCompleted)
                        {
                            goto TR_0005;
                        }
                        else
                        {
                            this.<>1__state = num = 1;
                            this.<>u__1 = awaiter2;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, BadgeWorkerBase.<ShowHide>d__33>(ref awaiter2, ref this);
                        }
                    }
                    return;
                TR_0009:
                    awaiter.GetResult();
                    awaiter = new TaskAwaiter();
                    goto TR_0008;
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
                return;
            TR_0004:
                this.<>1__state = -2;
                this.<>t__builder.SetResult();
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }
    }
}

