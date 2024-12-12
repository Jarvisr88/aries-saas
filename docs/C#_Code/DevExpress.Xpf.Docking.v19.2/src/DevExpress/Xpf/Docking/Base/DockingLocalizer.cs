namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Utils.Localization;
    using DevExpress.Utils.Localization.Internal;
    using DevExpress.Xpf.Core;
    using System;

    public class DockingLocalizer : DXLocalizer<DockingStringId>
    {
        static DockingLocalizer()
        {
            SetActiveLocalizerProvider(new DefaultActiveLocalizerProvider<DockingStringId>(CreateDefaultLocalizer()));
        }

        public static XtraLocalizer<DockingStringId> CreateDefaultLocalizer() => 
            new DockingResXLocalizer();

        public override XtraLocalizer<DockingStringId> CreateResXLocalizer() => 
            new DockingResXLocalizer();

        public static string GetString(DockingStringId id) => 
            XtraLocalizer<DockingStringId>.Active.GetLocalizedString(id);

        protected override void PopulateStringTable()
        {
            this.AddString(DockingStringId.MenuItemShowCaption, "Show caption");
            this.AddString(DockingStringId.MenuItemShowControl, "Show control");
            this.AddString(DockingStringId.MenuItemCaptionImageLocation, "Caption image location");
            this.AddString(DockingStringId.MenuItemBeforeText, "Before text");
            this.AddString(DockingStringId.MenuItemAfterText, "After text");
            this.AddString(DockingStringId.MenuItemCaptionLocation, "Caption location");
            this.AddString(DockingStringId.MenuItemLeft, "Left");
            this.AddString(DockingStringId.MenuItemRight, "Right");
            this.AddString(DockingStringId.MenuItemTop, "Top");
            this.AddString(DockingStringId.MenuItemBottom, "Bottom");
            this.AddString(DockingStringId.MenuItemHideCustomizationWindow, "Hide customization window");
            this.AddString(DockingStringId.MenuItemShowCustomizationWindow, "Show customization window");
            this.AddString(DockingStringId.MenuItemBeginCustomization, "Begin customization");
            this.AddString(DockingStringId.MenuItemEndCustomization, "End customization");
            this.AddString(DockingStringId.MenuItemDock, "Dock");
            this.AddString(DockingStringId.MenuItemFloat, "Float");
            this.AddString(DockingStringId.MenuItemAutoHide, "Auto Hide");
            this.AddString(DockingStringId.MenuItemHide, "Hide");
            this.AddString(DockingStringId.MenuItemClose, "Close");
            this.AddString(DockingStringId.MenuItemClosedPanels, "Closed panels");
            this.AddString(DockingStringId.MenuItemExpandGroup, "Expand");
            this.AddString(DockingStringId.MenuItemCollapseGroup, "Collapse");
            this.AddString(DockingStringId.MenuItemHideItem, "Hide item");
            this.AddString(DockingStringId.MenuItemRestoreItem, "Restore item");
            this.AddString(DockingStringId.MenuItemGroupItems, "Group");
            this.AddString(DockingStringId.MenuItemGroupOrientation, "Group orientation");
            this.AddString(DockingStringId.MenuItemHorizontal, "Horizontal");
            this.AddString(DockingStringId.MenuItemVertical, "Vertical");
            this.AddString(DockingStringId.MenuItemRename, "Rename");
            this.AddString(DockingStringId.MenuItemShowCaptionImage, "Show caption image");
            this.AddString(DockingStringId.MenuItemStyle, "Group style");
            this.AddString(DockingStringId.MenuItemStyleNoBorder, "No border");
            this.AddString(DockingStringId.MenuItemStyleGroup, "Group");
            this.AddString(DockingStringId.MenuItemStyleGroupBox, "GroupBox");
            this.AddString(DockingStringId.MenuItemStyleTabbed, "Tabbed");
            this.AddString(DockingStringId.MenuItemUngroup, "Ungroup");
            this.AddString(DockingStringId.MenuItemHorizontalAlignment, "Horizontal alignment");
            this.AddString(DockingStringId.MenuItemHorizontalAlignmentLeft, "Left");
            this.AddString(DockingStringId.MenuItemHorizontalAlignmentRight, "Right");
            this.AddString(DockingStringId.MenuItemHorizontalAlignmentCenter, "Center");
            this.AddString(DockingStringId.MenuItemHorizontalAlignmentStretch, "Stretch");
            this.AddString(DockingStringId.MenuItemVerticalAlignment, "Vertical alignment");
            this.AddString(DockingStringId.MenuItemVerticalAlignmentTop, "Top");
            this.AddString(DockingStringId.MenuItemVerticalAlignmentBottom, "Bottom");
            this.AddString(DockingStringId.MenuItemVerticalAlignmentCenter, "Center");
            this.AddString(DockingStringId.MenuItemVerticalAlignmentStretch, "Stretch");
            this.AddString(DockingStringId.MenuItemNewHorizontalTabGroup, "New horizontal tab group");
            this.AddString(DockingStringId.MenuItemNewVerticalTabGroup, "New vertical tab group");
            this.AddString(DockingStringId.MenuItemMoveToPreviousTabGroup, "Move to previous tab group");
            this.AddString(DockingStringId.MenuItemMoveToNextTabGroup, "Move to next tab group");
            this.AddString(DockingStringId.MenuItemCloseAllButThis, "Close all but this");
            this.AddString(DockingStringId.MenuItemPinTab, "Pin Tab");
            this.AddString(DockingStringId.TitleCustomizationForm, "Customization");
            this.AddString(DockingStringId.TitleHiddenItemsList, "Hidden Items");
            this.AddString(DockingStringId.TitleLayoutTreeView, "Layout Tree");
            this.AddString(DockingStringId.CheckBoxShowInvisibleItems, "Show invisible items");
            this.AddString(DockingStringId.ButtonSave, "Save");
            this.AddString(DockingStringId.ButtonRestore, "Restore");
            this.AddString(DockingStringId.LayoutPanelCaptionFormat, "{0}");
            this.AddString(DockingStringId.LayoutGroupCaptionFormat, "{0}");
            this.AddString(DockingStringId.LayoutControlItemCaptionFormat, "{0}:");
            this.AddString(DockingStringId.TabCaptionFormat, "{0}");
            this.AddString(DockingStringId.WindowTitleFormat, "{0} - [{1}]");
            this.AddString(DockingStringId.DefaultLabelContent, "Label");
            this.AddString(DockingStringId.DefaultEmptySpaceContent, "Empty Space Item");
            this.AddString(DockingStringId.DefaultSeparatorContent, "Separator");
            this.AddString(DockingStringId.DefaultSplitterContent, "Splitter");
            this.AddString(DockingStringId.NewGroupCaption, "Group");
            this.AddString(DockingStringId.ControlButtonClose, "Close");
            this.AddString(DockingStringId.ControlButtonAutoHide, "Auto Hide");
            this.AddString(DockingStringId.ControlButtonTogglePinStatus, "Toggle pin status");
            this.AddString(DockingStringId.ControlButtonMinimize, "Minimize");
            this.AddString(DockingStringId.ControlButtonMaximize, "Maximize");
            this.AddString(DockingStringId.ControlButtonRestore, "Restore");
            this.AddString(DockingStringId.ControlButtonScrollNext, "Scroll next");
            this.AddString(DockingStringId.ControlButtonScrollPrev, "Scroll previous");
            this.AddString(DockingStringId.ControlButtonHide, "Hide");
            this.AddString(DockingStringId.ControlButtonExpand, "Expand");
            this.AddString(DockingStringId.ControlButtonCollapse, "Collapse");
            this.AddString(DockingStringId.DocumentSelectorPanels, "Active Panels");
            this.AddString(DockingStringId.DocumentSelectorDocuments, "Active Documents");
            this.AddString(DockingStringId.ClosedPanelsCategory, "Closed Panels");
            this.AddString(DockingStringId.DTLoadLayoutWarning, "The current layout will be cleared. Do you want to continue?");
            this.AddString(DockingStringId.DTLoadLayoutWarningCaption, "Loading Layout");
            this.AddString(DockingStringId.DTEmptyPanelText, "Drag and drop controls here to build your layout.");
            this.AddString(DockingStringId.DTEmptyGroupText, "Right-click here to create panels via a context menu.");
            this.AddString(DockingStringId.DTLayoutControlItemCaption, "Layout Item");
            this.AddString(DockingStringId.DTDocumentPanelCaption, "Document");
            this.AddString(DockingStringId.DTLayoutPanelCaption, "Panel");
            this.AddString(DockingStringId.MenuItemAddPanel, "Add Panel");
            this.AddString(DockingStringId.MenuItemRemovePanel, "Remove Panel");
            this.AddString(DockingStringId.MenuItemHidePanel, "Close Panel");
            this.AddString(DockingStringId.MenuItemAddDocument, "Add Document");
            this.AddString(DockingStringId.MenuItemRemoveDocument, "Remove Document");
            this.AddString(DockingStringId.MenuItemHideDocument, "Close Document");
            this.AddString(DockingStringId.MenuItemCreateDefaultLayout, "Create Default Layout");
            this.AddString(DockingStringId.MenuItemDTCaptionLocation, "Caption Location");
            this.AddString(DockingStringId.MenuItemDTGroupStyle, "Group Style");
            this.AddString(DockingStringId.MenuItemDTGroupOrientation, "Group Orientation");
            this.AddString(DockingStringId.MenuItemCaptionHorizontalAlignment, "Caption Horizontal Alignment");
            this.AddString(DockingStringId.MenuItemCaptionVerticalAlignment, "Caption Vertical Alignment");
            this.AddString(DockingStringId.MenuItemControlHorizontalAlignment, "Control Horizontal Alignment");
            this.AddString(DockingStringId.MenuItemControlVerticalAlignment, "Control Vertical Alignment");
            this.AddString(DockingStringId.MenuItemResetLayout, "Reset Layout");
            this.AddString(DockingStringId.MenuItemRemoveItem, "Remove Item");
            this.AddString(DockingStringId.MenuItemRemoveAll, "Clear");
            this.AddString(DockingStringId.MenuItemContentHorizontalAlignment, "Content Horizontal Alignment");
            this.AddString(DockingStringId.MenuItemContentVerticalAlignment, "Content Vertical Alignment");
            this.AddString(DockingStringId.ReplaceDialogTitle, "Add new control");
            this.AddString(DockingStringId.ReplaceDialogText, "Choose an action:");
            this.AddString(DockingStringId.ShowCustomizationPanel, "Show Customization Panel");
            this.AddString(DockingStringId.NoItemSelected, "(No item selected)");
            this.AddString(DockingStringId.ToolTipEditCaption, "Click here to edit item's caption");
            this.AddString(DockingStringId.ToolTipDeleteItem, "Click here to delete selected item");
            this.AddString(DockingStringId.ToolTipChangeItemType, "Click here to change item type");
            this.AddString(DockingStringId.ToolTipCreateItem, "Click here to create new item");
            this.AddString(DockingStringId.DockingOperations, "Docking operations");
            this.AddString(DockingStringId.FloatingOperations, "Floating operations");
            this.AddString(DockingStringId.AutoHideOperations, "Auto-hide operations");
            this.AddString(DockingStringId.CloseOperations, "Close operations");
            this.AddString(DockingStringId.LoadLayoutOperation, "Load layout");
            this.AddString(DockingStringId.ResetLayoutOperation, "Reset layout");
            this.AddString(DockingStringId.ButtonNewLayoutItemFormat, "New ({0})");
            this.AddString(DockingStringId.ButtonCreateLayoutItem, "Append the control to the panel");
            this.AddString(DockingStringId.ButtonCreateLayoutItemDetails, "Choosing the option creates a new LayoutGroup and assigns it to the panel's Content. The new and existing controls will be moved to the created LayoutGroup.");
            this.AddString(DockingStringId.ButtonReplaceControl, "Replace the existing control");
            this.AddString(DockingStringId.ButtonReplaceControlDetails, "Choose this option to replace the existing control with the new one.");
            this.AddString(DockingStringId.ButtonDoNothing, "Cancel");
            this.AddString(DockingStringId.ButtonDoNothingDetails, "Do not add the control to the panel");
            this.AddString(DockingStringId.CheckBoxAskNextTime, "Ask next time");
            this.AddString(DockingStringId.MenuItemResetCustomization, "Reset Customization Settings");
            this.AddString(DockingStringId.MenuItemMDIStyle, "MDI Style");
        }
    }
}

