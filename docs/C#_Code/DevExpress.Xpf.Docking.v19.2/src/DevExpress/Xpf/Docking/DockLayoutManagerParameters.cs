namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Docking.Base;
    using System;
    using System.Collections;
    using System.Windows;

    public static class DockLayoutManagerParameters
    {
        private static Hashtable valueCache = new Hashtable();

        static DockLayoutManagerParameters()
        {
            valueCache[DockLayoutManagerParameter.DockingItemIntervalHorz] = 4.0;
            valueCache[DockLayoutManagerParameter.DockingItemIntervalVert] = 4.0;
            valueCache[DockLayoutManagerParameter.DockingRootMargin] = new Thickness(2.0);
            valueCache[DockLayoutManagerParameter.LayoutItemIntervalHorz] = 16.0;
            valueCache[DockLayoutManagerParameter.LayoutItemIntervalVert] = 4.0;
            valueCache[DockLayoutManagerParameter.LayoutGroupIntervalHorz] = 12.0;
            valueCache[DockLayoutManagerParameter.LayoutGroupIntervalVert] = 12.0;
            valueCache[DockLayoutManagerParameter.LayoutRootMargin] = new Thickness(12.0);
            valueCache[DockLayoutManagerParameter.CaptionToControlDistanceLeft] = 6.0;
            valueCache[DockLayoutManagerParameter.CaptionToControlDistanceTop] = 4.0;
            valueCache[DockLayoutManagerParameter.CaptionToControlDistanceRight] = 6.0;
            valueCache[DockLayoutManagerParameter.CaptionToControlDistanceBottom] = 4.0;
            valueCache[DockLayoutManagerParameter.LayoutPanelCaptionFormat] = DockingLocalizer.GetString(DockingStringId.LayoutPanelCaptionFormat);
            valueCache[DockLayoutManagerParameter.LayoutGroupCaptionFormat] = DockingLocalizer.GetString(DockingStringId.LayoutGroupCaptionFormat);
            valueCache[DockLayoutManagerParameter.LayoutControlItemCaptionFormat] = DockingLocalizer.GetString(DockingStringId.LayoutControlItemCaptionFormat);
            valueCache[DockLayoutManagerParameter.TabCaptionFormat] = DockingLocalizer.GetString(DockingStringId.TabCaptionFormat);
            valueCache[DockLayoutManagerParameter.WindowTitleFormat] = DockingLocalizer.GetString(DockingStringId.WindowTitleFormat);
            valueCache[DockLayoutManagerParameter.AutoHidePanelsFitToContainer] = true;
            valueCache[DockLayoutManagerParameter.DisposePanelContentAfterRemovingPanel] = false;
            valueCache[DockLayoutManagerParameter.ActivateItemOnTabHeaderMiddleClick] = false;
            valueCache[DockLayoutManagerParameter.UseLegacyDockPreviewCalculator] = false;
            valueCache[DockLayoutManagerParameter.AutoHidePanelsAutoSizeDependsOnCaption] = true;
            valueCache[DockLayoutManagerParameter.SquashDockItemActivatedEvents] = true;
            valueCache[DockLayoutManagerParameter.CheckIsUsingItemsSource] = false;
        }

        public static bool DisposePanelContentAfterRemovingPanel
        {
            get => 
                (bool) valueCache[DockLayoutManagerParameter.DisposePanelContentAfterRemovingPanel];
            set => 
                valueCache[DockLayoutManagerParameter.DisposePanelContentAfterRemovingPanel] = value;
        }

        public static double DockingItemIntervalHorz
        {
            get => 
                (double) valueCache[DockLayoutManagerParameter.DockingItemIntervalHorz];
            set => 
                valueCache[DockLayoutManagerParameter.DockingItemIntervalHorz] = value;
        }

        public static double DockingItemIntervalVert
        {
            get => 
                (double) valueCache[DockLayoutManagerParameter.DockingItemIntervalVert];
            set => 
                valueCache[DockLayoutManagerParameter.DockingItemIntervalVert] = value;
        }

        public static Thickness DockingRootMargin
        {
            get => 
                (Thickness) valueCache[DockLayoutManagerParameter.DockingRootMargin];
            set => 
                valueCache[DockLayoutManagerParameter.DockingRootMargin] = value;
        }

        public static double LayoutItemIntervalHorz
        {
            get => 
                (double) valueCache[DockLayoutManagerParameter.LayoutItemIntervalHorz];
            set => 
                valueCache[DockLayoutManagerParameter.LayoutItemIntervalHorz] = value;
        }

        public static double LayoutItemIntervalVert
        {
            get => 
                (double) valueCache[DockLayoutManagerParameter.LayoutItemIntervalVert];
            set => 
                valueCache[DockLayoutManagerParameter.LayoutItemIntervalVert] = value;
        }

        public static double LayoutGroupIntervalHorz
        {
            get => 
                (double) valueCache[DockLayoutManagerParameter.LayoutGroupIntervalHorz];
            set => 
                valueCache[DockLayoutManagerParameter.LayoutGroupIntervalHorz] = value;
        }

        public static double LayoutGroupIntervalVert
        {
            get => 
                (double) valueCache[DockLayoutManagerParameter.LayoutGroupIntervalVert];
            set => 
                valueCache[DockLayoutManagerParameter.LayoutGroupIntervalVert] = value;
        }

        public static Thickness LayoutRootMargin
        {
            get => 
                (Thickness) valueCache[DockLayoutManagerParameter.LayoutRootMargin];
            set => 
                valueCache[DockLayoutManagerParameter.LayoutRootMargin] = value;
        }

        public static double CaptionToControlDistanceLeft
        {
            get => 
                (double) valueCache[DockLayoutManagerParameter.CaptionToControlDistanceLeft];
            set => 
                valueCache[DockLayoutManagerParameter.CaptionToControlDistanceLeft] = value;
        }

        public static double CaptionToControlDistanceTop
        {
            get => 
                (double) valueCache[DockLayoutManagerParameter.CaptionToControlDistanceTop];
            set => 
                valueCache[DockLayoutManagerParameter.CaptionToControlDistanceTop] = value;
        }

        public static double CaptionToControlDistanceRight
        {
            get => 
                (double) valueCache[DockLayoutManagerParameter.CaptionToControlDistanceRight];
            set => 
                valueCache[DockLayoutManagerParameter.CaptionToControlDistanceRight] = value;
        }

        public static double CaptionToControlDistanceBottom
        {
            get => 
                (double) valueCache[DockLayoutManagerParameter.CaptionToControlDistanceBottom];
            set => 
                valueCache[DockLayoutManagerParameter.CaptionToControlDistanceBottom] = value;
        }

        public static string LayoutPanelCaptionFormat
        {
            get => 
                (string) valueCache[DockLayoutManagerParameter.LayoutPanelCaptionFormat];
            set => 
                valueCache[DockLayoutManagerParameter.LayoutPanelCaptionFormat] = value;
        }

        public static string LayoutGroupCaptionFormat
        {
            get => 
                (string) valueCache[DockLayoutManagerParameter.LayoutGroupCaptionFormat];
            set => 
                valueCache[DockLayoutManagerParameter.LayoutGroupCaptionFormat] = value;
        }

        public static string LayoutControlItemCaptionFormat
        {
            get => 
                (string) valueCache[DockLayoutManagerParameter.LayoutControlItemCaptionFormat];
            set => 
                valueCache[DockLayoutManagerParameter.LayoutControlItemCaptionFormat] = value;
        }

        public static string TabCaptionFormat
        {
            get => 
                (string) valueCache[DockLayoutManagerParameter.TabCaptionFormat];
            set => 
                valueCache[DockLayoutManagerParameter.TabCaptionFormat] = value;
        }

        public static string WindowTitleFormat
        {
            get => 
                (string) valueCache[DockLayoutManagerParameter.WindowTitleFormat];
            set => 
                valueCache[DockLayoutManagerParameter.WindowTitleFormat] = value;
        }

        public static bool AutoHidePanelsFitToContainer
        {
            get => 
                (bool) valueCache[DockLayoutManagerParameter.AutoHidePanelsFitToContainer];
            set => 
                valueCache[DockLayoutManagerParameter.AutoHidePanelsFitToContainer] = value;
        }

        public static bool ActivateItemOnTabHeaderMiddleClick
        {
            get => 
                (bool) valueCache[DockLayoutManagerParameter.ActivateItemOnTabHeaderMiddleClick];
            set => 
                valueCache[DockLayoutManagerParameter.ActivateItemOnTabHeaderMiddleClick] = value;
        }

        public static DevExpress.Xpf.Docking.LogicalTreeStructure? LogicalTreeStructure
        {
            get => 
                (DevExpress.Xpf.Docking.LogicalTreeStructure?) valueCache[DockLayoutManagerParameter.LogicalTreeStructure];
            set => 
                valueCache[DockLayoutManagerParameter.LogicalTreeStructure] = value;
        }

        public static bool UseLegacyDockPreviewCalculator
        {
            get => 
                (bool) valueCache[DockLayoutManagerParameter.UseLegacyDockPreviewCalculator];
            set => 
                valueCache[DockLayoutManagerParameter.UseLegacyDockPreviewCalculator] = value;
        }

        public static bool AutoHidePanelsAutoSizeDependsOnCaption
        {
            get => 
                (bool) valueCache[DockLayoutManagerParameter.AutoHidePanelsAutoSizeDependsOnCaption];
            set => 
                valueCache[DockLayoutManagerParameter.AutoHidePanelsAutoSizeDependsOnCaption] = value;
        }

        public static bool SquashDockItemActivatedEvents
        {
            get => 
                (bool) valueCache[DockLayoutManagerParameter.SquashDockItemActivatedEvents];
            set => 
                valueCache[DockLayoutManagerParameter.SquashDockItemActivatedEvents] = value;
        }

        public static bool CheckLayoutGroupIsUsingItemsSource
        {
            get => 
                (bool) valueCache[DockLayoutManagerParameter.CheckIsUsingItemsSource];
            set => 
                valueCache[DockLayoutManagerParameter.CheckIsUsingItemsSource] = value;
        }
    }
}

