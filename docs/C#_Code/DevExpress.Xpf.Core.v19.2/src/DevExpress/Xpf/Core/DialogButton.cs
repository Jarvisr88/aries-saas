namespace DevExpress.Xpf.Core
{
    using DevExpress.Data.Utils;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class DialogButton : ContentControl
    {
        public static readonly object NotSetContent = new object();
        public static readonly ICommand NotSetCommand = new NotSetCommandImpl();
        public static readonly DependencyProperty CommandButtonStyleProperty;
        public static readonly DependencyProperty CommandProperty;
        public static readonly DependencyProperty IsCancelProperty;
        public static readonly DependencyProperty IsDefaultProperty;
        private static readonly DependencyPropertyKey ActualCommandPropertyKey;
        public static readonly DependencyProperty ActualCommandProperty;
        private static readonly DependencyPropertyKey ActualContentPropertyKey;
        public static readonly DependencyProperty ActualContentProperty;
        private static readonly DependencyPropertyKey ActualIsCancelPropertyKey;
        public static readonly DependencyProperty ActualIsCancelProperty;
        private static readonly DependencyPropertyKey ActualIsDefaultPropertyKey;
        public static readonly DependencyProperty ActualIsDefaultProperty;
        public static readonly DependencyProperty DialogUICommandTagProperty;
        public static readonly DependencyProperty DialogResultProperty;
        public static readonly DependencyProperty CommandsSourceProperty;
        private static readonly DependencyPropertyKey UICommandPropertyKey;
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public static readonly DependencyProperty UICommandProperty;
        private static readonly DependencyPropertyKey CommandButtonVisibilityPropertyKey;
        public static readonly DependencyProperty CommandButtonVisibilityProperty;
        private readonly PropertyChangedWeakEventHandler<DialogButton> onUICommandPropertyChanged;
        private object actualDialogUICommandTag;
        private readonly HierarchyCollection<DevExpress.Mvvm.UICommand, DialogButton> commandsSourceCollection;
        private IDisposable commandsSourceCollectionBinding;

        internal event EventHandler Executed;

        static DialogButton()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DialogButton), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            DependencyPropertyRegistrator<DialogButton> registrator1 = DependencyPropertyRegistrator<DialogButton>.New().OverrideMetadata(ContentControl.ContentProperty, NotSetContent, d => d.UpdateActualContent(), FrameworkPropertyMetadataOptions.None).AddOwner<Style>(System.Linq.Expressions.Expression.Lambda<Func<DialogButton, Style>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DialogButton.get_CommandButtonStyle)), parameters), out CommandButtonStyleProperty, DialogButtonsControl.CommandButtonStyleProperty);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DialogButton), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<DialogButton> registrator2 = registrator1.Register<ICommand>(System.Linq.Expressions.Expression.Lambda<Func<DialogButton, ICommand>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DialogButton.get_Command)), expressionArray2), out CommandProperty, NotSetCommand, d => d.UpdateActualCommand(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DialogButton), "d");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            bool? defaultValue = null;
            frameworkOptions = null;
            DependencyPropertyRegistrator<DialogButton> registrator3 = registrator2.Register<bool?>(System.Linq.Expressions.Expression.Lambda<Func<DialogButton, bool?>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DialogButton.get_IsDefault)), expressionArray3), out IsDefaultProperty, defaultValue, d => d.UpdateActualIsDefault(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DialogButton), "d");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            defaultValue = null;
            frameworkOptions = null;
            DependencyPropertyRegistrator<DialogButton> registrator4 = registrator3.Register<bool?>(System.Linq.Expressions.Expression.Lambda<Func<DialogButton, bool?>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DialogButton.get_IsCancel)), expressionArray4), out IsCancelProperty, defaultValue, d => d.UpdateActualIsCancel(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DialogButton), "d");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DialogButton> registrator5 = registrator4.RegisterReadOnly<ICommand>(System.Linq.Expressions.Expression.Lambda<Func<DialogButton, ICommand>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DialogButton.get_ActualCommand)), expressionArray5), out ActualCommandPropertyKey, out ActualCommandProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DialogButton), "d");
            ParameterExpression[] expressionArray6 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DialogButton> registrator6 = registrator5.RegisterReadOnly<object>(System.Linq.Expressions.Expression.Lambda<Func<DialogButton, object>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DialogButton.get_ActualContent)), expressionArray6), out ActualContentPropertyKey, out ActualContentProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DialogButton), "d");
            ParameterExpression[] expressionArray7 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DialogButton> registrator7 = registrator6.RegisterReadOnly<bool>(System.Linq.Expressions.Expression.Lambda<Func<DialogButton, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DialogButton.get_ActualIsDefault)), expressionArray7), out ActualIsDefaultPropertyKey, out ActualIsDefaultProperty, false, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DialogButton), "d");
            ParameterExpression[] expressionArray8 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DialogButton> registrator8 = registrator7.RegisterReadOnly<bool>(System.Linq.Expressions.Expression.Lambda<Func<DialogButton, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DialogButton.get_ActualIsCancel)), expressionArray8), out ActualIsCancelPropertyKey, out ActualIsCancelProperty, false, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DialogButton), "d");
            ParameterExpression[] expressionArray9 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DialogButton> registrator9 = registrator8.Register<object>(System.Linq.Expressions.Expression.Lambda<Func<DialogButton, object>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DialogButton.get_DialogUICommandTag)), expressionArray9), out DialogUICommandTagProperty, null, d => d.OnDialogUICommandTagChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DialogButton), "d");
            ParameterExpression[] expressionArray10 = new ParameterExpression[] { expression };
            MessageResult? nullable3 = null;
            frameworkOptions = null;
            DependencyPropertyRegistrator<DialogButton> registrator10 = registrator9.Register<MessageResult?>(System.Linq.Expressions.Expression.Lambda<Func<DialogButton, MessageResult?>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DialogButton.get_DialogResult)), expressionArray10), out DialogResultProperty, nullable3, d => d.OnDialogUICommandTagChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DialogButton), "d");
            ParameterExpression[] expressionArray11 = new ParameterExpression[] { expression };
            DependencyPropertyRegistrator<DialogButton> registrator11 = registrator10.AddOwner<IEnumerable>(System.Linq.Expressions.Expression.Lambda<Func<DialogButton, IEnumerable>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DialogButton.get_CommandsSource)), expressionArray11), out CommandsSourceProperty, DialogButtonsControl.CommandsSourceProperty, null, (d, e) => d.OnCommandsSourceChanged(e));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DialogButton), "d");
            ParameterExpression[] expressionArray12 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DialogButton> registrator12 = registrator11.RegisterReadOnly<DevExpress.Mvvm.UICommand>(System.Linq.Expressions.Expression.Lambda<Func<DialogButton, DevExpress.Mvvm.UICommand>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DialogButton.get_UICommand)), expressionArray12), out UICommandPropertyKey, out UICommandProperty, null, (d, e) => d.OnUICommandChanged(e), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DialogButton), "d");
            ParameterExpression[] expressionArray13 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator12.RegisterReadOnly<Visibility>(System.Linq.Expressions.Expression.Lambda<Func<DialogButton, Visibility>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DialogButton.get_CommandButtonVisibility)), expressionArray13), out CommandButtonVisibilityPropertyKey, out CommandButtonVisibilityProperty, Visibility.Visible, frameworkOptions).OverrideDefaultStyleKey();
        }

        public DialogButton()
        {
            Action<DialogButton, object, PropertyChangedEventArgs> onEventAction = <>c.<>9__24_0;
            if (<>c.<>9__24_0 == null)
            {
                Action<DialogButton, object, PropertyChangedEventArgs> local1 = <>c.<>9__24_0;
                onEventAction = <>c.<>9__24_0 = (owner, sender, e) => owner.OnUICommandPropertyChanged(sender, e);
            }
            this.onUICommandPropertyChanged = new PropertyChangedWeakEventHandler<DialogButton>(this, onEventAction);
            Action<DevExpress.Mvvm.UICommand, DialogButton> attachAction = <>c.<>9__24_1;
            if (<>c.<>9__24_1 == null)
            {
                Action<DevExpress.Mvvm.UICommand, DialogButton> local2 = <>c.<>9__24_1;
                attachAction = <>c.<>9__24_1 = (item, owner) => owner.StartTrackCommandSourceItem(item);
            }
            this.commandsSourceCollection = new HierarchyCollection<DevExpress.Mvvm.UICommand, DialogButton>(this, attachAction, <>c.<>9__24_2 ??= (item, onwer) => onwer.StopTrackCommandSourceItem(item), null, true);
            this.UpdateActualCommand();
        }

        public static DevExpress.Mvvm.UICommand GetUICommandByDialogResult(IEnumerable<DevExpress.Mvvm.UICommand> uiCommands, MessageResult dialogResult) => 
            GetUICommandByTag(uiCommands, dialogResult.ToString());

        public static DevExpress.Mvvm.UICommand GetUICommandByTag(IEnumerable<DevExpress.Mvvm.UICommand> uiCommands, object tag)
        {
            DevExpress.Mvvm.UICommand command = tag as DevExpress.Mvvm.UICommand;
            if (command != null)
            {
                return command;
            }
            string tagString = tag as string;
            return ((tagString == null) ? uiCommands.Where<DevExpress.Mvvm.UICommand>(delegate (DevExpress.Mvvm.UICommand uiCommand) {
                Func<DevExpress.Mvvm.UICommand, object> evaluator = <>c.<>9__78_4;
                if (<>c.<>9__78_4 == null)
                {
                    Func<DevExpress.Mvvm.UICommand, object> local1 = <>c.<>9__78_4;
                    evaluator = <>c.<>9__78_4 = x => x.Tag;
                }
                return Equals(uiCommand.With<DevExpress.Mvvm.UICommand, object>(evaluator), tag);
            }).FirstOrDefault<DevExpress.Mvvm.UICommand>() : uiCommands.Where<DevExpress.Mvvm.UICommand>(delegate (DevExpress.Mvvm.UICommand uiCommand) {
                Func<DevExpress.Mvvm.UICommand, object> evaluator = <>c.<>9__78_1;
                if (<>c.<>9__78_1 == null)
                {
                    Func<DevExpress.Mvvm.UICommand, object> local1 = <>c.<>9__78_1;
                    evaluator = <>c.<>9__78_1 = x => x.Tag;
                }
                Func<object, string> func2 = <>c.<>9__78_2;
                if (<>c.<>9__78_2 == null)
                {
                    Func<object, string> local2 = <>c.<>9__78_2;
                    func2 = <>c.<>9__78_2 = x => x.ToString();
                }
                return string.Equals(uiCommand.With<DevExpress.Mvvm.UICommand, object>(evaluator).With<object, string>(func2), tagString, StringComparison.Ordinal);
            }).FirstOrDefault<DevExpress.Mvvm.UICommand>());
        }

        private void OnCommandsSourceChanged(DependencyPropertyChangedEventArgs e)
        {
            this.UpdateCommandsSourceCollection();
        }

        private void OnCommandsSourceItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.PropertyName))
            {
                ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DevExpress.Mvvm.UICommand), "x");
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                if (e.PropertyName != ExpressionHelper.GetPropertyName<DevExpress.Mvvm.UICommand, object>(System.Linq.Expressions.Expression.Lambda<Func<DevExpress.Mvvm.UICommand, object>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DevExpress.Mvvm.UICommand.get_Tag)), parameters)))
                {
                    return;
                }
            }
            this.UpdateUICommand();
        }

        private void OnDialogUICommandTagChanged()
        {
            this.actualDialogUICommandTag = (this.DialogResult == null) ? this.DialogUICommandTag : this.DialogResult.Value.ToString();
            this.UpdateCommandsSourceCollection();
        }

        private void OnUICommandChanged(DependencyPropertyChangedEventArgs e)
        {
            DevExpress.Mvvm.UICommand oldValue = (DevExpress.Mvvm.UICommand) e.OldValue;
            DevExpress.Mvvm.UICommand newValue = (DevExpress.Mvvm.UICommand) e.NewValue;
            if (oldValue != null)
            {
                oldValue.PropertyChanged -= this.onUICommandPropertyChanged.Handler;
            }
            if (newValue != null)
            {
                newValue.PropertyChanged += this.onUICommandPropertyChanged.Handler;
            }
            this.UpdateActualProperties();
        }

        private void OnUICommandPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.UpdateActualProperties();
        }

        private void StartTrackCommandSourceItem(DevExpress.Mvvm.UICommand uiCommand)
        {
            if (uiCommand != null)
            {
                uiCommand.PropertyChanged += new PropertyChangedEventHandler(this.OnCommandsSourceItemPropertyChanged);
            }
        }

        private void StopTrackCommandSourceItem(DevExpress.Mvvm.UICommand uiCommand)
        {
            if (uiCommand != null)
            {
                uiCommand.PropertyChanged -= new PropertyChangedEventHandler(this.OnCommandsSourceItemPropertyChanged);
            }
        }

        private void UpdateActualCommand()
        {
            UICommandWrapper actualCommand = (UICommandWrapper) this.ActualCommand;
            this.ActualCommand = null;
            if (actualCommand != null)
            {
                actualCommand.Destroy();
            }
            this.ActualCommand = new UICommandWrapper(this.UICommand, Equals(this.Command, NotSetCommand) ? null : this.Command, delegate {
                if (this.Executed != null)
                {
                    this.Executed(this, EventArgs.Empty);
                }
            });
        }

        private void UpdateActualContent()
        {
            object content;
            if (!Equals(base.Content, NotSetContent))
            {
                content = base.Content;
            }
            else
            {
                Func<DevExpress.Mvvm.UICommand, object> evaluator = <>c.<>9__41_0;
                if (<>c.<>9__41_0 == null)
                {
                    Func<DevExpress.Mvvm.UICommand, object> local1 = <>c.<>9__41_0;
                    evaluator = <>c.<>9__41_0 = x => x.Caption;
                }
                content = this.UICommand.With<DevExpress.Mvvm.UICommand, object>(evaluator);
            }
            this.ActualContent = content;
        }

        private void UpdateActualIsCancel()
        {
            this.ActualIsCancel = (this.IsCancel != null) ? this.IsCancel.Value : ((this.UICommand != null) ? this.UICommand.IsCancel : false);
        }

        private void UpdateActualIsDefault()
        {
            this.ActualIsDefault = (this.IsDefault != null) ? this.IsDefault.Value : ((this.UICommand != null) ? this.UICommand.IsDefault : false);
        }

        private void UpdateActualProperties()
        {
            this.UpdateActualCommand();
            this.UpdateActualContent();
            this.UpdateActualIsDefault();
            this.UpdateActualIsCancel();
        }

        private void UpdateCommandsSourceCollection()
        {
            if (this.commandsSourceCollectionBinding != null)
            {
                this.commandsSourceCollectionBinding.Dispose();
            }
            this.commandsSourceCollectionBinding = null;
            if ((this.actualDialogUICommandTag == null) || (this.CommandsSource == null))
            {
                this.commandsSourceCollection.Clear();
            }
            else
            {
                Func<object, DevExpress.Mvvm.UICommand> itemConverter = <>c.<>9__70_0;
                if (<>c.<>9__70_0 == null)
                {
                    Func<object, DevExpress.Mvvm.UICommand> local1 = <>c.<>9__70_0;
                    itemConverter = <>c.<>9__70_0 = x => UICommandContainer.GetUICommand(x);
                }
                this.commandsSourceCollectionBinding = CollectionBindingHelper.BindOneWay<DevExpress.Mvvm.UICommand, object>(this.commandsSourceCollection, itemConverter, this.CommandsSource, false, false);
            }
            this.UpdateUICommand();
        }

        private void UpdateUICommand()
        {
            if ((this.actualDialogUICommandTag != null) && ((this.CommandsSource != null) && !(this.CommandsSource is DialogButtonCollection)))
            {
                this.UICommand = GetUICommandByTag(this.commandsSourceCollection, this.actualDialogUICommandTag);
                this.CommandButtonVisibility = (this.UICommand == null) ? Visibility.Collapsed : Visibility.Visible;
            }
            else
            {
                this.UICommand = null;
                this.CommandButtonVisibility = Visibility.Visible;
            }
        }

        public Style CommandButtonStyle
        {
            get => 
                (Style) base.GetValue(CommandButtonStyleProperty);
            set => 
                base.SetValue(CommandButtonStyleProperty, value);
        }

        public ICommand Command
        {
            get => 
                (ICommand) base.GetValue(CommandProperty);
            set => 
                base.SetValue(CommandProperty, value);
        }

        public bool? IsDefault
        {
            get => 
                (bool?) base.GetValue(IsDefaultProperty);
            set => 
                base.SetValue(IsDefaultProperty, value);
        }

        public bool? IsCancel
        {
            get => 
                (bool?) base.GetValue(IsCancelProperty);
            set => 
                base.SetValue(IsCancelProperty, value);
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public ICommand ActualCommand
        {
            get => 
                (ICommand) base.GetValue(ActualCommandProperty);
            private set => 
                base.SetValue(ActualCommandPropertyKey, value);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public object ActualContent
        {
            get => 
                base.GetValue(ActualContentProperty);
            private set => 
                base.SetValue(ActualContentPropertyKey, value);
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool ActualIsDefault
        {
            get => 
                (bool) base.GetValue(ActualIsDefaultProperty);
            private set => 
                base.SetValue(ActualIsDefaultPropertyKey, value);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool ActualIsCancel
        {
            get => 
                (bool) base.GetValue(ActualIsCancelProperty);
            private set => 
                base.SetValue(ActualIsCancelPropertyKey, value);
        }

        public IEnumerable CommandsSource
        {
            get => 
                (IEnumerable) base.GetValue(CommandsSourceProperty);
            set => 
                base.SetValue(CommandsSourceProperty, value);
        }

        public object DialogUICommandTag
        {
            get => 
                base.GetValue(DialogUICommandTagProperty);
            set => 
                base.SetValue(DialogUICommandTagProperty, value);
        }

        public MessageResult? DialogResult
        {
            get => 
                (MessageResult?) base.GetValue(DialogResultProperty);
            set => 
                base.SetValue(DialogResultProperty, value);
        }

        public Visibility CommandButtonVisibility
        {
            get => 
                (Visibility) base.GetValue(CommandButtonVisibilityProperty);
            private set => 
                base.SetValue(CommandButtonVisibilityPropertyKey, value);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public DevExpress.Mvvm.UICommand UICommand
        {
            get => 
                (DevExpress.Mvvm.UICommand) base.GetValue(UICommandProperty);
            private set => 
                base.SetValue(UICommandPropertyKey, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DialogButton.<>c <>9 = new DialogButton.<>c();
            public static Action<DialogButton, object, PropertyChangedEventArgs> <>9__24_0;
            public static Action<UICommand, DialogButton> <>9__24_1;
            public static Action<UICommand, DialogButton> <>9__24_2;
            public static Func<UICommand, object> <>9__41_0;
            public static Func<object, UICommand> <>9__70_0;
            public static Func<UICommand, object> <>9__78_1;
            public static Func<object, string> <>9__78_2;
            public static Func<UICommand, object> <>9__78_4;

            internal void <.cctor>b__22_0(DialogButton d)
            {
                d.UpdateActualContent();
            }

            internal void <.cctor>b__22_1(DialogButton d)
            {
                d.UpdateActualCommand();
            }

            internal void <.cctor>b__22_2(DialogButton d)
            {
                d.UpdateActualIsDefault();
            }

            internal void <.cctor>b__22_3(DialogButton d)
            {
                d.UpdateActualIsCancel();
            }

            internal void <.cctor>b__22_4(DialogButton d)
            {
                d.OnDialogUICommandTagChanged();
            }

            internal void <.cctor>b__22_5(DialogButton d)
            {
                d.OnDialogUICommandTagChanged();
            }

            internal void <.cctor>b__22_6(DialogButton d, DependencyPropertyChangedEventArgs e)
            {
                d.OnCommandsSourceChanged(e);
            }

            internal void <.cctor>b__22_7(DialogButton d, DependencyPropertyChangedEventArgs e)
            {
                d.OnUICommandChanged(e);
            }

            internal void <.ctor>b__24_0(DialogButton owner, object sender, PropertyChangedEventArgs e)
            {
                owner.OnUICommandPropertyChanged(sender, e);
            }

            internal void <.ctor>b__24_1(UICommand item, DialogButton owner)
            {
                owner.StartTrackCommandSourceItem(item);
            }

            internal void <.ctor>b__24_2(UICommand item, DialogButton onwer)
            {
                onwer.StopTrackCommandSourceItem(item);
            }

            internal object <GetUICommandByTag>b__78_1(UICommand x) => 
                x.Tag;

            internal string <GetUICommandByTag>b__78_2(object x) => 
                x.ToString();

            internal object <GetUICommandByTag>b__78_4(UICommand x) => 
                x.Tag;

            internal object <UpdateActualContent>b__41_0(UICommand x) => 
                x.Caption;

            internal UICommand <UpdateCommandsSourceCollection>b__70_0(object x) => 
                UICommandContainer.GetUICommand(x);
        }

        private interface ICommandWrapper
        {
        }

        private class NotSetCommandImpl : ICommand
        {
            event EventHandler ICommand.CanExecuteChanged
            {
                add
                {
                }
                remove
                {
                }
            }

            bool ICommand.CanExecute(object parameter) => 
                true;

            void ICommand.Execute(object parameter)
            {
            }
        }

        internal sealed class UICommandWrapper : ICommand, DialogButton.ICommandWrapper
        {
            private readonly UICommand uiCommand;
            private readonly ICommand additionalCommand;
            private readonly Action onExecuteFallback;
            private EventHandler canExecuteChanged;

            event EventHandler ICommand.CanExecuteChanged
            {
                add
                {
                    this.canExecuteChanged += value;
                }
                remove
                {
                    this.canExecuteChanged -= value;
                }
            }

            public UICommandWrapper(UICommand uiCommand, ICommand additionalCommand, Action onExecuteFallback)
            {
                this.uiCommand = uiCommand;
                this.additionalCommand = additionalCommand;
                this.onExecuteFallback = onExecuteFallback;
                foreach (ICommand command in this.Commands)
                {
                    command.CanExecuteChanged += new EventHandler(this.OnInnerCommandCanExecuteChanged);
                }
            }

            public void Destroy()
            {
                foreach (ICommand command in this.Commands)
                {
                    command.CanExecuteChanged -= new EventHandler(this.OnInnerCommandCanExecuteChanged);
                }
            }

            private void OnInnerCommandCanExecuteChanged(object sender, EventArgs e)
            {
                if (this.canExecuteChanged != null)
                {
                    this.canExecuteChanged(this, e);
                }
            }

            bool ICommand.CanExecute(object parameter)
            {
                CancelEventArgs args1 = parameter as CancelEventArgs;
                CancelEventArgs args2 = args1;
                if (args1 == null)
                {
                    CancelEventArgs local1 = args1;
                    args2 = new CancelEventArgs();
                }
                CancelEventArgs args = args2;
                return this.Commands.All<ICommand>(x => x.CanExecute(args));
            }

            void ICommand.Execute(object parameter)
            {
                CancelEventArgs args = (parameter as CancelEventArgs) ?? new CancelEventArgs();
                foreach (ICommand command in this.Commands)
                {
                    if (args.Cancel)
                    {
                        break;
                    }
                    command.Execute(args);
                }
                if (!args.Cancel)
                {
                    if (this.uiCommand != null)
                    {
                        ((IUICommand) this.uiCommand).RaiseExecuted();
                    }
                    else
                    {
                        this.onExecuteFallback();
                    }
                }
            }

            private IEnumerable<ICommand> Commands
            {
                get
                {
                    Func<UICommand, ICommand> evaluator = <>c.<>9__13_0;
                    if (<>c.<>9__13_0 == null)
                    {
                        Func<UICommand, ICommand> local1 = <>c.<>9__13_0;
                        evaluator = <>c.<>9__13_0 = x => x.Command;
                    }
                    return this.uiCommand.With<UICommand, ICommand>(evaluator).YieldIfNotNull<ICommand>().Concat<ICommand>(this.additionalCommand.YieldIfNotNull<ICommand>());
                }
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly DialogButton.UICommandWrapper.<>c <>9 = new DialogButton.UICommandWrapper.<>c();
                public static Func<UICommand, ICommand> <>9__13_0;

                internal ICommand <get_Commands>b__13_0(UICommand x) => 
                    x.Command;
            }
        }
    }
}

