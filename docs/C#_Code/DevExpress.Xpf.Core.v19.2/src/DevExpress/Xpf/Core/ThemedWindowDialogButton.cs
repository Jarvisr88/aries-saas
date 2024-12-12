namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Input;

    public class ThemedWindowDialogButton : SimpleButton
    {
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public static readonly DependencyProperty PlacementProperty;
        public static readonly DependencyProperty DialogResultProperty;
        public static readonly DependencyProperty UICommandProperty;
        public static readonly DependencyProperty AllowCloseWindowProperty;
        public static readonly DependencyProperty AlignmentProperty;
        private ThemedWindow window;

        static ThemedWindowDialogButton()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ThemedWindowDialogButton), new FrameworkPropertyMetadata(typeof(ThemedWindowDialogButton)));
            Button.IsDefaultProperty.OverrideMetadata(typeof(ThemedWindowDialogButton), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(ThemedWindowDialogButton.OnIsDefaultPropertyChanged)));
            PlacementProperty = DependencyProperty.Register("Placement", typeof(Dock), typeof(ThemedWindowDialogButton), new FrameworkPropertyMetadata(Dock.Right, new PropertyChangedCallback(ThemedWindowDialogButton.OnDialogButtonAlignmentPropertyChanged)));
            AlignmentProperty = DependencyProperty.Register("Alignment", typeof(DialogButtonAlignment), typeof(ThemedWindowDialogButton), new FrameworkPropertyMetadata(DialogButtonAlignment.Right, new PropertyChangedCallback(ThemedWindowDialogButton.OnDialogButtonAlignmentPropertyChanged)));
            DialogResultProperty = DependencyProperty.Register("DialogResult", typeof(MessageBoxResult), typeof(ThemedWindowDialogButton), new FrameworkPropertyMetadata(MessageBoxResult.None));
            UICommandProperty = DependencyProperty.Register("UICommand", typeof(DevExpress.Mvvm.UICommand), typeof(ThemedWindowDialogButton), new FrameworkPropertyMetadata(null));
            AllowCloseWindowProperty = DependencyProperty.Register("AllowCloseWindow", typeof(bool), typeof(ThemedWindowDialogButton), new FrameworkPropertyMetadata(true));
        }

        public ThemedWindowDialogButton()
        {
            this.UICommand = new DevExpress.Mvvm.UICommand();
        }

        private void CalcButtonResult()
        {
            CalcButtonResult(this.Window, this.DialogResult, this.UICommand);
        }

        internal static void CalcButtonResult(ThemedWindow themedWindow, MessageBoxResult messageBoxResult, DevExpress.Mvvm.UICommand uiCommandResult)
        {
            if (themedWindow != null)
            {
                themedWindow.DialogButtonResult = messageBoxResult;
                themedWindow.DialogButtonCommandResult = uiCommandResult;
                if (themedWindow.IsDialog)
                {
                    themedWindow.DialogResult = new bool?(CalcDialogResult(messageBoxResult));
                }
            }
        }

        private static bool CalcDialogResult(MessageBoxResult dialogResult)
        {
            bool flag = false;
            switch (dialogResult)
            {
                case MessageBoxResult.OK:
                    flag = true;
                    break;

                case MessageBoxResult.Yes:
                    flag = true;
                    break;

                default:
                    break;
            }
            return flag;
        }

        internal void DoClick()
        {
            this.OnClick();
        }

        private ThemedWindow GetWindow() => 
            TreeHelper.GetParent<ThemedWindow>(this, null, true, true);

        private ThemedWindowDialogButtonsControl GetWindowDialogFooter() => 
            TreeHelper.GetParent<ThemedWindowDialogButtonsControl>(this, null, true, true);

        private bool NeedClose() => 
            (this.Window.DialogResult == null) && ((this.Window != null) && (this.AllowCloseWindow && (!ThemedWindowsHelper.GetIsWindowClosingWithCustomCommand(this.Window) && (!(base.CommandParameter is CancelEventArgs) || !((CancelEventArgs) base.CommandParameter).Cancel))));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.SetBindings();
            this.SetDefaultButtonKeyGesture(base.IsDefault);
            this.SetUICommandTag(this.DialogResult);
        }

        protected override void OnClick()
        {
            ThemedWindowsHelper.SetIsWindowClosingWithCustomCommand(this.Window, false);
            this.SetCommandParameter();
            bool isCancel = base.IsCancel;
            base.IsCancel = false;
            base.OnClick();
            base.IsCancel = isCancel;
            if (this.NeedClose())
            {
                this.CalcButtonResult();
            }
        }

        private void OnDialogButtonAlignmentChanged(object newValue, object oldValue)
        {
            if ((newValue is Dock) && ((Dock) newValue).Equals(Dock.Left))
            {
                this.ActualAlignment = DialogButtonAlignment.Left;
            }
            else
            {
                this.ActualAlignment = (DialogButtonAlignment) newValue;
            }
        }

        private static void OnDialogButtonAlignmentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ThemedWindowDialogButton) d).OnDialogButtonAlignmentChanged(e.NewValue, e.OldValue);
        }

        private void OnIsDefaultChanged(bool newValue, bool oldValue)
        {
            if ((this.Window != null) && newValue)
            {
                this.Window.InputBindings.Add(new KeyBinding(new DelegateCommand(new Action(this.OnKeyGesturedCommand)), new KeyGesture(Key.Space)));
            }
        }

        private static void OnIsDefaultPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ThemedWindowDialogButton) d).OnIsDefaultChanged((bool) e.NewValue, (bool) e.OldValue);
        }

        private void OnKeyGesturedCommand()
        {
            this.OnClick();
        }

        private void SetBindings()
        {
            Binding binding;
            if (!(base.DataContext is DevExpress.Mvvm.UICommand))
            {
                ThemedWindowDialogButtonsControl windowDialogFooter = this.GetWindowDialogFooter();
                if (windowDialogFooter != null)
                {
                    binding = new Binding {
                        Source = windowDialogFooter,
                        Path = new PropertyPath("DataContext", new object[0])
                    };
                    base.SetBinding(FrameworkElement.DataContextProperty, binding);
                }
            }
            else
            {
                binding = new Binding {
                    Source = base.DataContext,
                    Path = new PropertyPath("Id", new object[0]),
                    Converter = new MessageBoxResultConverterExtension(),
                    FallbackValue = MessageBoxResult.None
                };
                base.SetBinding(DialogResultProperty, binding);
                binding = new Binding {
                    Source = base.DataContext,
                    Path = new PropertyPath("Caption", new object[0])
                };
                base.SetBinding(ContentControl.ContentProperty, binding);
                binding = new Binding {
                    Source = base.DataContext,
                    Path = new PropertyPath("Command", new object[0])
                };
                base.SetBinding(ButtonBase.CommandProperty, binding);
                binding = new Binding {
                    Source = base.DataContext,
                    Path = new PropertyPath("IsCancel", new object[0])
                };
                base.SetBinding(Button.IsCancelProperty, binding);
                binding = new Binding {
                    Source = base.DataContext,
                    Path = new PropertyPath("IsDefault", new object[0])
                };
                base.SetBinding(Button.IsDefaultProperty, binding);
                binding = new Binding {
                    Source = base.DataContext,
                    Path = new PropertyPath("AllowCloseWindow", new object[0])
                };
                base.SetBinding(AllowCloseWindowProperty, binding);
                Binding binding1 = new Binding();
                binding1.Source = base.DataContext;
                base.SetBinding(UICommandProperty, binding1);
            }
        }

        private void SetCommandParameter()
        {
            ICommand command4;
            if (base.Command == null)
            {
                ICommand command3;
                DevExpress.Mvvm.UICommand command1 = this.UICommand;
                if (command1 != null)
                {
                    command3 = command1.Command;
                }
                else
                {
                    DevExpress.Mvvm.UICommand local1 = command1;
                    command3 = null;
                }
                if (command3 == null)
                {
                    return;
                }
            }
            ICommand command = null;
            DevExpress.Mvvm.UICommand uICommand = this.UICommand;
            if (uICommand != null)
            {
                command4 = uICommand.Command;
            }
            else
            {
                DevExpress.Mvvm.UICommand local2 = uICommand;
                command4 = null;
            }
            if (command4 != null)
            {
                command = this.UICommand.Command;
            }
            else if (base.Command != null)
            {
                command = base.Command;
            }
            if ((command != null) && (base.CommandParameter == null))
            {
                List<Type> source = command.GetType().GenericTypeArguments.ToList<Type>();
                if (!source.Any<Type>())
                {
                    base.CommandParameter ??= new CancelEventArgs();
                }
                else
                {
                    object obj1;
                    Type local3 = source.FirstOrDefault<Type>();
                    if (local3 == null)
                    {
                        Type local4 = local3;
                        obj1 = null;
                    }
                    else
                    {
                        ConstructorInfo constructor = local3.GetConstructor(Type.EmptyTypes);
                        if (constructor != null)
                        {
                            obj1 = constructor.Invoke(null);
                        }
                        else
                        {
                            ConstructorInfo local5 = constructor;
                            obj1 = null;
                        }
                    }
                    this.CommandParameter = obj1;
                }
            }
        }

        private void SetDefaultButtonKeyGesture(bool newValue)
        {
            if ((this.Window != null) && newValue)
            {
                this.Window.InputBindings.Add(new KeyBinding(new DelegateCommand(new Action(this.OnKeyGesturedCommand)), new KeyGesture(Key.Space)));
            }
        }

        private void SetUICommandTag(MessageBoxResult dialogResult)
        {
            if (this.UICommand != null)
            {
                this.UICommand.Tag ??= (((this.Window == null) || !ThemedWindowsHelper.GetUseMvvmMessageResultAsDialogResult(this.Window)) ? ((object) dialogResult) : ((object) dialogResult.ToMessageResult()));
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public DialogButtonAlignment ActualAlignment { get; internal set; }

        public ThemedWindow Window
        {
            get
            {
                ThemedWindow window = this.window;
                if (this.window == null)
                {
                    ThemedWindow local1 = this.window;
                    window = this.window = this.GetWindow();
                }
                return window;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Dock Placement
        {
            get => 
                (Dock) base.GetValue(PlacementProperty);
            set => 
                base.SetValue(PlacementProperty, value);
        }

        public DialogButtonAlignment Alignment
        {
            get => 
                (DialogButtonAlignment) base.GetValue(AlignmentProperty);
            set => 
                base.SetValue(AlignmentProperty, value);
        }

        public MessageBoxResult DialogResult
        {
            get => 
                (MessageBoxResult) base.GetValue(DialogResultProperty);
            set => 
                base.SetValue(DialogResultProperty, value);
        }

        public DevExpress.Mvvm.UICommand UICommand
        {
            get => 
                (DevExpress.Mvvm.UICommand) base.GetValue(UICommandProperty);
            set => 
                base.SetValue(UICommandProperty, value);
        }

        public bool AllowCloseWindow
        {
            get => 
                (bool) base.GetValue(AllowCloseWindowProperty);
            set => 
                base.SetValue(AllowCloseWindowProperty, value);
        }
    }
}

