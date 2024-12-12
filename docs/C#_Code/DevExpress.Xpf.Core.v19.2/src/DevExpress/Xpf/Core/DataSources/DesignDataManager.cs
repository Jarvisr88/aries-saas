namespace DevExpress.Xpf.Core.DataSources
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Data;

    public class DesignDataManager : DependencyObject
    {
        public static readonly DependencyProperty DesignDataProperty;
        private static readonly Dictionary<Type, IDesignDataUpdater> updaters = new Dictionary<Type, IDesignDataUpdater>();

        static DesignDataManager()
        {
            Type ownerType = typeof(DesignDataManager);
            DesignDataProperty = DependencyPropertyManager.RegisterAttached("DesignData", typeof(IDesignDataSettings), ownerType, new FrameworkPropertyMetadata(new PropertyChangedCallback(DesignDataManager.OnDesignDataChanged)));
            updaters[typeof(CollectionViewSource)] = new CollectionViewSourceDesignDataManager();
            RegisterDomainDataSourceUpdater();
        }

        public static IDesignDataSettings GetDesignData(DependencyObject sender)
        {
            if (sender == null)
            {
                throw new ArgumentNullException("sender");
            }
            return (IDesignDataSettings) sender.GetValue(DesignDataProperty);
        }

        private static void OnDesignDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(d))
            {
                Type key = d.GetType();
                if (updaters.ContainsKey(key))
                {
                    updaters[key].UpdateDesignData(d);
                }
            }
        }

        private static void RegisterDomainDataSourceUpdater()
        {
        }

        public static void RegisterUpdater(Type type, IDesignDataUpdater updater)
        {
            updaters[type] = updater;
        }

        public static void SetDesignData(DependencyObject sender, IDesignDataSettings value)
        {
            if (sender == null)
            {
                throw new ArgumentNullException("sender");
            }
            sender.SetValue(DesignDataProperty, value);
        }
    }
}

