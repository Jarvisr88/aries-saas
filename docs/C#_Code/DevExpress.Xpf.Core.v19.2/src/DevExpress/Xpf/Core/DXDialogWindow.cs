namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;

    public class DXDialogWindow : DXWindow, IWindowSurrogate
    {
        internal const string SetSourceException = "Cannot use CommandsSource if CommandButtons collection is not empty.";
        private const string SetFooterSourceException = "Cannot use CommandsSource if FooterButtons collection is not empty.";
        internal const string ButtonsCollectionChangedException = "Cannot change CommandButtons collection if CommandSource is set.";
        private const string FooterButtonsCollectionChangedException = "Cannot change FooterButtons collection if CommandSource is set.";
        internal const string ShowDXDialogWindowException = "Cannot use ShowDialogWindow(MessageBoxButton dialogButtons) if CommandButtons collection is not empty. \r\n            Use ShowDialogWindow() instead.";
        internal const string CloseOnEscapeException = "CloseOnEscape is not supported by DXDialogWindow. Instead, you can define default and cancel commands. \r\nIf you populate Dialog buttons from UICommands source, you can set the UICommand.IsDefault and UICommand.IsCancel properties. Default buttons are executed on Enter. Cancel buttons - on Escape.";
        internal const string ShowDialogWindowCoreException = "Dialog is already shown.";
        public static readonly DependencyProperty CommandButtonStyleProperty = DependencyProperty.Register("CommandButtonStyle", typeof(Style), typeof(DXDialogWindow), new PropertyMetadata(null));
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public static readonly DependencyProperty CommandButtonsPanelProperty = DependencyProperty.Register("CommandButtonsPanel", typeof(ItemsPanelTemplate), typeof(DXDialogWindow), new PropertyMetadata(null));
        [Browsable(false), IgnoreDependencyPropertiesConsistencyChecker, EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly DependencyProperty ActualDialogWindowProperty;
        public static readonly DependencyProperty CommandsSourceProperty;
        private static readonly DependencyPropertyKey ActualFooterPropertyKey;
        public static readonly DependencyProperty ActualFooterProperty;
        private static readonly DependencyPropertyKey HasFooterButtonsPropertyKey;
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public static readonly DependencyProperty HasFooterButtonsProperty;
        private readonly Dictionary<UIElement, int> customFootersDictionary;
        private readonly SortedDictionary<int, List<UIElement>> customFooters;
        private bool footerIsWindowLogicalChild;
        private NonLogicalDecorator footerPresenter;
        private readonly DialogButtonCollection footerButtons;
        private readonly HierarchyCollection<object, DXDialogWindow> footerCommandsSourceCollection;
        private IDisposable footerCommandsSourceCollectionBinding;
        internal MessageBoxResult dialogWindowResult;
        internal UICommand dialogWindowResultCommand;
        protected bool IsDialogMode;
        private IList commandsSourceListRef;

        event EventHandler IWindowSurrogate.Activated
        {
            add
            {
                base.Activated += value;
            }
            remove
            {
                base.Activated -= value;
            }
        }

        event EventHandler IWindowSurrogate.Closed
        {
            add
            {
                base.Closed += value;
            }
            remove
            {
                base.Closed -= value;
            }
        }

        event CancelEventHandler IWindowSurrogate.Closing
        {
            add
            {
                base.Closing += value;
            }
            remove
            {
                base.Closing -= value;
            }
        }

        event EventHandler IWindowSurrogate.Deactivated
        {
            add
            {
                base.Deactivated += value;
            }
            remove
            {
                base.Deactivated -= value;
            }
        }

        static DXDialogWindow()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DXDialogWindow), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<DXDialogWindow> registrator1 = DependencyPropertyRegistrator<DXDialogWindow>.New().RegisterReadOnly<bool>(System.Linq.Expressions.Expression.Lambda<Func<DXDialogWindow, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DXDialogWindow.get_HasFooterButtons)), parameters), out HasFooterButtonsPropertyKey, out HasFooterButtonsProperty, false, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DXDialogWindow), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DXDialogWindow> registrator2 = registrator1.RegisterReadOnly<UIElement>(System.Linq.Expressions.Expression.Lambda<Func<DXDialogWindow, UIElement>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DXDialogWindow.get_ActualFooter)), expressionArray2), out ActualFooterPropertyKey, out ActualFooterProperty, null, (d, e) => d.OnActualFooterChanged(e), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DXDialogWindow), "d");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DXDialogWindow> registrator3 = registrator2.Register<IEnumerable<UICommand>>(System.Linq.Expressions.Expression.Lambda<Func<DXDialogWindow, IEnumerable<UICommand>>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DXDialogWindow.get_CommandsSource)), expressionArray3), out CommandsSourceProperty, null, (d, e) => d.OnCommandsSourceChanged(e), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DependencyObject), "d");
            System.Linq.Expressions.Expression[] arguments = new System.Linq.Expressions.Expression[] { expression };
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            registrator3.RegisterAttached<DependencyObject, DXDialogWindow>(System.Linq.Expressions.Expression.Lambda<Func<DependencyObject, DXDialogWindow>>(System.Linq.Expressions.Expression.Call(null, (MethodInfo) methodof(DXDialogWindow.GetActualDialogWindow), arguments), expressionArray5), out ActualDialogWindowProperty, null, 0x20).OverrideDefaultStyleKey();
        }

        public DXDialogWindow()
        {
            this.customFootersDictionary = new Dictionary<UIElement, int>();
            this.customFooters = new SortedDictionary<int, List<UIElement>>();
            this.footerButtons = new DialogButtonCollection();
            Action<object, DXDialogWindow> attachAction = <>c.<>9__66_0;
            if (<>c.<>9__66_0 == null)
            {
                Action<object, DXDialogWindow> local1 = <>c.<>9__66_0;
                attachAction = <>c.<>9__66_0 = (item, owner) => owner.AttachUICommand(item);
            }
            this.footerCommandsSourceCollection = new HierarchyCollection<object, DXDialogWindow>(this, attachAction, <>c.<>9__66_1 ??= (item, owner) => owner.DetachUICommand(item), null, true);
            this.footerCommandsSourceCollection.CollectionChanged += (s, e) => (this.HasFooterButtons = this.footerCommandsSourceCollection.Any<object>());
            this.FooterButtons.CollectionChanged += delegate (object s, NotifyCollectionChangedEventArgs e) {
                if (this.CommandsSource != null)
                {
                    throw new InvalidOperationException("Cannot change FooterButtons collection if CommandSource is set.");
                }
                this.SetFooterCommandsSource(this.FooterButtons);
            };
            this.ActualFooter = this.CreateDefaultFooter();
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            base.Unloaded += new RoutedEventHandler(this.OnUnloaded);
            this.CommandButtons = new DXDialogWindowCommandButtonsCollection();
            SetActualDialogWindow(this, this);
        }

        public DXDialogWindow(string title) : this()
        {
            base.Title = title;
        }

        public DXDialogWindow(string title, IEnumerable<UICommand> commands) : this()
        {
            base.Title = title;
            this.CommandsSource = commands;
        }

        public DXDialogWindow(string title, MessageBoxButton dialogButtons, MessageBoxResult? defaultButton = new MessageBoxResult?(), MessageBoxResult? cancelButton = new MessageBoxResult?()) : this(title, GenerateUICommands(dialogButtons, defaultButton, cancelButton))
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public void AddCustomFooter(UIElement footer)
        {
            Func<DependencyObject, DependencyObject> next = <>c.<>9__26_0;
            if (<>c.<>9__26_0 == null)
            {
                Func<DependencyObject, DependencyObject> local1 = <>c.<>9__26_0;
                next = <>c.<>9__26_0 = x => LayoutHelper.GetParent(x, true);
            }
            int num = LinqExtensions.Unfold<DependencyObject>(footer, next, x => ReferenceEquals(x, this)).Count<DependencyObject>();
            this.customFootersDictionary.Add(footer, num);
            this.customFooters.GetOrAdd<int, List<UIElement>>(num, (<>c.<>9__26_2 ??= () => new List<UIElement>())).Add(footer);
            this.UpdateActualFooter();
        }

        private void AttachUICommand(object item)
        {
            this.DoWithFooterCommand(item, delegate (IUICommand x) {
                x.Executed += new EventHandler(this.OnUICommandExecuted);
            }, delegate (DialogButton x) {
                x.Executed += new EventHandler(this.OnDialogButtonExecuted);
            });
        }

        protected virtual void BeforeShow()
        {
        }

        protected virtual void ClearButton(UIElement element)
        {
            Button button = (Button) element;
            this.UnsubscribeButton(button);
            button.DataContext = null;
        }

        protected virtual void CloseCore(UICommand command)
        {
            MessageBoxResult? nullable1;
            this.dialogWindowResultCommand = command;
            if (command.Tag is MessageBoxResult)
            {
                nullable1 = new MessageBoxResult?((MessageBoxResult) command.Tag);
            }
            else
            {
                nullable1 = null;
            }
            this.CloseDialog(nullable1);
        }

        private void CloseDialog(MessageBoxResult? result)
        {
            if (!this.IsDialogMode)
            {
                base.Close();
            }
            else
            {
                bool? nullable = true;
                if (result != null)
                {
                    this.dialogWindowResult = result.Value;
                    if ((this.dialogWindowResult == MessageBoxResult.OK) || (this.dialogWindowResult == MessageBoxResult.Yes))
                    {
                        nullable = true;
                    }
                    else if ((this.dialogWindowResult != MessageBoxResult.No) && (this.dialogWindowResult != MessageBoxResult.Cancel))
                    {
                        nullable = null;
                    }
                    else
                    {
                        nullable = false;
                    }
                }
                bool? nullable2 = nullable;
                bool? dialogResult = base.DialogResult;
                if ((nullable2.GetValueOrDefault() == dialogResult.GetValueOrDefault()) ? ((nullable2 != null) != (dialogResult != null)) : true)
                {
                    base.DialogResult = nullable;
                }
                else
                {
                    base.Close();
                }
            }
        }

        protected virtual Button CreateButton(UICommand command)
        {
            DXDialogWindowUICommandWrapper wrapper = new DXDialogWindowUICommandWrapper(command);
            Button button1 = new Button();
            button1.DataContext = wrapper;
            Button button = button1;
            button.SetBinding(ButtonBase.CommandProperty, new Binding("RealCommand"));
            button.SetBinding(ContentControl.ContentProperty, new Binding("UICommand.Caption"));
            Binding binding = new Binding();
            binding.Path = new PropertyPath(CommandButtonStyleProperty);
            binding.Source = this;
            button.SetBinding(FrameworkElement.StyleProperty, binding);
            button.SetBinding(Button.IsDefaultProperty, new Binding("UICommand.IsDefault"));
            button.SetBinding(Button.IsCancelProperty, new Binding("UICommand.IsCancel"));
            this.SubscribeButton(button);
            return button;
        }

        protected virtual DialogButtonsControl CreateDefaultFooter()
        {
            DialogButtonsControl control = new DialogButtonsControl();
            Binding binding = new Binding();
            binding.Path = new PropertyPath(DialogButtonsControl.CommandsSourceProperty);
            binding.Source = control;
            binding.Mode = BindingMode.OneWay;
            control.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            return control;
        }

        private void DetachUICommand(object item)
        {
            this.DoWithFooterCommand(item, delegate (IUICommand x) {
                x.Executed -= new EventHandler(this.OnUICommandExecuted);
            }, delegate (DialogButton x) {
                x.Executed -= new EventHandler(this.OnDialogButtonExecuted);
            });
        }

        bool IWindowSurrogate.Activate() => 
            base.Activate();

        void IWindowSurrogate.Close()
        {
            base.Close();
        }

        void IWindowSurrogate.Hide()
        {
            base.Hide();
        }

        void IWindowSurrogate.Show()
        {
            this.Show();
        }

        bool? IWindowSurrogate.ShowDialog() => 
            this.ShowDialog();

        private void DoWithFooterCommand(object commandsSourceItem, Action<IUICommand> uiCommandAction, Action<DialogButton> uiCommandButtonAction)
        {
            UICommandContainer container = commandsSourceItem as UICommandContainer;
            if (container != null)
            {
                IUICommand uICommand = container.UICommand;
                if (uICommand != null)
                {
                    uiCommandAction(uICommand);
                }
            }
            else
            {
                DialogButton button = commandsSourceItem as DialogButton;
                if (button != null)
                {
                    uiCommandButtonAction(button);
                }
            }
        }

        public static List<UICommand> GenerateUICommands(MessageBoxButton dialogButtons, MessageBoxResult? defaultButton = new MessageBoxResult?(), MessageBoxResult? cancelButton = new MessageBoxResult?()) => 
            UICommand.GenerateFromMessageBoxButton(dialogButtons, new DXDialogWindowMessageBoxButtonLocalizer(), defaultButton, cancelButton);

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static DXDialogWindow GetActualDialogWindow(DependencyObject d) => 
            (DXDialogWindow) d.GetValue(ActualDialogWindowProperty);

        private static List<UICommand> GetDefaultAndCancelCommands(IEnumerable<UICommand> commandsSource, bool includeDefaultButtons = true, bool includeCancelButtons = true)
        {
            if (commandsSource == null)
            {
                return null;
            }
            List<UICommand> list = new List<UICommand>();
            foreach (UICommand command in commandsSource)
            {
                if (command.IsDefault & includeDefaultButtons)
                {
                    list.Add(command);
                }
                if (command.IsCancel & includeCancelButtons)
                {
                    list.Add(command);
                }
            }
            return list;
        }

        private void OnActualFooterChanged(DependencyPropertyChangedEventArgs e)
        {
            UIElement oldValue = (UIElement) e.OldValue;
            UIElement newValue = (UIElement) e.NewValue;
            if (oldValue != null)
            {
                BindingOperations.ClearBinding(oldValue, DialogButtonsControl.CommandButtonStyleProperty);
                if (this.footerIsWindowLogicalChild)
                {
                    base.RemoveLogicalChild(oldValue);
                }
                this.footerIsWindowLogicalChild = false;
            }
            if (newValue != null)
            {
                this.footerIsWindowLogicalChild = ReferenceEquals(LogicalTreeHelper.GetParent(newValue), null);
                if (this.footerIsWindowLogicalChild)
                {
                    base.AddLogicalChild(newValue);
                }
                Binding binding = new Binding();
                binding.Path = new PropertyPath(CommandButtonStyleProperty);
                binding.Source = this;
                binding.Mode = BindingMode.OneWay;
                BindingOperations.SetBinding(newValue, DialogButtonsControl.CommandButtonStyleProperty, binding);
            }
            this.UpdateFooterPresenter();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.footerPresenter != null)
            {
                this.footerPresenter.Child = null;
                this.footerPresenter = null;
            }
            this.footerPresenter = (NonLogicalDecorator) base.GetTemplateChild("FooterPresenter");
            this.UpdateFooterPresenter();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            this.OnClosedCore();
        }

        private void OnClosedCore()
        {
            this.IsDialogMode = false;
            if (this.CommandsSource != null)
            {
                this.CommandButtons.ClearSource();
                this.SetFooterCommandsSource(null);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (e.Cancel)
            {
                this.dialogWindowResult = MessageBoxResult.None;
                this.dialogWindowResultCommand = null;
            }
        }

        private void OnCommandsSourceChanged(DependencyPropertyChangedEventArgs e)
        {
            IEnumerable<UICommand> oldValue = (IEnumerable<UICommand>) e.OldValue;
            IEnumerable<UICommand> newValue = (IEnumerable<UICommand>) e.NewValue;
            if (this.FooterButtons.Count != 0)
            {
                throw new InvalidOperationException("Cannot use CommandsSource if FooterButtons collection is not empty.");
            }
            if (newValue == null)
            {
                this.commandsSourceListRef = null;
                this.SetFooterCommandsSource(null);
            }
            else
            {
                UICommandContainerCollection footerCommandsSource = new UICommandContainerCollection();
                this.commandsSourceListRef = newValue as IList;
                this.commandsSourceListRef ??= newValue.ToList<UICommand>();
                UICommandContainerCollection collection1 = footerCommandsSource;
                if (<>c.<>9__87_0 == null)
                {
                    UICommandContainerCollection local1 = footerCommandsSource;
                    collection1 = (UICommandContainerCollection) (<>c.<>9__87_0 = delegate (UICommand command) {
                        Func<UICommand, UICommandContainer> evaluator = <>c.<>9__87_1;
                        if (<>c.<>9__87_1 == null)
                        {
                            Func<UICommand, UICommandContainer> local1 = <>c.<>9__87_1;
                            evaluator = <>c.<>9__87_1 = x => new UICommandContainer(x);
                        }
                        return command.With<UICommand, UICommandContainer>(evaluator);
                    });
                }
                CollectionBindingHelper.Bind<UICommandContainer, UICommand>((IList<UICommandContainer>) <>c.<>9__87_0, (Func<UICommand, UICommandContainer>) collection1, <>c.<>9__87_2 ??= x => x.UICommand, this.commandsSourceListRef, false, false);
                this.SetFooterCommandsSource(footerCommandsSource);
            }
            this.CommandButtons.SetSource(newValue, new Func<UICommand, UIElement>(this.CreateButton), new Action<UIElement>(this.ClearButton), true);
        }

        private void OnDialogButtonExecuted(object sender, EventArgs e)
        {
            MessageResult? dialogResult = ((DialogButton) sender).DialogResult;
            if (dialogResult != null)
            {
                this.CloseDialog(new MessageBoxResult?(dialogResult.Value.ToMessageBoxResult()));
            }
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            foreach (UIElement element in this.CommandButtons)
            {
                if (element is Button)
                {
                    this.SubscribeButton((Button) element);
                }
            }
        }

        private void OnUICommandExecuted(object sender, EventArgs e)
        {
            UICommand command = (UICommand) sender;
            if ((command != null) && command.AllowCloseWindow)
            {
                this.CloseCore((UICommand) sender);
            }
        }

        private void OnUnloaded(object sender, EventArgs e)
        {
            foreach (UIElement element in this.CommandButtons)
            {
                if (element is Button)
                {
                    this.UnsubscribeButton((Button) element);
                }
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void RemoveCustomFooter(UIElement footer)
        {
            int key = this.customFootersDictionary[footer];
            this.customFootersDictionary.Remove(footer);
            List<UIElement> list = this.customFooters[key];
            list.Remove(footer);
            if (list.Count == 0)
            {
                this.customFooters.Remove(key);
            }
            this.UpdateActualFooter();
        }

        private static void SetActualDialogWindow(DependencyObject d, DXDialogWindow window)
        {
            d.SetValue(ActualDialogWindowProperty, window);
        }

        private void SetFooterCommandsSource(IEnumerable footerCommandsSource)
        {
            if (this.footerCommandsSourceCollectionBinding != null)
            {
                this.footerCommandsSourceCollectionBinding.Dispose();
            }
            this.footerCommandsSourceCollectionBinding = null;
            this.footerCommandsSourceCollection.Clear();
            if (footerCommandsSource != null)
            {
                Func<object, object> itemConverter = <>c.<>9__42_0;
                if (<>c.<>9__42_0 == null)
                {
                    Func<object, object> local1 = <>c.<>9__42_0;
                    itemConverter = <>c.<>9__42_0 = x => x;
                }
                this.footerCommandsSourceCollectionBinding = CollectionBindingHelper.BindOneWay<object, object>(this.footerCommandsSourceCollection, itemConverter, footerCommandsSource, false, false);
            }
            DialogButtonsControl.SetCommandsSource(this, footerCommandsSource);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void Show()
        {
            this.BeforeShow();
            base.Show();
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool? ShowDialog()
        {
            this.ShowDialogWindowCore();
            return base.DialogResult;
        }

        public UICommand ShowDialogWindow()
        {
            this.ShowDialogWindowUICommand();
            if ((this.CommandsSource != null) && ((this.dialogWindowResult == MessageBoxResult.None) && (this.dialogWindowResultCommand == null)))
            {
                UICommand command = this.CommandsSource.FirstOrDefault<UICommand>(UICommandPredicate(MessageBoxResult.Cancel));
                command ??= this.CommandsSource.FirstOrDefault<UICommand>(UICommandPredicate(MessageBoxResult.No));
                command ??= this.CommandsSource.FirstOrDefault<UICommand>(UICommandPredicate(MessageBoxResult.OK));
                if (command != null)
                {
                    this.dialogWindowResult = (MessageBoxResult) command.Id;
                }
            }
            return this.dialogWindowResultCommand;
        }

        public MessageBoxResult ShowDialogWindow(MessageBoxButton dialogButtons, MessageBoxResult? defaultButton = new MessageBoxResult?(), MessageBoxResult? cancelButton = new MessageBoxResult?())
        {
            this.ShowDialogWindowMessageBoxResult(dialogButtons, defaultButton, cancelButton);
            return this.dialogWindowResult;
        }

        protected virtual void ShowDialogWindowCore()
        {
            this.BeforeShow();
            this.IsDialogMode = true;
            this.dialogWindowResult = MessageBoxResult.None;
            this.dialogWindowResultCommand = null;
            base.ShowDialog();
        }

        private void ShowDialogWindowMessageBoxResult(MessageBoxButton dialogButtons, MessageBoxResult? defaultButton = new MessageBoxResult?(), MessageBoxResult? cancelButton = new MessageBoxResult?())
        {
            if ((this.CommandsSource == null) && (this.CommandButtons.Count > 0))
            {
                throw new InvalidOperationException("Cannot use ShowDialogWindow(MessageBoxButton dialogButtons) if CommandButtons collection is not empty. \r\n            Use ShowDialogWindow() instead.");
            }
            this.CommandsSource = GenerateUICommands(dialogButtons, defaultButton, cancelButton);
            this.ShowDialogWindowCore();
        }

        private void ShowDialogWindowUICommand()
        {
            this.ShowDialogWindowCore();
        }

        private void SubscribeButton(Button button)
        {
            this.UnsubscribeButton(button);
            DXDialogWindowUICommandWrapper dataContext = button.DataContext as DXDialogWindowUICommandWrapper;
            if (dataContext != null)
            {
                dataContext.Subscribe();
                dataContext.CommandExecuted += new CancelEventHandler(this.UICommandExecuted);
            }
        }

        private void UICommandExecuted(object sender, CancelEventArgs e)
        {
            UICommand uICommand = ((DXDialogWindowUICommandWrapper) sender).UICommand;
            if (!e.Cancel)
            {
                this.CloseCore(uICommand);
            }
        }

        private static Func<UICommand, bool> UICommandPredicate(MessageBoxResult messageBoxResult) => 
            delegate (UICommand x) {
                if (x.Id == null)
                {
                    return false;
                }
                MessageBoxResult? id = x.Id as MessageBoxResult?;
                return ((MessageBoxResult) ((id != null) ? id.GetValueOrDefault() : MessageBoxResult.None)) == messageBoxResult;
            };

        private void UnsubscribeButton(Button button)
        {
            DXDialogWindowUICommandWrapper dataContext = button.DataContext as DXDialogWindowUICommandWrapper;
            if (dataContext != null)
            {
                dataContext.Unsubscribe();
                dataContext.CommandExecuted -= new CancelEventHandler(this.UICommandExecuted);
            }
        }

        private void UpdateActualFooter()
        {
            UIElement local3;
            List<UIElement> source = this.customFooters.Values.LastOrDefault<List<UIElement>>();
            if (source != null)
            {
                local3 = source.First<UIElement>();
            }
            else
            {
                List<UIElement> local2 = source;
                local3 = null;
            }
            this.ActualFooter = local3;
        }

        private void UpdateFooterPresenter()
        {
            this.footerPresenter.Do<NonLogicalDecorator>(x => x.Child = this.ActualFooter);
            base.Dispatcher.BeginInvoke(delegate {
                Func<NonLogicalDecorator, ContentPresenter> evaluator = <>c.<>9__33_2;
                if (<>c.<>9__33_2 == null)
                {
                    Func<NonLogicalDecorator, ContentPresenter> local1 = <>c.<>9__33_2;
                    evaluator = <>c.<>9__33_2 = x => LayoutTreeHelper.GetVisualParents(x, null).OfType<ContentPresenter>().FirstOrDefault<ContentPresenter>();
                }
                Action<ContentPresenter> action = <>c.<>9__33_3;
                if (<>c.<>9__33_3 == null)
                {
                    Action<ContentPresenter> local2 = <>c.<>9__33_3;
                    action = <>c.<>9__33_3 = x => x.InvalidateMeasure();
                }
                this.footerPresenter.With<NonLogicalDecorator, ContentPresenter>(evaluator).Do<ContentPresenter>(action);
            }, new object[0]);
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool HasFooterButtons
        {
            get => 
                (bool) base.GetValue(HasFooterButtonsProperty);
            private set => 
                base.SetValue(HasFooterButtonsPropertyKey, value);
        }

        public UIElement ActualFooter
        {
            get => 
                (UIElement) base.GetValue(ActualFooterProperty);
            private set => 
                base.SetValue(ActualFooterPropertyKey, value);
        }

        protected override IEnumerator LogicalChildren
        {
            get
            {
                IEnumerator[] args = new IEnumerator[] { base.LogicalChildren, this.footerIsWindowLogicalChild ? new SingleLogicalChildEnumerator(this.ActualFooter) : null };
                return new MergedEnumerator(args);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DialogButtonCollection FooterButtons =>
            this.footerButtons;

        public IEnumerable<UICommand> CommandsSource
        {
            get => 
                (IEnumerable<UICommand>) base.GetValue(CommandsSourceProperty);
            set => 
                base.SetValue(CommandsSourceProperty, value);
        }

        public Style CommandButtonStyle
        {
            get => 
                (Style) base.GetValue(CommandButtonStyleProperty);
            set => 
                base.SetValue(CommandButtonStyleProperty, value);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the FooterTemplate property instead")]
        public ItemsPanelTemplate CommandButtonsPanel
        {
            get => 
                (ItemsPanelTemplate) base.GetValue(CommandButtonsPanelProperty);
            set => 
                base.SetValue(CommandButtonsPanelProperty, value);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Obsolete("Use the FooterButtons property instead"), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public DXDialogWindowCommandButtonsCollection CommandButtons { get; private set; }

        Window IWindowSurrogate.RealWindow =>
            this;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXDialogWindow.<>c <>9 = new DXDialogWindow.<>c();
            public static Func<DependencyObject, DependencyObject> <>9__26_0;
            public static Func<List<UIElement>> <>9__26_2;
            public static Func<NonLogicalDecorator, ContentPresenter> <>9__33_2;
            public static Action<ContentPresenter> <>9__33_3;
            public static Func<object, object> <>9__42_0;
            public static Action<object, DXDialogWindow> <>9__66_0;
            public static Action<object, DXDialogWindow> <>9__66_1;
            public static Func<UICommand, UICommandContainer> <>9__87_1;
            public static Func<UICommand, UICommandContainer> <>9__87_0;
            public static Func<UICommandContainer, UICommand> <>9__87_2;

            internal void <.cctor>b__15_0(DXDialogWindow d, DependencyPropertyChangedEventArgs e)
            {
                d.OnActualFooterChanged(e);
            }

            internal void <.cctor>b__15_1(DXDialogWindow d, DependencyPropertyChangedEventArgs e)
            {
                d.OnCommandsSourceChanged(e);
            }

            internal void <.ctor>b__66_0(object item, DXDialogWindow owner)
            {
                owner.AttachUICommand(item);
            }

            internal void <.ctor>b__66_1(object item, DXDialogWindow owner)
            {
                owner.DetachUICommand(item);
            }

            internal DependencyObject <AddCustomFooter>b__26_0(DependencyObject x) => 
                LayoutHelper.GetParent(x, true);

            internal List<UIElement> <AddCustomFooter>b__26_2() => 
                new List<UIElement>();

            internal UICommandContainer <OnCommandsSourceChanged>b__87_0(UICommand command)
            {
                Func<UICommand, UICommandContainer> evaluator = <>9__87_1;
                if (<>9__87_1 == null)
                {
                    Func<UICommand, UICommandContainer> local1 = <>9__87_1;
                    evaluator = <>9__87_1 = x => new UICommandContainer(x);
                }
                return command.With<UICommand, UICommandContainer>(evaluator);
            }

            internal UICommandContainer <OnCommandsSourceChanged>b__87_1(UICommand x) => 
                new UICommandContainer(x);

            internal UICommand <OnCommandsSourceChanged>b__87_2(UICommandContainer x) => 
                x.UICommand;

            internal object <SetFooterCommandsSource>b__42_0(object x) => 
                x;

            internal ContentPresenter <UpdateFooterPresenter>b__33_2(NonLogicalDecorator x) => 
                LayoutTreeHelper.GetVisualParents(x, null).OfType<ContentPresenter>().FirstOrDefault<ContentPresenter>();

            internal void <UpdateFooterPresenter>b__33_3(ContentPresenter x)
            {
                x.InvalidateMeasure();
            }
        }
    }
}

