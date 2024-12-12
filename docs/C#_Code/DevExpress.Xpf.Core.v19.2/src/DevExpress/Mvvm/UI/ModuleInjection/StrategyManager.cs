namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using DevExpress.Mvvm.ModuleInjection;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class StrategyManager : IStrategyManager
    {
        private static IStrategyManager _default;
        private static StrategyManager _instance = new StrategyManager();
        private readonly Dictionary<Type, Type> Strategies = new Dictionary<Type, Type>();
        private readonly Dictionary<Type, Type> WindowStrategies = new Dictionary<Type, Type>();

        static StrategyManager()
        {
            _instance.RegisterDefaultStrategies();
        }

        public IStrategy CreateStrategy(DependencyObject target) => 
            CreateStrategy<IStrategy>(this.Strategies, target);

        private static TStrategy CreateStrategy<TStrategy>(Dictionary<Type, Type> strategies, DependencyObject target)
        {
            if (target == null)
            {
                return default(TStrategy);
            }
            Type type = target.GetType();
            Type type2 = null;
            while (true)
            {
                foreach (Type type3 in strategies.Keys)
                {
                    if (type3 == type)
                    {
                        type2 = strategies[type3];
                        break;
                    }
                }
                if ((type.BaseType == null) || (type2 != null))
                {
                    if (type2 == null)
                    {
                        ModuleInjectionException.NoStrategy(target.GetType());
                    }
                    return (TStrategy) Activator.CreateInstance(type2, (object[]) null);
                }
            }
        }

        public IWindowStrategy CreateWindowStrategy(DependencyObject target) => 
            CreateStrategy<IWindowStrategy>(this.WindowStrategies, target);

        public void RegisterDefaultStrategies()
        {
            this.RegisterStrategy<Panel, PanelStrategy<Panel, PanelWrapper>>();
            this.RegisterStrategy<ContentPresenter, ContentPresenterStrategy<ContentPresenter, ContentPresenterWrapper>>();
            this.RegisterStrategy<ContentControl, ContentPresenterStrategy<ContentControl, ContentControlWrapper>>();
            this.RegisterStrategy<ItemsControl, ItemsControlStrategy<ItemsControl, ItemsControlWrapper>>();
            this.RegisterStrategy<Selector, SelectorStrategy<Selector, SelectorWrapper>>();
            this.RegisterStrategy<TabControl, SelectorStrategy<TabControl, TabControlWrapper>>();
            this.RegisterWindowStrategy<Window, WindowStrategy<Window, WindowWrapper>>();
            this.RegisterWindowStrategy<DXDialogWindow, WindowStrategy<DXDialogWindow, DXDialogWindowWrapper>>();
            this.RegisterWindowStrategy<ThemedWindow, WindowStrategy<ThemedWindow, ThemedWindowWrapper>>();
        }

        public void RegisterStrategy<TTarget, TStrategy>() where TTarget: DependencyObject where TStrategy: IStrategy, new()
        {
            RegisterStrategy<TTarget, TStrategy>(this.Strategies);
        }

        private static void RegisterStrategy<TTarget, TStrategy>(Dictionary<Type, Type> strategies) where TStrategy: new()
        {
            Type key = typeof(TTarget);
            Type type2 = typeof(TStrategy);
            RuntimeHelpers.RunClassConstructor(key.TypeHandle);
            if (strategies.ContainsKey(key))
            {
                strategies[key] = type2;
            }
            else
            {
                strategies.Add(key, type2);
            }
        }

        public void RegisterWindowStrategy<TTarget, TStrategy>() where TTarget: DependencyObject where TStrategy: IWindowStrategy, new()
        {
            RegisterStrategy<TTarget, TStrategy>(this.WindowStrategies);
        }

        public static IStrategyManager Default
        {
            get => 
                _default ?? _instance;
            set => 
                _default = value;
        }
    }
}

