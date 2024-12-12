namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.InteropServices;

    public static class CompatibilitySettings
    {
        private static bool isSealed;
        private static DevExpress.Xpf.Core.CompatibilityMode compatibilityMode;
        private static bool allowGlyphRunRenderingInInplaceEditors;
        private static bool useLightweightTemplatesInStandardButtons;
        private static bool useThemedWindowInServices;
        private static bool useThemedMessageBoxInServices;
        private static DevExpress.Xpf.Core.DXBindingResolvingMode dxBindingResolvingMode;
        private static bool renderPDFPageContentWithDirectX;
        private static bool useLightweightBarItems;
        private static bool allowRecyclingRibbonItems;
        private static bool useLegacyFilterEditor;
        private static bool useDateNavigatorInDateEdit;
        private static bool convertOccurrenceToNormalWhenDragBetweenResources;
        private static bool allowEditValueBindingInInplaceEditors;
        private static bool makeGetWindowReturnActualFloatPanelWindow;
        private static bool enableDPICorrection;
        private static DevExpress.Xpf.Core.SchedulerAppearanceStyle schedulerAppearanceStyle;
        private static bool useLegacyDeleteButtonInButtonEdit;
        private static bool useLegacyColumnFilterPopup;
        private static bool useLegacySchedulerCellDecoration;
        private static bool allowEditTextExpressionInFormatRule;
        private static bool useFriendlyDateRangePresentation = true;

        static CompatibilitySettings()
        {
            AssignCompatibilityModeLatest();
        }

        private static void AssignCompatibilityMode_V17_2()
        {
            AssignCompatibilityMode_V18_1();
            useLightweightTemplatesInStandardButtons = false;
            dxBindingResolvingMode = DevExpress.Xpf.Core.DXBindingResolvingMode.LegacyStaticTyping;
        }

        private static void AssignCompatibilityMode_V18_1()
        {
            AssignCompatibilityMode_V18_2();
            useThemedWindowInServices = false;
            useThemedMessageBoxInServices = false;
            useFriendlyDateRangePresentation = false;
            renderPDFPageContentWithDirectX = false;
            useLightweightBarItems = false;
            allowRecyclingRibbonItems = false;
            useDateNavigatorInDateEdit = false;
            convertOccurrenceToNormalWhenDragBetweenResources = true;
            allowEditValueBindingInInplaceEditors = false;
            makeGetWindowReturnActualFloatPanelWindow = false;
        }

        private static void AssignCompatibilityMode_V18_2()
        {
            AssignCompatibilityMode_V19_1();
            useLegacyFilterEditor = true;
            schedulerAppearanceStyle = DevExpress.Xpf.Core.SchedulerAppearanceStyle.Classic;
            useLegacyDeleteButtonInButtonEdit = true;
        }

        private static void AssignCompatibilityMode_V19_1()
        {
            AssignCompatibilityMode_V19_2();
            useLegacyColumnFilterPopup = true;
        }

        private static void AssignCompatibilityMode_V19_2()
        {
            AssignCompatibilityModeLatest();
        }

        private static void AssignCompatibilityModeDefaultValues(DevExpress.Xpf.Core.CompatibilityMode value)
        {
            CheckSealed();
            switch (value)
            {
                case DevExpress.Xpf.Core.CompatibilityMode.v17_2:
                    AssignCompatibilityMode_V17_2();
                    break;

                case DevExpress.Xpf.Core.CompatibilityMode.v18_1:
                    AssignCompatibilityMode_V18_1();
                    break;

                case DevExpress.Xpf.Core.CompatibilityMode.v18_2:
                    AssignCompatibilityMode_V18_2();
                    break;

                case DevExpress.Xpf.Core.CompatibilityMode.v19_1:
                    AssignCompatibilityMode_V19_1();
                    break;

                case DevExpress.Xpf.Core.CompatibilityMode.v19_2:
                    AssignCompatibilityMode_V19_2();
                    break;

                default:
                    AssignCompatibilityModeLatest();
                    break;
            }
            Seal();
        }

        private static void AssignCompatibilityModeLatest()
        {
            enableDPICorrection = true;
            allowGlyphRunRenderingInInplaceEditors = true;
            useLightweightTemplatesInStandardButtons = true;
            useThemedWindowInServices = true;
            useThemedMessageBoxInServices = true;
            dxBindingResolvingMode = DevExpress.Xpf.Core.DXBindingResolvingMode.DynamicTyping;
            useFriendlyDateRangePresentation = true;
            renderPDFPageContentWithDirectX = true;
            useLightweightBarItems = true;
            allowRecyclingRibbonItems = true;
            useLegacyFilterEditor = false;
            useDateNavigatorInDateEdit = true;
            convertOccurrenceToNormalWhenDragBetweenResources = false;
            allowEditValueBindingInInplaceEditors = true;
            makeGetWindowReturnActualFloatPanelWindow = true;
            schedulerAppearanceStyle = DevExpress.Xpf.Core.SchedulerAppearanceStyle.Outlook2019;
            useLegacyDeleteButtonInButtonEdit = false;
            useLegacyColumnFilterPopup = false;
            useLegacySchedulerCellDecoration = false;
            allowEditTextExpressionInFormatRule = false;
        }

        private static void CheckSealed()
        {
            if (isSealed)
            {
                throw new ArgumentException("sealed");
            }
        }

        private static T GetProperty<T>(T value)
        {
            Seal();
            return value;
        }

        private static void Seal()
        {
            isSealed = true;
        }

        private static void SetProperty<T>(ref T container, T value, Action<T> action = null)
        {
            CheckSealed();
            if (!Equals((T) container, value))
            {
                container = value;
                if (action != null)
                {
                    action(value);
                }
            }
        }

        public static bool EnableDPICorrection
        {
            get => 
                GetProperty<bool>(enableDPICorrection);
            set => 
                SetProperty<bool>(ref enableDPICorrection, value, null);
        }

        public static bool UseDateNavigatorInDateEdit
        {
            get => 
                GetProperty<bool>(useDateNavigatorInDateEdit);
            set => 
                SetProperty<bool>(ref useDateNavigatorInDateEdit, value, null);
        }

        public static bool UseLightweightBarItems
        {
            get => 
                GetProperty<bool>(useLightweightBarItems);
            set => 
                SetProperty<bool>(ref useLightweightBarItems, value, null);
        }

        public static bool AllowRecyclingRibbonItems
        {
            get => 
                GetProperty<bool>(allowRecyclingRibbonItems);
            set => 
                SetProperty<bool>(ref allowRecyclingRibbonItems, value, null);
        }

        public static bool AllowGlyphRunRenderingInInplaceEditors
        {
            get => 
                GetProperty<bool>(allowGlyphRunRenderingInInplaceEditors);
            set => 
                SetProperty<bool>(ref allowGlyphRunRenderingInInplaceEditors, value, null);
        }

        public static bool UseLightweightTemplatesInStandardButtons
        {
            get => 
                GetProperty<bool>(useLightweightTemplatesInStandardButtons);
            set => 
                SetProperty<bool>(ref useLightweightTemplatesInStandardButtons, value, null);
        }

        public static bool UseThemedWindowInServices
        {
            get => 
                GetProperty<bool>(useThemedWindowInServices);
            set => 
                SetProperty<bool>(ref useThemedWindowInServices, value, null);
        }

        public static bool UseThemedMessageBoxInServices
        {
            get => 
                GetProperty<bool>(useThemedMessageBoxInServices);
            set => 
                SetProperty<bool>(ref useThemedMessageBoxInServices, value, null);
        }

        public static DevExpress.Xpf.Core.DXBindingResolvingMode DXBindingResolvingMode
        {
            get => 
                GetProperty<DevExpress.Xpf.Core.DXBindingResolvingMode>(dxBindingResolvingMode);
            set => 
                SetProperty<DevExpress.Xpf.Core.DXBindingResolvingMode>(ref dxBindingResolvingMode, value, null);
        }

        public static bool RenderPDFPageContentWithDirectX
        {
            get => 
                GetProperty<bool>(renderPDFPageContentWithDirectX);
            set => 
                SetProperty<bool>(ref renderPDFPageContentWithDirectX, value, null);
        }

        public static DevExpress.Xpf.Core.SchedulerAppearanceStyle SchedulerAppearanceStyle
        {
            get => 
                GetProperty<DevExpress.Xpf.Core.SchedulerAppearanceStyle>(schedulerAppearanceStyle);
            set => 
                SetProperty<DevExpress.Xpf.Core.SchedulerAppearanceStyle>(ref schedulerAppearanceStyle, value, null);
        }

        public static DevExpress.Xpf.Core.CompatibilityMode CompatibilityMode
        {
            get => 
                GetProperty<DevExpress.Xpf.Core.CompatibilityMode>(compatibilityMode);
            set => 
                SetProperty<DevExpress.Xpf.Core.CompatibilityMode>(ref compatibilityMode, value, new Action<DevExpress.Xpf.Core.CompatibilityMode>(CompatibilitySettings.AssignCompatibilityModeDefaultValues));
        }

        public static bool ConvertOccurrenceToNormalWhenDragBetweenResources
        {
            get => 
                GetProperty<bool>(convertOccurrenceToNormalWhenDragBetweenResources);
            set => 
                SetProperty<bool>(ref convertOccurrenceToNormalWhenDragBetweenResources, value, null);
        }

        public static bool AllowEditValueBindingInInplaceEditors
        {
            get => 
                GetProperty<bool>(allowEditValueBindingInInplaceEditors);
            set => 
                SetProperty<bool>(ref allowEditValueBindingInInplaceEditors, value, null);
        }

        public static bool UseFriendlyDateRangePresentation
        {
            get => 
                GetProperty<bool>(useFriendlyDateRangePresentation);
            set => 
                SetProperty<bool>(ref useFriendlyDateRangePresentation, value, null);
        }

        public static bool UseLegacyFilterEditor
        {
            get => 
                GetProperty<bool>(useLegacyFilterEditor);
            set => 
                SetProperty<bool>(ref useLegacyFilterEditor, value, null);
        }

        public static bool MakeGetWindowReturnActualFloatPanelWindow
        {
            get => 
                GetProperty<bool>(makeGetWindowReturnActualFloatPanelWindow);
            set => 
                SetProperty<bool>(ref makeGetWindowReturnActualFloatPanelWindow, value, null);
        }

        public static bool UseLegacyDeleteButtonInButtonEdit
        {
            get => 
                GetProperty<bool>(useLegacyDeleteButtonInButtonEdit);
            set => 
                SetProperty<bool>(ref useLegacyDeleteButtonInButtonEdit, value, null);
        }

        public static bool UseLegacyColumnFilterPopup
        {
            get => 
                GetProperty<bool>(useLegacyColumnFilterPopup);
            set => 
                SetProperty<bool>(ref useLegacyColumnFilterPopup, value, null);
        }

        public static bool UseLegacySchedulerCellDecoration
        {
            get => 
                GetProperty<bool>(useLegacySchedulerCellDecoration);
            set => 
                SetProperty<bool>(ref useLegacySchedulerCellDecoration, value, null);
        }

        public static bool AllowEditTextExpressionInFormatRule
        {
            get => 
                GetProperty<bool>(allowEditTextExpressionInFormatRule);
            set => 
                SetProperty<bool>(ref allowEditTextExpressionInFormatRule, value, null);
        }
    }
}

