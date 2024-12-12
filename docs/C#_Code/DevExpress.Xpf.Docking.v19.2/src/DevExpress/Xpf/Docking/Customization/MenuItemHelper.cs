namespace DevExpress.Xpf.Docking.Customization
{
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Docking.Images;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public class MenuItemHelper
    {
        static MenuItemHelper()
        {
            ContentCache = new Dictionary<MenuItems, object>();
            ContentCache.Add(MenuItems.ShowCaption, DockingLocalizer.GetString(DockingStringId.MenuItemShowCaption));
            ContentCache.Add(MenuItems.ShowControl, DockingLocalizer.GetString(DockingStringId.MenuItemShowControl));
            ContentCache.Add(MenuItems.CaptionImageLocation, DockingLocalizer.GetString(DockingStringId.MenuItemCaptionImageLocation));
            ContentCache.Add(MenuItems.BeforeText, DockingLocalizer.GetString(DockingStringId.MenuItemBeforeText));
            ContentCache.Add(MenuItems.AfterText, DockingLocalizer.GetString(DockingStringId.MenuItemAfterText));
            ContentCache.Add(MenuItems.CaptionLocation, DockingLocalizer.GetString(DockingStringId.MenuItemCaptionLocation));
            ContentCache.Add(MenuItems.Left, DockingLocalizer.GetString(DockingStringId.MenuItemLeft));
            ContentCache.Add(MenuItems.Right, DockingLocalizer.GetString(DockingStringId.MenuItemRight));
            ContentCache.Add(MenuItems.Top, DockingLocalizer.GetString(DockingStringId.MenuItemTop));
            ContentCache.Add(MenuItems.Bottom, DockingLocalizer.GetString(DockingStringId.MenuItemBottom));
            ContentCache.Add(MenuItems.HideCustomizationWindow, DockingLocalizer.GetString(DockingStringId.MenuItemHideCustomizationWindow));
            ContentCache.Add(MenuItems.ShowCustomizationWindow, DockingLocalizer.GetString(DockingStringId.MenuItemShowCustomizationWindow));
            ContentCache.Add(MenuItems.BeginCustomization, DockingLocalizer.GetString(DockingStringId.MenuItemBeginCustomization));
            ContentCache.Add(MenuItems.EndCustomization, DockingLocalizer.GetString(DockingStringId.MenuItemEndCustomization));
            ContentCache.Add(MenuItems.Dockable, DockingLocalizer.GetString(DockingStringId.MenuItemDock));
            ContentCache.Add(MenuItems.Floating, DockingLocalizer.GetString(DockingStringId.MenuItemFloat));
            ContentCache.Add(MenuItems.AutoHide, DockingLocalizer.GetString(DockingStringId.MenuItemAutoHide));
            ContentCache.Add(MenuItems.Hide, DockingLocalizer.GetString(DockingStringId.MenuItemHide));
            ContentCache.Add(MenuItems.Close, DockingLocalizer.GetString(DockingStringId.MenuItemClose));
            ContentCache.Add(MenuItems.ClosedPanels, DockingLocalizer.GetString(DockingStringId.MenuItemClosedPanels));
            ContentCache.Add(MenuItems.ExpandGroup, DockingLocalizer.GetString(DockingStringId.MenuItemExpandGroup));
            ContentCache.Add(MenuItems.CollapseGroup, DockingLocalizer.GetString(DockingStringId.MenuItemCollapseGroup));
            ContentCache.Add(MenuItems.HideItem, DockingLocalizer.GetString(DockingStringId.MenuItemHideItem));
            ContentCache.Add(MenuItems.RestoreItem, DockingLocalizer.GetString(DockingStringId.MenuItemRestoreItem));
            ContentCache.Add(MenuItems.GroupItems, DockingLocalizer.GetString(DockingStringId.MenuItemGroupItems));
            ContentCache.Add(MenuItems.GroupOrientation, DockingLocalizer.GetString(DockingStringId.MenuItemGroupOrientation));
            ContentCache.Add(MenuItems.Horizontal, DockingLocalizer.GetString(DockingStringId.MenuItemHorizontal));
            ContentCache.Add(MenuItems.Vertical, DockingLocalizer.GetString(DockingStringId.MenuItemVertical));
            ContentCache.Add(MenuItems.Rename, DockingLocalizer.GetString(DockingStringId.MenuItemRename));
            ContentCache.Add(MenuItems.ShowCaptionImage, DockingLocalizer.GetString(DockingStringId.MenuItemShowCaptionImage));
            ContentCache.Add(MenuItems.Style, DockingLocalizer.GetString(DockingStringId.MenuItemStyle));
            ContentCache.Add(MenuItems.StyleNoBorder, DockingLocalizer.GetString(DockingStringId.MenuItemStyleNoBorder));
            ContentCache.Add(MenuItems.StyleGroup, DockingLocalizer.GetString(DockingStringId.MenuItemStyleGroup));
            ContentCache.Add(MenuItems.StyleGroupBox, DockingLocalizer.GetString(DockingStringId.MenuItemStyleGroupBox));
            ContentCache.Add(MenuItems.StyleTabbed, DockingLocalizer.GetString(DockingStringId.MenuItemStyleTabbed));
            ContentCache.Add(MenuItems.Ungroup, DockingLocalizer.GetString(DockingStringId.MenuItemUngroup));
            ContentCache.Add(MenuItems.HorizontalAlignment, DockingLocalizer.GetString(DockingStringId.MenuItemHorizontalAlignment));
            ContentCache.Add(MenuItems.HorizontalAlignmentLeft, DockingLocalizer.GetString(DockingStringId.MenuItemHorizontalAlignmentLeft));
            ContentCache.Add(MenuItems.HorizontalAlignmentRight, DockingLocalizer.GetString(DockingStringId.MenuItemHorizontalAlignmentRight));
            ContentCache.Add(MenuItems.HorizontalAlignmentCenter, DockingLocalizer.GetString(DockingStringId.MenuItemHorizontalAlignmentCenter));
            ContentCache.Add(MenuItems.HorizontalAlignmentStretch, DockingLocalizer.GetString(DockingStringId.MenuItemHorizontalAlignmentStretch));
            ContentCache.Add(MenuItems.VerticalAlignment, DockingLocalizer.GetString(DockingStringId.MenuItemVerticalAlignment));
            ContentCache.Add(MenuItems.VerticalAlignmentTop, DockingLocalizer.GetString(DockingStringId.MenuItemVerticalAlignmentTop));
            ContentCache.Add(MenuItems.VerticalAlignmentBottom, DockingLocalizer.GetString(DockingStringId.MenuItemVerticalAlignmentBottom));
            ContentCache.Add(MenuItems.VerticalAlignmentCenter, DockingLocalizer.GetString(DockingStringId.MenuItemVerticalAlignmentCenter));
            ContentCache.Add(MenuItems.VerticalAlignmentStretch, DockingLocalizer.GetString(DockingStringId.MenuItemVerticalAlignmentStretch));
            ContentCache.Add(MenuItems.NewHorizontalTabbedGroup, DockingLocalizer.GetString(DockingStringId.MenuItemNewHorizontalTabGroup));
            ContentCache.Add(MenuItems.NewVerticalTabbedGroup, DockingLocalizer.GetString(DockingStringId.MenuItemNewVerticalTabGroup));
            ContentCache.Add(MenuItems.MoveToPreviousTabGroup, DockingLocalizer.GetString(DockingStringId.MenuItemMoveToPreviousTabGroup));
            ContentCache.Add(MenuItems.MoveToNextTabGroup, DockingLocalizer.GetString(DockingStringId.MenuItemMoveToNextTabGroup));
            ContentCache.Add(MenuItems.CloseAllButThis, DockingLocalizer.GetString(DockingStringId.MenuItemCloseAllButThis));
            ContentCache.Add(MenuItems.PinTab, DockingLocalizer.GetString(DockingStringId.MenuItemPinTab));
            GlyphCache = new Dictionary<MenuItems, ImageSource>();
            GlyphCache.Add(MenuItems.BeginCustomization, ImageHelper.GetImage("Customization"));
            GlyphCache.Add(MenuItems.EndCustomization, ImageHelper.GetImage("Customization"));
            GlyphCache.Add(MenuItems.NewHorizontalTabbedGroup, ImageHelper.GetImage("NewHorizontalTabGroup"));
            GlyphCache.Add(MenuItems.NewVerticalTabbedGroup, ImageHelper.GetImage("NewVerticalTabGroup"));
            MenuItemsUniqueNames = new Dictionary<MenuItems, string>();
            MenuItemsUniqueNames.Add(MenuItems.ShowCaption, "MenuItemShowCaption");
            MenuItemsUniqueNames.Add(MenuItems.ShowControl, "MenuItemShowControl");
            MenuItemsUniqueNames.Add(MenuItems.CaptionImageLocation, "MenuItemCaptionImageLocation");
            MenuItemsUniqueNames.Add(MenuItems.BeforeText, "MenuItemBeforeText");
            MenuItemsUniqueNames.Add(MenuItems.AfterText, "MenuItemAfterText");
            MenuItemsUniqueNames.Add(MenuItems.CaptionLocation, "MenuItemCaptionLocation");
            MenuItemsUniqueNames.Add(MenuItems.Left, "MenuItemLeft");
            MenuItemsUniqueNames.Add(MenuItems.Right, "MenuItemRight");
            MenuItemsUniqueNames.Add(MenuItems.Top, "MenuItemTop");
            MenuItemsUniqueNames.Add(MenuItems.Bottom, "MenuItemBottom");
            MenuItemsUniqueNames.Add(MenuItems.HideCustomizationWindow, "MenuItemHideCustomizationWindow");
            MenuItemsUniqueNames.Add(MenuItems.ShowCustomizationWindow, "MenuItemShowCustomizationWindow");
            MenuItemsUniqueNames.Add(MenuItems.BeginCustomization, "MenuItemBeginCustomization");
            MenuItemsUniqueNames.Add(MenuItems.EndCustomization, "MenuItemEndCustomization");
            MenuItemsUniqueNames.Add(MenuItems.Dockable, "MenuItemDockable");
            MenuItemsUniqueNames.Add(MenuItems.Floating, "MenuItemFloating");
            MenuItemsUniqueNames.Add(MenuItems.AutoHide, "MenuItemAutoHide");
            MenuItemsUniqueNames.Add(MenuItems.Hide, "MenuItemHide");
            MenuItemsUniqueNames.Add(MenuItems.Close, "MenuItemClose");
            MenuItemsUniqueNames.Add(MenuItems.ClosedPanels, "MenuItemClosedPanels");
            MenuItemsUniqueNames.Add(MenuItems.ExpandGroup, "MenuItemExpandGroup");
            MenuItemsUniqueNames.Add(MenuItems.HideItem, "MenuItemHideItem");
            MenuItemsUniqueNames.Add(MenuItems.RestoreItem, "MenuItemRestoreItem");
            MenuItemsUniqueNames.Add(MenuItems.GroupItems, "MenuItemGroupItems");
            MenuItemsUniqueNames.Add(MenuItems.GroupOrientation, "MenuItemGroupOrientation");
            MenuItemsUniqueNames.Add(MenuItems.Horizontal, "MenuItemHorizontal");
            MenuItemsUniqueNames.Add(MenuItems.Vertical, "MenuItemVertical");
            MenuItemsUniqueNames.Add(MenuItems.Rename, "MenuItemRename");
            MenuItemsUniqueNames.Add(MenuItems.ShowCaptionImage, "MenuItemShowCaptionImage");
            MenuItemsUniqueNames.Add(MenuItems.Style, "MenuItemStyle");
            MenuItemsUniqueNames.Add(MenuItems.StyleNoBorder, "MenuItemStyleNoBorder");
            MenuItemsUniqueNames.Add(MenuItems.StyleGroup, "MenuItemStyleGroup");
            MenuItemsUniqueNames.Add(MenuItems.StyleGroupBox, "MenuItemStyleGroupBox");
            MenuItemsUniqueNames.Add(MenuItems.StyleTabbed, "MenuItemStyleTabbed");
            MenuItemsUniqueNames.Add(MenuItems.Ungroup, "MenuItemUngroup");
            MenuItemsUniqueNames.Add(MenuItems.HorizontalAlignment, "MenuItemHorizontalAlignment");
            MenuItemsUniqueNames.Add(MenuItems.HorizontalAlignmentLeft, "MenuItemHorizontalAlignmentLeft");
            MenuItemsUniqueNames.Add(MenuItems.HorizontalAlignmentRight, "MenuItemHorizontalAlignmentRight");
            MenuItemsUniqueNames.Add(MenuItems.HorizontalAlignmentCenter, "MenuItemHorizontalAlignmentCenter");
            MenuItemsUniqueNames.Add(MenuItems.HorizontalAlignmentStretch, "MenuItemHorizontalAlignmentStretch");
            MenuItemsUniqueNames.Add(MenuItems.VerticalAlignment, "MenuItemVerticalAlignment");
            MenuItemsUniqueNames.Add(MenuItems.VerticalAlignmentTop, "MenuItemVerticalAlignmentTop");
            MenuItemsUniqueNames.Add(MenuItems.VerticalAlignmentBottom, "MenuItemVerticalAlignmentBottom");
            MenuItemsUniqueNames.Add(MenuItems.VerticalAlignmentCenter, "MenuItemVerticalAlignmentCenter");
            MenuItemsUniqueNames.Add(MenuItems.VerticalAlignmentStretch, "MenuItemVerticalAlignmentStretch");
            MenuItemsUniqueNames.Add(MenuItems.NewHorizontalTabbedGroup, "MenuItemNewHorizontalTabbedGroup");
            MenuItemsUniqueNames.Add(MenuItems.NewVerticalTabbedGroup, "MenuItemNewVerticalTabbedGroup");
            MenuItemsUniqueNames.Add(MenuItems.MoveToPreviousTabGroup, "MenuItemMoveToPreviousTabGroup");
            MenuItemsUniqueNames.Add(MenuItems.MoveToNextTabGroup, "MenuItemMoveToNextTabGroup");
            MenuItemsUniqueNames.Add(MenuItems.CloseAllButThis, "MenuItemCloseAllButThis");
            MenuItemsUniqueNames.Add(MenuItems.PinTab, "MenuItemPinTab");
            MenuItemsUniqueNames.Add(MenuItems.TabOperationsSeparator, "TabOperationsSeparator");
            MenuItemsUniqueNames.Add(MenuItems.DocumentHostOperationsSeparator, "DocumentHostOperationsSeparator");
            MenuItemsUniqueNames.Add(MenuItems.CustomizationOperationsSeparator, "CustomizationOperationsSeparator");
            MenuItemsUniqueNames.Add(MenuItems.ClosedPanelsSeparator, "ClosedPanelsSeparator");
        }

        public static object GetContent(MenuItems type)
        {
            object obj2;
            return (ContentCache.TryGetValue(type, out obj2) ? obj2 : null);
        }

        public static ImageSource GetGlyph(MenuItems type)
        {
            ImageSource source;
            return (GlyphCache.TryGetValue(type, out source) ? source : null);
        }

        public static MenuItems? GetMenuItemByUniqueName(string name)
        {
            MenuItems? nullable;
            using (Dictionary<MenuItems, string>.KeyCollection.Enumerator enumerator = MenuItemsUniqueNames.Keys.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        MenuItems current = enumerator.Current;
                        if (MenuItemsUniqueNames[current] != name)
                        {
                            continue;
                        }
                        nullable = new MenuItems?(current);
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return nullable;
        }

        public static string GetUniqueName(MenuItems type)
        {
            string str;
            return (MenuItemsUniqueNames.TryGetValue(type, out str) ? str : CustomizationController.GetUniqueMenuItemName());
        }

        public static Dictionary<MenuItems, object> ContentCache { get; private set; }

        public static Dictionary<MenuItems, ImageSource> GlyphCache { get; private set; }

        public static Dictionary<MenuItems, string> MenuItemsUniqueNames { get; private set; }
    }
}

