namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Automation;
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Themes;
    using DevExpress.Xpf.Editors.Validation.Native;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Markup;
    using System.Windows.Media;

    [ContentProperty("EditValue")]
    public abstract class TextEditBase : BaseEdit, ITextExportSettings, IExportSettings
    {
        public static readonly DependencyProperty TextProperty;
        public static readonly DependencyProperty EditNonEditableTemplateProperty;
        public static readonly DependencyProperty AcceptsReturnProperty;
        public static readonly DependencyProperty TextWrappingProperty;
        public static readonly DependencyProperty PrintTextWrappingProperty;
        public static readonly DependencyProperty VerticalScrollBarVisibilityProperty;
        public static readonly DependencyProperty HorizontalScrollBarVisibilityProperty;
        public static readonly DependencyProperty MaxLengthProperty;
        public static readonly DependencyProperty ShowTooltipForTrimmedTextProperty;
        public static readonly DependencyProperty SelectAllOnGotFocusProperty;
        public static readonly DependencyProperty SelectAllOnMouseUpProperty;
        public static readonly DependencyProperty NullTextForegroundProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty TextInputSettingsProperty;
        internal static readonly bool ShowTooltipForTrimmedTextDefaultValue = true;
        public static readonly DependencyProperty AcceptsTabProperty;
        public static readonly DependencyProperty TextTrimmingProperty;
        private readonly CommandBinding selectAllBinding;
        private readonly CommandBinding cutBinding;
        private readonly CommandBinding pasteBinding;
        private readonly CommandBinding undoBinding;
        private readonly CommandBinding deleteBinding;
        private readonly Locker assignSettingsLocker = new Locker();

        static TextEditBase()
        {
            Type ownerType = typeof(TextEditBase);
            EditNonEditableTemplateProperty = DependencyPropertyManager.Register("EditNonEditableTemplate", typeof(ControlTemplate), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(TextEditBase.OnEditNonEditableTemplateChanged)));
            AcceptsReturnProperty = TextBoxBase.AcceptsReturnProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(new PropertyChangedCallback(TextEditBase.OnAcceptsReturnChanged)));
            TextWrappingProperty = TextBlock.TextWrappingProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(new PropertyChangedCallback(TextEditBase.OnTextWrappingChanged)));
            PrintTextWrappingProperty = DependencyPropertyManager.Register("PrintTextWrapping", typeof(System.Windows.TextWrapping?), typeof(TextEditBase), new PropertyMetadata(null, new PropertyChangedCallback(TextEditBase.OnTextWrappingChanged)));
            VerticalScrollBarVisibilityProperty = ScrollViewer.VerticalScrollBarVisibilityProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(ScrollBarVisibility.Hidden));
            HorizontalScrollBarVisibilityProperty = ScrollViewer.HorizontalScrollBarVisibilityProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(ScrollBarVisibility.Hidden));
            MaxLengthProperty = DependencyPropertyManager.Register("MaxLength", typeof(int), ownerType, new FrameworkPropertyMetadata(0));
            ShowTooltipForTrimmedTextProperty = DependencyPropertyManager.Register("ShowTooltipForTrimmedText", typeof(bool), ownerType, new FrameworkPropertyMetadata(ShowTooltipForTrimmedTextDefaultValue, (d, e) => ((TextEditBase) d).OnShowTooltipForTrimmedTextChanged()));
            SelectAllOnGotFocusProperty = DependencyPropertyManager.Register("SelectAllOnGotFocus", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            SelectAllOnMouseUpProperty = DependencyPropertyManager.Register("SelectAllOnMouseUp", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            TextInputSettingsProperty = DependencyPropertyManager.Register("TextInputSettings", typeof(TextInputSettingsBase), ownerType, new FrameworkPropertyMetadata((d, e) => ((TextEditBase) d).TextInputSettingsChanged((TextInputSettingsBase) e.OldValue, (TextInputSettingsBase) e.NewValue)));
            BaseEdit.NullValueProperty.OverrideMetadata(typeof(PasswordBoxEdit), new FrameworkPropertyMetadata(string.Empty));
            AcceptsTabProperty = TextBoxBase.AcceptsTabProperty.AddOwner(ownerType);
            TextProperty = DependencyPropertyManager.Register("Text", typeof(string), ownerType, new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(TextEditBase.OnTextChanged), new CoerceValueCallback(TextEditBase.OnCoerceTextProperty), true, UpdateSourceTrigger.LostFocus));
            TextTrimmingProperty = TextBlock.TextTrimmingProperty.AddOwner(ownerType, new PropertyMetadata(System.Windows.TextTrimming.CharacterEllipsis, (d, e) => ((TextEditBase) d).OnTextTrimmingChanged()));
            CommandManager.RegisterClassCommandBinding(ownerType, new CommandBinding(ApplicationCommands.SelectAll, (d, e) => ((TextEditBase) d).SelectAll(), (d, e) => ((TextEditBase) d).CanSelectAll(d, e)));
            CommandManager.RegisterClassCommandBinding(ownerType, new CommandBinding(ApplicationCommands.Cut, (d, e) => ((TextEditBase) d).Cut(), (d, e) => ((TextEditBase) d).CanCut(d, e)));
            CommandManager.RegisterClassCommandBinding(ownerType, new CommandBinding(ApplicationCommands.Delete, (d, e) => ((TextEditBase) d).Delete(), (d, e) => ((TextEditBase) d).CanDelete(d, e)));
            CommandManager.RegisterClassCommandBinding(ownerType, new CommandBinding(ApplicationCommands.Copy, (d, e) => ((TextEditBase) d).Copy(), (d, e) => ((TextEditBase) d).CanCopy(d, e)));
            CommandManager.RegisterClassCommandBinding(ownerType, new CommandBinding(ApplicationCommands.Paste, (d, e) => ((TextEditBase) d).Paste(), (d, e) => ((TextEditBase) d).CanPaste(d, e)));
            CommandManager.RegisterClassCommandBinding(ownerType, new CommandBinding(ApplicationCommands.Undo, (d, e) => ((TextEditBase) d).Undo(), (d, e) => ((TextEditBase) d).CanUndo(d, e)));
            Control.BackgroundProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata((d, e) => ((TextEditBase) d).OnBackgroundChanged()));
            UIElement.FocusableProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata((d, e) => ((TextEditBase) d).OnFocusableChanged()));
            Control.HorizontalContentAlignmentProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata((d, e) => ((TextEditBase) d).OnHorizontalContentAlignmentChanged()));
            Control.PaddingProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata((d, e) => ((TextEditBase) d).OnPaddingChanged()));
            Control.VerticalContentAlignmentProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(new PropertyChangedCallback(TextEditBase.OnVerticalContentAlignmentChanged)));
            NullTextForegroundProperty = DependencyPropertyManager.Register("NullTextForeground", typeof(Brush), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, (d, e) => ((TextEditBase) d).OnNullTextForegroundChanged()));
        }

        protected TextEditBase()
        {
            this.EditBox = this.CreateEditBoxWrapper();
            this.selectAllBinding = new CommandBinding(ApplicationCommands.SelectAll, new ExecutedRoutedEventHandler(this.SelectAll));
            this.cutBinding = new CommandBinding(ApplicationCommands.Cut, new ExecutedRoutedEventHandler(this.Cut), new CanExecuteRoutedEventHandler(this.CanCut));
            this.deleteBinding = new CommandBinding(ApplicationCommands.Delete, new ExecutedRoutedEventHandler(this.Delete), new CanExecuteRoutedEventHandler(this.CanDelete));
            this.pasteBinding = new CommandBinding(ApplicationCommands.Paste, new ExecutedRoutedEventHandler(this.Paste), new CanExecuteRoutedEventHandler(this.CanPaste));
            this.undoBinding = new CommandBinding(ApplicationCommands.Undo, new ExecutedRoutedEventHandler(this.Undo), new CanExecuteRoutedEventHandler(this.CanUndo));
        }

        protected internal virtual void CanCopy(object sender, CanExecuteRoutedEventArgs e)
        {
            this.SetCanExecuteParameters(e, this.EditStrategy.CanCopy());
        }

        protected internal virtual void CanCut(object sender, CanExecuteRoutedEventArgs e)
        {
            this.SetCanExecuteParameters(e, this.EditStrategy.CanCut());
        }

        protected internal virtual void CanDelete(object sender, CanExecuteRoutedEventArgs e)
        {
            this.SetCanExecuteParameters(e, this.EditStrategy.CanDelete());
        }

        protected internal virtual void CanPaste(object sender, CanExecuteRoutedEventArgs e)
        {
            this.SetCanExecuteParameters(e, this.EditStrategy.CanPaste());
        }

        protected internal virtual void CanSelectAll(object sender, CanExecuteRoutedEventArgs e)
        {
            this.SetCanExecuteParameters(e, this.EditStrategy.CanSelectAll());
        }

        protected internal virtual void CanUndo(object sender, CanExecuteRoutedEventArgs e)
        {
            this.SetCanExecuteParameters(e, this.EditStrategy.CanUndo());
        }

        public void Clear()
        {
            this.EditStrategy.Clear();
        }

        public virtual void Copy()
        {
            this.EditStrategy.Copy();
        }

        protected internal virtual void Copy(object sender, ExecutedRoutedEventArgs e)
        {
            this.Copy();
        }

        protected abstract EditBoxWrapper CreateEditBoxWrapper();
        protected override EditStrategyBase CreateEditStrategy() => 
            new TextEditStrategy(this);

        protected internal virtual TextInputSettingsBase CreateTextInputSettings() => 
            new DevExpress.Xpf.Editors.TextInputSettings(this);

        public virtual void Cut()
        {
            this.EditStrategy.Cut();
        }

        protected internal virtual void Cut(object sender, ExecutedRoutedEventArgs e)
        {
            this.Cut();
        }

        public virtual void Delete()
        {
            this.EditStrategy.Delete();
        }

        protected internal virtual void Delete(object sender, ExecutedRoutedEventArgs e)
        {
            this.Delete();
        }

        protected virtual void EditBoxMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.EditStrategy.MouseUp(e);
        }

        private void EditBoxOnSelectionChanged(object sender, RoutedEventArgs routedEventArgs)
        {
        }

        private void EditBoxPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.EditStrategy.PreviewMouseDown(e);
        }

        private void EditBoxPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.EditStrategy.PreviewMouseUp(e);
        }

        protected override void EndInitInternal(bool callBase)
        {
            base.EndInitInternal(callBase);
            this.assignSettingsLocker.DoLockedAction(new Action(this.UpdateTextInputSettings));
        }

        internal override void FlushPendingEditActions(UpdateEditorSource updateEditor)
        {
            this.EditStrategy.FlushPendingEditActions(updateEditor);
        }

        protected internal virtual CharacterCasing GetActualCharactedCasing() => 
            CharacterCasing.Normal;

        internal System.Windows.TextWrapping GetActualTextWrapping() => 
            (!base.IsPrintingMode || (this.PrintTextWrapping == null)) ? this.TextWrapping : this.PrintTextWrapping.Value;

        protected virtual string GetExportText() => 
            this.PropertyProvider.DisplayText;

        protected virtual TextDecorationCollection GetPrintTextDecorations() => 
            ExportSettingDefaultValue.TextDecorations;

        protected virtual object GetTextValueInternal() => 
            this.EditStrategy.IsNullValue(base.EditValue) ? null : base.EditValue;

        protected virtual bool? GetXlsExportNativeFormatInternal() => 
            ExportSettingDefaultValue.XlsExportNativeFormat;

        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters) => 
            (base.EditMode == EditMode.Standalone) ? base.HitTestCore(hitTestParameters) : new PointHitTestResult(this, hitTestParameters.HitPoint);

        internal bool IsInactiveModeWithTextBlock() => 
            this.IsInplaceMode && (base.EditCore is TextBlock);

        protected virtual bool IsTextBlockModeCore() => 
            base.EditMode == EditMode.InplaceInactive;

        protected override bool NeedsEnter(ModifierKeys modifiers) => 
            this.EditStrategy.NeedsEnter;

        protected internal override bool NeedsKey(Key key, ModifierKeys modifiers) => 
            (this.EditBox != null) ? (this.EditStrategy.NeedsKey(key, modifiers) && base.NeedsKey(key, modifiers)) : base.NeedsKey(key, modifiers);

        protected virtual void OnAcceptsReturnChanged()
        {
        }

        protected static void OnAcceptsReturnChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((TextEditBase) obj).OnAcceptsReturnChanged();
        }

        private void OnBackgroundChanged()
        {
            this.SyncTextBlockProperty(Control.BackgroundProperty, TextBlock.BackgroundProperty);
        }

        protected virtual object OnCoerceText(string text) => 
            this.EditStrategy.CoerceText(text);

        private static object OnCoerceTextProperty(DependencyObject d, object text) => 
            ((TextEditBase) d).OnCoerceText((string) text);

        private void OnCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (ReferenceEquals(e.Command, ApplicationCommands.Paste))
            {
                this.IsBrowserPasteCommandExecuted = BrowserInteropHelper.IsBrowserHosted;
            }
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new TextEditAutomationPeer(this);

        protected virtual void OnEditBoxMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!e.Handled && ((base.EditCore is TextBox) && ReferenceEquals(Mouse.Captured, base.EditCore)))
            {
                base.EditCore.ReleaseMouseCapture();
            }
        }

        protected virtual void OnEditBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            this.EditStrategy.SyncWithEditor();
        }

        protected override void OnEditCoreAssigned()
        {
            if (base.IsPrintingMode && (base.EditCore is TextBlock))
            {
                ((TextBlock) base.EditCore).Margin = new Thickness(0.0);
            }
            this.SyncTextBlockPadding();
            this.SyncTextBlockTextWrapping();
            this.SyncTextBlockTextAlignment();
            this.SyncTextBlockProperty(Control.VerticalContentAlignmentProperty, FrameworkElement.VerticalAlignmentProperty);
            this.UpdateAllowIsTextTrimmed();
            this.SyncTextBlockProperty(Control.BackgroundProperty, TextBlock.BackgroundProperty);
            this.SyncTextBlockProperty(TextTrimmingProperty);
            this.SyncTextBlockProperty(Control.IsTabStopProperty, KeyboardNavigation.IsTabStopProperty);
            this.SyncTextBlockProperty(UIElement.FocusableProperty);
            this.SyncTextBlockProperty(null, FrameworkElement.FocusVisualStyleProperty);
            this.SyncTextBlockForegroundPropertyForNullText();
            base.OnEditCoreAssigned();
        }

        protected override void OnEditModeChanged(EditMode oldValue, EditMode newValue)
        {
            base.OnEditModeChanged(oldValue, newValue);
            this.UpdateAllowIsTextTrimmed();
        }

        protected virtual void OnEditNonEditableTemplateChanged(ControlTemplate template)
        {
            base.UpdateActualEditorControlTemplate();
        }

        protected static void OnEditNonEditableTemplateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((TextEditBase) obj).OnEditNonEditableTemplateChanged((ControlTemplate) e.NewValue);
        }

        private void OnFocusableChanged()
        {
            this.SyncTextBlockProperty(UIElement.FocusableProperty);
        }

        protected virtual void OnHorizontalContentAlignmentChanged()
        {
            this.SyncTextBlockTextAlignment();
        }

        protected override void OnIsNullTextVisibleChanged(bool isVisible)
        {
            base.OnIsNullTextVisibleChanged(isVisible);
            this.SyncTextBlockForegroundPropertyForNullText();
        }

        protected override void OnIsTabStopPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnIsTabStopPropertyChanged(e);
            this.SyncTextBlockProperty(Control.IsTabStopProperty, KeyboardNavigation.IsTabStopProperty);
        }

        private void OnIsTextTrimmedChanged(object sender, RoutedEventArgs e)
        {
            this.EditStrategy.CoerceToolTip();
        }

        private void OnNullTextForegroundChanged()
        {
            this.SyncTextBlockForegroundPropertyForNullText();
        }

        private void OnPaddingChanged()
        {
            this.SyncTextBlockPadding();
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            base.OnPreviewTextInput(e);
            if (!string.IsNullOrEmpty(e.Text))
            {
                this.EditStrategy.OnPreviewTextInput(e);
            }
        }

        private void OnShowTooltipForTrimmedTextChanged()
        {
            this.UpdateAllowIsTextTrimmed();
        }

        protected virtual void OnTextChanged(string oldText, string text)
        {
            this.EditStrategy.OnTextChanged(oldText, text);
        }

        protected static void OnTextChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((TextEditBase) obj).OnTextChanged((string) e.OldValue, (string) e.NewValue);
        }

        private void OnTextTrimmingChanged()
        {
            this.SyncTextBlockProperty(TextTrimmingProperty);
        }

        protected virtual void OnTextWrappingChanged()
        {
            this.EditStrategy.UpdateDisplayText();
            this.SyncTextBlockTextWrapping();
        }

        protected static void OnTextWrappingChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((TextEditBase) obj).OnTextWrappingChanged();
        }

        protected virtual void OnVerticalContentAlignmentChanged()
        {
            this.SyncTextBlockProperty(Control.VerticalContentAlignmentProperty, FrameworkElement.VerticalAlignmentProperty);
        }

        protected static void OnVerticalContentAlignmentChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((TextEditBase) obj).OnVerticalContentAlignmentChanged();
        }

        public virtual void Paste()
        {
            this.EditStrategy.Paste();
        }

        protected internal virtual void Paste(object sender, ExecutedRoutedEventArgs e)
        {
            if (!BrowserInteropHelper.IsBrowserHosted)
            {
                this.Paste();
            }
        }

        internal void PerformKeyboardSelectAll()
        {
            if (this.IsMousePressed)
            {
                Func<FrameworkElement, bool> evaluator = <>c.<>9__170_0;
                if (<>c.<>9__170_0 == null)
                {
                    Func<FrameworkElement, bool> local1 = <>c.<>9__170_0;
                    evaluator = <>c.<>9__170_0 = x => x.IsMouseOver;
                }
                if (!base.EditCore.IfNot<FrameworkElement>(evaluator).ReturnSuccess<FrameworkElement>())
                {
                    return;
                }
            }
            this.SelectAll();
        }

        protected override void ProcessActivatingKeyCore(Key key, ModifierKeys modifiers)
        {
            if (base.Settings.IsPasteGesture(key, modifiers))
            {
                this.EditBox.ExecuteCommand(ApplicationCommands.Paste, null);
            }
            else
            {
                base.ProcessActivatingKeyCore(key, modifiers);
            }
        }

        private void ProcessBrowserPasteCommand(TextChangedEventArgs e)
        {
            List<TextChange> list = new List<TextChange>(e.Changes);
            if (list.Count > 0)
            {
                TextChange change = list[0];
                string text = this.EditBox.Text.Substring(change.Offset, change.AddedLength);
                this.EditStrategy.Paste(text);
            }
            this.IsBrowserPasteCommandExecuted = false;
        }

        private void ProcessPreviewTextInputStart(object sender, TextCompositionEventArgs e)
        {
            this.IsInImeInput = false;
        }

        private void ProcessPreviewTextInputUpdate(object sender, TextCompositionEventArgs e)
        {
            this.IsInImeInput = true;
        }

        private void ProcessTextInput(object sender, TextCompositionEventArgs e)
        {
            this.IsInImeInput = false;
        }

        private void ReInitializeStrategy()
        {
            try
            {
                this.EditStrategy.Release();
            }
            finally
            {
                this.EditStrategy.Initialize();
            }
        }

        public override void SelectAll()
        {
            base.SelectAll();
            this.EditStrategy.SelectAll();
        }

        private void SelectAll(object sender, ExecutedRoutedEventArgs e)
        {
            this.SelectAll();
        }

        protected virtual void SetCanExecuteParameters(CanExecuteRoutedEventArgs e, bool canExecute)
        {
            e.CanExecute = (base.EditMode != EditMode.InplaceInactive) & canExecute;
            if (base.EditMode == EditMode.InplaceInactive)
            {
                e.ContinueRouting = true;
            }
            else
            {
                e.ContinueRouting = !canExecute;
                e.Handled = !canExecute;
            }
        }

        protected override void SubscribeEditEventsCore()
        {
            base.SubscribeEditEventsCore();
            TextBox editCore = base.EditCore as TextBox;
            if (editCore != null)
            {
                TextCompositionManager.AddPreviewTextInputStartHandler(editCore, new TextCompositionEventHandler(this.ProcessPreviewTextInputStart));
                TextCompositionManager.AddPreviewTextInputUpdateHandler(editCore, new TextCompositionEventHandler(this.ProcessPreviewTextInputUpdate));
                TextCompositionManager.AddTextInputHandler(editCore, new TextCompositionEventHandler(this.ProcessTextInput));
                editCore.MouseLeftButtonUp += new MouseButtonEventHandler(this.OnEditBoxMouseLeftButtonUp);
                editCore.TextChanged += new TextChangedEventHandler(this.OnEditBoxTextChanged);
                editCore.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(this.EditBoxPreviewMouseLeftButtonDown);
                editCore.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(this.EditBoxPreviewMouseLeftButtonUp);
                editCore.AddHandler(UIElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.EditBoxMouseLeftButtonUp), true);
                editCore.SelectionChanged += new RoutedEventHandler(this.EditBoxOnSelectionChanged);
            }
            if (this.EditBox != null)
            {
                this.EditBox.AddPreviewExecutedHandler(new ExecutedRoutedEventHandler(this.OnCommandExecuted));
                this.EditBox.AddCommandBinding(this.selectAllBinding);
                this.EditBox.AddCommandBinding(this.cutBinding);
                this.EditBox.AddCommandBinding(this.deleteBinding);
                this.EditBox.AddCommandBinding(this.pasteBinding);
                this.EditBox.AddCommandBinding(this.undoBinding);
            }
            TextBlockService.AddIsTextTrimmedChangedHandler(base.EditCore, new RoutedEventHandler(this.OnIsTextTrimmedChanged));
            this.ReInitializeStrategy();
        }

        private void SyncTextBlockForegroundPropertyForNullText()
        {
            if (this.IsInactiveModeWithTextBlock())
            {
                if (this.PropertyProvider.IsNullTextVisible)
                {
                    TextEditThemeKeyExtension extension1 = new TextEditThemeKeyExtension();
                    extension1.ResourceKey = TextEditThemeKeys.NullTextForeground;
                    extension1.ThemeName = ThemeHelper.GetEditorThemeName(this);
                    object resourceKey = extension1;
                    Brush newValue = this.NullTextForeground ?? (base.TryFindResource(resourceKey) as Brush);
                    if (newValue != null)
                    {
                        this.SyncTextBlockProperty(newValue, TextBlock.ForegroundProperty);
                        return;
                    }
                }
                base.EditCore.ClearValue(TextBlock.ForegroundProperty);
            }
        }

        private void SyncTextBlockPadding()
        {
            this.SyncTextBlockProperty(Control.PaddingProperty, TextBlock.PaddingProperty);
        }

        private void SyncTextBlockProperty(DependencyProperty property)
        {
            this.SyncTextBlockProperty(property, property);
        }

        protected void SyncTextBlockProperty(object newValue, DependencyProperty textBlockProperty)
        {
            if (this.IsInactiveModeWithTextBlock())
            {
                ValueSource valueSource = System.Windows.DependencyPropertyHelper.GetValueSource(base.EditCore, textBlockProperty);
                if ((valueSource.BaseValueSource <= BaseValueSource.DefaultStyleTrigger) || (valueSource.BaseValueSource == BaseValueSource.Local))
                {
                    base.EditCore.SetValue(textBlockProperty, newValue);
                }
            }
        }

        private void SyncTextBlockProperty(DependencyProperty editorProperty, DependencyProperty textBlockProperty)
        {
            this.SyncTextBlockProperty(base.GetValue(editorProperty), textBlockProperty);
        }

        private void SyncTextBlockTextAlignment()
        {
            TextAlignment center;
            switch (base.HorizontalContentAlignment)
            {
                case HorizontalAlignment.Center:
                    center = TextAlignment.Center;
                    break;

                case HorizontalAlignment.Right:
                    center = TextAlignment.Right;
                    break;

                case HorizontalAlignment.Stretch:
                    center = TextAlignment.Justify;
                    break;

                default:
                    center = TextAlignment.Left;
                    break;
            }
            this.SyncTextBlockProperty(center, TextBlock.TextAlignmentProperty);
        }

        private void SyncTextBlockTextWrapping()
        {
            this.SyncTextBlockProperty(this.GetActualTextWrapping(), TextBlock.TextWrappingProperty);
        }

        protected virtual void TextInputSettingsChanged(TextInputSettingsBase oldValue, TextInputSettingsBase newValue)
        {
            this.PropertyProvider.SetTextInputSettings(newValue);
            if (!base.IsInSupportInitializing)
            {
                this.EditStrategy.SyncWithValue();
                this.assignSettingsLocker.DoIfNotLocked(() => this.DoValidate());
            }
        }

        public virtual void Undo()
        {
            this.EditStrategy.Undo();
        }

        protected internal virtual void Undo(object sender, ExecutedRoutedEventArgs e)
        {
            this.Undo();
        }

        protected override void UnsubscribeEditEventsCore()
        {
            base.UnsubscribeEditEventsCore();
            TextBox editCore = base.EditCore as TextBox;
            if (editCore != null)
            {
                TextCompositionManager.RemovePreviewTextInputStartHandler(editCore, new TextCompositionEventHandler(this.ProcessPreviewTextInputStart));
                TextCompositionManager.RemovePreviewTextInputUpdateHandler(editCore, new TextCompositionEventHandler(this.ProcessPreviewTextInputUpdate));
                TextCompositionManager.RemoveTextInputHandler(editCore, new TextCompositionEventHandler(this.ProcessTextInput));
                editCore.MouseLeftButtonUp -= new MouseButtonEventHandler(this.OnEditBoxMouseLeftButtonUp);
                editCore.TextChanged -= new TextChangedEventHandler(this.OnEditBoxTextChanged);
                editCore.PreviewMouseLeftButtonDown -= new MouseButtonEventHandler(this.EditBoxPreviewMouseLeftButtonDown);
                editCore.RemoveHandler(UIElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.EditBoxMouseLeftButtonUp));
                editCore.PreviewMouseLeftButtonUp -= new MouseButtonEventHandler(this.EditBoxPreviewMouseLeftButtonUp);
            }
            if (this.EditBox != null)
            {
                this.EditBox.RemovePreviewExecutedHandler(new ExecutedRoutedEventHandler(this.OnCommandExecuted));
                this.EditBox.RemoveCommandBinding(this.selectAllBinding);
                this.EditBox.RemoveCommandBinding(this.cutBinding);
                this.EditBox.RemoveCommandBinding(this.deleteBinding);
                this.EditBox.RemoveCommandBinding(this.pasteBinding);
                this.EditBox.RemoveCommandBinding(this.undoBinding);
            }
            TextBlockService.RemoveIsTextTrimmedChangedHandler(base.EditCore, new RoutedEventHandler(this.OnIsTextTrimmedChanged));
        }

        private void UpdateAllowIsTextTrimmed()
        {
            if (this.IsInactiveModeWithTextBlock())
            {
                TextBlockService.SetAllowIsTextTrimmed(base.EditCore, (base.EditMode == EditMode.InplaceInactive) && this.ShowTooltipForTrimmedText);
            }
            else
            {
                if (base.EditCore != null)
                {
                    TextBlockService.SetAllowIsTextTrimmed(base.EditCore, this.ShowTooltipForTrimmedText);
                }
                this.EditStrategy.CoerceToolTip();
            }
        }

        protected virtual void UpdateEditBoxWrapper()
        {
            this.EditBox = this.CreateEditBoxWrapper();
        }

        protected virtual void UpdateTextInputSettings()
        {
            TextInputSettingsBase base2 = this.CreateTextInputSettings();
            if ((this.TextInputSettings == null) || (this.TextInputSettings.GetType() != base2.GetType()))
            {
                this.TextInputSettings = base2;
            }
            this.TextInputSettings.AssignProperties();
        }

        protected TextEditStrategy EditStrategy
        {
            get => 
                base.EditStrategy as TextEditStrategy;
            set => 
                base.EditStrategy = value;
        }

        protected internal EditBoxWrapper EditBox { get; private set; }

        protected internal bool IsInImeInput { get; private set; }

        public string EditText =>
            this.EditBox.Text;

        public Brush NullTextForeground
        {
            get => 
                (Brush) base.GetValue(NullTextForegroundProperty);
            set => 
                base.SetValue(NullTextForegroundProperty, value);
        }

        [Description(""), Browsable(false)]
        public ControlTemplate EditNonEditableTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(EditNonEditableTemplateProperty);
            set => 
                base.SetValue(EditNonEditableTemplateProperty, value);
        }

        [Category("CommonProperties"), Description("Gets or sets the editor's text. This is a dependency property.")]
        public string Text
        {
            get => 
                (string) base.GetValue(TextProperty);
            set => 
                base.SetValue(TextProperty, value);
        }

        [Description("Gets or sets whether the text wraps when it reaches the edge of the text box. This is a dependency property."), Category("Text ")]
        public System.Windows.TextWrapping TextWrapping
        {
            get => 
                (System.Windows.TextWrapping) base.GetValue(TextWrappingProperty);
            set => 
                base.SetValue(TextWrappingProperty, value);
        }

        [Description("Gets or sets whether a cell's value is automatically wrapped when it is printed. This is a dependency property.")]
        public System.Windows.TextWrapping? PrintTextWrapping
        {
            get => 
                (System.Windows.TextWrapping?) base.GetValue(PrintTextWrappingProperty);
            set => 
                base.SetValue(PrintTextWrappingProperty, value);
        }

        [Description("Gets or sets whether a horizontal scroll bar is shown. This is a dependency property.")]
        public ScrollBarVisibility HorizontalScrollBarVisibility
        {
            get => 
                (ScrollBarVisibility) base.GetValue(HorizontalScrollBarVisibilityProperty);
            set => 
                base.SetValue(HorizontalScrollBarVisibilityProperty, value);
        }

        [Description("Gets or sets whether a vertical scroll bar is shown. This is a dependency property.")]
        public ScrollBarVisibility VerticalScrollBarVisibility
        {
            get => 
                (ScrollBarVisibility) base.GetValue(VerticalScrollBarVisibilityProperty);
            set => 
                base.SetValue(VerticalScrollBarVisibilityProperty, value);
        }

        [Description("Gets or sets the maximum number of characters an end-user can enter into the editor. This is a dependency property.")]
        public int MaxLength
        {
            get => 
                (int) base.GetValue(MaxLengthProperty);
            set => 
                base.SetValue(MaxLengthProperty, value);
        }

        [Category("Behavior"), Description("Gets or sets whether to select the entire text when the editor gets focus via keyboard. This is a dependency property.")]
        public bool SelectAllOnGotFocus
        {
            get => 
                (bool) base.GetValue(SelectAllOnGotFocusProperty);
            set => 
                base.SetValue(SelectAllOnGotFocusProperty, value);
        }

        [Category("Behavior")]
        public bool SelectAllOnMouseUp
        {
            get => 
                (bool) base.GetValue(SelectAllOnMouseUpProperty);
            set => 
                base.SetValue(SelectAllOnMouseUpProperty, value);
        }

        [Description("Gets or sets whether an end-user can insert tabulation characters into a text. This is a dependency property."), Category("Text ")]
        public bool AcceptsTab
        {
            get => 
                (bool) base.GetValue(AcceptsTabProperty);
            set => 
                base.SetValue(AcceptsTabProperty, value);
        }

        [Description("Gets or sets the text trimming behavior. This is a dependency property."), Category("Text ")]
        public System.Windows.TextTrimming TextTrimming
        {
            get => 
                (System.Windows.TextTrimming) base.GetValue(TextTrimmingProperty);
            set => 
                base.SetValue(TextTrimmingProperty, value);
        }

        [Description("Gets or sets whether to invoke a tooltip for the editor whose content is trimmed. This is a dependency property.")]
        public bool ShowTooltipForTrimmedText
        {
            get => 
                (bool) base.GetValue(ShowTooltipForTrimmedTextProperty);
            set => 
                base.SetValue(ShowTooltipForTrimmedTextProperty, value);
        }

        [Category("Text "), Description("Gets or sets whether an end-user can insert return characters into a text. This is a dependency property.")]
        public bool AcceptsReturn
        {
            get => 
                (bool) base.GetValue(AcceptsReturnProperty);
            set => 
                base.SetValue(AcceptsReturnProperty, value);
        }

        [Description("Gets the total number of text lines.")]
        public int LineCount
        {
            get
            {
                Func<EditBoxWrapper, int> evaluator = <>c.<>9__103_0;
                if (<>c.<>9__103_0 == null)
                {
                    Func<EditBoxWrapper, int> local1 = <>c.<>9__103_0;
                    evaluator = <>c.<>9__103_0 = x => x.LineCount;
                }
                return this.EditBox.Return<EditBoxWrapper, int>(evaluator, (<>c.<>9__103_1 ??= () => 0));
            }
        }

        public TextInputSettingsBase TextInputSettings
        {
            get => 
                (TextInputSettingsBase) base.GetValue(TextInputSettingsProperty);
            protected set => 
                base.SetValue(TextInputSettingsProperty, value);
        }

        private TextEditPropertyProvider PropertyProvider =>
            (TextEditPropertyProvider) base.PropertyProvider;

        internal bool IsBrowserPasteCommandExecuted { get; set; }

        private bool IsMousePressed =>
            (Mouse.LeftButton == MouseButtonState.Pressed) || (Mouse.RightButton == MouseButtonState.Pressed);

        internal bool CanSelectAllOnGotFocus =>
            this.SelectAllOnGotFocus && (base.EditMode == EditMode.Standalone);

        HorizontalAlignment ITextExportSettings.HorizontalAlignment =>
            (base.HorizontalContentAlignment != HorizontalAlignment.Stretch) ? base.HorizontalContentAlignment : ExportSettingDefaultValue.HorizontalAlignment;

        VerticalAlignment ITextExportSettings.VerticalAlignment =>
            (base.VerticalContentAlignment != VerticalAlignment.Stretch) ? base.VerticalContentAlignment : ExportSettingDefaultValue.VerticalAlignment;

        string ITextExportSettings.Text =>
            this.GetExportText();

        object ITextExportSettings.TextValue =>
            this.GetTextValueInternal();

        string ITextExportSettings.TextValueFormatString =>
            base.DisplayFormatString;

        FontFamily ITextExportSettings.FontFamily =>
            base.FontFamily;

        FontStyle ITextExportSettings.FontStyle =>
            base.FontStyle;

        FontWeight ITextExportSettings.FontWeight =>
            base.FontWeight;

        double ITextExportSettings.FontSize =>
            base.FontSize;

        Thickness ITextExportSettings.Padding =>
            base.Padding;

        System.Windows.TextWrapping ITextExportSettings.TextWrapping =>
            this.GetActualTextWrapping();

        bool ITextExportSettings.NoTextExport =>
            ExportSettingDefaultValue.NoTextExport;

        bool? ITextExportSettings.XlsExportNativeFormat =>
            this.GetXlsExportNativeFormatInternal();

        string ITextExportSettings.XlsxFormatString =>
            ExportSettingDefaultValue.XlsxFormatString;

        TextDecorationCollection ITextExportSettings.TextDecorations =>
            this.GetPrintTextDecorations();

        System.Windows.TextTrimming ITextExportSettings.TextTrimming =>
            this.TextTrimming;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TextEditBase.<>c <>9 = new TextEditBase.<>c();
            public static Func<EditBoxWrapper, int> <>9__103_0;
            public static Func<int> <>9__103_1;
            public static Func<FrameworkElement, bool> <>9__170_0;

            internal void <.cctor>b__16_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TextEditBase) d).OnShowTooltipForTrimmedTextChanged();
            }

            internal void <.cctor>b__16_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TextEditBase) d).TextInputSettingsChanged((TextInputSettingsBase) e.OldValue, (TextInputSettingsBase) e.NewValue);
            }

            internal void <.cctor>b__16_10(object d, CanExecuteRoutedEventArgs e)
            {
                ((TextEditBase) d).CanCopy(d, e);
            }

            internal void <.cctor>b__16_11(object d, ExecutedRoutedEventArgs e)
            {
                ((TextEditBase) d).Paste();
            }

            internal void <.cctor>b__16_12(object d, CanExecuteRoutedEventArgs e)
            {
                ((TextEditBase) d).CanPaste(d, e);
            }

            internal void <.cctor>b__16_13(object d, ExecutedRoutedEventArgs e)
            {
                ((TextEditBase) d).Undo();
            }

            internal void <.cctor>b__16_14(object d, CanExecuteRoutedEventArgs e)
            {
                ((TextEditBase) d).CanUndo(d, e);
            }

            internal void <.cctor>b__16_15(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TextEditBase) d).OnBackgroundChanged();
            }

            internal void <.cctor>b__16_16(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TextEditBase) d).OnFocusableChanged();
            }

            internal void <.cctor>b__16_17(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TextEditBase) d).OnHorizontalContentAlignmentChanged();
            }

            internal void <.cctor>b__16_18(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TextEditBase) d).OnPaddingChanged();
            }

            internal void <.cctor>b__16_19(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TextEditBase) d).OnNullTextForegroundChanged();
            }

            internal void <.cctor>b__16_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TextEditBase) d).OnTextTrimmingChanged();
            }

            internal void <.cctor>b__16_3(object d, ExecutedRoutedEventArgs e)
            {
                ((TextEditBase) d).SelectAll();
            }

            internal void <.cctor>b__16_4(object d, CanExecuteRoutedEventArgs e)
            {
                ((TextEditBase) d).CanSelectAll(d, e);
            }

            internal void <.cctor>b__16_5(object d, ExecutedRoutedEventArgs e)
            {
                ((TextEditBase) d).Cut();
            }

            internal void <.cctor>b__16_6(object d, CanExecuteRoutedEventArgs e)
            {
                ((TextEditBase) d).CanCut(d, e);
            }

            internal void <.cctor>b__16_7(object d, ExecutedRoutedEventArgs e)
            {
                ((TextEditBase) d).Delete();
            }

            internal void <.cctor>b__16_8(object d, CanExecuteRoutedEventArgs e)
            {
                ((TextEditBase) d).CanDelete(d, e);
            }

            internal void <.cctor>b__16_9(object d, ExecutedRoutedEventArgs e)
            {
                ((TextEditBase) d).Copy();
            }

            internal int <get_LineCount>b__103_0(EditBoxWrapper x) => 
                x.LineCount;

            internal int <get_LineCount>b__103_1() => 
                0;

            internal bool <PerformKeyboardSelectAll>b__170_0(FrameworkElement x) => 
                x.IsMouseOver;
        }
    }
}

