namespace DevExpress.Xpf.Editors.Native
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Popups;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Windows.Controls;

    public static class LookUpEditHelper
    {
        public static void FocusEditCore(LookUpEditBase editor)
        {
            editor.FocusCore();
        }

        public static bool GetClosePopupOnMouseUp(LookUpEditBase editor) => 
            ((BaseLookUpStyleSettings) editor.PropertyProvider.StyleSettings).GetClosePopupOnMouseUp(editor);

        public static object GetEditValue(ISelectorEdit editor)
        {
            object obj2;
            if (!editor.EditStrategy.ProvideEditValue(editor.EditStrategy.EditValue, out obj2, UpdateEditorSource.TextInput))
            {
                return null;
            }
            LookUpEditBase base2 = editor as LookUpEditBase;
            if ((base2 == null) || (!base2.EditStrategy.IsInTokenMode || !GetIsSingleSelection(base2)))
            {
                return obj2;
            }
            IList list = obj2 as IList;
            int currentEditableTokenIndex = ((LookUpEditBase) editor).EditStrategy.GetCurrentEditableTokenIndex();
            return (((currentEditableTokenIndex == -1) || ((list == null) || (list.Count <= currentEditableTokenIndex))) ? null : list[currentEditableTokenIndex]);
        }

        public static IEnumerable<string> GetHighlightedColumns(LookUpEditBase editor) => 
            ((LookUpEditBasePropertyProvider) editor.PropertyProvider).GetHighlightedColumns();

        internal static HighlightedTextCriteria GetHighlightedTextCriteria(FilterCondition filterCondition) => 
            (filterCondition == FilterCondition.StartsWith) ? HighlightedTextCriteria.StartsWith : HighlightedTextCriteria.Contains;

        public static bool GetIsAllowItemHighlighting(LookUpEditBase editor) => 
            (((BaseLookUpStyleSettings) editor.PropertyProvider.StyleSettings).GetSelectionEventMode(editor) == SelectionEventMode.MouseEnter) && editor.AllowItemHighlighting;

        public static bool GetIsAsyncServerMode(LookUpEditBase editor) => 
            editor.ItemsProvider.IsAsyncServerMode;

        public static bool GetIsInTokenMode(LookUpEditBase editor) => 
            editor.EditStrategy.IsInTokenMode;

        public static bool GetIsServerMode(LookUpEditBase editor) => 
            editor.ItemsProvider.IsServerMode;

        public static bool GetIsSingleSelection(LookUpEditBase editor) => 
            (editor.Settings.StyleSettings == null) || (((BaseLookUpStyleSettings) editor.Settings.StyleSettings).GetSelectionMode(editor) == SelectionMode.Single);

        public static IItemsProvider2 GetItemsProvider(LookUpEditBase editor) => 
            editor.ItemsProvider;

        public static IItemsProvider2 GetItemsProvider(LookUpEditSettingsBase settings) => 
            settings.ItemsProvider;

        public static IPopupContentOwner GetPopupContentOwner(PopupBaseEdit baseEdit) => 
            baseEdit.PopupContentOwner;

        public static int GetSelectedIndex(LookUpEditBase editor) => 
            editor.EditStrategy.SelectorUpdater.GetIndexFromEditValue(editor.EditStrategy.EditValue);

        public static object GetSelectedItem(ISelectorEdit editor) => 
            editor.GetCurrentSelectedItem();

        public static IEnumerable GetSelectedItems(LookUpEditBase editor) => 
            ((ISelectorEdit) editor).GetCurrentSelectedItems();

        public static IEnumerable GetSelectedValues(LookUpEditBase editor)
        {
            List<object> list = new List<object>();
            foreach (object obj2 in ((ISelectorEdit) editor).GetCurrentSelectedItems())
            {
                object valueFromItem = editor.ItemsProvider.GetValueFromItem(obj2, null);
                if (valueFromItem != null)
                {
                    list.Add(valueFromItem);
                }
            }
            return list;
        }

        public static VisualClientOwner GetVisualClient(PopupBaseEdit editor) => 
            editor.VisualClient;

        public static bool HasPopupContent(PopupBaseEdit baseEdit)
        {
            IPopupContentOwner popupContentOwner = GetPopupContentOwner(baseEdit);
            return ((popupContentOwner != null) && (popupContentOwner.Child != null));
        }

        public static bool IsInLookUpMode(LookUpEditSettingsBase settings) => 
            !string.IsNullOrEmpty(settings.ValueMember) || !string.IsNullOrEmpty(settings.DisplayMember);

        public static void RaisePopupContentSelectionChangedEvent(LookUpEditBase editor, IList removed, IList added)
        {
            editor.RaisePopupContentSelectionChanged(removed, added);
        }

        public static void SetHighlightedText(ISupportTextHighlighting settings, string text, FilterCondition filterCondition)
        {
            settings.UpdateHighlightedText(text, GetHighlightedTextCriteria(filterCondition));
        }

        public static void UpdatePopupButtons(LookUpEditBase editor)
        {
            editor.UpdatePopupButtons();
        }
    }
}

