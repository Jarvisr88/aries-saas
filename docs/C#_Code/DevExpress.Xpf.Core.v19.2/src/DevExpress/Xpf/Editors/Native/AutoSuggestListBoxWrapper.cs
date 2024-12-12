namespace DevExpress.Xpf.Editors.Native
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Popups;
    using DevExpress.Xpf.Editors.Themes;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class AutoSuggestListBoxWrapper : SelectorWrapper
    {
        static AutoSuggestListBoxWrapper()
        {
            EventManager.RegisterClassHandler(typeof(ListBoxItem), Mouse.MouseDownEvent, new MouseButtonEventHandler(AutoSuggestListBoxWrapper.HandleMouseDown));
            EventManager.RegisterClassHandler(typeof(ListBoxItem), Mouse.MouseUpEvent, new MouseButtonEventHandler(AutoSuggestListBoxWrapper.HandleMouseUp));
        }

        public AutoSuggestListBoxWrapper(System.Windows.Controls.ListBox element) : base(element)
        {
            this.NavigationHelper = this.CreateNavigationHelper();
        }

        public override void ClearEditor()
        {
            this.ListBox.SelectionChanged -= new SelectionChangedEventHandler(this.ListBoxOnSelectionChanged);
            this.ListBox.SelectedItem = null;
            SetSelectorWrapper(this.ListBox, null);
        }

        private void ClosePopupIfNeeded(MouseButtonEventArgs e)
        {
            if ((this.OwnerEdit != null) && !this.OwnerEdit.IsReadOnly)
            {
                e.Handled = true;
                this.OwnerEdit.ClosePopup();
            }
        }

        protected virtual ListBoxNavigationHelper CreateNavigationHelper() => 
            new ListBoxNavigationHelper(this.ListBox);

        public override IEnumerable GetSelectedItems() => 
            this.ListBox.SelectedItems;

        private static void HandleMouseDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem d = (ListBoxItem) sender;
            SelectorWrapper selectorWrapper = GetSelectorWrapper(d);
            if (selectorWrapper != null)
            {
                selectorWrapper.NotifyMouseDown(d, e);
            }
        }

        private static void HandleMouseUp(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem d = (ListBoxItem) sender;
            SelectorWrapper selectorWrapper = GetSelectorWrapper(d);
            if (selectorWrapper != null)
            {
                selectorWrapper.NotifyMouseUp(d, e);
            }
        }

        private void ListBoxOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!this.OwnerEdit.AcceptValueOnPopupContentSelectionChanged || (this.SelectedItem == null))
            {
                base.PostPopupValue = this.SelectedItem != null;
            }
            else
            {
                this.PropertyProvider.GetService<ValueContainerService>().SetEditValue(this.EditStrategy.CalcEditableItem(this.SelectedItem), UpdateEditorSource.TextInput);
                this.OwnerEdit.ForceChangeDisplayText();
            }
        }

        private void MakeSelectionVisible()
        {
            if (this.ListBox.SelectionMode != SelectionMode.Single)
            {
                if (this.ListBox.SelectedItems.Count > 0)
                {
                    this.ListBox.ScrollIntoView(this.ListBox.SelectedItems[this.ListBox.SelectedItems.Count - 1]);
                }
            }
            else if (this.SelectedItem != null)
            {
                this.ListBox.ScrollIntoView(this.SelectedItem);
            }
        }

        public override void NotifyMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.SelectionEventMode == DevExpress.Xpf.Editors.Popups.SelectionEventMode.MouseDown)
            {
                this.ClosePopupIfNeeded(e);
            }
        }

        public override void NotifyMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.SelectionEventMode == DevExpress.Xpf.Editors.Popups.SelectionEventMode.MouseUp)
            {
                this.ClosePopupIfNeeded(e);
            }
        }

        public override bool ProcessKeyDown(KeyEventArgs e) => 
            e.Handled;

        public override bool ProcessPreviewKeyDown(KeyEventArgs e)
        {
            if ((this.OwnerEdit == null) || this.OwnerEdit.IsReadOnly)
            {
                return false;
            }
            if (ModifierKeysHelper.IsAltPressed(ModifierKeysHelper.GetKeyboardModifiers(e)))
            {
                return false;
            }
            if (this.ListBox.SelectionMode != SelectionMode.Single)
            {
                return false;
            }
            Key systemKey = e.Key;
            if (e.Key == Key.System)
            {
                systemKey = e.SystemKey;
            }
            if (systemKey <= Key.Prior)
            {
                if ((systemKey != Key.Return) && (systemKey == Key.Prior))
                {
                    e.Handled = true;
                    this.NavigationHelper.MovePageUp();
                }
            }
            else if (systemKey == Key.Next)
            {
                e.Handled = true;
                this.NavigationHelper.MovePageDown();
            }
            else if (systemKey == Key.Up)
            {
                e.Handled = true;
                this.NavigationHelper.MovePrev();
            }
            else if (systemKey == Key.Down)
            {
                e.Handled = true;
                this.NavigationHelper.MoveNext();
            }
            this.MakeSelectionVisible();
            return true;
        }

        public override void SetupEditor()
        {
            if (!this.ListBox.IsPropertySet(FrameworkElement.StyleProperty))
            {
                AutoSuggestListBoxThemeKeyExtension resourceKey = new AutoSuggestListBoxThemeKeyExtension();
                resourceKey.ResourceKey = AutoSuggestListBoxThemeKeys.Style;
                resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(this.ListBox);
                object obj2 = this.ListBox.TryFindResource(resourceKey);
                if (obj2 != null)
                {
                    this.ListBox.SetCurrentValue(FrameworkElement.StyleProperty, obj2);
                }
            }
            this.ListBox.SelectionMode = SelectionMode.Single;
            this.SelectionEventMode = this.PropertyProvider.SelectionEventMode;
            this.ClosePopupOnClick = this.PropertyProvider.ClosePopupOnClick;
            SetSelectorWrapper(this.ListBox, this);
            this.ListBox.SelectionChanged += new SelectionChangedEventHandler(this.ListBoxOnSelectionChanged);
        }

        public override void SyncValuesWithOwnerEdit(bool resetTotals)
        {
        }

        public override void SyncWithOwnerEdit(bool syncDataSource)
        {
        }

        public System.Windows.Controls.ListBox ListBox =>
            (System.Windows.Controls.ListBox) base.Selector;

        private ListBoxNavigationHelper NavigationHelper { get; set; }

        public AutoSuggestEdit OwnerEdit =>
            (AutoSuggestEdit) BaseEdit.GetOwnerEdit(this.ListBox);

        public AutoSuggestEditStrategy EditStrategy =>
            (AutoSuggestEditStrategy) this.OwnerEdit.EditStrategy;

        private AutoSuggestEditPropertyProvider PropertyProvider =>
            (AutoSuggestEditPropertyProvider) ActualPropertyProvider.GetProperties(this.OwnerEdit);

        private DevExpress.Xpf.Editors.Popups.SelectionEventMode SelectionEventMode { get; set; }

        private bool ClosePopupOnClick { get; set; }

        public override object SelectedItem =>
            this.ListBox.SelectedItem;
    }
}

