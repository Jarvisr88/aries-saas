namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using DevExpress.Mvvm.UI.Interactivity;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class UIRegion : UIRegionBase
    {
        public static readonly DependencyProperty RegionProperty;
        private IStrategy strategy;

        static UIRegion()
        {
            RegionProperty = DependencyProperty.RegisterAttached("Region", typeof(string), typeof(UIRegion), new PropertyMetadata(null, (d, e) => OnRegionChanged(d, (string) e.OldValue, (string) e.NewValue)));
        }

        protected override void DoClear()
        {
            this.Strategy.Clear();
        }

        protected override void DoInject(object vm, Type viewType)
        {
            this.Strategy.Inject(vm, viewType);
        }

        protected override void DoUninject(object vm)
        {
            this.Strategy.Remove(vm);
        }

        public static string GetRegion(DependencyObject obj) => 
            (string) obj.GetValue(RegionProperty);

        protected override object GetView(object viewModel) => 
            this.Strategy.GetView(viewModel);

        protected override void OnAttached()
        {
            base.OnAttached();
            base.Target.Loaded += new RoutedEventHandler(this.OnLoaded);
            if (base.Target.IsLoaded)
            {
                this.OnLoaded(base.Target.Object, EventArgs.Empty);
            }
        }

        protected override void OnDetaching()
        {
            base.Target.Loaded -= new RoutedEventHandler(this.OnLoaded);
            base.OnDetaching();
        }

        protected override void OnInitialized()
        {
            this.Strategy.Initialize(new StrategyOwner(this));
            base.OnInitialized();
            this.OnSelectedViewModelChanged(base.SelectedViewModel, base.SelectedViewModel, false);
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            if (base.SelectedViewModel != null)
            {
                this.OnSelectedViewModelChanged(base.SelectedViewModel, base.SelectedViewModel, false);
            }
            else
            {
                base.SelectedViewModel = this.Strategy.SelectedViewModel;
            }
        }

        private static void OnRegionChanged(DependencyObject obj, string oldValue, string newValue)
        {
            BehaviorCollection source = Interaction.GetBehaviors(obj);
            UIRegion region = source.OfType<UIRegion>().FirstOrDefault<UIRegion>();
            if (region != null)
            {
                source.Remove(region);
            }
            if (!string.IsNullOrEmpty(newValue))
            {
                UIRegion region1 = new UIRegion();
                region1.RegionName = newValue;
                source.Add(region1);
            }
        }

        protected override void OnSelectedViewModelChanged(object oldValue, object newValue, bool focus)
        {
            if (this.Strategy.IsInitialized && base.Target.IsLoaded)
            {
                this.Strategy.Select(newValue, focus);
            }
        }

        protected override void OnUninitializing()
        {
            this.Strategy.Uninitialize();
            base.OnUninitializing();
        }

        public static void SetRegion(DependencyObject obj, string value)
        {
            obj.SetValue(RegionProperty, value);
        }

        protected override object ActualParentViewModel =>
            base.ParentViewModel ?? this.Strategy.GetParentViewModel();

        private IStrategy Strategy
        {
            get
            {
                IStrategy strategy = this.strategy;
                if (this.strategy == null)
                {
                    IStrategy local1 = this.strategy;
                    strategy = this.strategy = base.ActualStrategyManager.CreateStrategy(base.AssociatedObject);
                }
                return strategy;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly UIRegion.<>c <>9 = new UIRegion.<>c();

            internal void <.cctor>b__4_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                UIRegion.OnRegionChanged(d, (string) e.OldValue, (string) e.NewValue);
            }
        }

        private class StrategyOwner : UIRegionBase.StrategyOwnerBase
        {
            public StrategyOwner(UIRegion owner) : base(owner, owner.AssociatedObject)
            {
            }

            public override void SelectViewModel(object viewModel)
            {
                if ((this.Owner.Target != null) && this.Owner.Target.IsLoaded)
                {
                    base.SelectViewModel(viewModel);
                }
            }

            protected UIRegion Owner =>
                (UIRegion) base.Owner;
        }
    }
}

