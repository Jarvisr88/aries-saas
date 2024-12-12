namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Editors.Validation.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;

    [DXToolboxBrowsable(false)]
    public class TokenEditor : Control, IScrollBarThumbDragDeltaListener
    {
        public static readonly DependencyProperty EditValueProperty;
        public static readonly DependencyProperty ActiveEditorProperty;
        public static readonly DependencyProperty IsTextEditableProperty;
        public static readonly DependencyProperty IsReadOnlyProperty;
        public static readonly DependencyProperty FocusPopupOnOpenProperty;
        private static readonly DependencyPropertyKey ActiveEditorPropertyKey;
        public static readonly DependencyProperty EnableTokenWrappingProperty;
        public static readonly DependencyProperty TokenBorderTemplateProperty;
        public static readonly DependencyProperty ShowTokenButtonsProperty;
        public static readonly DependencyProperty TokenButtonsProperty;
        public static readonly DependencyProperty NewTokenPositionProperty;
        public static readonly DependencyProperty TokenTextTrimmingProperty;
        public static readonly DependencyProperty TokenMaxWidthProperty;
        public static readonly DependencyProperty EditModeProperty;
        public static readonly DependencyProperty AllowEditTokensProperty;
        public static readonly DependencyProperty CharacterCasingProperty;
        public static readonly DependencyProperty NullTextForegroundProperty;
        public static readonly DependencyProperty MaxTextLengthProperty;
        public static readonly DependencyProperty EditBehaviorProperty;
        private static readonly DependencyPropertyKey SelectedTokensPropertyKey;
        public static readonly DependencyProperty SelectedTokensProperty;
        public static readonly DependencyProperty DeleteTokenButtonTemplateProperty;
        public static readonly DependencyProperty TokenStyleProperty;
        public static readonly DependencyProperty NewTokenTextProperty;
        public static readonly DependencyProperty OwnerTokenEditorProperty;
        private static readonly DependencyPropertyKey OwnerTokenEditorPropertyKey;
        private Locker changeFocusedTokenLocker = new Locker();
        private Locker processEditValueLocker = new Locker();
        private Locker editValueLocker = new Locker();
        private Locker selectionLocker = new Locker();
        private Locker activateTokenLocker = new Locker();
        private Func<IList<object>> _getValueBeforeAcceptPopup;
        private string[] newLineSplitters = new string[] { "\r\n", "\r", "\n" };
        private TokenEditorPanel tokenEditorPanel;
        private ButtonInfo deleteButtonInfo;
        private TokenEditorKeyboardHelper keyboardHelper;
        private TokenEditorSelection selection;
        private TokenEditorPresenter focusedToken;
        private InplaceEditorOwnerBase cellEditorOwner;
        private EventHandler<EventArgs> textChanged;
        private EventHandler<EventArgs> valueChanged;
        private EventHandler<EventArgs> tokenClosed;
        private Action postponeOnGotFocusAction;
        private Action postponeSyncWithValue;

        public event EventHandler<EventArgs> TextChanged
        {
            add
            {
                this.textChanged += value;
            }
            remove
            {
                this.textChanged -= value;
            }
        }

        public event EventHandler<EventArgs> TokenClosed
        {
            add
            {
                this.tokenClosed += value;
            }
            remove
            {
                this.tokenClosed -= value;
            }
        }

        public event EventHandler<EventArgs> ValueChanged
        {
            add
            {
                this.valueChanged += value;
            }
            remove
            {
                this.valueChanged -= value;
            }
        }

        static TokenEditor()
        {
            Type forType = typeof(TokenEditor);
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(forType));
            EditValueProperty = DependencyProperty.Register("EditValue", typeof(object), forType, new FrameworkPropertyMetadata((d, e) => ((TokenEditor) d).OnEditValueChanged(e.OldValue, e.NewValue)));
            ActiveEditorPropertyKey = DependencyProperty.RegisterReadOnly("ActiveEditor", typeof(ButtonEdit), forType, new FrameworkPropertyMetadata(null, (d, e) => ((TokenEditor) d).OnActiveEditorChanged()));
            ActiveEditorProperty = ActiveEditorPropertyKey.DependencyProperty;
            IsTextEditableProperty = DependencyProperty.Register("IsTextEditable", typeof(bool), forType);
            IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly", typeof(bool), forType, new FrameworkPropertyMetadata(false, (d, e) => ((TokenEditor) d).OnIsReadOnlyChanged()));
            FocusPopupOnOpenProperty = DependencyProperty.Register("FocusPopupOnOpen", typeof(bool), forType);
            EnableTokenWrappingProperty = DependencyProperty.Register("EnableTokenWrapping", typeof(bool), forType);
            TokenBorderTemplateProperty = DependencyProperty.Register("TokenBorderTemplate", typeof(ControlTemplate), forType, new FrameworkPropertyMetadata(null, (d, e) => ((TokenEditor) d).OnTokenBorderTemplateChanged(), (d, e) => ((TokenEditor) d).OnCoerceTokenBorderTemplate(e as ControlTemplate)));
            TokenButtonsProperty = DependencyProperty.Register("TokenButtons", typeof(ButtonInfoCollection), forType, new FrameworkPropertyMetadata(null, (d, e) => ((TokenEditor) d).OnTokenButtonsChanged((ButtonInfoCollection) e.OldValue, (ButtonInfoCollection) e.NewValue)));
            ShowTokenButtonsProperty = DependencyProperty.Register("ShowTokenButtons", typeof(bool), forType, new FrameworkPropertyMetadata(true, (d, e) => ((TokenEditor) d).OnShowTokenButtonsChanged()));
            NewTokenPositionProperty = DependencyProperty.Register("NewTokenPosition", typeof(DevExpress.Xpf.Editors.NewTokenPosition), forType, new FrameworkPropertyMetadata(DevExpress.Xpf.Editors.NewTokenPosition.Near, (d, e) => ((TokenEditor) d).OnNewTokenPositionChanged()));
            TokenTextTrimmingProperty = DependencyProperty.Register("TokenTextTrimming", typeof(TextTrimming), forType);
            TokenMaxWidthProperty = DependencyProperty.Register("TokenMaxWidth", typeof(double), forType);
            EditModeProperty = DependencyProperty.Register("EditMode", typeof(DevExpress.Xpf.Editors.EditMode), forType);
            AllowEditTokensProperty = DependencyProperty.Register("AllowEditTokens", typeof(bool), forType, new FrameworkPropertyMetadata(true));
            CharacterCasingProperty = DependencyProperty.Register("CharacterCasing", typeof(System.Windows.Controls.CharacterCasing), forType, new FrameworkPropertyMetadata(System.Windows.Controls.CharacterCasing.Normal));
            NullTextForegroundProperty = DependencyProperty.Register("NullTextForeground", typeof(Brush), forType);
            MaxTextLengthProperty = DependencyProperty.Register("MaxTextLength", typeof(int), forType);
            SelectedTokensPropertyKey = DependencyProperty.RegisterReadOnly("SelectedTokens", typeof(ObservableCollection<object>), forType, new FrameworkPropertyMetadata(null));
            SelectedTokensProperty = SelectedTokensPropertyKey.DependencyProperty;
            EditBehaviorProperty = DependencyProperty.Register("EditBehavior", typeof(ITokenEditorBehavior), forType);
            DeleteTokenButtonTemplateProperty = DependencyProperty.Register("DeleteTokenButtonTemplate", typeof(DataTemplate), forType);
            CommandManager.RegisterClassCommandBinding(forType, new CommandBinding(ApplicationCommands.Copy, (d, e) => ((TokenEditor) d).CopyTokens(), (d, e) => ((TokenEditor) d).CanCopy(d, e)));
            CommandManager.RegisterClassCommandBinding(forType, new CommandBinding(ApplicationCommands.Paste, (d, e) => ((TokenEditor) d).PasteTokens(), (d, e) => ((TokenEditor) d).CanPaste(d, e)));
            TokenStyleProperty = DependencyProperty.Register("TokenStyle", typeof(Style), forType, new FrameworkPropertyMetadata(null, (d, e) => ((TokenEditor) d).OnTokenStyleChanged()));
            NewTokenTextProperty = DependencyProperty.Register("NewTokenText", typeof(string), forType, new FrameworkPropertyMetadata(null, (d, e) => ((TokenEditor) d).OnNewTokenTextChanged()));
            OwnerTokenEditorPropertyKey = DependencyProperty.RegisterAttachedReadOnly("OwnerTokenEditor", forType, forType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
            OwnerTokenEditorProperty = OwnerTokenEditorPropertyKey.DependencyProperty;
        }

        public TokenEditor()
        {
            SetOwnerTokenEditor(this, this);
            base.Loaded += new RoutedEventHandler(this.TokenEditorLoaded);
            base.Unloaded += new RoutedEventHandler(this.TokenEditorUnloaded);
            base.LayoutUpdated += new EventHandler(this.TokenEditorLayoutUpdated);
            this.ImmediateActionsManager = new DevExpress.Xpf.Editors.ImmediateActionsManager(this);
            this.SelectedTokens = new ObservableCollection<object>();
            this.SelectedTokens.CollectionChanged += new NotifyCollectionChangedEventHandler(this.SelectedTokensCollectionChanged);
        }

        public void ActivateNewToken()
        {
            if (FocusHelper.IsKeyboardFocusWithin(this) && ((this.NewToken != null) && !this.NewToken.IsEditorActivated))
            {
                this.ActivateNewTokenCore();
            }
        }

        private void ActivateNewTokenCore()
        {
            if ((this.NewToken != null) && this.ShowNewToken)
            {
                if (this.IsNewTokenFocused())
                {
                    this.ActivateToken(this.NewToken, true);
                }
                else
                {
                    this.SetFocusedAndActivateToken(this.NewToken);
                }
            }
        }

        public void ActivateToken(int index)
        {
            if ((this.tokenEditorPanel != null) && this.CanActivateToken())
            {
                TokenEditorPresenter tokenByVisibleIndex = this.tokenEditorPanel.GetTokenByVisibleIndex(index);
                if (tokenByVisibleIndex != null)
                {
                    if (!ReferenceEquals(this.FocusedToken, tokenByVisibleIndex))
                    {
                        this.SetFocusedAndActivateToken(tokenByVisibleIndex);
                    }
                    else
                    {
                        this.ActivateToken(tokenByVisibleIndex, true);
                    }
                }
            }
        }

        private bool ActivateToken(TokenEditorPresenter token, bool showEditor = true)
        {
            if (!this.CanActivateToken())
            {
                return false;
            }
            this.activateTokenLocker.LockOnce();
            if (this.ProcessTokenActivating(token))
            {
                return false;
            }
            this.tokenEditorPanel.BringIntoView(token);
            token.IsEditorActivated = true;
            if (showEditor)
            {
                Action<CellEditor> action = <>c.<>9__381_0;
                if (<>c.<>9__381_0 == null)
                {
                    Action<CellEditor> local1 = <>c.<>9__381_0;
                    action = <>c.<>9__381_0 = delegate (CellEditor x) {
                        x.ShowEditor(true);
                    };
                }
                token.InplaceEditor.Do<CellEditor>(action);
                token.FocusEditCore();
            }
            return true;
        }

        public void AfterAcceptPopupValue()
        {
            this.ProcessTokensChangedOnAcceptPopupValue();
            if (this.IsNewTokenFocused())
            {
                this.ImmediateActionsManager.EnqueueAction(new Action(this.ActivateNewToken));
            }
        }

        private void AfterTokenRemoved(TokenEditorPresenter token, List<object> removedTokens)
        {
            this.editValueLocker.DoLockedAction(new Action(this.RaiseValueChanged));
            this.UpdateTokens();
            if (!this.IsNewTokenFocused())
            {
                if (this.HasActiveEditor)
                {
                    this.FocusedToken.CommitEditor();
                }
                this.ResetFocusedToken();
                int visibleIndex = this.tokenEditorPanel.GetVisibleIndex(token);
                if (this.tokenEditorPanel.Items.Count == 0)
                {
                    visibleIndex = this.tokenEditorPanel.GetVisibleIndex(this.NewToken);
                }
                this.SetFocusedAndActivateTokenByIndex(visibleIndex, false);
            }
            if (this.FocusedToken != null)
            {
                this.FocusedToken.FocusEditCore();
            }
            if (removedTokens != null)
            {
                this.ProcessTokensRemoved(removedTokens);
            }
        }

        public void BeforeAcceptPopupValue()
        {
            List<object> list1;
            IEnumerable<object> valueExceptEditableTokens = this.GetValueExceptEditableTokens();
            if (valueExceptEditableTokens != null)
            {
                list1 = valueExceptEditableTokens.ToList<object>();
            }
            else
            {
                IEnumerable<object> local1 = valueExceptEditableTokens;
                list1 = null;
            }
            List<object> value = list1;
            this._getValueBeforeAcceptPopup = () => value;
        }

        internal void BeforeCancelEdit()
        {
            if (this.IsNewTokenFocused() && ((this.EditValueInternal != null) && this.RemoveValueByIndex(this.EditableTokenIndex)))
            {
                this.RaiseValueChanged();
            }
        }

        internal bool CanActivateToken() => 
            this.CanEditing && (this.IsNewTokenFocused() || this.AllowEditTokens);

        public void CancelActiveToken()
        {
            if (this.ActiveEditor != null)
            {
                this.FocusedToken.InplaceEditor.CancelEditInVisibleEditor();
            }
        }

        private void CanCopy(object d, CanExecuteRoutedEventArgs e)
        {
            this.SetCanExecuteParameters(e, true);
        }

        private void CanPaste(object d, CanExecuteRoutedEventArgs e)
        {
            this.SetCanExecuteParameters(e, this.IsTextEditable && !this.IsReadOnly);
        }

        private bool CanProcessMouseDown() => 
            (this.tokenEditorPanel != null) && ((this.tokenEditorPanel.Items.Count != 0) && this.tokenEditorPanel.HasMeasuredTokens);

        internal bool CanRemoveToken() => 
            !this.IsReadOnly && this.HasOwnerEdit;

        private void ChangeFocusedTokenOnMouseDown(TokenEditorPresenter token, MouseButtonEventArgs e)
        {
            this.tokenEditorPanel.LockBringIntoView = true;
            this.FocusedToken = token;
            if (!this.ActivateToken(this.FocusedToken, false))
            {
                this.tokenEditorPanel.LockBringIntoView = false;
            }
            else
            {
                this.activateTokenLocker.DoLockedAction(() => this.CellEditorOwner.ProcessMouseLeftButtonDown(e));
                this.tokenEditorPanel.LockBringIntoView = false;
                this.ImmediateActionsManager.EnqueueAction(() => this.tokenEditorPanel.BringIntoView(this.FocusedToken));
            }
        }

        internal void ClearAndRemoveNewTokenValue(bool shouldRaiseEvent = true)
        {
            if (this.EditableTokenIndex >= 0)
            {
                this.processEditValueLocker.DoLockedAction(() => this.tokenEditorPanel.ClearNewTokenValue());
                this.RemoveValueByIndex(this.EditableTokenIndex);
                if (shouldRaiseEvent)
                {
                    this.RaiseValueChanged();
                }
            }
        }

        private void ClearEditableTokens()
        {
            Action<TokenEditorCustomItem> action = <>c.<>9__236_0;
            if (<>c.<>9__236_0 == null)
            {
                Action<TokenEditorCustomItem> local1 = <>c.<>9__236_0;
                action = <>c.<>9__236_0 = x => x.EditableTokens = new List<int>();
            }
            this.EditValueInternal.Do<TokenEditorCustomItem>(action);
        }

        public void CommitActiveEditor()
        {
            if (this.tokenEditorPanel != null)
            {
                if (this.IsNewTokenFocused())
                {
                    this.processEditValueLocker.DoLockedAction(() => this.tokenEditorPanel.ClearNewTokenValue());
                }
                if (this.HasFocusedToken)
                {
                    this.FocusedToken.CommitEditor();
                }
            }
        }

        public void Copy()
        {
            if (this.HasActiveEditor)
            {
                this.ActiveEditor.Copy();
            }
            else
            {
                this.CopyTokens();
            }
        }

        private void CopyTokens()
        {
            DXClipboard.SetText(this.GetSelectedTokensText());
        }

        private void CreateActualNewTokenEditSettings()
        {
            // Unresolved stack state at '00000053'
        }

        private void CreateActualTokenEditSettings()
        {
            // Unresolved stack state at '000000A2'
        }

        private InplaceEditorOwnerBase CreateCellEditorOwner() => 
            new DevExpress.Xpf.Editors.Internal.CellEditorOwner(this);

        private ButtonInfo CreateDeleteButtonInfo() => 
            new ButtonInfo { 
                Template = this.DeleteTokenButtonTemplate,
                Command = this.CreateDeleteCommand(),
                RaiseClickEventInInplaceInactiveMode = true
            };

        private ICommand CreateDeleteCommand() => 
            new DelegateCommand<Button>(delegate (Button x) {
                this.DeleteCommandImpl(x);
            }, x => this.CanRemoveToken(), true);

        private void CreateEditSettings()
        {
            this.CreateActualNewTokenEditSettings();
            this.CreateActualTokenEditSettings();
        }

        private TokenEditorKeyboardHelper CreateKeyboardHelper()
        {
            this.keyboardHelper = new TokenEditorKeyboardHelper(this);
            return this.keyboardHelper;
        }

        private TokenEditorSelection CreateSelection()
        {
            this.selection = new TokenEditorSelection(this);
            return this.selection;
        }

        private void CreateTokenEditorPanel()
        {
            ScrollViewer viewer = LayoutHelper.FindElementByName(this, "PART_ScrollViewer") as ScrollViewer;
            if (viewer != null)
            {
                this.tokenEditorPanel = viewer.Content as TokenEditorPanel;
                this.tokenEditorPanel.Owner = this;
                this.tokenEditorPanel.ScrollOwner = viewer;
                viewer.ScrollChanged += new ScrollChangedEventHandler(this.OnScrollChanged);
                this.UpdateTokens();
            }
        }

        public void Cut()
        {
            if (this.HasActiveEditor)
            {
                this.ActiveEditor.Cut();
            }
        }

        private void DeleteCommandImpl(Button sender)
        {
            if (sender != null)
            {
                TokenEditorPresenter token = LayoutHelper.FindLayoutOrVisualParentObject<TokenEditorPresenter>(sender, false, null);
                if (token != null)
                {
                    this.RemoveToken(token);
                }
            }
        }

        void IScrollBarThumbDragDeltaListener.OnScrollBarThumbDragDelta(DragDeltaEventArgs e)
        {
            if (this.tokenEditorPanel != null)
            {
                this.tokenEditorPanel.OnThumbDragDelta(e);
            }
        }

        void IScrollBarThumbDragDeltaListener.OnScrollBarThumbMouseMove(MouseEventArgs e)
        {
            if (this.tokenEditorPanel != null)
            {
                this.tokenEditorPanel.OnThumbMouseMove(e);
            }
        }

        internal int EditableIndexOfToken(TokenEditorPresenter token) => 
            this.tokenEditorPanel.GetEditableIndex(token);

        private int FindNewFocusedTokenOnVerticalNavigation(TokenEditorLineInfo line)
        {
            Rect relativeElementRect = LayoutHelper.GetRelativeElementRect(this.FocusedToken, this);
            List<int> lineIndexes = new List<int>();
            line.Tokens.ForEach(delegate (TokenInfo x) {
                lineIndexes.Add(x.VisibleIndex);
            });
            lineIndexes.Reverse();
            int num = -1;
            Rect maxBounds = new Rect();
            foreach (int num2 in lineIndexes)
            {
                TokenEditorPresenter container = this.tokenEditorPanel.GetContainer(this.tokenEditorPanel.ConvertToEditableIndex(num2)) as TokenEditorPresenter;
                if (container != null)
                {
                    Rect targetBounds = LayoutHelper.GetRelativeElementRect(container, this);
                    Rect rect = new Rect(relativeElementRect.X, targetBounds.Y, relativeElementRect.Width, targetBounds.Height);
                    targetBounds.Intersect(rect);
                    if (this.IsGreaterBounds(targetBounds, maxBounds))
                    {
                        maxBounds = targetBounds;
                        num = num2;
                    }
                }
            }
            if ((num == -1) && (lineIndexes.Count > 0))
            {
                num = lineIndexes[0];
            }
            return num;
        }

        private List<object> GetAddedTokens()
        {
            List<object> addedTokens = new List<object>();
            this.EditValueInternal.EditableTokens.ForEach(delegate (int x) {
                addedTokens.Add(this.GetTokenValue(x));
            });
            return addedTokens;
        }

        public int GetCharacterIndexFromLineIndex(int lineIndex) => 
            this.HasActiveEditor ? this.ActiveEditor.GetCharacterIndexFromLineIndex(lineIndex) : -1;

        public int GetCharacterIndexFromPoint(Point point, bool snapToText) => 
            this.HasActiveEditor ? this.ActiveEditor.GetCharacterIndexFromPoint(point, snapToText) : -1;

        internal ButtonEditSettings GetEditSettings(TokenEditorPresenter token)
        {
            if ((this.ActualNewTokenEditSettings == null) || (this.ActualTokenEditSettings == null))
            {
                this.CreateEditSettings();
            }
            return (this.IsNewToken(token) ? this.ActualNewTokenEditSettings : this.ActualTokenEditSettings);
        }

        public int GetFirstVisibleLineIndex() => 
            this.HasActiveEditor ? this.ActiveEditor.GetFirstVisibleLineIndex() : -1;

        private IList<CustomItem> GetInnerEditValue()
        {
            Func<TokenEditorCustomItem, IList<CustomItem>> evaluator = <>c.<>9__342_0;
            if (<>c.<>9__342_0 == null)
            {
                Func<TokenEditorCustomItem, IList<CustomItem>> local1 = <>c.<>9__342_0;
                evaluator = <>c.<>9__342_0 = x => x.EditValue as IList<CustomItem>;
            }
            return this.EditValueInternal.Return<TokenEditorCustomItem, IList<CustomItem>>(evaluator, (<>c.<>9__342_1 ??= ((Func<IList<CustomItem>>) (() => null))));
        }

        private List<UIElement> GetInplaceEditorContainers() => 
            this.tokenEditorPanel.GetInplaceEditorContainers();

        public int GetLastVisibleLineIndex() => 
            this.HasActiveEditor ? this.ActiveEditor.GetLastVisibleLineIndex() : -1;

        public int GetLineIndexFromCharacterIndex(int charIndex) => 
            this.HasActiveEditor ? this.ActiveEditor.GetLineIndexFromCharacterIndex(charIndex) : -1;

        public int GetLineLength(int lineIndex) => 
            this.HasActiveEditor ? this.ActiveEditor.GetLineLength(lineIndex) : 0;

        public string GetLineText(int lineIndex) => 
            this.HasActiveEditor ? this.ActiveEditor.GetLineText(lineIndex) : string.Empty;

        internal string GetNullText()
        {
            Func<TokenEditorCustomItem, string> evaluator = <>c.<>9__260_0;
            if (<>c.<>9__260_0 == null)
            {
                Func<TokenEditorCustomItem, string> local1 = <>c.<>9__260_0;
                evaluator = <>c.<>9__260_0 = x => string.IsNullOrEmpty(x.NullText) ? null : x.NullText;
            }
            return this.EditValueInternal.Return<TokenEditorCustomItem, string>(evaluator, (<>c.<>9__260_1 ??= ((Func<string>) (() => null))));
        }

        public static TokenEditor GetOwnerTokenEditor(DependencyObject element) => 
            (element != null) ? ((TokenEditor) element.GetValue(OwnerTokenEditorProperty)) : null;

        internal List<object> GetSelectedTokens()
        {
            List<object> tokens = new List<object>();
            IList<CustomItem> listValue = this.GetInnerEditValue();
            if (listValue != null)
            {
                this.Selection.SelectedTokensIndexes.ForEach(delegate (int x) {
                    tokens.Add(listValue[x]);
                });
            }
            return tokens;
        }

        internal string GetSelectedTokensText()
        {
            List<object> selectedTokens = this.GetSelectedTokens();
            if (selectedTokens.Count == 0)
            {
                return string.Empty;
            }
            string result = "";
            string separator = this.OwnerEdit.SeparatorString;
            selectedTokens.ForEach(delegate (object x) {
                result = result + ((CustomItem) x).DisplayText + separator;
            });
            return result.Remove(result.LastIndexOf(separator), separator.Length);
        }

        internal int GetTokenByHorizontalOffset() => 
            this.tokenEditorPanel.OffsetToIndex(this.tokenEditorPanel.HorizontalOffset);

        private TokenEditorPresenter GetTokenByVisibleIndex(int visibleIndex) => 
            this.tokenEditorPanel.GetTokenByVisibleIndex(visibleIndex);

        internal TokenEditorPanel GetTokenEditorPanel() => 
            this.tokenEditorPanel;

        private object GetTokenValue(int index)
        {
            Func<TokenEditorCustomItem, object> evaluator = <>c.<>9__360_0;
            if (<>c.<>9__360_0 == null)
            {
                Func<TokenEditorCustomItem, object> local1 = <>c.<>9__360_0;
                evaluator = <>c.<>9__360_0 = x => x.EditValue;
            }
            IList<CustomItem> list = (IList<CustomItem>) this.EditValueInternal.Return<TokenEditorCustomItem, object>(evaluator, (<>c.<>9__360_1 ??= () => null));
            if ((list == null) || ((list.Count <= index) || (index == -1)))
            {
                return null;
            }
            CustomItem item = list[index];
            return ((item.EditValue is LookUpEditableItem) ? ((LookUpEditableItem) item.EditValue).EditValue : item.EditValue);
        }

        private IEnumerable<object> GetValueExceptEditableTokens()
        {
            List<int> editableTokens;
            IList<object> value = this.GetValueFromCustomItem(this.EditValueInternal);
            TokenEditorCustomItem editValueInternal = this.EditValueInternal;
            if (editValueInternal != null)
            {
                editableTokens = editValueInternal.EditableTokens;
            }
            else
            {
                TokenEditorCustomItem local1 = editValueInternal;
                editableTokens = null;
            }
            if ((editableTokens == null) || (value == null))
            {
                return null;
            }
            foreach (object obj2 in from x in this.EditValueInternal.EditableTokens
                where (x > -1) && (x < value.Count)
                select value[x])
            {
                value.Remove(obj2);
            }
            return value;
        }

        private CustomItem GetValueForEditableItem(IList<CustomItem> listEditValue)
        {
            CustomItem item = ((this.EditableTokenIndex <= -1) || (this.EditableTokenIndex >= listEditValue.Count)) ? null : listEditValue[this.EditableTokenIndex];
            return ((item != null) ? item : new CustomItem());
        }

        private IList<object> GetValueFromCustomItem(TokenEditorCustomItem item)
        {
            IList<CustomItem> source = ((item != null) ? ((IList<CustomItem>) item.EditValue) : null) as IList<CustomItem>;
            if (source == null)
            {
                return null;
            }
            Func<CustomItem, object> selector = <>c.<>9__228_0;
            if (<>c.<>9__228_0 == null)
            {
                Func<CustomItem, object> local1 = <>c.<>9__228_0;
                selector = <>c.<>9__228_0 = x => x.EditValue;
            }
            Func<object, bool> predicate = <>c.<>9__228_1;
            if (<>c.<>9__228_1 == null)
            {
                Func<object, bool> local2 = <>c.<>9__228_1;
                predicate = <>c.<>9__228_1 = x => x != null;
            }
            return source.Select<CustomItem, object>(selector).Where<object>(predicate).ToList<object>();
        }

        internal Dictionary<int, UIElement> GetVisibleTokens() => 
            this.tokenEditorPanel?.GetVisibleTokens();

        private void InvalidateLayout()
        {
            base.InvalidateMeasure();
        }

        private bool InvokePostpone()
        {
            if (this.postponeOnGotFocusAction == null)
            {
                return false;
            }
            this.postponeOnGotFocusAction();
            this.postponeOnGotFocusAction = null;
            return true;
        }

        private bool IsActiveEditorHasEditBox() => 
            this.ActiveEditor.EditBox != null;

        private bool IsFocusFromCellEditor(IInputElement element) => 
            !ReferenceEquals(element, this) && (LayoutHelper.FindLayoutOrVisualParentObject<TokenEditor>((DependencyObject) element, true, null) == this);

        private bool IsGreaterBounds(Rect targetBounds, Rect maxBounds) => 
            targetBounds.Width > maxBounds.Width;

        private bool IsNewToken(TokenEditorPresenter container) => 
            ReferenceEquals(container, this.NewToken);

        private bool IsNewTokenAdded() => 
            this.IsNewTokenFocused() && ((this.EditValueInternal != null) && ((this.EditableTokenIndex != -1) && !this.IsNewTokenEmpty()));

        private bool IsNewTokenEmpty()
        {
            IList<CustomItem> editValue = this.EditValueInternal.EditValue as IList<CustomItem>;
            if (editValue == null)
            {
                return true;
            }
            CustomItem item = editValue[this.EditableTokenIndex];
            return ((item == null) || ((item.EditValue == null) && string.IsNullOrEmpty(item.DisplayText)));
        }

        internal bool IsNewTokenFocused() => 
            ReferenceEquals(this.NewToken, this.FocusedToken);

        private bool IsNewTokenIndex(int visibleIndex) => 
            this.tokenEditorPanel.IsNewTokenIndex(visibleIndex);

        private bool IsNewTokenVisible() => 
            (this.NewToken != null) ? new Rect(0.0, 0.0, base.ActualWidth, base.ActualHeight).IntersectsWith(this.NewToken.TransformToVisual(this).TransformBounds(new Rect(0.0, 0.0, this.NewToken.ActualWidth, this.NewToken.ActualHeight))) : false;

        private bool IsTextContainsSeparatorString(string text) => 
            !this.newLineSplitters.Contains<string>(this.OwnerEdit.SeparatorString) ? text.Contains(this.OwnerEdit.SeparatorString) : this.newLineSplitters.Any<string>(x => text.Contains(x));

        internal bool IsValueChanged() => 
            (this.ActiveEditor != null) && (this.ActiveEditor.EditValue != null);

        internal void MakeVisibleToken(TokenEditorPresenter token)
        {
            this.tokenEditorPanel.BringIntoView(token);
        }

        internal void NavigateToVerticalEnd()
        {
            this.tokenEditorPanel.ScrollToVerticalEnd();
            base.UpdateLayout();
            this.SetFocusedTokenByIndexWithLock(this.tokenEditorPanel.MaxVisibleIndex, false);
        }

        internal void NavigateToVerticalStart()
        {
            this.tokenEditorPanel.ScrollToVerticalStart();
            base.UpdateLayout();
            this.SetFocusedTokenByIndexWithLock(this.tokenEditorPanel.MinVisibleIndex, false);
        }

        public bool NeedsActivationKeyInInactiveMode(Key key, ModifierKeys modifiers) => 
            (this.NewToken != null) && this.NewToken.IsActivatingKey(key, modifiers);

        public bool NeedsKey(Key key, ModifierKeys modifiers) => 
            this.KeyboardHelper.NeedsKey(key, modifiers);

        private void OnActiveEditorChanged()
        {
            this.EditBehavior.Do<ITokenEditorBehavior>(x => x.SetActiveToken(this.ActiveEditor));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.CreateTokenEditorPanel();
        }

        internal bool OnCellEditorPaste()
        {
            if ((this.OwnerEdit == null) || !DXClipboard.ContainsText())
            {
                return false;
            }
            string text = DXClipboard.GetText();
            bool flag = !string.IsNullOrEmpty(text) && this.IsTextContainsSeparatorString(text);
            if (flag)
            {
                this.PasteTokens();
            }
            return flag;
        }

        protected virtual object OnCoerceTokenBorderTemplate(ControlTemplate value) => 
            value ?? this.TokenBorderTemplate;

        internal void OnEditorPreviewLostFocus(bool isEditorLostFocus)
        {
            if (isEditorLostFocus)
            {
                this.ProcessLostFocus();
            }
        }

        private void OnEditValueChanged(object oldValue, object newValue)
        {
            if (this.tokenEditorPanel != null)
            {
                this.ImmediateActionsManager.EnqueueAction(() => this.tokenEditorPanel.EnsureOffset());
            }
        }

        private void OnFocusedTokenChanged(TokenEditorPresenter newFocused)
        {
            this.SelectFocusedToken();
            this.UpdateTokenEditorsFocused(newFocused);
        }

        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnGotKeyboardFocus(e);
            this.ProcessGotFocus(e);
        }

        private void OnIsReadOnlyChanged()
        {
            this.ResetTokens();
        }

        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnLostKeyboardFocus(e);
            IInputElement focusedElement = Keyboard.FocusedElement;
            if (!this.IsEditorKeyboardFocused && ((focusedElement != null) && (this.HasOwnerEdit && (!ReferenceEquals(this.OwnerEdit, focusedElement) && (!this.OwnerEdit.IsChildElement(focusedElement as DependencyObject, null) || ((this.OwnerEdit.EditMode != DevExpress.Xpf.Editors.EditMode.Standalone) && !LayoutHelper.IsChildElement(this, focusedElement as DependencyObject)))))))
            {
                this.ProcessLostFocus();
            }
        }

        internal void OnNavigateDown(bool isShiftPressed)
        {
            this.OnVerticalNavigation(false, isShiftPressed);
        }

        internal bool OnNavigateHorizontalEnd(bool activateToken)
        {
            if (!this.ShouldScroll(this.tokenEditorPanel.MaxVisibleIndex))
            {
                this.SetFocusedTokenByIndexWithLock(this.tokenEditorPanel.MaxVisibleIndex, activateToken);
            }
            else
            {
                this.tokenEditorPanel.ScrollToHorizontalEnd();
                this.ImmediateActionsManager.EnqueueAction(delegate {
                    this.SetFocusedTokenByIndexWithLock(this.tokenEditorPanel.MaxVisibleIndex, activateToken);
                });
            }
            return true;
        }

        internal bool OnNavigateHorizontalStart(bool activateToken) => 
            this.ScrollToIndex(this.tokenEditorPanel.MinVisibleIndex, activateToken);

        internal bool OnNavigateLeft(int index, bool activateToken) => 
            this.ScrollToIndex(index, activateToken);

        internal bool OnNavigateRight(int index, bool activateToken)
        {
            if (this.ShouldScroll(index))
            {
                return this.ScrollRightAndMoveFocusedToken(index, activateToken);
            }
            this.SetFocusedTokenByIndexWithLock(index, activateToken);
            return true;
        }

        internal void OnNavigateUp(bool isShiftPressed)
        {
            this.OnVerticalNavigation(true, isShiftPressed);
        }

        protected virtual void OnNewTokenPositionChanged()
        {
            if (this.tokenEditorPanel != null)
            {
                this.tokenEditorPanel.UpdateMeasureStrategy();
                this.InvalidateLayout();
            }
        }

        private void OnNewTokenTextChanged()
        {
            this.ResetTokens();
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            if (e.Key == Key.Return)
            {
                e.Handled = true;
                if (this.postponeSyncWithValue != null)
                {
                    this.postponeSyncWithValue();
                    this.postponeSyncWithValue = null;
                }
            }
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);
            if ((this.tokenEditorPanel != null) && this.tokenEditorPanel.IsLoaded)
            {
                this.changeFocusedTokenLocker.DoLockedAction(() => this.ProcessMouseLeftButtonDown(e));
            }
            else
            {
                ReraiseAction reraiseAction = new ReraiseAction(delegate {
                    Action <>9__1;
                    Action action = <>9__1;
                    if (<>9__1 == null)
                    {
                        Action local1 = <>9__1;
                        action = <>9__1 = () => this.ProcessMouseLeftButtonDown(e);
                    }
                    this.changeFocusedTokenLocker.DoLockedAction(action);
                }, new Func<bool>(this.CanProcessMouseDown), this.ImmediateActionsManager);
                this.ImmediateActionsManager.EnqueueAction(() => reraiseAction.Perform());
            }
        }

        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            base.OnPreviewMouseWheel(e);
            this.ProcessMouseWheel((double) e.Delta);
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ReferenceEquals(e.Property, Control.ForegroundProperty))
            {
                this.HasForeground = true;
            }
        }

        public void OnRestoreDisplayText()
        {
            this.CancelActiveToken();
        }

        private void OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            this.Selection.UpdateVisibleSelection();
        }

        private void OnShowTokenButtonsChanged()
        {
            this.ResetTokens();
        }

        internal void OnStartEditing(ButtonEdit editor)
        {
            if (!base.IsLoaded)
            {
                this.ImmediateActionsManager.EnqueueAction(() => this.OnStartEditing(editor));
            }
            else
            {
                this.StartEditingCore(editor);
            }
        }

        protected virtual void OnTokenBorderTemplateChanged()
        {
        }

        protected virtual void OnTokenButtonsChanged(ButtonInfoCollection oldValue, ButtonInfoCollection newValue)
        {
        }

        internal void OnTokenHidden()
        {
            this.IsInProcessNewValue = false;
            this.OnTokenHiddenCore();
        }

        private void OnTokenHiddenCore()
        {
            this.ProcessTokenDeactivated(this.EditableIndexOfToken(this.FocusedToken));
            this.ActiveEditor = null;
            bool flag = false;
            if (!this.changeFocusedTokenLocker.IsLocked)
            {
                if (this.HasFocusedToken)
                {
                    this.FocusedToken.HideEditor();
                }
                if (this.IsNewTokenFocused() || (this.FocusedToken == null))
                {
                    if (this.IsNewTokenAdded())
                    {
                        this.ProcessTokensAdded();
                    }
                    this.TryRemoveEmptyValue();
                    this.UpdateNewToken();
                    flag = !this.IsPopupCloseInProgress;
                }
            }
            this.ClearEditableTokens();
            this.UpdateTokens();
            this.RaiseTokenClosed();
            if (flag)
            {
                this.ImmediateActionsManager.EnqueueAction(new Action(this.ActivateNewToken));
            }
        }

        private void OnTokenStyleChanged()
        {
            this.ResetTokens();
        }

        private void OnVerticalNavigation(bool up, bool isShiftPressed)
        {
            if (this.FocusedToken == null)
            {
                if (up)
                {
                    this.tokenEditorPanel.LineUp();
                }
                else
                {
                    this.tokenEditorPanel.LineDown();
                }
            }
            else
            {
                int editableIndex = this.EditableIndexOfToken(this.FocusedToken);
                int index = this.tokenEditorPanel.ConvertToVisibleIndex(editableIndex);
                TokenEditorLineInfo lineRelativeToken = this.tokenEditorPanel.GetLineRelativeToken(index, up);
                if (lineRelativeToken != null)
                {
                    int newFocusedIndex = this.FindNewFocusedTokenOnVerticalNavigation(lineRelativeToken);
                    if (newFocusedIndex != -1)
                    {
                        if (isShiftPressed)
                        {
                            this.SelectTokensOnVerticalNavigation(up, index, newFocusedIndex);
                        }
                        else
                        {
                            this.SetFocusedTokenByIndexWithLock(newFocusedIndex, false);
                        }
                        this.tokenEditorPanel.BringIntoViewByIndex(newFocusedIndex);
                    }
                }
            }
        }

        public void Paste()
        {
            if (this.HasActiveEditor)
            {
                this.ActiveEditor.Paste();
            }
            else
            {
                this.PasteTokens();
            }
        }

        private void PasteTokens()
        {
            string text = DXClipboard.GetText();
            if (!string.IsNullOrEmpty(text))
            {
                List<string> list = this.SplitText(text);
                if (this.EditValueInternal != null)
                {
                    IList<CustomItem> editValue = this.EditValueInternal.EditValue as IList<CustomItem>;
                    this.EditValueInternal.UseTokenDisplayText = true;
                    if ((this.EditableTokenIndex != -1) && (editValue.Count > this.EditableTokenIndex))
                    {
                        editValue.RemoveAt(this.EditableTokenIndex);
                    }
                    this.EditValueInternal.EditableTokens = new List<int>();
                    foreach (string str2 in list)
                    {
                        CustomItem item = new CustomItem();
                        item.EditValue = str2;
                        item.DisplayText = str2;
                        editValue.Add(item);
                        this.EditValueInternal.EditableTokens.Add(editValue.Count - 1);
                    }
                    this.RaiseTextChanged();
                    if (!this.TryCancelTokensAdding())
                    {
                        this.ProcessTokensAdded();
                    }
                    this.EditValueInternal.EditableTokens = new List<int>();
                    this.RaiseValueChanged();
                    this.ImmediateActionsManager.EnqueueAction(() => this.tokenEditorPanel.OnActiveTokenEditValueChanged(this.FocusedToken));
                }
            }
        }

        internal void PostponeOnGotFocus(Action action)
        {
            this.postponeOnGotFocusAction = action;
        }

        internal void ProcessActiveEditorEditValueChanged(string oldValue, string newValue)
        {
            if (!this.processEditValueLocker.IsLocked && ((this.EditValueInternal != null) && (this.EditableTokenIndex > -1)))
            {
                this.IsInProcessNewValue = true;
                this.EditValueInternal.UseTokenDisplayText = true;
                TokenTextChangingEventArgs args = new TokenTextChangingEventArgs(oldValue, newValue);
                this.ProcessTokenTextChanging(args);
                string str = args.Handled ? args.Text : newValue;
                CustomItem item1 = new CustomItem();
                item1.EditValue = str;
                item1.DisplayText = str;
                CustomItem item = item1;
                (this.EditValueInternal.EditValue as IList<CustomItem>)[this.EditableTokenIndex] = item;
                this.RaiseTextChanged();
                this.ImmediateActionsManager.EnqueueAction(() => this.tokenEditorPanel.OnActiveTokenEditValueChanged(this.FocusedToken));
            }
        }

        internal bool ProcessCommitKey(KeyEventArgs e)
        {
            if (!this.IsNewTokenAdded())
            {
                return true;
            }
            bool flag = this.TryCancelTokensAdding();
            if (!flag && (e.Key == Key.Tab))
            {
                this.CommitActiveEditor();
                this.PostponeOnGotFocus(new Action(this.ActivateNewToken));
                e.Handled = true;
            }
            return !flag;
        }

        private void ProcessGotFocus(KeyboardFocusChangedEventArgs e)
        {
            if (this.activateTokenLocker.IsLocked)
            {
                this.activateTokenLocker.Unlock();
            }
            else if (!this.InvokePostpone())
            {
                if (this.IsEditorKeyboardFocused && (ReferenceEquals(e.NewFocus, this) && this.HasActiveEditor))
                {
                    this.FocusedToken.InplaceEditor.FocusEditCore();
                }
                else if (!this.IsFocusFromCellEditor(e.OldFocus) && ReferenceEquals(e.NewFocus, this))
                {
                    if (this.NewToken == null)
                    {
                        ReraiseAction reraiseAction = new ReraiseAction(delegate {
                            if (this.IsNewTokenVisible())
                            {
                                this.ActivateNewToken();
                            }
                        }, () => this.NewToken != null, this.ImmediateActionsManager);
                        this.ImmediateActionsManager.EnqueueAction(() => reraiseAction.Perform());
                    }
                    else if (!this.NewToken.IsEditorActivated && this.IsNewTokenVisible())
                    {
                        this.ActivateNewToken();
                    }
                    else if (LayoutHelper.FindParentObject<TokenEditorPresenter>((DependencyObject) Keyboard.FocusedElement) != this.NewToken)
                    {
                        this.NewToken.InplaceEditor.FocusEditCore();
                    }
                }
            }
        }

        internal bool ProcessKeyDown(KeyEventArgs e) => 
            this.KeyboardHelper.ProcessPreviewKeyDown(e);

        internal void ProcessKeyDownFromCellEditor(KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                this.CellEditorOwner.MoveFocus(e);
                e.Handled = true;
            }
        }

        private void ProcessLostFocus()
        {
            this.CommitActiveEditor();
            this.ResetFocusedToken();
        }

        private void ProcessMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (this.tokenEditorPanel != null)
            {
                Func<HitTestResult, DependencyObject> evaluator = <>c.<>9__330_0;
                if (<>c.<>9__330_0 == null)
                {
                    Func<HitTestResult, DependencyObject> local1 = <>c.<>9__330_0;
                    evaluator = <>c.<>9__330_0 = x => x.VisualHit;
                }
                DependencyObject child = VisualTreeHelper.HitTest(this.tokenEditorPanel, e.GetPosition(this.tokenEditorPanel)).Return<HitTestResult, DependencyObject>(evaluator, <>c.<>9__330_1 ??= ((Func<DependencyObject>) (() => null)));
                bool flag = LayoutHelper.FindLayoutOrVisualParentObject<Button>(child, false, null) != null;
                if (flag && (!this.HasOwnerEdit || (this.OwnerEdit.EditMode == DevExpress.Xpf.Editors.EditMode.InplaceInactive)))
                {
                    e.Handled = true;
                }
                else if (this.HasOwnerEdit && (this.OwnerEdit.EditMode != DevExpress.Xpf.Editors.EditMode.InplaceInactive))
                {
                    TokenEditorPresenter container = LayoutHelper.FindLayoutOrVisualParentObject<TokenEditorPresenter>(child, false, null);
                    if (container == null)
                    {
                        if (this.tokenEditorPanel.Equals(LayoutHelper.FindLayoutOrVisualParentObject<TokenEditorPanel>(child, false, null)))
                        {
                            this.PostponeOnGotFocus(new Action(this.CommitActiveEditor));
                        }
                    }
                    else if (this.KeyboardHelper.IsCtrlPressed() && (!this.IsNewToken(container) && !flag))
                    {
                        this.SelectTokenViaCtrl(container);
                    }
                    else
                    {
                        if (!ReferenceEquals(this.FocusedToken, container) && this.IsNewTokenFocused())
                        {
                            this.ClearAndRemoveNewTokenValue(!flag);
                        }
                        if (!flag && !container.IsEditorActivated)
                        {
                            this.ChangeFocusedTokenOnMouseDown(container, e);
                        }
                    }
                }
            }
        }

        private void ProcessMouseWheel(double delta)
        {
            if (delta > 0.0)
            {
                if (this.tokenEditorPanel.Orientation == Orientation.Horizontal)
                {
                    this.tokenEditorPanel.LineLeft();
                }
                else
                {
                    this.tokenEditorPanel.LineUp();
                }
            }
            else if (this.tokenEditorPanel.Orientation == Orientation.Horizontal)
            {
                this.tokenEditorPanel.LineRight();
            }
            else
            {
                this.tokenEditorPanel.LineDown();
            }
        }

        private void ProcessTokenActivated(int index)
        {
            TokenStateChangedEventArgs args = new TokenStateChangedEventArgs(this.GetTokenValue(index), this.ActiveEditor);
            this.EditBehavior.Do<ITokenEditorBehavior>(x => x.ProcessTokenActivated(args));
        }

        internal bool ProcessTokenActivating(TokenEditorPresenter token)
        {
            int index = this.EditableIndexOfToken(token);
            TokenActivatingEventArgs args = new TokenActivatingEventArgs((index != -1) ? this.GetTokenValue(index) : null);
            this.EditBehavior.Do<ITokenEditorBehavior>(delegate (ITokenEditorBehavior x) {
                x.ProcessTokenActivating(args);
            });
            return args.IsCancel;
        }

        private void ProcessTokenDeactivated(int index)
        {
            TokenStateChangedEventArgs args = new TokenStateChangedEventArgs(this.GetTokenValue(index), this.ActiveEditor);
            this.EditBehavior.Do<ITokenEditorBehavior>(x => x.ProcessTokenDeactivated(args));
        }

        private void ProcessTokensAdded()
        {
            TokensChangedEventArgs args = new TokensChangedEventArgs(this.GetAddedTokens(), null);
            this.RaiseTokensChanged(args);
        }

        private void ProcessTokensChangedOnAcceptPopupValue()
        {
            IList<object> source = this._getValueBeforeAcceptPopup();
            this._getValueBeforeAcceptPopup = null;
            List<object> addedTokens = null;
            List<object> removedTokens = null;
            if (this.EditValueInternal != null)
            {
                IList<object> valueFromCustomItem = this.GetValueFromCustomItem(this.EditValueInternal);
                if (valueFromCustomItem == null)
                {
                    removedTokens = (source != null) ? source.ToList<object>() : null;
                }
                else if (source == null)
                {
                    addedTokens = valueFromCustomItem.ToList<object>();
                }
                else
                {
                    List<object> second = valueFromCustomItem.Intersect<object>(source).ToList<object>();
                    List<object> list6 = valueFromCustomItem.Except<object>(second).ToList<object>();
                    List<object> list7 = source.Except<object>(second).ToList<object>();
                    addedTokens = (list6.Count > 0) ? list6 : null;
                    removedTokens = (list7.Count > 0) ? list7 : null;
                }
            }
            if ((addedTokens != null) || (removedTokens != null))
            {
                this.RaiseTokensChanged(new TokensChangedEventArgs(addedTokens, removedTokens));
            }
        }

        private void ProcessTokensRemoved(List<object> removedTokens)
        {
            TokensChangedEventArgs args = new TokensChangedEventArgs(null, removedTokens);
            this.RaiseTokensChanged(args);
        }

        private bool ProcessTokensRemoving(List<object> removedTokens)
        {
            TokensChangingEventArgs args = new TokensChangingEventArgs(null, removedTokens);
            this.EditBehavior.Do<ITokenEditorBehavior>(delegate (ITokenEditorBehavior x) {
                x.ProcessTokensChanging(args);
            });
            return args.IsCancel;
        }

        private void ProcessTokenTextChanging(TokenTextChangingEventArgs args)
        {
            this.EditBehavior.Do<ITokenEditorBehavior>(x => x.ProcessTokenTextChanging(args));
        }

        internal int ProvideIndex(object value)
        {
            if (value != null)
            {
                IList list = (this.EditValueInternal != null) ? (this.EditValueInternal.EditValue as IList) : null;
                if (list != null)
                {
                    using (IEnumerator enumerator = list.GetEnumerator())
                    {
                        while (true)
                        {
                            if (!enumerator.MoveNext())
                            {
                                break;
                            }
                            object current = enumerator.Current;
                            Func<CustomItem, object> evaluator = <>c.<>9__240_0;
                            if (<>c.<>9__240_0 == null)
                            {
                                Func<CustomItem, object> local1 = <>c.<>9__240_0;
                                evaluator = <>c.<>9__240_0 = x => x.EditValue;
                            }
                            if (value.Equals((current as CustomItem).Return<CustomItem, object>(evaluator, <>c.<>9__240_1 ??= () => null)))
                            {
                                return list.IndexOf(current);
                            }
                        }
                    }
                }
            }
            return -1;
        }

        internal bool ProvideValue(int index, out object value)
        {
            value = null;
            IList list = (this.EditValueInternal != null) ? (this.EditValueInternal.EditValue as IList) : null;
            if ((list == null) || ((index < 0) || (index >= list.Count)))
            {
                return false;
            }
            CustomItem item = list[index] as CustomItem;
            value = item?.EditValue;
            return (item != null);
        }

        private void RaiseTextChanged()
        {
            if (this.textChanged != null)
            {
                this.textChanged(this, EventArgs.Empty);
            }
        }

        private void RaiseTokenClosed()
        {
            if (this.tokenClosed != null)
            {
                this.tokenClosed(this, EventArgs.Empty);
            }
        }

        private void RaiseTokensChanged(TokensChangedEventArgs args)
        {
            if (this.CanRaiseTokensChanged)
            {
                this.EditBehavior.Do<ITokenEditorBehavior>(x => x.ProcessTokensChanged(args));
            }
        }

        private void RaiseValueChanged()
        {
            if (this.valueChanged != null)
            {
                this.valueChanged(this, EventArgs.Empty);
            }
        }

        private void RemoveDeleteButton()
        {
            if ((this.ActualTokenEditSettings != null) && this.ActualTokenEditSettings.Buttons.Contains(this.DeleteButtonInfo))
            {
                this.ActualTokenEditSettings.Buttons.Remove(this.DeleteButtonInfo);
            }
            this.deleteButtonInfo = null;
        }

        internal void RemoveSelectedTokens()
        {
            if (!this.IsReadOnly && this.Selection.HasSelectedTokens)
            {
                List<object> removedTokens = this.RemoveTokensByIndexes(this.Selection.SelectedTokensIndexes);
                this.Selection.ResetSelection();
                this.AfterTokenRemoved(this.FocusedToken, removedTokens);
            }
        }

        private void RemoveToken(TokenEditorPresenter token)
        {
            List<int> indexes = new List<int>();
            indexes.Add(this.EditableIndexOfToken(token));
            List<object> removedTokens = this.RemoveTokensByIndexes(indexes);
            this.AfterTokenRemoved(token, removedTokens);
        }

        internal void RemoveToken(int index)
        {
            if (!this.IsReadOnly && (this.tokenEditorPanel.Items.Count > 0))
            {
                this.RemoveToken(this.tokenEditorPanel.GetTokenByVisibleIndex(index));
            }
        }

        private List<object> RemoveTokensByIndexes(List<int> indexes)
        {
            if ((indexes == null) || ((indexes.Count == 0) || (this.EditValueInternal == null)))
            {
                return null;
            }
            List<object> removedTokens = new List<object>();
            indexes.ForEach(delegate (int x) {
                removedTokens.Add(this.GetTokenValue(x));
            });
            if (this.ProcessTokensRemoving(removedTokens))
            {
                return null;
            }
            IList<CustomItem> editValue = this.EditValueInternal.EditValue as IList<CustomItem>;
            if (editValue != null)
            {
                List<CustomItem> list2 = new List<CustomItem>(editValue);
                foreach (int num in indexes)
                {
                    if ((num != -1) && (list2.Count > num))
                    {
                        editValue.Remove(list2[num]);
                    }
                }
                this.UpdateEditValueInternalAfterRemove(editValue);
            }
            return removedTokens;
        }

        internal bool RemoveValueByIndex(int index)
        {
            IList editValue = this.EditValueInternal.EditValue as IList;
            if ((index >= editValue.Count) || (index <= -1))
            {
                return false;
            }
            editValue.RemoveAt(index);
            this.ClearEditableTokens();
            return true;
        }

        private void ResetEditSettings()
        {
            this.ActualNewTokenEditSettings = null;
            this.ActualTokenEditSettings = null;
        }

        private void ResetFocusedToken()
        {
            this.ActiveEditor = null;
            this.FocusedToken = null;
        }

        private void ResetTokens()
        {
            if (this.tokenEditorPanel != null)
            {
                this.RemoveDeleteButton();
                this.tokenEditorPanel.Clear();
                this.ResetEditSettings();
                this.UpdateTokens();
                this.InvalidateLayout();
            }
        }

        private bool ScrollLeftAndDoAction(int index, Action action)
        {
            bool flag = this.ScrollLeftByIndex(index);
            this.ImmediateActionsManager.EnqueueAction(delegate {
                action();
            });
            return flag;
        }

        private bool ScrollLeftAndMoveFocusedToken(int index, bool shouldActivate = false) => 
            this.ScrollLeftAndDoAction(index, delegate {
                this.SetFocusedTokenByIndexWithLock(index, shouldActivate);
            });

        private bool ScrollLeftAndSelectToken(int newSelectedIndex) => 
            this.ScrollLeftAndDoAction(newSelectedIndex, delegate {
                this.SelectTokenByKeyboard(newSelectedIndex, true);
            });

        private bool ScrollLeftByIndex(int index)
        {
            this.tokenEditorPanel.ScrollLeft(index);
            return true;
        }

        private bool ScrollRightAndDoAction(int index, Action action)
        {
            bool flag = this.ScrollRightByIndex(index);
            this.ImmediateActionsManager.EnqueueAction(delegate {
                action();
            });
            return flag;
        }

        private bool ScrollRightAndMoveFocusedToken(int index, bool shouldActivate = false) => 
            this.ScrollRightAndDoAction(index, delegate {
                this.SetFocusedTokenByIndexWithLock(index, shouldActivate);
            });

        private bool ScrollRightAndSelectToken(int newSelectedIndex) => 
            this.ScrollRightAndDoAction(newSelectedIndex, delegate {
                this.SelectTokenByKeyboard(newSelectedIndex, false);
            });

        private bool ScrollRightByIndex(int index)
        {
            this.tokenEditorPanel.ScrollRight(index);
            return true;
        }

        public void ScrollToHome()
        {
            if (this.HasActiveEditor && this.IsActiveEditorHasEditBox())
            {
                this.ActiveEditor.EditBox.ScrollToHome();
            }
        }

        private bool ScrollToIndex(int index, bool activateToken)
        {
            if (this.ShouldScroll(index))
            {
                return this.ScrollLeftAndMoveFocusedToken(index, activateToken);
            }
            this.SetFocusedTokenByIndexWithLock(index, activateToken);
            return true;
        }

        public void Select(int start, int length)
        {
            if (this.HasActiveEditor)
            {
                this.ActiveEditor.Select(start, length);
            }
        }

        public void SelectAll()
        {
            if (this.HasActiveEditor)
            {
                this.ActiveEditor.SelectAll();
            }
        }

        private void SelectedTokensCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.Selection.SyncSelectionWithOwner();
        }

        private void SelectFocusedToken()
        {
            if (!this.selectionLocker.IsLocked)
            {
                this.Selection.ResetSelection();
                if ((this.FocusedToken != null) && !this.IsNewTokenFocused())
                {
                    int index = this.EditableIndexOfToken(this.FocusedToken);
                    this.SelectToken(index);
                    this.Selection.SetStartSelectionIndex(index);
                }
            }
        }

        internal void SelectToken(int index)
        {
            this.Selection.SelectTokenByIndex(index);
        }

        private void SelectTokenByKeyboard(int newSelectedIndex, bool isLeft)
        {
            int index = this.tokenEditorPanel.ConvertToEditableIndex(newSelectedIndex);
            int num2 = isLeft ? 1 : -1;
            if (isLeft ? (index < this.Selection.StartSelectionIndex) : (index > this.Selection.StartSelectionIndex))
            {
                this.SelectToken(index);
            }
            else
            {
                this.UnselectToken(index + num2);
            }
            this.selectionLocker.DoLockedAction(() => this.SetFocusedTokenByIndexWithLock(newSelectedIndex, false));
        }

        internal bool SelectTokenOnNavigateLeft(int index)
        {
            if (this.ShouldScroll(index))
            {
                return this.ScrollLeftAndSelectToken(index);
            }
            this.SelectTokenByKeyboard(index, true);
            return true;
        }

        internal bool SelectTokenOnNavigateRight(int index)
        {
            if (this.ShouldScroll(index))
            {
                return this.ScrollRightAndSelectToken(index);
            }
            this.SelectTokenByKeyboard(index, false);
            return true;
        }

        private void SelectTokensOnVerticalNavigation(bool up, int focusedIndex, int newFocusedIndex)
        {
            List<int> newSelection = new List<int>();
            int num = this.tokenEditorPanel.ConvertToEditableIndex(newFocusedIndex);
            if (up)
            {
                int visibleIndex = newFocusedIndex + 1;
                while (true)
                {
                    if (visibleIndex >= focusedIndex)
                    {
                        this.SelectTokensOnVerticalNavigationCore(newSelection, focusedIndex, num < this.Selection.StartSelectionIndex);
                        break;
                    }
                    newSelection.Add(this.tokenEditorPanel.ConvertToEditableIndex(visibleIndex));
                    visibleIndex++;
                }
            }
            else
            {
                int visibleIndex = focusedIndex + 1;
                while (true)
                {
                    if (visibleIndex >= newFocusedIndex)
                    {
                        this.SelectTokensOnVerticalNavigationCore(newSelection, focusedIndex, num > this.Selection.StartSelectionIndex);
                        break;
                    }
                    newSelection.Add(this.tokenEditorPanel.ConvertToEditableIndex(visibleIndex));
                    visibleIndex++;
                }
            }
            this.SelectToken(this.tokenEditorPanel.ConvertToEditableIndex(newFocusedIndex));
            this.selectionLocker.DoLockedAction(() => this.SetFocusedTokenByIndexWithLock(newFocusedIndex, false));
        }

        private void SelectTokensOnVerticalNavigationCore(List<int> newSelection, int oldFocused, bool canAdd)
        {
            if (!newSelection.Contains(this.Selection.StartSelectionIndex) & canAdd)
            {
                newSelection.ForEach(x => this.SelectToken(x));
            }
            else
            {
                this.tokenEditorPanel.GetIndexesInLine(oldFocused).ForEach(x => this.UnselectToken(this.tokenEditorPanel.ConvertToEditableIndex(x)));
            }
        }

        private void SelectTokenViaCtrl(TokenEditorPresenter token)
        {
            if (this.HasActiveEditor)
            {
                this.FocusedToken.CommitEditor();
                this.FocusedToken.IsEditorActivated = false;
            }
            this.Selection.SelectToken(token);
            this.selectionLocker.DoLockedAction<TokenEditorPresenter>(delegate {
                TokenEditorPresenter presenter;
                this.FocusedToken = presenter = token;
                return presenter;
            });
        }

        private void SetCanExecuteParameters(CanExecuteRoutedEventArgs e, bool canExecute)
        {
            e.CanExecute = (this.EditMode != DevExpress.Xpf.Editors.EditMode.InplaceInactive) & canExecute;
            if (this.EditMode == DevExpress.Xpf.Editors.EditMode.InplaceInactive)
            {
                e.ContinueRouting = true;
            }
            else
            {
                e.ContinueRouting = !canExecute;
                e.Handled = !canExecute;
            }
        }

        public void SetEditValue(object value)
        {
            this.EditValue = value;
            if (!this.editValueLocker.IsLocked)
            {
                if (this.HasActiveEditor)
                {
                    this.UpdateActiveEditorValue(value);
                }
                else if (this.ShouldAssignNullText())
                {
                    this.ImmediateActionsManager.EnqueueAction(() => this.UpdateNewToken());
                }
                if ((this.OwnerEdit == null) || (this.OwnerEdit.EditMode == DevExpress.Xpf.Editors.EditMode.InplaceInactive))
                {
                    this.UpdateTokens();
                }
                this.InvalidateLayout();
            }
        }

        private void SetEditValueInternal(object newValue)
        {
            base.SetCurrentValue(EditValueProperty, newValue);
        }

        private void SetFocusedAndActivateToken(TokenEditorPresenter container)
        {
            if (!ReferenceEquals(container, this.NewToken) || this.ShowNewToken)
            {
                this.FocusedToken = container;
                this.ActivateToken(this.FocusedToken, true);
            }
        }

        private void SetFocusedAndActivateTokenByIndex(int index, bool activate = true)
        {
            if (this.IsNewTokenIndex(index))
            {
                this.SetFocusedAndActivateToken(this.NewToken);
            }
            else
            {
                TokenEditorPresenter tokenByVisibleIndex = this.GetTokenByVisibleIndex(index);
                if (tokenByVisibleIndex != null)
                {
                    if (activate)
                    {
                        this.SetFocusedAndActivateToken(tokenByVisibleIndex);
                    }
                    else
                    {
                        this.FocusedToken = tokenByVisibleIndex;
                    }
                }
            }
        }

        internal void SetFocusedTokenByEditableIndex(int index, bool shouldActivate = false)
        {
            this.SetFocusedTokenByIndexWithLock(this.tokenEditorPanel.ConvertToVisibleIndex(index), shouldActivate);
        }

        internal void SetFocusedTokenByIndexWithLock(int index, bool shouldActivate = false)
        {
            bool activate = shouldActivate || this.IsNewTokenIndex(index);
            this.changeFocusedTokenLocker.DoLockedAction(delegate {
                if (activate)
                {
                    this.SetFocusedAndActivateTokenByIndex(index, true);
                }
                else
                {
                    TokenEditorPresenter tokenByVisibleIndex = this.GetTokenByVisibleIndex(index);
                    if (tokenByVisibleIndex != null)
                    {
                        this.FocusedToken = tokenByVisibleIndex;
                    }
                }
            });
        }

        private void SetIsInProcessValue(object oldValue, object newValue)
        {
            this.IsInProcessNewValue = oldValue != newValue;
        }

        internal static void SetOwnerTokenEditor(DependencyObject element, TokenEditor value)
        {
            if (element != null)
            {
                element.SetValue(OwnerTokenEditorPropertyKey, value);
            }
        }

        private void SetProperty(DependencyProperty dependencyProperty, object newValue)
        {
            base.SetCurrentValue(dependencyProperty, newValue);
        }

        private bool ShouldAssignNullText()
        {
            TokenItemData data1;
            TokenEditorPresenter newToken = this.NewToken;
            if (newToken != null)
            {
                data1 = newToken.ItemData;
            }
            else
            {
                TokenEditorPresenter local1 = newToken;
                data1 = null;
            }
            if (data1 == null)
            {
                return false;
            }
            TokenItemData itemData = this.NewToken.ItemData;
            return (this.GetNullText() != itemData.Settings.NullText);
        }

        private bool ShouldScroll(int index) => 
            this.tokenEditorPanel.ShouldScroll(index);

        private bool ShowNullText() => 
            this.ShouldAssignNullText() && ((this.EditValueInternal != null) && (this.EditValueInternal.EditValue == null));

        private List<string> SplitText(string text)
        {
            if (this.newLineSplitters.Contains<string>(this.OwnerEdit.SeparatorString))
            {
                return text.Split(this.newLineSplitters, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            }
            string[] separator = new string[] { this.OwnerEdit.SeparatorString };
            return text.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
        }

        private void StartEditingCore(ButtonEdit editor)
        {
            this.ActiveEditor = editor;
            this.SelectFocusedToken();
            if (this.HasFocusedToken)
            {
                int index = this.EditableIndexOfToken(this.FocusedToken);
                this.UpdateEditValueOnStartEditing(index);
                this.ProcessTokenActivated(index);
            }
        }

        public void SyncWithValue(UpdateEditorSource updateSource)
        {
            if (updateSource != UpdateEditorSource.TextInput)
            {
                this.SyncWithValueCore();
            }
            else
            {
                this.postponeSyncWithValue = new Action(this.SyncWithValueCore);
            }
        }

        private void SyncWithValueCore()
        {
            if (this.HasActiveEditor)
            {
                this.CommitActiveEditor();
            }
            this.UpdateTokens();
        }

        private void TokenEditorLayoutUpdated(object sender, EventArgs e)
        {
            this.ImmediateActionsManager.ExecuteActions();
        }

        private void TokenEditorLoaded(object sender, RoutedEventArgs e)
        {
            this.UpdateTokens();
        }

        private void TokenEditorUnloaded(object sender, RoutedEventArgs e)
        {
            if ((this.OwnerEdit != null) && (this.OwnerEdit.EditMode != DevExpress.Xpf.Editors.EditMode.Standalone))
            {
                this.RemoveDeleteButton();
            }
        }

        private bool TryCancelTokensAdding()
        {
            TokensChangingEventArgs args = new TokensChangingEventArgs(this.GetAddedTokens(), null);
            this.EditBehavior.Do<ITokenEditorBehavior>(delegate (ITokenEditorBehavior x) {
                x.ProcessTokensChanging(args);
            });
            if (args.IsCancel)
            {
                IList<CustomItem> listValue = (IList<CustomItem>) this.EditValueInternal.EditValue;
                List<CustomItem> originValue = new List<CustomItem>(listValue);
                this.EditValueInternal.EditableTokens.ForEach(delegate (int x) {
                    listValue.Remove(originValue[x]);
                });
                this.ClearEditableTokens();
                if (listValue.Count == 0)
                {
                    this.EditValueInternal.EditValue = null;
                }
                this.RaiseValueChanged();
            }
            return args.IsCancel;
        }

        private void TryRemoveEmptyValue()
        {
            if ((this.EditValueInternal != null) && (this.EditableTokenIndex != -1))
            {
                IList<CustomItem> editValue = this.EditValueInternal.EditValue as IList<CustomItem>;
                if (editValue != null)
                {
                    CustomItem item = editValue[this.EditableTokenIndex];
                    if ((item == null) || ((item.EditValue == null) && string.IsNullOrEmpty(item.DisplayText)))
                    {
                        editValue.RemoveAt(this.EditableTokenIndex);
                    }
                }
            }
        }

        public void Undo()
        {
            if (this.HasActiveEditor)
            {
                this.ActiveEditor.Undo();
            }
        }

        internal void UnselectToken(int index)
        {
            this.Selection.UnselectTokenByIndex(index);
        }

        private void UpdateActiveEditorContent(IList<CustomItem> listEditValue)
        {
            this.processEditValueLocker.DoLockedAction(delegate {
                CustomItem valueForEditableItem = this.GetValueForEditableItem(listEditValue);
                if (!this.IsInProcessNewValue)
                {
                    this.SetIsInProcessValue(this.FocusedToken.Item.EditValue, valueForEditableItem.EditValue);
                }
                this.FocusedToken.Item = valueForEditableItem;
            });
        }

        private void UpdateActiveEditorValue(object newValue)
        {
            this.UpdateActiveEditorValueCore(newValue);
        }

        private void UpdateActiveEditorValueCore(object newValue)
        {
            IList<CustomItem> innerEditValue = this.GetInnerEditValue();
            if ((innerEditValue != null) && ((this.EditableTokenIndex > -1) || this.IsNewTokenFocused()))
            {
                this.UpdateActiveEditorContent(innerEditValue);
            }
        }

        private void UpdateEditValueInternalAfterRemove(IList<CustomItem> listEditValue)
        {
            if (this.EditValueInternal != null)
            {
                this.ClearEditableTokens();
                if (listEditValue.Count == 0)
                {
                    this.EditValueInternal.EditValue = null;
                }
                else
                {
                    this.EditValueInternal.EditValue = new List<CustomItem>(listEditValue);
                }
            }
        }

        private void UpdateEditValueOnStartEditing(int index)
        {
            if (this.EditValueInternal == null)
            {
                List<int> list2 = new List<int>();
                list2.Add(index);
                TokenEditorCustomItem newValue = new TokenEditorCustomItem();
                newValue.EditableTokens = list2;
                List<CustomItem> list3 = new List<CustomItem>();
                list3.Add(new CustomItem());
                newValue.EditValue = list3;
                this.SetEditValueInternal(newValue);
            }
            else
            {
                if (this.IsNewTokenFocused())
                {
                    IList<CustomItem> editValue = this.EditValueInternal.EditValue as IList<CustomItem>;
                    if (editValue != null)
                    {
                        editValue.Add(new CustomItem());
                    }
                }
                List<int> list1 = new List<int>();
                list1.Add(index);
                this.EditValueInternal.EditableTokens = list1;
            }
        }

        private void UpdateNewToken()
        {
            this.tokenEditorPanel.ClearNewTokenValue();
        }

        private void UpdatePresenterItems(IList<CustomItem> items)
        {
            if (this.tokenEditorPanel != null)
            {
                List<CustomItem> displayItems = this.tokenEditorPanel.DisplayItems;
                List<CustomItem> list2 = (items != null) ? new List<CustomItem>(items) : new List<CustomItem>();
                this.tokenEditorPanel.DisplayItems = list2;
            }
        }

        private void UpdateTokenEditorsFocused(TokenEditorPresenter newFocused)
        {
            foreach (UIElement element in this.GetInplaceEditorContainers())
            {
                ((TokenEditorPresenter) element).IsTokenFocused = ReferenceEquals(element, newFocused);
            }
        }

        private void UpdateTokens()
        {
            this.UpdatePresenterItems(this.GetInnerEditValue());
        }

        public LookUpEditBase OwnerEdit =>
            (LookUpEditBase) base.GetValue(BaseEdit.OwnerEditProperty);

        public string NewTokenText
        {
            get => 
                (string) base.GetValue(NewTokenTextProperty);
            set => 
                this.SetProperty(NewTokenTextProperty, value);
        }

        public Style TokenStyle
        {
            get => 
                (Style) base.GetValue(TokenStyleProperty);
            set => 
                base.SetValue(TokenStyleProperty, value);
        }

        public DataTemplate DeleteTokenButtonTemplate
        {
            get => 
                (DataTemplate) base.GetValue(DeleteTokenButtonTemplateProperty);
            set => 
                base.SetValue(DeleteTokenButtonTemplateProperty, value);
        }

        public ITokenEditorBehavior EditBehavior
        {
            get => 
                (ITokenEditorBehavior) base.GetValue(EditBehaviorProperty);
            set => 
                base.SetValue(EditBehaviorProperty, value);
        }

        public ObservableCollection<object> SelectedTokens
        {
            get => 
                (ObservableCollection<object>) base.GetValue(SelectedTokensProperty);
            private set => 
                base.SetValue(SelectedTokensPropertyKey, value);
        }

        public Brush NullTextForeground
        {
            get => 
                (Brush) base.GetValue(NullTextForegroundProperty);
            set => 
                base.SetValue(NullTextForegroundProperty, value);
        }

        public int MaxTextLength
        {
            get => 
                (int) base.GetValue(MaxTextLengthProperty);
            set => 
                base.SetValue(MaxTextLengthProperty, value);
        }

        public System.Windows.Controls.CharacterCasing CharacterCasing
        {
            get => 
                (System.Windows.Controls.CharacterCasing) base.GetValue(CharacterCasingProperty);
            set => 
                base.SetValue(CharacterCasingProperty, value);
        }

        public bool AllowEditTokens
        {
            get => 
                (bool) base.GetValue(AllowEditTokensProperty);
            set => 
                base.SetValue(AllowEditTokensProperty, value);
        }

        public DevExpress.Xpf.Editors.EditMode EditMode
        {
            get => 
                (DevExpress.Xpf.Editors.EditMode) base.GetValue(EditModeProperty);
            set => 
                base.SetValue(EditModeProperty, value);
        }

        public double TokenMaxWidth
        {
            get => 
                (double) base.GetValue(TokenMaxWidthProperty);
            set => 
                base.SetValue(TokenMaxWidthProperty, value);
        }

        public TextTrimming TokenTextTrimming
        {
            get => 
                (TextTrimming) base.GetValue(TokenTextTrimmingProperty);
            set => 
                base.SetValue(TokenTextTrimmingProperty, value);
        }

        public bool EnableTokenWrapping
        {
            get => 
                (bool) base.GetValue(EnableTokenWrappingProperty);
            set => 
                base.SetValue(EnableTokenWrappingProperty, value);
        }

        public DevExpress.Xpf.Editors.NewTokenPosition NewTokenPosition
        {
            get => 
                (DevExpress.Xpf.Editors.NewTokenPosition) base.GetValue(NewTokenPositionProperty);
            set => 
                base.SetValue(NewTokenPositionProperty, value);
        }

        public bool ShowNewTokenFromEnd =>
            this.NewTokenPosition == DevExpress.Xpf.Editors.NewTokenPosition.Far;

        public ControlTemplate TokenBorderTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(TokenBorderTemplateProperty);
            set => 
                base.SetValue(TokenBorderTemplateProperty, value);
        }

        public ButtonInfoCollection TokenButtons
        {
            get => 
                (ButtonInfoCollection) base.GetValue(TokenButtonsProperty);
            set => 
                base.SetValue(TokenButtonsProperty, value);
        }

        public bool ShowTokenButtons
        {
            get => 
                (bool) base.GetValue(ShowTokenButtonsProperty);
            set => 
                base.SetValue(ShowTokenButtonsProperty, value);
        }

        public ButtonEdit ActiveEditor
        {
            get => 
                (ButtonEdit) base.GetValue(ActiveEditorProperty);
            private set => 
                base.SetValue(ActiveEditorPropertyKey, value);
        }

        public object EditValue
        {
            get => 
                base.GetValue(EditValueProperty);
            set => 
                base.SetValue(EditValueProperty, value);
        }

        public bool IsTextEditable
        {
            get => 
                (bool) base.GetValue(IsTextEditableProperty);
            set => 
                base.SetValue(IsTextEditableProperty, value);
        }

        public bool IsReadOnly
        {
            get => 
                (bool) base.GetValue(IsReadOnlyProperty);
            set => 
                base.SetValue(IsReadOnlyProperty, value);
        }

        public bool FocusPopupOnOpen
        {
            get => 
                (bool) base.GetValue(FocusPopupOnOpenProperty);
            set => 
                base.SetValue(FocusPopupOnOpenProperty, value);
        }

        public bool IsEditorKeyboardFocused
        {
            get
            {
                Func<LookUpEditBase, bool> evaluator = <>c.<>9__99_0;
                if (<>c.<>9__99_0 == null)
                {
                    Func<LookUpEditBase, bool> local1 = <>c.<>9__99_0;
                    evaluator = <>c.<>9__99_0 = x => x.GetIsEditorKeyboardFocused();
                }
                return this.OwnerEdit.Return<LookUpEditBase, bool>(evaluator, (<>c.<>9__99_1 ??= () => false));
            }
        }

        public bool IsPopupCloseInProgress
        {
            get
            {
                Func<LookUpEditBase, bool> evaluator = <>c.<>9__101_0;
                if (<>c.<>9__101_0 == null)
                {
                    Func<LookUpEditBase, bool> local1 = <>c.<>9__101_0;
                    evaluator = <>c.<>9__101_0 = x => x.GetIsPopupCloseInProgress();
                }
                return this.OwnerEdit.Return<LookUpEditBase, bool>(evaluator, (<>c.<>9__101_1 ??= () => false));
            }
        }

        public bool ShowNewToken =>
            (!this.CanEditing || (this.NewTokenPosition == DevExpress.Xpf.Editors.NewTokenPosition.None)) ? this.ShowNullText() : true;

        public bool HasSelection =>
            this.Selection.HasSelectedTokens;

        public string SelectedText
        {
            get => 
                this.HasActiveEditor ? this.ActiveEditor.SelectedText : string.Empty;
            set
            {
                if (this.HasActiveEditor)
                {
                    this.ActiveEditor.SelectedText = value;
                }
            }
        }

        public int SelectionLength
        {
            get => 
                this.HasActiveEditor ? this.ActiveEditor.SelectionLength : 0;
            set
            {
                if (this.HasActiveEditor)
                {
                    this.ActiveEditor.SelectionLength = value;
                }
            }
        }

        public int SelectionStart
        {
            get => 
                this.HasActiveEditor ? this.ActiveEditor.SelectionStart : -1;
            set
            {
                if (this.HasActiveEditor)
                {
                    this.ActiveEditor.SelectionStart = value;
                }
            }
        }

        public int CaretIndex
        {
            get => 
                this.HasActiveEditor ? this.ActiveEditor.CaretIndex : -1;
            set
            {
                if (this.HasActiveEditor)
                {
                    this.ActiveEditor.CaretIndex = value;
                }
            }
        }

        public bool CanUndo =>
            this.HasActiveEditor ? ((TextEditStrategy) this.ActiveEditor.EditStrategy).CanUndo() : false;

        public int MaxLength
        {
            get => 
                this.HasActiveEditor ? this.ActiveEditor.MaxLength : 0;
            set
            {
                if (this.HasActiveEditor)
                {
                    this.ActiveEditor.MaxLength = value;
                }
            }
        }

        public string Text
        {
            get
            {
                if (!this.HasActiveEditor)
                {
                    return string.Empty;
                }
                Func<object, string> evaluator = <>c.<>9__124_0;
                if (<>c.<>9__124_0 == null)
                {
                    Func<object, string> local1 = <>c.<>9__124_0;
                    evaluator = <>c.<>9__124_0 = x => x.ToString();
                }
                return this.ActiveEditor.EditValue.Return<object, string>(evaluator, (<>c.<>9__124_1 ??= () => string.Empty));
            }
        }

        public bool IsUndoEnabled
        {
            get => 
                this.HasActiveEditor && (this.IsActiveEditorHasEditBox() && this.ActiveEditor.EditBox.IsUndoEnabled);
            set
            {
                if (this.HasActiveEditor && this.IsActiveEditorHasEditBox())
                {
                    this.ActiveEditor.EditBox.IsUndoEnabled = value;
                }
            }
        }

        public int LinesCount
        {
            get
            {
                Func<TokenEditorPanel, int> evaluator = <>c.<>9__129_0;
                if (<>c.<>9__129_0 == null)
                {
                    Func<TokenEditorPanel, int> local1 = <>c.<>9__129_0;
                    evaluator = <>c.<>9__129_0 = x => x.LinesCount;
                }
                return this.tokenEditorPanel.Return<TokenEditorPanel, int>(evaluator, (<>c.<>9__129_1 ??= () => 1));
            }
        }

        private bool CanRaiseTokensChanged =>
            ReferenceEquals(this._getValueBeforeAcceptPopup, null);

        internal bool HasForeground { get; set; }

        private ButtonInfo DeleteButtonInfo
        {
            get
            {
                ButtonInfo deleteButtonInfo = this.deleteButtonInfo;
                if (this.deleteButtonInfo == null)
                {
                    ButtonInfo local1 = this.deleteButtonInfo;
                    deleteButtonInfo = this.deleteButtonInfo = this.CreateDeleteButtonInfo();
                }
                return deleteButtonInfo;
            }
        }

        private bool HasOwnerEdit =>
            this.OwnerEdit != null;

        private bool CanEditing =>
            this.IsTextEditable && !this.IsReadOnly;

        private int EditableTokenIndex
        {
            get
            {
                Func<TokenEditorCustomItem, int> evaluator = <>c.<>9__152_0;
                if (<>c.<>9__152_0 == null)
                {
                    Func<TokenEditorCustomItem, int> local1 = <>c.<>9__152_0;
                    evaluator = <>c.<>9__152_0 = x => x.EditableTokenIndex;
                }
                return this.EditValueInternal.Return<TokenEditorCustomItem, int>(evaluator, (<>c.<>9__152_1 ??= () => -1));
            }
        }

        private TokenEditorKeyboardHelper KeyboardHelper =>
            this.keyboardHelper ?? this.CreateKeyboardHelper();

        private TokenEditorSelection Selection =>
            this.selection ?? this.CreateSelection();

        private TokenEditorPresenter NewToken
        {
            get
            {
                Func<TokenEditorPanel, TokenEditorPresenter> evaluator = <>c.<>9__160_0;
                if (<>c.<>9__160_0 == null)
                {
                    Func<TokenEditorPanel, TokenEditorPresenter> local1 = <>c.<>9__160_0;
                    evaluator = <>c.<>9__160_0 = x => x.NewTokenPresenter;
                }
                return this.tokenEditorPanel.Return<TokenEditorPanel, TokenEditorPresenter>(evaluator, (<>c.<>9__160_1 ??= ((Func<TokenEditorPresenter>) (() => null))));
            }
        }

        private bool HasFocusedToken =>
            this.FocusedToken != null;

        internal TokenEditorCustomItem EditValueInternal =>
            this.EditValue as TokenEditorCustomItem;

        internal bool HasActiveEditor =>
            this.ActiveEditor != null;

        internal DevExpress.Xpf.Editors.ImmediateActionsManager ImmediateActionsManager { get; private set; }

        internal ButtonEditSettings ActualNewTokenEditSettings { get; private set; }

        internal ButtonEditSettings ActualTokenEditSettings { get; private set; }

        public TokenEditorPresenter FocusedToken
        {
            get => 
                this.focusedToken;
            set
            {
                if (!ReferenceEquals(this.focusedToken, value))
                {
                    this.focusedToken = value;
                    this.OnFocusedTokenChanged(value);
                }
            }
        }

        internal InplaceEditorOwnerBase CellEditorOwner
        {
            get
            {
                InplaceEditorOwnerBase cellEditorOwner = this.cellEditorOwner;
                if (this.cellEditorOwner == null)
                {
                    InplaceEditorOwnerBase local1 = this.cellEditorOwner;
                    cellEditorOwner = this.cellEditorOwner = this.CreateCellEditorOwner();
                }
                return cellEditorOwner;
            }
        }

        internal bool IsInProcessNewValue { get; set; }

        ScrollBar IScrollBarThumbDragDeltaListener.ScrollBar { get; set; }

        Orientation IScrollBarThumbDragDeltaListener.Orientation =>
            (this.tokenEditorPanel != null) ? this.tokenEditorPanel.Orientation : Orientation.Horizontal;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TokenEditor.<>c <>9 = new TokenEditor.<>c();
            public static Func<LookUpEditBase, bool> <>9__99_0;
            public static Func<bool> <>9__99_1;
            public static Func<LookUpEditBase, bool> <>9__101_0;
            public static Func<bool> <>9__101_1;
            public static Func<object, string> <>9__124_0;
            public static Func<string> <>9__124_1;
            public static Func<TokenEditorPanel, int> <>9__129_0;
            public static Func<int> <>9__129_1;
            public static Func<TokenEditorCustomItem, int> <>9__152_0;
            public static Func<int> <>9__152_1;
            public static Func<TokenEditorPanel, TokenEditorPresenter> <>9__160_0;
            public static Func<TokenEditorPresenter> <>9__160_1;
            public static Func<CustomItem, object> <>9__228_0;
            public static Func<object, bool> <>9__228_1;
            public static Action<TokenEditorCustomItem> <>9__236_0;
            public static Func<CustomItem, object> <>9__240_0;
            public static Func<object> <>9__240_1;
            public static Func<TokenEditorCustomItem, string> <>9__260_0;
            public static Func<string> <>9__260_1;
            public static Func<ITokenEditorBehavior, ButtonEditSettings> <>9__285_0;
            public static Func<ButtonEditSettings> <>9__285_1;
            public static Func<ITokenEditorBehavior, ButtonEditSettings> <>9__286_0;
            public static Func<ButtonEditSettings> <>9__286_1;
            public static Func<HitTestResult, DependencyObject> <>9__330_0;
            public static Func<DependencyObject> <>9__330_1;
            public static Func<TokenEditorCustomItem, IList<CustomItem>> <>9__342_0;
            public static Func<IList<CustomItem>> <>9__342_1;
            public static Func<TokenEditorCustomItem, object> <>9__360_0;
            public static Func<object> <>9__360_1;
            public static Action<CellEditor> <>9__381_0;

            internal void <.cctor>b__26_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TokenEditor) d).OnEditValueChanged(e.OldValue, e.NewValue);
            }

            internal void <.cctor>b__26_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TokenEditor) d).OnActiveEditorChanged();
            }

            internal void <.cctor>b__26_10(object d, ExecutedRoutedEventArgs e)
            {
                ((TokenEditor) d).PasteTokens();
            }

            internal void <.cctor>b__26_11(object d, CanExecuteRoutedEventArgs e)
            {
                ((TokenEditor) d).CanPaste(d, e);
            }

            internal void <.cctor>b__26_12(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TokenEditor) d).OnTokenStyleChanged();
            }

            internal void <.cctor>b__26_13(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TokenEditor) d).OnNewTokenTextChanged();
            }

            internal void <.cctor>b__26_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TokenEditor) d).OnIsReadOnlyChanged();
            }

            internal void <.cctor>b__26_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TokenEditor) d).OnTokenBorderTemplateChanged();
            }

            internal object <.cctor>b__26_4(DependencyObject d, object e) => 
                ((TokenEditor) d).OnCoerceTokenBorderTemplate(e as ControlTemplate);

            internal void <.cctor>b__26_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TokenEditor) d).OnTokenButtonsChanged((ButtonInfoCollection) e.OldValue, (ButtonInfoCollection) e.NewValue);
            }

            internal void <.cctor>b__26_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TokenEditor) d).OnShowTokenButtonsChanged();
            }

            internal void <.cctor>b__26_7(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TokenEditor) d).OnNewTokenPositionChanged();
            }

            internal void <.cctor>b__26_8(object d, ExecutedRoutedEventArgs e)
            {
                ((TokenEditor) d).CopyTokens();
            }

            internal void <.cctor>b__26_9(object d, CanExecuteRoutedEventArgs e)
            {
                ((TokenEditor) d).CanCopy(d, e);
            }

            internal void <ActivateToken>b__381_0(CellEditor x)
            {
                x.ShowEditor(true);
            }

            internal void <ClearEditableTokens>b__236_0(TokenEditorCustomItem x)
            {
                x.EditableTokens = new List<int>();
            }

            internal ButtonEditSettings <CreateActualNewTokenEditSettings>b__286_0(ITokenEditorBehavior x) => 
                x.NewTokenEditSettings;

            internal ButtonEditSettings <CreateActualNewTokenEditSettings>b__286_1() => 
                null;

            internal ButtonEditSettings <CreateActualTokenEditSettings>b__285_0(ITokenEditorBehavior x) => 
                x.TokenEditSettings;

            internal ButtonEditSettings <CreateActualTokenEditSettings>b__285_1() => 
                null;

            internal int <get_EditableTokenIndex>b__152_0(TokenEditorCustomItem x) => 
                x.EditableTokenIndex;

            internal int <get_EditableTokenIndex>b__152_1() => 
                -1;

            internal bool <get_IsEditorKeyboardFocused>b__99_0(LookUpEditBase x) => 
                x.GetIsEditorKeyboardFocused();

            internal bool <get_IsEditorKeyboardFocused>b__99_1() => 
                false;

            internal bool <get_IsPopupCloseInProgress>b__101_0(LookUpEditBase x) => 
                x.GetIsPopupCloseInProgress();

            internal bool <get_IsPopupCloseInProgress>b__101_1() => 
                false;

            internal int <get_LinesCount>b__129_0(TokenEditorPanel x) => 
                x.LinesCount;

            internal int <get_LinesCount>b__129_1() => 
                1;

            internal TokenEditorPresenter <get_NewToken>b__160_0(TokenEditorPanel x) => 
                x.NewTokenPresenter;

            internal TokenEditorPresenter <get_NewToken>b__160_1() => 
                null;

            internal string <get_Text>b__124_0(object x) => 
                x.ToString();

            internal string <get_Text>b__124_1() => 
                string.Empty;

            internal IList<CustomItem> <GetInnerEditValue>b__342_0(TokenEditorCustomItem x) => 
                x.EditValue as IList<CustomItem>;

            internal IList<CustomItem> <GetInnerEditValue>b__342_1() => 
                null;

            internal string <GetNullText>b__260_0(TokenEditorCustomItem x) => 
                string.IsNullOrEmpty(x.NullText) ? null : x.NullText;

            internal string <GetNullText>b__260_1() => 
                null;

            internal object <GetTokenValue>b__360_0(TokenEditorCustomItem x) => 
                x.EditValue;

            internal object <GetTokenValue>b__360_1() => 
                null;

            internal object <GetValueFromCustomItem>b__228_0(CustomItem x) => 
                x.EditValue;

            internal bool <GetValueFromCustomItem>b__228_1(object x) => 
                x != null;

            internal DependencyObject <ProcessMouseLeftButtonDown>b__330_0(HitTestResult x) => 
                x.VisualHit;

            internal DependencyObject <ProcessMouseLeftButtonDown>b__330_1() => 
                null;

            internal object <ProvideIndex>b__240_0(CustomItem x) => 
                x.EditValue;

            internal object <ProvideIndex>b__240_1() => 
                null;
        }
    }
}

