namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls.Primitives;

    public static class DockLayoutManagerHelper
    {
        private static Dictionary<DockLayoutManagerRule, string> errors = new Dictionary<DockLayoutManagerRule, string>();

        static DockLayoutManagerHelper()
        {
            errors.Add(DockLayoutManagerRule.WrongLayoutRoot, "Only LayoutGroup can be root of layout");
            errors.Add(DockLayoutManagerRule.WrongContent, "Only LayoutGroup or UIElement can be used as content");
            errors.Add(DockLayoutManagerRule.InconsistentLayout, "Only LayoutControlItem or LayoutGroup can be hosted within layout");
            errors.Add(DockLayoutManagerRule.ItemCanNotBeHidden, "Only LayoutControlItem or LayoutGroup can be hidden from layout to customization window");
            errors.Add(DockLayoutManagerRule.ItemCanNotBeHosted, "LayoutControlItem can be hosted only in LayoutGroup");
            errors.Add(DockLayoutManagerRule.WrongDocument, "Only DocumentPanel can be hosted within DocumentGroup");
            errors.Add(DockLayoutManagerRule.WrongAutoHiddenPanel, "AutoHideGroup's elements must be LayoutPanel objects");
            errors.Add(DockLayoutManagerRule.WrongPanel, "TabbedGroup's elements must be LayoutPanel objects");
            errors.Add(DockLayoutManagerRule.FloatGroupsCollection, "FloatGroup can be added only to DockLayoutManager.FloatGroups collection");
            errors.Add(DockLayoutManagerRule.AutoHideGroupsCollection, "AutoHideGroup can be added only to DockLayoutManager.AutoHideGroups collection");
            errors.Add(DockLayoutManagerRule.ItemsSourceInUse, "Operation is not valid while ItemsSource is in use. Access and modify elements with LayoutGroup.ItemsSource instead.");
            errors.Add(DockLayoutManagerRule.ItemCollectionMustBeEmpty, "Items collection must be empty before using ItemsSource.");
            errors.Add(DockLayoutManagerRule.WidthIsNotSupported, "The BaseLayoutItem.Width property is not supported. Use the BaseLayoutItem.ItemWidth property instead.");
            errors.Add(DockLayoutManagerRule.HeightIsNotSupported, "The BaseLayoutItem.Height property is not supported. Use the BaseLayoutItem.ItemHeight property instead.");
        }

        public static string GetRule(DockLayoutManagerRule rule)
        {
            string str;
            return (errors.TryGetValue(rule, out str) ? str : string.Empty);
        }

        public static bool IsPopup(object obj) => 
            (obj != null) && (obj is Popup);

        public static bool IsPopupRoot(object obj) => 
            (obj != null) && obj.GetType().Name.EndsWith("PopupRoot");
    }
}

