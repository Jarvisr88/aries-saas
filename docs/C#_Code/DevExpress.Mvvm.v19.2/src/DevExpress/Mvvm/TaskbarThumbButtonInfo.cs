namespace DevExpress.Mvvm
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    public class TaskbarThumbButtonInfo : Freezable, ITaskbarThumbButtonInfo
    {
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(TaskbarThumbButtonInfo), new PropertyMetadata(null));
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(TaskbarThumbButtonInfo), new PropertyMetadata(null));
        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(TaskbarThumbButtonInfo), new PropertyMetadata(""));
        public static readonly DependencyProperty DismissWhenClickedProperty = DependencyProperty.Register("DismissWhenClicked", typeof(bool), typeof(TaskbarThumbButtonInfo), new PropertyMetadata(false));
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(System.Windows.Media.ImageSource), typeof(TaskbarThumbButtonInfo), new PropertyMetadata(null));
        public static readonly DependencyProperty IsBackgroundVisibleProperty = DependencyProperty.Register("IsBackgroundVisible", typeof(bool), typeof(TaskbarThumbButtonInfo), new PropertyMetadata(true));
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.Register("IsEnabled", typeof(bool), typeof(TaskbarThumbButtonInfo), new PropertyMetadata(true));
        public static readonly DependencyProperty IsInteractiveProperty = DependencyProperty.Register("IsInteractive", typeof(bool), typeof(TaskbarThumbButtonInfo), new PropertyMetadata(true));
        public static readonly DependencyProperty VisibilityProperty = DependencyProperty.Register("Visibility", typeof(System.Windows.Visibility), typeof(TaskbarThumbButtonInfo), new PropertyMetadata(System.Windows.Visibility.Visible));

        public event EventHandler Click;

        protected override Freezable CreateInstanceCore() => 
            this;

        public System.Action Action { get; set; }

        public object CommandParameter
        {
            get => 
                base.GetValue(CommandParameterProperty);
            set => 
                base.SetValue(CommandParameterProperty, value);
        }

        public ICommand Command
        {
            get => 
                (ICommand) base.GetValue(CommandProperty);
            set => 
                base.SetValue(CommandProperty, value);
        }

        public string Description
        {
            get => 
                (string) base.GetValue(DescriptionProperty);
            set => 
                base.SetValue(DescriptionProperty, value);
        }

        public bool DismissWhenClicked
        {
            get => 
                (bool) base.GetValue(DismissWhenClickedProperty);
            set => 
                base.SetValue(DismissWhenClickedProperty, value);
        }

        public System.Windows.Media.ImageSource ImageSource
        {
            get => 
                (System.Windows.Media.ImageSource) base.GetValue(ImageSourceProperty);
            set => 
                base.SetValue(ImageSourceProperty, value);
        }

        public bool IsBackgroundVisible
        {
            get => 
                (bool) base.GetValue(IsBackgroundVisibleProperty);
            set => 
                base.SetValue(IsBackgroundVisibleProperty, value);
        }

        public bool IsEnabled
        {
            get => 
                (bool) base.GetValue(IsEnabledProperty);
            set => 
                base.SetValue(IsEnabledProperty, value);
        }

        public bool IsInteractive
        {
            get => 
                (bool) base.GetValue(IsInteractiveProperty);
            set => 
                base.SetValue(IsInteractiveProperty, value);
        }

        public System.Windows.Visibility Visibility
        {
            get => 
                (System.Windows.Visibility) base.GetValue(VisibilityProperty);
            set => 
                base.SetValue(VisibilityProperty, value);
        }

        EventHandler ITaskbarThumbButtonInfo.Click
        {
            get => 
                this.Click;
            set => 
                this.Click = value;
        }
    }
}

