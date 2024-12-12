namespace DevExpress.Xpf.Docking
{
    using DevExpress.Utils.Localization;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Bars.Themes;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking.Base;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;

    public class MDIMenuBar : Bar, IDockLayoutManagerListener
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ShowRestoreButtonProperty = DependencyProperty.Register("ShowRestoreButton", typeof(bool), typeof(MDIMenuBar));
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ShowMinimizeButtonProperty = DependencyProperty.Register("ShowMinimizeButton", typeof(bool), typeof(MDIMenuBar));
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ShowCloseButtonProperty = DependencyProperty.Register("ShowCloseButton", typeof(bool), typeof(MDIMenuBar));
        public const string MDIMenuBarCategory = "MDIButtons";
        public const string MDIMenuBarItemPrefix = "__MDIMenuBarItem__";

        public MDIMenuBar(DockLayoutManager manager, BarManager mdiBarManager)
        {
            BindingHelper.SetBinding(this, ShowRestoreButtonProperty, manager, "ActiveMDIItem.ShowRestoreButton");
            BindingHelper.SetBinding(this, ShowMinimizeButtonProperty, manager, "ActiveMDIItem.ShowMinimizeButton");
            BindingHelper.SetBinding(this, ShowCloseButtonProperty, manager, "ActiveMDIItem.ShowCloseButton");
            DockLayoutManager.SetDockLayoutManager(this, manager);
            MergingProperties.SetElementMergingBehavior(this, ElementMergingBehavior.None);
            base.Name = UniqueNameHelper.GetMDIBarName();
            base.IsMainMenu = true;
            BarManagerCategory category1 = new BarManagerCategory();
            category1.Name = "MDIButtons";
            BarManagerCategory category = category1;
            BarItem item = this.CreateBarButtonItem(ItemType.Minimize, manager);
            BarItem item2 = this.CreateBarButtonItem(ItemType.Restore, manager);
            BarItem item3 = this.CreateBarButtonItem(ItemType.Close, manager);
            mdiBarManager.Categories.Add(category);
            mdiBarManager.Items.Add(item);
            mdiBarManager.Items.Add(item2);
            mdiBarManager.Items.Add(item3);
            base.ItemLinks.Add(item);
            base.ItemLinks.Add(item2);
            base.ItemLinks.Add(item3);
            mdiBarManager.Bars.Add(this);
        }

        private BarItem CreateBarButtonItem(ItemType type, DockLayoutManager manager)
        {
            MDIButtonSettings settings = MDIButtonSettings.GetSettings(type);
            BarMDIButtonItem item1 = new BarMDIButtonItem();
            item1.Name = GetBarItemName(type);
            item1.CategoryName = "MDIButtons";
            item1.Content = settings.Content;
            item1.Hint = settings.Hint;
            item1.KeyGesture = settings.KeyGesture;
            item1.Command = settings.Command;
            BarMDIButtonItem target = item1;
            BindingHelper.SetBinding(target, BarItem.CommandParameterProperty, manager, DockLayoutManager.ActiveMDIItemProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(target, BarItem.IsVisibleProperty, this, settings.IsVisibleBindingPath);
            BindingHelper.SetBinding(target, BarItem.CommandTargetProperty, this, DockLayoutManager.DockLayoutManagerProperty, BindingMode.OneWay);
            return target;
        }

        void IDockLayoutManagerListener.Subscribe(DockLayoutManager manager)
        {
        }

        void IDockLayoutManagerListener.Unsubscribe(DockLayoutManager manager)
        {
        }

        public BarMDIButtonItem GetBarItem(ItemType type) => 
            (base.Manager != null) ? ((BarMDIButtonItem) base.Manager.Items[GetBarItemName(type)]) : null;

        private static string GetBarItemName(ItemType type) => 
            "__MDIMenuBarItem__" + type;

        private static BarItemThemeKeyExtension GetBorderStyleKey(DockLayoutManager manager, bool forRibbonControl = false)
        {
            BarItemThemeKeyExtension extension1 = new BarItemThemeKeyExtension();
            extension1.ThemeName = DockLayoutManagerExtension.GetThemeName(manager);
            extension1.ResourceKey = forRibbonControl ? BarItemThemeKeys.BorderStyleInRibbonPageHeader : BarItemThemeKeys.BorderStyleInMainMenu;
            return extension1;
        }

        public void UpdateMDIButtonBorderStyle(DockLayoutManager manager, Style style)
        {
            foreach (BarItemLinkBase base2 in base.ItemLinks)
            {
                if (style == null)
                {
                    base2.ClearValue(BarItemLinkBase.CustomResourcesProperty);
                    continue;
                }
                ResourceDictionary dictionary = new ResourceDictionary {
                    { 
                        GetBorderStyleKey(manager, false),
                        style
                    },
                    { 
                        GetBorderStyleKey(manager, true),
                        style
                    }
                };
                base2.CustomResources = dictionary;
            }
        }

        public enum ItemType
        {
            Minimize,
            Restore,
            Close
        }

        public class MDIButtonSettings
        {
            protected MDIButtonSettings()
            {
            }

            public static MDIMenuBar.MDIButtonSettings GetSettings(MDIMenuBar.ItemType type)
            {
                switch (type)
                {
                    case MDIMenuBar.ItemType.Minimize:
                        return new MinimizeMDIButtonSettings();

                    case MDIMenuBar.ItemType.Restore:
                        return new RestoreMDIButtonSettings();

                    case MDIMenuBar.ItemType.Close:
                        return new CloseMDIButtonSettings();
                }
                throw new NotSupportedException(type.ToString());
            }

            public ICommand Command { get; private set; }

            public object Content { get; private set; }

            public string Hint { get; private set; }

            public System.Windows.Input.KeyGesture KeyGesture { get; private set; }

            public string IsVisibleBindingPath { get; private set; }

            private class CloseMDIButtonSettings : MDIMenuBar.MDIButtonSettings
            {
                internal CloseMDIButtonSettings()
                {
                    base.Command = DockControllerCommand.Close;
                    base.Hint = XtraLocalizer<DockingStringId>.Active.GetLocalizedString(DockingStringId.ControlButtonClose);
                    base.KeyGesture = new KeyGesture(Key.F4, ModifierKeys.Control);
                    base.IsVisibleBindingPath = "ShowCloseButton";
                }
            }

            private class MinimizeMDIButtonSettings : MDIMenuBar.MDIButtonSettings
            {
                internal MinimizeMDIButtonSettings()
                {
                    base.Command = MDIControllerCommand.Minimize;
                    base.Hint = XtraLocalizer<DockingStringId>.Active.GetLocalizedString(DockingStringId.ControlButtonMinimize);
                    base.IsVisibleBindingPath = "ShowMinimizeButton";
                }
            }

            private class RestoreMDIButtonSettings : MDIMenuBar.MDIButtonSettings
            {
                internal RestoreMDIButtonSettings()
                {
                    base.Command = MDIControllerCommand.Restore;
                    base.Hint = XtraLocalizer<DockingStringId>.Active.GetLocalizedString(DockingStringId.ControlButtonRestore);
                    base.IsVisibleBindingPath = "ShowRestoreButton";
                }
            }
        }
    }
}

