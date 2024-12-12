namespace DevExpress.Xpf.DocumentViewer
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows.Input;

    public class CommandSetZoomFactorAndModeItem : CommandToggleButton
    {
        private bool isSeparator;
        private double zoomFactor;
        private DevExpress.Xpf.DocumentViewer.ZoomMode zoomMode;
        private System.Windows.Input.KeyGesture keyGesture;

        public bool IsSeparator
        {
            get => 
                this.isSeparator;
            set => 
                base.SetProperty<bool>(ref this.isSeparator, value, Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(CommandSetZoomFactorAndModeItem)), (MethodInfo) methodof(CommandSetZoomFactorAndModeItem.get_IsSeparator)), new ParameterExpression[0]));
        }

        public double ZoomFactor
        {
            get => 
                this.zoomFactor;
            set => 
                base.SetProperty<double>(ref this.zoomFactor, value, Expression.Lambda<Func<double>>(Expression.Property(Expression.Constant(this, typeof(CommandSetZoomFactorAndModeItem)), (MethodInfo) methodof(CommandSetZoomFactorAndModeItem.get_ZoomFactor)), new ParameterExpression[0]));
        }

        public DevExpress.Xpf.DocumentViewer.ZoomMode ZoomMode
        {
            get => 
                this.zoomMode;
            set => 
                base.SetProperty<DevExpress.Xpf.DocumentViewer.ZoomMode>(ref this.zoomMode, value, Expression.Lambda<Func<DevExpress.Xpf.DocumentViewer.ZoomMode>>(Expression.Property(Expression.Constant(this, typeof(CommandSetZoomFactorAndModeItem)), (MethodInfo) methodof(CommandSetZoomFactorAndModeItem.get_ZoomMode)), new ParameterExpression[0]));
        }

        public System.Windows.Input.KeyGesture KeyGesture
        {
            get => 
                this.keyGesture;
            set => 
                base.SetProperty<System.Windows.Input.KeyGesture>(ref this.keyGesture, value, Expression.Lambda<Func<System.Windows.Input.KeyGesture>>(Expression.Property(Expression.Constant(this, typeof(CommandSetZoomFactorAndModeItem)), (MethodInfo) methodof(CommandSetZoomFactorAndModeItem.get_KeyGesture)), new ParameterExpression[0]));
        }
    }
}

