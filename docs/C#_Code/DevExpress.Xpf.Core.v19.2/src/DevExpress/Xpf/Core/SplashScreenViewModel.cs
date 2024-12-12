namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class SplashScreenViewModel : ViewModelBase, ISupportSplashScreen
    {
        public const double ProgressDefaultValue = 0.0;
        public const double MaxProgressDefaultValue = 100.0;
        public const string StateDefaultValue = "Loading...";
        private static SplashScreenViewModel designTimeData;
        private bool allowDisableMarquee = false;

        public SplashScreenViewModel()
        {
            this.IsIndeterminate = true;
            this.MaxProgress = 100.0;
            this.Progress = 0.0;
            this.State = "Loading...";
            this.allowDisableMarquee = true;
        }

        public SplashScreenViewModel Clone()
        {
            SplashScreenViewModel model = new SplashScreenViewModel {
                allowDisableMarquee = false,
                MaxProgress = this.MaxProgress,
                Progress = this.Progress,
                State = this.State,
                IsIndeterminate = this.IsIndeterminate
            };
            model.allowDisableMarquee = true;
            return model;
        }

        private void DisableMarquee()
        {
            if (this.allowDisableMarquee)
            {
                this.IsIndeterminate = false;
            }
        }

        public static SplashScreenViewModel DesignTimeData =>
            designTimeData ??= new SplashScreenViewModel();

        public bool IsIndeterminate
        {
            get => 
                base.GetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(SplashScreenViewModel)), (MethodInfo) methodof(SplashScreenViewModel.get_IsIndeterminate)), new ParameterExpression[0]));
            set => 
                base.SetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(SplashScreenViewModel)), (MethodInfo) methodof(SplashScreenViewModel.get_IsIndeterminate)), new ParameterExpression[0]), value);
        }

        public double MaxProgress
        {
            get => 
                base.GetProperty<double>(Expression.Lambda<Func<double>>(Expression.Property(Expression.Constant(this, typeof(SplashScreenViewModel)), (MethodInfo) methodof(SplashScreenViewModel.get_MaxProgress)), new ParameterExpression[0]));
            set => 
                base.SetProperty<double>(Expression.Lambda<Func<double>>(Expression.Property(Expression.Constant(this, typeof(SplashScreenViewModel)), (MethodInfo) methodof(SplashScreenViewModel.get_MaxProgress)), new ParameterExpression[0]), value, new Action(this.DisableMarquee));
        }

        public double Progress
        {
            get => 
                base.GetProperty<double>(Expression.Lambda<Func<double>>(Expression.Property(Expression.Constant(this, typeof(SplashScreenViewModel)), (MethodInfo) methodof(SplashScreenViewModel.get_Progress)), new ParameterExpression[0]));
            set => 
                base.SetProperty<double>(Expression.Lambda<Func<double>>(Expression.Property(Expression.Constant(this, typeof(SplashScreenViewModel)), (MethodInfo) methodof(SplashScreenViewModel.get_Progress)), new ParameterExpression[0]), value, new Action(this.DisableMarquee));
        }

        public object State
        {
            get => 
                base.GetProperty<object>(Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(SplashScreenViewModel)), (MethodInfo) methodof(SplashScreenViewModel.get_State)), new ParameterExpression[0]));
            set => 
                base.SetProperty<object>(Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(SplashScreenViewModel)), (MethodInfo) methodof(SplashScreenViewModel.get_State)), new ParameterExpression[0]), value);
        }
    }
}

