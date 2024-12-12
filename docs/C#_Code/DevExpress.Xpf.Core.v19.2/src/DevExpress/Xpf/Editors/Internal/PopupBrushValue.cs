namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class PopupBrushValue : BindableBase
    {
        private object brush;
        private DevExpress.Xpf.Editors.BrushType brushType;
        private readonly Locker invalidateBrushLocker = new Locker();

        public event EventHandler BrushTypeChanged;

        protected virtual void InvalidateBrush()
        {
        }

        protected void InvalidateBrushInternal()
        {
            this.invalidateBrushLocker.DoLockedActionIfNotLocked(new Action(this.InvalidateBrush));
        }

        protected virtual void InvalidateParams()
        {
        }

        protected void InvalidateParamsInternal()
        {
            this.invalidateBrushLocker.DoLockedActionIfNotLocked(new Action(this.InvalidateParams));
        }

        private void RaiseBrushTypeChanged()
        {
            if (this.BrushTypeChanged != null)
            {
                this.BrushTypeChanged(this, EventArgs.Empty);
            }
        }

        public DevExpress.Xpf.Editors.BrushType BrushType
        {
            get => 
                this.brushType;
            set => 
                base.SetProperty<DevExpress.Xpf.Editors.BrushType>(ref this.brushType, value, Expression.Lambda<Func<DevExpress.Xpf.Editors.BrushType>>(Expression.Property(Expression.Constant(this, typeof(PopupBrushValue)), (MethodInfo) methodof(PopupBrushValue.get_BrushType)), new ParameterExpression[0]), new Action(this.RaiseBrushTypeChanged));
        }

        [Browsable(false)]
        public object Brush
        {
            get => 
                this.brush;
            set => 
                base.SetProperty<object>(ref this.brush, value, Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(PopupBrushValue)), (MethodInfo) methodof(PopupBrushValue.get_Brush)), new ParameterExpression[0]), new Action(this.InvalidateParamsInternal));
        }
    }
}

