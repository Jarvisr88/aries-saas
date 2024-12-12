namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;

    public class BehaviorProvider : BindableBase
    {
        private Locker updateZoomLocker = new Locker();
        private double zoomFactor = 1.0;
        private DevExpress.Xpf.DocumentViewer.ZoomMode zoomMode = DevExpress.Xpf.DocumentViewer.ZoomMode.ActualSize;
        private double rotateAngle;
        private int pageIndex;
        private Size viewport;
        private Size pageSize;
        private Size pageVisibleSize;

        public event EventHandler<PageIndexChangedEventArgs> PageIndexChanged;

        public event EventHandler<RotateAngleChangedEventArgs> RotateAngleChanged;

        public event EventHandler<ZoomChangedEventArgs> ZoomChanged;

        public BehaviorProvider()
        {
            this.ZoomHelper = this.CreateZoomHelper();
        }

        public bool CanZoomIn()
        {
            List<double> zoomFactors = this.GetZoomFactors();
            return (this.FindCurrentZoomIndex(zoomFactors) < (zoomFactors.Count - 1));
        }

        public bool CanZoomOut()
        {
            List<double> zoomFactors = this.GetZoomFactors();
            int num = this.FindCurrentZoomIndex(zoomFactors);
            return ((num <= (zoomFactors.Count - 1)) && (num > 0));
        }

        internal virtual DevExpress.Xpf.DocumentViewer.ZoomHelper CreateZoomHelper() => 
            new DevExpress.Xpf.DocumentViewer.ZoomHelper();

        private int FindCurrentZoomIndex(List<double> zoomFactors)
        {
            int num = zoomFactors.BinarySearch(this.ZoomFactor);
            return ((num >= 0) ? num : (-num - 1));
        }

        public double GetNextZoomFactor(bool isZoomIn)
        {
            List<double> zoomFactors = this.GetZoomFactors();
            return zoomFactors[Math.Min(Math.Max(this.FindCurrentZoomIndex(zoomFactors) + (isZoomIn ? 1 : -1), 0), zoomFactors.Count - 1)];
        }

        protected virtual List<double> GetZoomFactors()
        {
            if (this.ZoomHelper == null)
            {
                return new List<double>(this.DefaultZoomLevels);
            }
            List<double> source = new List<double>(this.DefaultZoomLevels);
            source.Add(this.ZoomHelper.CalcZoomFactor(DevExpress.Xpf.DocumentViewer.ZoomMode.FitToWidth));
            source.Add(this.ZoomHelper.CalcZoomFactor(DevExpress.Xpf.DocumentViewer.ZoomMode.PageLevel));
            List<double> list = source.Distinct<double>().ToList<double>();
            list.Sort();
            return list;
        }

        private DevExpress.Xpf.DocumentViewer.ZoomMode GetZoomModeByZoomFactor(double zoomFactor) => 
            !this.ZoomHelper.CalcZoomFactor(DevExpress.Xpf.DocumentViewer.ZoomMode.FitToWidth).AreClose(zoomFactor) ? (!this.ZoomHelper.CalcZoomFactor(DevExpress.Xpf.DocumentViewer.ZoomMode.FitToVisible).AreClose(zoomFactor) ? (!this.ZoomHelper.CalcZoomFactor(DevExpress.Xpf.DocumentViewer.ZoomMode.PageLevel).AreClose(zoomFactor) ? (!zoomFactor.AreClose(1.0) ? DevExpress.Xpf.DocumentViewer.ZoomMode.Custom : DevExpress.Xpf.DocumentViewer.ZoomMode.ActualSize) : DevExpress.Xpf.DocumentViewer.ZoomMode.PageLevel) : DevExpress.Xpf.DocumentViewer.ZoomMode.FitToVisible) : DevExpress.Xpf.DocumentViewer.ZoomMode.FitToWidth;

        protected virtual void OnPageIndexChanged()
        {
            this.RaisePageIndexChanged();
        }

        protected virtual void OnPageSizeChanged()
        {
            this.ZoomHelper.PageSize = this.PageSize;
            this.UpdateZoomFactor();
        }

        protected virtual void OnPageVisibleSizeChanged()
        {
            this.ZoomHelper.PageVisibleSize = this.PageVisibleSize;
            this.UpdateZoomFactor();
        }

        protected virtual void OnRotateAngleChanged(double oldValue, double newValue)
        {
            this.ZoomHelper.PageSize = this.PageSize;
            this.UpdateZoomFactor();
            this.RaiseRotateAngleChanged(oldValue, newValue);
        }

        protected virtual void OnViewportChanged()
        {
            this.ZoomHelper.Viewport = this.Viewport;
            this.UpdateZoomFactor();
        }

        protected virtual void OnZoomFactorChanged(double oldValue, double newValue)
        {
            DevExpress.Xpf.DocumentViewer.ZoomMode zoomMode = this.ZoomMode;
            this.UpdateZoomMode();
            this.RaiseZoomChanged(oldValue, newValue, zoomMode, this.ZoomMode);
        }

        protected virtual void OnZoomModeChanged()
        {
            this.UpdateZoomFactor();
        }

        private void RaisePageIndexChanged()
        {
            if (this.PageIndexChanged != null)
            {
                this.PageIndexChanged(this, new PageIndexChangedEventArgs(this.PageIndex));
            }
        }

        private void RaiseRotateAngleChanged(double oldValue, double newValue)
        {
            if (this.RotateAngleChanged != null)
            {
                this.RotateAngleChanged(this, new RotateAngleChangedEventArgs(oldValue, newValue));
            }
        }

        private void RaiseZoomChanged(double oldZoomFactor, double newZoomFactor, DevExpress.Xpf.DocumentViewer.ZoomMode oldZoomMode, DevExpress.Xpf.DocumentViewer.ZoomMode newZoomMode)
        {
            if (this.ZoomChanged != null)
            {
                this.ZoomChanged(this, new ZoomChangedEventArgs(oldZoomFactor, newZoomFactor, oldZoomMode, newZoomMode));
            }
        }

        protected void UpdateZoomFactor()
        {
            this.updateZoomLocker.DoLockedActionIfNotLocked(() => this.ZoomFactor = this.ZoomHelper.CalcZoomFactor(this.ZoomMode, this.ZoomFactor));
        }

        protected void UpdateZoomMode()
        {
            this.updateZoomLocker.DoLockedActionIfNotLocked(() => this.ZoomMode = this.GetZoomModeByZoomFactor(this.ZoomFactor));
        }

        private void Zoom(bool isZoomIn)
        {
            double nextZoomFactor = this.GetNextZoomFactor(isZoomIn);
            this.ZoomFactor = nextZoomFactor;
        }

        public void ZoomIn()
        {
            this.Zoom(true);
        }

        public void ZoomOut()
        {
            this.Zoom(false);
        }

        protected virtual List<double> DefaultZoomLevels
        {
            get
            {
                List<double> list1 = new List<double>();
                list1.Add(0.1);
                list1.Add(0.25);
                list1.Add(0.5);
                list1.Add(0.75);
                list1.Add(1.0);
                list1.Add(1.25);
                list1.Add(1.5);
                list1.Add(2.0);
                list1.Add(4.0);
                list1.Add(5.0);
                return list1;
            }
        }

        private DevExpress.Xpf.DocumentViewer.ZoomHelper ZoomHelper { get; set; }

        public DevExpress.Xpf.DocumentViewer.ZoomMode ZoomMode
        {
            get => 
                this.zoomMode;
            set => 
                base.SetProperty<DevExpress.Xpf.DocumentViewer.ZoomMode>(ref this.zoomMode, value, System.Linq.Expressions.Expression.Lambda<Func<DevExpress.Xpf.DocumentViewer.ZoomMode>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(BehaviorProvider)), (MethodInfo) methodof(BehaviorProvider.get_ZoomMode)), new ParameterExpression[0]), new Action(this.OnZoomModeChanged));
        }

        public double ZoomFactor
        {
            get => 
                this.zoomFactor;
            set
            {
                double oldValue = this.zoomFactor;
                double newValue = Math.Min(Math.Max(value, ((IEnumerable<double>) this.DefaultZoomLevels).Min()), ((IEnumerable<double>) this.DefaultZoomLevels).Max());
                base.SetProperty<double>(ref this.zoomFactor, newValue, System.Linq.Expressions.Expression.Lambda<Func<double>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(BehaviorProvider)), (MethodInfo) methodof(BehaviorProvider.get_ZoomFactor)), new ParameterExpression[0]), () => this.OnZoomFactorChanged(oldValue, newValue));
            }
        }

        public Size Viewport
        {
            get => 
                this.viewport;
            set => 
                base.SetProperty<Size>(ref this.viewport, value, System.Linq.Expressions.Expression.Lambda<Func<Size>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(BehaviorProvider)), (MethodInfo) methodof(BehaviorProvider.get_Viewport)), new ParameterExpression[0]), new Action(this.OnViewportChanged));
        }

        public Size PageSize
        {
            get => 
                this.pageSize;
            set => 
                base.SetProperty<Size>(ref this.pageSize, value, System.Linq.Expressions.Expression.Lambda<Func<Size>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(BehaviorProvider)), (MethodInfo) methodof(BehaviorProvider.get_PageSize)), new ParameterExpression[0]), new Action(this.OnPageSizeChanged));
        }

        public Size PageVisibleSize
        {
            get => 
                this.pageVisibleSize;
            set => 
                base.SetProperty<Size>(ref this.pageVisibleSize, value, System.Linq.Expressions.Expression.Lambda<Func<Size>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(BehaviorProvider)), (MethodInfo) methodof(BehaviorProvider.get_PageVisibleSize)), new ParameterExpression[0]), new Action(this.OnPageVisibleSizeChanged));
        }

        public double RotateAngle
        {
            get => 
                this.rotateAngle;
            set
            {
                double oldValue = this.rotateAngle;
                base.SetProperty<double>(ref this.rotateAngle, value, System.Linq.Expressions.Expression.Lambda<Func<double>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(BehaviorProvider)), (MethodInfo) methodof(BehaviorProvider.get_RotateAngle)), new ParameterExpression[0]), () => this.OnRotateAngleChanged(oldValue, value));
            }
        }

        public int PageIndex
        {
            get => 
                this.pageIndex;
            set => 
                base.SetProperty<int>(ref this.pageIndex, value, System.Linq.Expressions.Expression.Lambda<Func<int>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(BehaviorProvider)), (MethodInfo) methodof(BehaviorProvider.get_PageIndex)), new ParameterExpression[0]), new Action(this.OnPageIndexChanged));
        }
    }
}

