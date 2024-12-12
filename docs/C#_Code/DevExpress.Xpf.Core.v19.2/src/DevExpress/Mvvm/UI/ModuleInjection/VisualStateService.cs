namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.ModuleInjection;
    using DevExpress.Mvvm.ModuleInjection.Native;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core.Serialization;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class VisualStateService : ServiceBaseGeneric<DependencyObject>, IVisualStateService, IVisualStateServiceImplementation
    {
        public static readonly DependencyProperty AutoSaveStateProperty;
        public static readonly DependencyProperty AutoRestoreStateProperty;
        private bool isLoaded;

        static VisualStateService()
        {
            DXSerializer.EnabledProperty.OverrideMetadata(typeof(VisualStateService), new UIPropertyMetadata(false));
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(VisualStateService), "x");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<VisualStateService> registrator1 = DependencyPropertyRegistrator<VisualStateService>.New().Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<VisualStateService, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(VisualStateService.get_AutoSaveState)), parameters), out AutoSaveStateProperty, true, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(VisualStateService), "x");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator1.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<VisualStateService, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(VisualStateService.get_AutoRestoreState)), expressionArray2), out AutoRestoreStateProperty, true, frameworkOptions);
        }

        private void CheckId()
        {
            object viewModel = this.GetViewModel();
            if (viewModel != null)
            {
                VisualStateServiceHelper.CheckServices(viewModel, true, true);
            }
        }

        void IVisualStateServiceImplementation.EnforceSaveState()
        {
            if (this.isLoaded && this.AutoSaveState)
            {
                this.SaveState(this.GetCurrentState());
            }
        }

        public string GetCurrentState()
        {
            Window window = Window.GetWindow(base.AssociatedObject);
            if ((window != null) && !window.IsLoaded)
            {
                return null;
            }
            string layoutVersion = DXSerializer.GetLayoutVersion(this.Target.Object);
            return SerializationHelper.SerializeToString(delegate (Stream x) {
                DXOptionsLayout options = new DXOptionsLayout();
                options.LayoutVersion = layoutVersion;
                options.AcceptNestedObjects = AcceptNestedObjects.IgnoreChildrenOfDisabledObjects;
                DXSerializer.Serialize(this.Target.Object, x, string.Empty, options);
            });
        }

        private IRegionImplementation GetRegion()
        {
            UIRegionBase inheritedService = UIRegionBase.GetInheritedService(this.Target.Object);
            return inheritedService?.ActualModuleManager.GetRegionImplementation(inheritedService.RegionName);
        }

        public string GetSavedState()
        {
            string str;
            IRegionImplementation region = this.GetRegion();
            object viewModel = this.GetViewModel();
            if ((region == null) || (viewModel == null))
            {
                return null;
            }
            region.GetSavedVisualState(viewModel, base.Name, out str);
            return str;
        }

        private object GetViewModel()
        {
            UIRegionBase service = UIRegionBase.GetInheritedService(this.Target.Object);
            if (service == null)
            {
                return null;
            }
            Func<DependencyObject, object> selector = <>c.<>9__33_0;
            if (<>c.<>9__33_0 == null)
            {
                Func<DependencyObject, object> local1 = <>c.<>9__33_0;
                selector = <>c.<>9__33_0 = x => new DOTargetWrapper(x).DataContext;
            }
            return LayoutTreeHelper.GetVisualParents(this.Target.Object, null).Select<DependencyObject, object>(selector).FirstOrDefault<object>(x => ((IUIRegion) service).ViewModels.Contains<object>(x));
        }

        private void InitDefaultState()
        {
            if (string.IsNullOrEmpty(this.DefaultState))
            {
                this.DefaultState = this.GetCurrentState();
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.Target = new DOTargetWrapper(base.AssociatedObject);
            if (this.Target.IsNull)
            {
                ModuleInjectionException.CannotAttach();
            }
            DXSerializer.SetEnabled(base.AssociatedObject, false);
            this.Target.Initialized += new EventHandler(this.OnInitialized);
            this.Target.Loaded += new RoutedEventHandler(this.OnLoaded);
            this.Target.Unloaded += new RoutedEventHandler(this.OnUnloaded);
            if (this.Target.IsInitialized)
            {
                this.OnInitialized(this.Target.Object, null);
            }
            if (this.Target.IsLoaded)
            {
                this.OnLoaded(this.Target.Object, null);
            }
            this.CheckId();
        }

        protected override void OnDetaching()
        {
            this.Target.Initialized -= new EventHandler(this.OnInitialized);
            this.Target.Loaded -= new RoutedEventHandler(this.OnLoaded);
            this.Target.Unloaded -= new RoutedEventHandler(this.OnUnloaded);
            this.Target = null;
            base.OnDetaching();
        }

        private void OnInitialized(object sender, EventArgs e)
        {
            this.InitDefaultState();
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            if (!this.isLoaded)
            {
                this.isLoaded = true;
                base.GetServicesClient().Do<ISupportServices>(x => x.ServiceContainer.RegisterService(base.Name, this, base.YieldToParent));
                this.CheckId();
                this.InitDefaultState();
                if (this.AutoRestoreState)
                {
                    this.RestoreState(this.GetSavedState());
                }
            }
        }

        protected override void OnServicesClientChanged(ISupportServices oldServiceClient, ISupportServices newServiceClient)
        {
            if (this.isLoaded)
            {
                base.OnServicesClientChanged(oldServiceClient, newServiceClient);
            }
        }

        private void OnUnloaded(object sender, EventArgs e)
        {
            if (this.isLoaded)
            {
                if (this.AutoSaveState)
                {
                    this.SaveState(this.GetCurrentState());
                }
                this.isLoaded = false;
                base.GetServicesClient().Do<ISupportServices>(x => x.ServiceContainer.UnregisterService(this));
            }
        }

        public void RestoreState(string state)
        {
            if (!string.IsNullOrEmpty(state))
            {
                SerializationHelper.DeserializeFromString(state, delegate (Stream x) {
                    DXOptionsLayout options = new DXOptionsLayout();
                    options.AcceptNestedObjects = AcceptNestedObjects.IgnoreChildrenOfDisabledObjects;
                    DXSerializer.Deserialize(this.Target.Object, x, string.Empty, options);
                });
            }
        }

        public void SaveState(string state)
        {
            IRegionImplementation region = this.GetRegion();
            object viewModel = this.GetViewModel();
            if (!string.IsNullOrEmpty(state) && ((region != null) && (viewModel != null)))
            {
                region.SaveVisualState(viewModel, base.Name, state);
            }
        }

        public bool AutoSaveState
        {
            get => 
                (bool) base.GetValue(AutoSaveStateProperty);
            set => 
                base.SetValue(AutoSaveStateProperty, value);
        }

        public bool AutoRestoreState
        {
            get => 
                (bool) base.GetValue(AutoRestoreStateProperty);
            set => 
                base.SetValue(AutoRestoreStateProperty, value);
        }

        public string DefaultState { get; private set; }

        protected DOTargetWrapper Target { get; private set; }

        string IVisualStateServiceImplementation.Id =>
            base.Name;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly VisualStateService.<>c <>9 = new VisualStateService.<>c();
            public static Func<DependencyObject, object> <>9__33_0;

            internal object <GetViewModel>b__33_0(DependencyObject x) => 
                new DOTargetWrapper(x).DataContext;
        }
    }
}

