namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class CameraPropertyProvider : ViewModelBase
    {
        public CameraPropertyProvider()
        {
            this.ErrorCaption = EditorLocalizer.GetString(EditorStringId.CameraErrorCaption);
            this.RefreshButtonCaption = EditorLocalizer.GetString(EditorStringId.CameraRefreshButtonCaption);
            this.NoDevicesErrorCaption = EditorLocalizer.GetString(EditorStringId.CameraNoDevicesErrorCaption);
        }

        private void UpdateSettingsButtonVisiblity()
        {
            this.IsSettingsButtonVisible = (this.ShowSettingsButton && !this.IsBusy) && this.HasDevices;
        }

        public string ErrorCaption
        {
            get => 
                base.GetProperty<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(CameraPropertyProvider)), (MethodInfo) methodof(CameraPropertyProvider.get_ErrorCaption)), new ParameterExpression[0]));
            set => 
                base.SetProperty<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(CameraPropertyProvider)), (MethodInfo) methodof(CameraPropertyProvider.get_ErrorCaption)), new ParameterExpression[0]), value);
        }

        public string NoDevicesErrorCaption
        {
            get => 
                base.GetProperty<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(CameraPropertyProvider)), (MethodInfo) methodof(CameraPropertyProvider.get_NoDevicesErrorCaption)), new ParameterExpression[0]));
            set => 
                base.SetProperty<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(CameraPropertyProvider)), (MethodInfo) methodof(CameraPropertyProvider.get_NoDevicesErrorCaption)), new ParameterExpression[0]), value);
        }

        public bool ShowSettingsButton
        {
            get => 
                base.GetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(CameraPropertyProvider)), (MethodInfo) methodof(CameraPropertyProvider.get_ShowSettingsButton)), new ParameterExpression[0]));
            set
            {
                base.SetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(CameraPropertyProvider)), (MethodInfo) methodof(CameraPropertyProvider.get_ShowSettingsButton)), new ParameterExpression[0]), value);
                this.UpdateSettingsButtonVisiblity();
            }
        }

        public bool IsSettingsButtonVisible
        {
            get => 
                base.GetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(CameraPropertyProvider)), (MethodInfo) methodof(CameraPropertyProvider.get_IsSettingsButtonVisible)), new ParameterExpression[0]));
            set => 
                base.SetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(CameraPropertyProvider)), (MethodInfo) methodof(CameraPropertyProvider.get_IsSettingsButtonVisible)), new ParameterExpression[0]), value);
        }

        public bool IsBusy
        {
            get => 
                base.GetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(CameraPropertyProvider)), (MethodInfo) methodof(CameraPropertyProvider.get_IsBusy)), new ParameterExpression[0]));
            set
            {
                base.SetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(CameraPropertyProvider)), (MethodInfo) methodof(CameraPropertyProvider.get_IsBusy)), new ParameterExpression[0]), value);
                this.UpdateSettingsButtonVisiblity();
            }
        }

        public bool HasDevices
        {
            get => 
                base.GetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(CameraPropertyProvider)), (MethodInfo) methodof(CameraPropertyProvider.get_HasDevices)), new ParameterExpression[0]));
            set
            {
                base.SetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(CameraPropertyProvider)), (MethodInfo) methodof(CameraPropertyProvider.get_HasDevices)), new ParameterExpression[0]), value);
                this.UpdateSettingsButtonVisiblity();
            }
        }

        public string RefreshButtonCaption
        {
            get => 
                base.GetProperty<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(CameraPropertyProvider)), (MethodInfo) methodof(CameraPropertyProvider.get_RefreshButtonCaption)), new ParameterExpression[0]));
            set => 
                base.SetProperty<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(CameraPropertyProvider)), (MethodInfo) methodof(CameraPropertyProvider.get_RefreshButtonCaption)), new ParameterExpression[0]), value);
        }

        public CameraSettingsProvider Settings
        {
            get => 
                base.GetProperty<CameraSettingsProvider>(Expression.Lambda<Func<CameraSettingsProvider>>(Expression.Property(Expression.Constant(this, typeof(CameraPropertyProvider)), (MethodInfo) methodof(CameraPropertyProvider.get_Settings)), new ParameterExpression[0]));
            set => 
                base.SetProperty<CameraSettingsProvider>(Expression.Lambda<Func<CameraSettingsProvider>>(Expression.Property(Expression.Constant(this, typeof(CameraPropertyProvider)), (MethodInfo) methodof(CameraPropertyProvider.get_Settings)), new ParameterExpression[0]), value);
        }

        public ICommand RefreshCommand { get; set; }
    }
}

