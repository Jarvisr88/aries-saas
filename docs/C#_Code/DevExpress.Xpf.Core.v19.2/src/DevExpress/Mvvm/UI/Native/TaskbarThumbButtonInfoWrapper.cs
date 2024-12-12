namespace DevExpress.Mvvm.UI.Native
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Shell;

    public class TaskbarThumbButtonInfoWrapper
    {
        public static readonly DependencyProperty TaskbarThumbButtonInfoProperty;
        public static readonly DependencyProperty ThumbButtonInfoProperty;
        public static readonly DependencyProperty DoNotProcessPropertyChangedProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ThumbButtonInfoCommandParameterProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ThumbButtonInfoCommandProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ThumbButtonInfoDescriptionProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ThumbButtonInfoDismissWhenClickedProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ThumbButtonInfoImageSourceProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ThumbButtonInfoIsBackgroundVisibleProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ThumbButtonInfoIsEnabledProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ThumbButtonInfoIsInteractiveProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ThumbButtonInfoVisibilityProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty TaskbarThumbButtonInfoCommandParameterProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty TaskbarThumbButtonInfoCommandProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty TaskbarThumbButtonInfoDescriptionProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty TaskbarThumbButtonInfoDismissWhenClickedProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty TaskbarThumbButtonInfoImageSourceProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty TaskbarThumbButtonInfoIsBackgroundVisibleProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty TaskbarThumbButtonInfoIsEnabledProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty TaskbarThumbButtonInfoIsInteractiveProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty TaskbarThumbButtonInfoVisibilityProperty;

        static TaskbarThumbButtonInfoWrapper()
        {
            Container<TaskbarThumbButtonInfo> defaultValue = new Container<TaskbarThumbButtonInfo>();
            TaskbarThumbButtonInfoProperty = DependencyProperty.RegisterAttached("TaskbarThumbButtonInfo", typeof(Container<TaskbarThumbButtonInfo>), typeof(TaskbarThumbButtonInfoWrapper), new PropertyMetadata(defaultValue, new PropertyChangedCallback(TaskbarThumbButtonInfoWrapper.OnTaskbarThumbButtonInfoChanged)));
            Container<ThumbButtonInfo> container2 = new Container<ThumbButtonInfo>();
            ThumbButtonInfoProperty = DependencyProperty.RegisterAttached("ThumbButtonInfo", typeof(Container<ThumbButtonInfo>), typeof(TaskbarThumbButtonInfoWrapper), new PropertyMetadata(container2, new PropertyChangedCallback(TaskbarThumbButtonInfoWrapper.OnThumbButtonInfoChanged)));
            DoNotProcessPropertyChangedProperty = DependencyProperty.RegisterAttached("DoNotProcessPropertyChanged", typeof(bool), typeof(TaskbarThumbButtonInfoWrapper), new PropertyMetadata(false));
            ThumbButtonInfoCommandParameterProperty = DependencyProperty.RegisterAttached("ThumbButtonInfoCommandParameter", typeof(object), typeof(TaskbarThumbButtonInfoWrapper), new PropertyMetadata(new object(), delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                Action<ThumbButtonInfo, TaskbarThumbButtonInfo> setPropertyAction = <>c.<>9__47_1;
                if (<>c.<>9__47_1 == null)
                {
                    Action<ThumbButtonInfo, TaskbarThumbButtonInfo> local1 = <>c.<>9__47_1;
                    setPropertyAction = <>c.<>9__47_1 = (x, y) => CopyIfNeeded<object>(y.CommandParameter, x.CommandParameter, v => y.CommandParameter = v);
                }
                OnThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }));
            ThumbButtonInfoCommandProperty = DependencyProperty.RegisterAttached("ThumbButtonInfoCommand", typeof(object), typeof(TaskbarThumbButtonInfoWrapper), new PropertyMetadata(new object(), delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                Action<ThumbButtonInfo, TaskbarThumbButtonInfo> setPropertyAction = <>c.<>9__47_4;
                if (<>c.<>9__47_4 == null)
                {
                    Action<ThumbButtonInfo, TaskbarThumbButtonInfo> local1 = <>c.<>9__47_4;
                    setPropertyAction = <>c.<>9__47_4 = (x, y) => CopyCommandIsNeeded(x, y);
                }
                OnThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }));
            ThumbButtonInfoDescriptionProperty = DependencyProperty.RegisterAttached("ThumbButtonInfoDescription", typeof(object), typeof(TaskbarThumbButtonInfoWrapper), new PropertyMetadata(new object(), delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                Action<ThumbButtonInfo, TaskbarThumbButtonInfo> setPropertyAction = <>c.<>9__47_6;
                if (<>c.<>9__47_6 == null)
                {
                    Action<ThumbButtonInfo, TaskbarThumbButtonInfo> local1 = <>c.<>9__47_6;
                    setPropertyAction = <>c.<>9__47_6 = (x, y) => CopyIfNeeded<string>(y.Description, x.Description, v => y.Description = v);
                }
                OnThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }));
            ThumbButtonInfoDismissWhenClickedProperty = DependencyProperty.RegisterAttached("ThumbButtonInfoDismissWhenClicked", typeof(object), typeof(TaskbarThumbButtonInfoWrapper), new PropertyMetadata(new object(), delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                Action<ThumbButtonInfo, TaskbarThumbButtonInfo> setPropertyAction = <>c.<>9__47_9;
                if (<>c.<>9__47_9 == null)
                {
                    Action<ThumbButtonInfo, TaskbarThumbButtonInfo> local1 = <>c.<>9__47_9;
                    setPropertyAction = <>c.<>9__47_9 = (x, y) => CopyIfNeeded<bool>(y.DismissWhenClicked, x.DismissWhenClicked, v => y.DismissWhenClicked = v);
                }
                OnThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }));
            ThumbButtonInfoImageSourceProperty = DependencyProperty.RegisterAttached("ThumbButtonInfoImageSource", typeof(object), typeof(TaskbarThumbButtonInfoWrapper), new PropertyMetadata(new object(), delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                Action<ThumbButtonInfo, TaskbarThumbButtonInfo> setPropertyAction = <>c.<>9__47_12;
                if (<>c.<>9__47_12 == null)
                {
                    Action<ThumbButtonInfo, TaskbarThumbButtonInfo> local1 = <>c.<>9__47_12;
                    setPropertyAction = <>c.<>9__47_12 = (x, y) => CopyIfNeeded<ImageSource>(y.ImageSource, x.ImageSource, v => y.ImageSource = v);
                }
                OnThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }));
            ThumbButtonInfoIsBackgroundVisibleProperty = DependencyProperty.RegisterAttached("ThumbButtonInfoIsBackgroundVisible", typeof(object), typeof(TaskbarThumbButtonInfoWrapper), new PropertyMetadata(new object(), delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                Action<ThumbButtonInfo, TaskbarThumbButtonInfo> setPropertyAction = <>c.<>9__47_15;
                if (<>c.<>9__47_15 == null)
                {
                    Action<ThumbButtonInfo, TaskbarThumbButtonInfo> local1 = <>c.<>9__47_15;
                    setPropertyAction = <>c.<>9__47_15 = (x, y) => CopyIfNeeded<bool>(y.IsBackgroundVisible, x.IsBackgroundVisible, v => y.IsBackgroundVisible = v);
                }
                OnThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }));
            ThumbButtonInfoIsEnabledProperty = DependencyProperty.RegisterAttached("ThumbButtonInfoIsEnabled", typeof(object), typeof(TaskbarThumbButtonInfoWrapper), new PropertyMetadata(new object(), delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                Action<ThumbButtonInfo, TaskbarThumbButtonInfo> setPropertyAction = <>c.<>9__47_18;
                if (<>c.<>9__47_18 == null)
                {
                    Action<ThumbButtonInfo, TaskbarThumbButtonInfo> local1 = <>c.<>9__47_18;
                    setPropertyAction = <>c.<>9__47_18 = (x, y) => CopyIfNeeded<bool>(y.IsEnabled, x.IsEnabled, v => y.IsEnabled = v);
                }
                OnThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }));
            ThumbButtonInfoIsInteractiveProperty = DependencyProperty.RegisterAttached("ThumbButtonInfoIsInteractive", typeof(object), typeof(TaskbarThumbButtonInfoWrapper), new PropertyMetadata(new object(), delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                Action<ThumbButtonInfo, TaskbarThumbButtonInfo> setPropertyAction = <>c.<>9__47_21;
                if (<>c.<>9__47_21 == null)
                {
                    Action<ThumbButtonInfo, TaskbarThumbButtonInfo> local1 = <>c.<>9__47_21;
                    setPropertyAction = <>c.<>9__47_21 = (x, y) => CopyIfNeeded<bool>(y.IsInteractive, x.IsInteractive, v => y.IsInteractive = v);
                }
                OnThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }));
            ThumbButtonInfoVisibilityProperty = DependencyProperty.RegisterAttached("ThumbButtonInfoVisibility", typeof(object), typeof(TaskbarThumbButtonInfoWrapper), new PropertyMetadata(new object(), delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                Action<ThumbButtonInfo, TaskbarThumbButtonInfo> setPropertyAction = <>c.<>9__47_24;
                if (<>c.<>9__47_24 == null)
                {
                    Action<ThumbButtonInfo, TaskbarThumbButtonInfo> local1 = <>c.<>9__47_24;
                    setPropertyAction = <>c.<>9__47_24 = (x, y) => CopyIfNeeded<Visibility>(y.Visibility, x.Visibility, v => y.Visibility = v);
                }
                OnThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }));
            TaskbarThumbButtonInfoCommandParameterProperty = DependencyProperty.RegisterAttached("TaskbarThumbButtonInfoCommandParameter", typeof(object), typeof(TaskbarThumbButtonInfoWrapper), new PropertyMetadata(new object(), delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                Action<TaskbarThumbButtonInfo, ThumbButtonInfo> setPropertyAction = <>c.<>9__47_27;
                if (<>c.<>9__47_27 == null)
                {
                    Action<TaskbarThumbButtonInfo, ThumbButtonInfo> local1 = <>c.<>9__47_27;
                    setPropertyAction = <>c.<>9__47_27 = (x, y) => y.CommandParameter = x.CommandParameter;
                }
                OnTaskbarThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }));
            TaskbarThumbButtonInfoCommandProperty = DependencyProperty.RegisterAttached("TaskbarThumbButtonInfoCommand", typeof(object), typeof(TaskbarThumbButtonInfoWrapper), new PropertyMetadata(new object(), delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                Action<TaskbarThumbButtonInfo, ThumbButtonInfo> setPropertyAction = <>c.<>9__47_29;
                if (<>c.<>9__47_29 == null)
                {
                    Action<TaskbarThumbButtonInfo, ThumbButtonInfo> local1 = <>c.<>9__47_29;
                    setPropertyAction = <>c.<>9__47_29 = (x, y) => y.Command = new ThumbButtonInfoCommand(x);
                }
                OnTaskbarThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }));
            TaskbarThumbButtonInfoDescriptionProperty = DependencyProperty.RegisterAttached("TaskbarThumbButtonInfoDescription", typeof(object), typeof(TaskbarThumbButtonInfoWrapper), new PropertyMetadata(new object(), delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                Action<TaskbarThumbButtonInfo, ThumbButtonInfo> setPropertyAction = <>c.<>9__47_31;
                if (<>c.<>9__47_31 == null)
                {
                    Action<TaskbarThumbButtonInfo, ThumbButtonInfo> local1 = <>c.<>9__47_31;
                    setPropertyAction = <>c.<>9__47_31 = (x, y) => y.Description = x.Description;
                }
                OnTaskbarThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }));
            TaskbarThumbButtonInfoDismissWhenClickedProperty = DependencyProperty.RegisterAttached("TaskbarThumbButtonInfoDismissWhenClicked", typeof(object), typeof(TaskbarThumbButtonInfoWrapper), new PropertyMetadata(new object(), delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                Action<TaskbarThumbButtonInfo, ThumbButtonInfo> setPropertyAction = <>c.<>9__47_33;
                if (<>c.<>9__47_33 == null)
                {
                    Action<TaskbarThumbButtonInfo, ThumbButtonInfo> local1 = <>c.<>9__47_33;
                    setPropertyAction = <>c.<>9__47_33 = (x, y) => y.DismissWhenClicked = x.DismissWhenClicked;
                }
                OnTaskbarThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }));
            TaskbarThumbButtonInfoImageSourceProperty = DependencyProperty.RegisterAttached("TaskbarThumbButtonInfoImageSource", typeof(object), typeof(TaskbarThumbButtonInfoWrapper), new PropertyMetadata(new object(), delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                Action<TaskbarThumbButtonInfo, ThumbButtonInfo> setPropertyAction = <>c.<>9__47_35;
                if (<>c.<>9__47_35 == null)
                {
                    Action<TaskbarThumbButtonInfo, ThumbButtonInfo> local1 = <>c.<>9__47_35;
                    setPropertyAction = <>c.<>9__47_35 = (x, y) => y.ImageSource = x.ImageSource;
                }
                OnTaskbarThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }));
            TaskbarThumbButtonInfoIsBackgroundVisibleProperty = DependencyProperty.RegisterAttached("TaskbarThumbButtonInfoIsBackgroundVisible", typeof(object), typeof(TaskbarThumbButtonInfoWrapper), new PropertyMetadata(new object(), delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                Action<TaskbarThumbButtonInfo, ThumbButtonInfo> setPropertyAction = <>c.<>9__47_37;
                if (<>c.<>9__47_37 == null)
                {
                    Action<TaskbarThumbButtonInfo, ThumbButtonInfo> local1 = <>c.<>9__47_37;
                    setPropertyAction = <>c.<>9__47_37 = (x, y) => y.IsBackgroundVisible = x.IsBackgroundVisible;
                }
                OnTaskbarThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }));
            TaskbarThumbButtonInfoIsEnabledProperty = DependencyProperty.RegisterAttached("TaskbarThumbButtonInfoIsEnabled", typeof(object), typeof(TaskbarThumbButtonInfoWrapper), new PropertyMetadata(new object(), delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                Action<TaskbarThumbButtonInfo, ThumbButtonInfo> setPropertyAction = <>c.<>9__47_39;
                if (<>c.<>9__47_39 == null)
                {
                    Action<TaskbarThumbButtonInfo, ThumbButtonInfo> local1 = <>c.<>9__47_39;
                    setPropertyAction = <>c.<>9__47_39 = (x, y) => y.IsEnabled = x.IsEnabled;
                }
                OnTaskbarThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }));
            TaskbarThumbButtonInfoIsInteractiveProperty = DependencyProperty.RegisterAttached("TaskbarThumbButtonInfoIsInteractive", typeof(object), typeof(TaskbarThumbButtonInfoWrapper), new PropertyMetadata(new object(), delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                Action<TaskbarThumbButtonInfo, ThumbButtonInfo> setPropertyAction = <>c.<>9__47_41;
                if (<>c.<>9__47_41 == null)
                {
                    Action<TaskbarThumbButtonInfo, ThumbButtonInfo> local1 = <>c.<>9__47_41;
                    setPropertyAction = <>c.<>9__47_41 = (x, y) => y.IsInteractive = x.IsInteractive;
                }
                OnTaskbarThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }));
            TaskbarThumbButtonInfoVisibilityProperty = DependencyProperty.RegisterAttached("TaskbarThumbButtonInfoVisibility", typeof(object), typeof(TaskbarThumbButtonInfoWrapper), new PropertyMetadata(new object(), delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                Action<TaskbarThumbButtonInfo, ThumbButtonInfo> setPropertyAction = <>c.<>9__47_43;
                if (<>c.<>9__47_43 == null)
                {
                    Action<TaskbarThumbButtonInfo, ThumbButtonInfo> local1 = <>c.<>9__47_43;
                    setPropertyAction = <>c.<>9__47_43 = (x, y) => y.Visibility = x.Visibility;
                }
                OnTaskbarThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }));
        }

        private static bool BeginSetProperties(DependencyObject d)
        {
            bool doNotProcessPropertyChanged = GetDoNotProcessPropertyChanged(d);
            SetDoNotProcessPropertyChanged(d, true);
            return doNotProcessPropertyChanged;
        }

        private static void ButtonInfoSetProperty<TSource, TDest>(TSource sourceInfo, TDest destInfo, Action<TSource, TDest> setPropertyAction) where TSource: DependencyObject where TDest: DependencyObject
        {
            bool oldValue = BeginSetProperties(destInfo);
            try
            {
                setPropertyAction(sourceInfo, destInfo);
            }
            finally
            {
                EndSetProperties(destInfo, oldValue);
            }
        }

        private static void ClearListenPropertyBinding(DependencyObject source, DependencyProperty property)
        {
            source.SetValue(property, source.GetValue(property));
        }

        private static void CopyCommandIsNeeded(ThumbButtonInfo thumbButtonInfo, TaskbarThumbButtonInfo taskbarThumbButtonInfo)
        {
            ThumbButtonInfoCommand command = thumbButtonInfo.Command as ThumbButtonInfoCommand;
            if (command == null)
            {
                taskbarThumbButtonInfo.Command = command;
            }
            else
            {
                CopyIfNeeded<ICommand>(taskbarThumbButtonInfo.Command, command.InternalCommand, x => taskbarThumbButtonInfo.Command = x);
                CopyIfNeeded<Action>(taskbarThumbButtonInfo.Action, command.Action, x => taskbarThumbButtonInfo.Action = x);
                ITaskbarThumbButtonInfo taskbarThumbButtonInfoInternal = taskbarThumbButtonInfo;
                CopyIfNeeded<EventHandler>(taskbarThumbButtonInfoInternal.Click, command.Click, x => taskbarThumbButtonInfoInternal.Click = x);
            }
        }

        private static void CopyIfNeeded<T>(T oldValue, T newValue, Action<T> setPropertyAction)
        {
            if (!Equals(oldValue, newValue))
            {
                setPropertyAction(newValue);
            }
        }

        private static void EndSetProperties(DependencyObject d, bool oldValue)
        {
            SetDoNotProcessPropertyChanged(d, oldValue);
        }

        public static bool GetDoNotProcessPropertyChanged(DependencyObject d) => 
            (bool) d.GetValue(DoNotProcessPropertyChangedProperty);

        public static Container<TaskbarThumbButtonInfo> GetTaskbarThumbButtonInfo(ThumbButtonInfo d) => 
            (Container<TaskbarThumbButtonInfo>) d.GetValue(TaskbarThumbButtonInfoProperty);

        public static Container<ThumbButtonInfo> GetThumbButtonInfo(TaskbarThumbButtonInfo d) => 
            (Container<ThumbButtonInfo>) d.GetValue(ThumbButtonInfoProperty);

        private static void OnTaskbarThumbButtonInfoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ThumbButtonInfo content = (ThumbButtonInfo) d;
            Container<TaskbarThumbButtonInfo> newValue = (Container<TaskbarThumbButtonInfo>) e.NewValue;
            Container<TaskbarThumbButtonInfo> oldValue = (Container<TaskbarThumbButtonInfo>) e.OldValue;
            if (oldValue.Content != null)
            {
                UnsubscribeFromTaskbarThumbButtonInfoPropertiesChanged(oldValue.Content);
                if (GetThumbButtonInfo(oldValue.Content).Content == content)
                {
                    SetThumbButtonInfo(oldValue.Content, new Container<ThumbButtonInfo>(null));
                }
            }
            if (newValue.Content != null)
            {
                SetThumbButtonInfo(newValue.Content, new Container<ThumbButtonInfo>(content));
                SubscribeToTaskbarThumbButtonInfoPropertiesChanged(newValue.Content);
            }
        }

        private static void OnTaskbarThumbButtonInfoPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e, Action<TaskbarThumbButtonInfo, ThumbButtonInfo> setPropertyAction)
        {
            TaskbarThumbButtonInfo info = (TaskbarThumbButtonInfo) d;
            if (!GetDoNotProcessPropertyChanged(info))
            {
                ButtonInfoSetProperty<TaskbarThumbButtonInfo, ThumbButtonInfo>(info, GetThumbButtonInfo(info).Content, setPropertyAction);
            }
        }

        private static void OnThumbButtonInfoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TaskbarThumbButtonInfo content = (TaskbarThumbButtonInfo) d;
            Container<ThumbButtonInfo> newValue = (Container<ThumbButtonInfo>) e.NewValue;
            Container<ThumbButtonInfo> oldValue = (Container<ThumbButtonInfo>) e.OldValue;
            if (oldValue.Content != null)
            {
                UnsubscribeFromThumbButtonInfoPropertiesChanged(oldValue.Content);
                if (GetTaskbarThumbButtonInfo(oldValue.Content).Content == content)
                {
                    SetTaskbarThumbButtonInfo(oldValue.Content, new Container<TaskbarThumbButtonInfo>(null));
                }
            }
            if (newValue.Content != null)
            {
                SetTaskbarThumbButtonInfo(newValue.Content, new Container<TaskbarThumbButtonInfo>(content));
                SubscribeToThumbButtonInfoPropertiesChanged(newValue.Content);
            }
        }

        private static void OnThumbButtonInfoPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e, Action<ThumbButtonInfo, TaskbarThumbButtonInfo> setPropertyAction)
        {
            ThumbButtonInfo info = (ThumbButtonInfo) d;
            if (!GetDoNotProcessPropertyChanged(info))
            {
                ButtonInfoSetProperty<ThumbButtonInfo, TaskbarThumbButtonInfo>(info, GetTaskbarThumbButtonInfo(info).Content, setPropertyAction);
            }
        }

        public static void SetDoNotProcessPropertyChanged(DependencyObject d, bool value)
        {
            d.SetValue(DoNotProcessPropertyChangedProperty, value);
        }

        public static void SetTaskbarThumbButtonInfo(ThumbButtonInfo d, Container<TaskbarThumbButtonInfo> value)
        {
            d.SetValue(TaskbarThumbButtonInfoProperty, value);
        }

        public static void SetThumbButtonInfo(TaskbarThumbButtonInfo d, Container<ThumbButtonInfo> value)
        {
            d.SetValue(ThumbButtonInfoProperty, value);
        }

        private static void SubscribeToTaskbarThumbButtonInfoPropertiesChanged(TaskbarThumbButtonInfo taskbarThumbButtonInfo)
        {
            Binding binding = new Binding("CommandParameter");
            binding.Source = taskbarThumbButtonInfo;
            binding.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(taskbarThumbButtonInfo, TaskbarThumbButtonInfoCommandParameterProperty, binding);
            Binding binding2 = new Binding("Command");
            binding2.Source = taskbarThumbButtonInfo;
            binding2.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(taskbarThumbButtonInfo, TaskbarThumbButtonInfoCommandProperty, binding2);
            Binding binding3 = new Binding("Description");
            binding3.Source = taskbarThumbButtonInfo;
            binding3.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(taskbarThumbButtonInfo, TaskbarThumbButtonInfoDescriptionProperty, binding3);
            Binding binding4 = new Binding("DismissWhenClicked");
            binding4.Source = taskbarThumbButtonInfo;
            binding4.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(taskbarThumbButtonInfo, TaskbarThumbButtonInfoDismissWhenClickedProperty, binding4);
            Binding binding5 = new Binding("ImageSource");
            binding5.Source = taskbarThumbButtonInfo;
            binding5.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(taskbarThumbButtonInfo, TaskbarThumbButtonInfoImageSourceProperty, binding5);
            Binding binding6 = new Binding("IsBackgroundVisible");
            binding6.Source = taskbarThumbButtonInfo;
            binding6.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(taskbarThumbButtonInfo, TaskbarThumbButtonInfoIsBackgroundVisibleProperty, binding6);
            Binding binding7 = new Binding("IsEnabled");
            binding7.Source = taskbarThumbButtonInfo;
            binding7.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(taskbarThumbButtonInfo, TaskbarThumbButtonInfoIsEnabledProperty, binding7);
            Binding binding8 = new Binding("IsInteractive");
            binding8.Source = taskbarThumbButtonInfo;
            binding8.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(taskbarThumbButtonInfo, TaskbarThumbButtonInfoIsInteractiveProperty, binding8);
            Binding binding9 = new Binding("Visibility");
            binding9.Source = taskbarThumbButtonInfo;
            binding9.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(taskbarThumbButtonInfo, TaskbarThumbButtonInfoVisibilityProperty, binding9);
        }

        private static void SubscribeToThumbButtonInfoPropertiesChanged(ThumbButtonInfo thumbButtonInfo)
        {
            Binding binding = new Binding("CommandParameter");
            binding.Source = thumbButtonInfo;
            binding.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(thumbButtonInfo, ThumbButtonInfoCommandParameterProperty, binding);
            Binding binding2 = new Binding("Command");
            binding2.Source = thumbButtonInfo;
            binding2.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(thumbButtonInfo, ThumbButtonInfoCommandProperty, binding2);
            Binding binding3 = new Binding("Description");
            binding3.Source = thumbButtonInfo;
            binding3.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(thumbButtonInfo, ThumbButtonInfoDescriptionProperty, binding3);
            Binding binding4 = new Binding("DismissWhenClicked");
            binding4.Source = thumbButtonInfo;
            binding4.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(thumbButtonInfo, ThumbButtonInfoDismissWhenClickedProperty, binding4);
            Binding binding5 = new Binding("ImageSource");
            binding5.Source = thumbButtonInfo;
            binding5.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(thumbButtonInfo, ThumbButtonInfoImageSourceProperty, binding5);
            Binding binding6 = new Binding("IsBackgroundVisible");
            binding6.Source = thumbButtonInfo;
            binding6.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(thumbButtonInfo, ThumbButtonInfoIsBackgroundVisibleProperty, binding6);
            Binding binding7 = new Binding("IsEnabled");
            binding7.Source = thumbButtonInfo;
            binding7.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(thumbButtonInfo, ThumbButtonInfoIsEnabledProperty, binding7);
            Binding binding8 = new Binding("IsInteractive");
            binding8.Source = thumbButtonInfo;
            binding8.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(thumbButtonInfo, ThumbButtonInfoIsInteractiveProperty, binding8);
            Binding binding9 = new Binding("Visibility");
            binding9.Source = thumbButtonInfo;
            binding9.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(thumbButtonInfo, ThumbButtonInfoVisibilityProperty, binding9);
        }

        private static void TaskbarThumbButtonInfoSetPropertiesCore(ThumbButtonInfo thumbButtonInfo, TaskbarThumbButtonInfo taskbarThumbButtonInfo)
        {
            CopyIfNeeded<object>(taskbarThumbButtonInfo.CommandParameter, thumbButtonInfo.CommandParameter, x => taskbarThumbButtonInfo.CommandParameter = x);
            CopyCommandIsNeeded(thumbButtonInfo, taskbarThumbButtonInfo);
            CopyIfNeeded<string>(taskbarThumbButtonInfo.Description, thumbButtonInfo.Description, x => taskbarThumbButtonInfo.Description = x);
            CopyIfNeeded<bool>(taskbarThumbButtonInfo.DismissWhenClicked, thumbButtonInfo.DismissWhenClicked, x => taskbarThumbButtonInfo.DismissWhenClicked = x);
            CopyIfNeeded<ImageSource>(taskbarThumbButtonInfo.ImageSource, thumbButtonInfo.ImageSource, x => taskbarThumbButtonInfo.ImageSource = x);
            CopyIfNeeded<bool>(taskbarThumbButtonInfo.IsBackgroundVisible, thumbButtonInfo.IsBackgroundVisible, x => taskbarThumbButtonInfo.IsBackgroundVisible = x);
            CopyIfNeeded<bool>(taskbarThumbButtonInfo.IsEnabled, thumbButtonInfo.IsEnabled, x => taskbarThumbButtonInfo.IsEnabled = x);
            CopyIfNeeded<bool>(taskbarThumbButtonInfo.IsInteractive, thumbButtonInfo.IsInteractive, x => taskbarThumbButtonInfo.IsInteractive = x);
            CopyIfNeeded<Visibility>(taskbarThumbButtonInfo.Visibility, thumbButtonInfo.Visibility, x => taskbarThumbButtonInfo.Visibility = x);
        }

        private static void ThumbButtonInfoSetPropertiesCore(TaskbarThumbButtonInfo taskbarThumbButtonInfo, ThumbButtonInfo thumbButtonInfo)
        {
            thumbButtonInfo.CommandParameter = taskbarThumbButtonInfo.CommandParameter;
            thumbButtonInfo.Command = new ThumbButtonInfoCommand(taskbarThumbButtonInfo);
            thumbButtonInfo.Description = taskbarThumbButtonInfo.Description;
            thumbButtonInfo.DismissWhenClicked = taskbarThumbButtonInfo.DismissWhenClicked;
            thumbButtonInfo.ImageSource = taskbarThumbButtonInfo.ImageSource;
            thumbButtonInfo.IsBackgroundVisible = taskbarThumbButtonInfo.IsBackgroundVisible;
            thumbButtonInfo.IsEnabled = taskbarThumbButtonInfo.IsEnabled;
            thumbButtonInfo.IsInteractive = taskbarThumbButtonInfo.IsInteractive;
            thumbButtonInfo.Visibility = taskbarThumbButtonInfo.Visibility;
        }

        private static void UnsubscribeFromTaskbarThumbButtonInfoPropertiesChanged(TaskbarThumbButtonInfo taskbarThumbButtonInfo)
        {
            ClearListenPropertyBinding(taskbarThumbButtonInfo, TaskbarThumbButtonInfoCommandParameterProperty);
            ClearListenPropertyBinding(taskbarThumbButtonInfo, TaskbarThumbButtonInfoCommandProperty);
            ClearListenPropertyBinding(taskbarThumbButtonInfo, TaskbarThumbButtonInfoDescriptionProperty);
            ClearListenPropertyBinding(taskbarThumbButtonInfo, TaskbarThumbButtonInfoDismissWhenClickedProperty);
            ClearListenPropertyBinding(taskbarThumbButtonInfo, TaskbarThumbButtonInfoImageSourceProperty);
            ClearListenPropertyBinding(taskbarThumbButtonInfo, TaskbarThumbButtonInfoIsBackgroundVisibleProperty);
            ClearListenPropertyBinding(taskbarThumbButtonInfo, TaskbarThumbButtonInfoIsEnabledProperty);
            ClearListenPropertyBinding(taskbarThumbButtonInfo, TaskbarThumbButtonInfoIsInteractiveProperty);
            ClearListenPropertyBinding(taskbarThumbButtonInfo, TaskbarThumbButtonInfoVisibilityProperty);
        }

        private static void UnsubscribeFromThumbButtonInfoPropertiesChanged(ThumbButtonInfo thumbButtonInfo)
        {
            ClearListenPropertyBinding(thumbButtonInfo, ThumbButtonInfoCommandParameterProperty);
            ClearListenPropertyBinding(thumbButtonInfo, ThumbButtonInfoCommandProperty);
            ClearListenPropertyBinding(thumbButtonInfo, ThumbButtonInfoDescriptionProperty);
            ClearListenPropertyBinding(thumbButtonInfo, ThumbButtonInfoDismissWhenClickedProperty);
            ClearListenPropertyBinding(thumbButtonInfo, ThumbButtonInfoImageSourceProperty);
            ClearListenPropertyBinding(thumbButtonInfo, ThumbButtonInfoIsBackgroundVisibleProperty);
            ClearListenPropertyBinding(thumbButtonInfo, ThumbButtonInfoIsEnabledProperty);
            ClearListenPropertyBinding(thumbButtonInfo, ThumbButtonInfoIsInteractiveProperty);
            ClearListenPropertyBinding(thumbButtonInfo, ThumbButtonInfoVisibilityProperty);
        }

        public static TaskbarThumbButtonInfo UnWrap(ThumbButtonInfo thumbButtonInfo)
        {
            TaskbarThumbButtonInfo content = GetTaskbarThumbButtonInfo(thumbButtonInfo).Content;
            if (content == null)
            {
                content = new TaskbarThumbButtonInfo();
                ButtonInfoSetProperty<ThumbButtonInfo, TaskbarThumbButtonInfo>(thumbButtonInfo, content, new Action<ThumbButtonInfo, TaskbarThumbButtonInfo>(TaskbarThumbButtonInfoWrapper.TaskbarThumbButtonInfoSetPropertiesCore));
                SetThumbButtonInfo(content, new Container<ThumbButtonInfo>(thumbButtonInfo));
            }
            return content;
        }

        public static ThumbButtonInfo Wrap(TaskbarThumbButtonInfo taskbarThumbButtonInfo)
        {
            ThumbButtonInfo content = GetThumbButtonInfo(taskbarThumbButtonInfo).Content;
            if (content == null)
            {
                content = new ThumbButtonInfo();
                ButtonInfoSetProperty<TaskbarThumbButtonInfo, ThumbButtonInfo>(taskbarThumbButtonInfo, content, new Action<TaskbarThumbButtonInfo, ThumbButtonInfo>(TaskbarThumbButtonInfoWrapper.ThumbButtonInfoSetPropertiesCore));
                SetTaskbarThumbButtonInfo(content, new Container<TaskbarThumbButtonInfo>(taskbarThumbButtonInfo));
            }
            return content;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TaskbarThumbButtonInfoWrapper.<>c <>9 = new TaskbarThumbButtonInfoWrapper.<>c();
            public static Action<ThumbButtonInfo, TaskbarThumbButtonInfo> <>9__47_1;
            public static Action<ThumbButtonInfo, TaskbarThumbButtonInfo> <>9__47_4;
            public static Action<ThumbButtonInfo, TaskbarThumbButtonInfo> <>9__47_6;
            public static Action<ThumbButtonInfo, TaskbarThumbButtonInfo> <>9__47_9;
            public static Action<ThumbButtonInfo, TaskbarThumbButtonInfo> <>9__47_12;
            public static Action<ThumbButtonInfo, TaskbarThumbButtonInfo> <>9__47_15;
            public static Action<ThumbButtonInfo, TaskbarThumbButtonInfo> <>9__47_18;
            public static Action<ThumbButtonInfo, TaskbarThumbButtonInfo> <>9__47_21;
            public static Action<ThumbButtonInfo, TaskbarThumbButtonInfo> <>9__47_24;
            public static Action<TaskbarThumbButtonInfo, ThumbButtonInfo> <>9__47_27;
            public static Action<TaskbarThumbButtonInfo, ThumbButtonInfo> <>9__47_29;
            public static Action<TaskbarThumbButtonInfo, ThumbButtonInfo> <>9__47_31;
            public static Action<TaskbarThumbButtonInfo, ThumbButtonInfo> <>9__47_33;
            public static Action<TaskbarThumbButtonInfo, ThumbButtonInfo> <>9__47_35;
            public static Action<TaskbarThumbButtonInfo, ThumbButtonInfo> <>9__47_37;
            public static Action<TaskbarThumbButtonInfo, ThumbButtonInfo> <>9__47_39;
            public static Action<TaskbarThumbButtonInfo, ThumbButtonInfo> <>9__47_41;
            public static Action<TaskbarThumbButtonInfo, ThumbButtonInfo> <>9__47_43;

            internal void <.cctor>b__47_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                Action<ThumbButtonInfo, TaskbarThumbButtonInfo> setPropertyAction = <>9__47_1;
                if (<>9__47_1 == null)
                {
                    Action<ThumbButtonInfo, TaskbarThumbButtonInfo> local1 = <>9__47_1;
                    setPropertyAction = <>9__47_1 = (x, y) => TaskbarThumbButtonInfoWrapper.CopyIfNeeded<object>(y.CommandParameter, x.CommandParameter, v => y.CommandParameter = v);
                }
                TaskbarThumbButtonInfoWrapper.OnThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }

            internal void <.cctor>b__47_1(ThumbButtonInfo x, TaskbarThumbButtonInfo y)
            {
                TaskbarThumbButtonInfoWrapper.CopyIfNeeded<object>(y.CommandParameter, x.CommandParameter, v => y.CommandParameter = v);
            }

            internal void <.cctor>b__47_11(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                Action<ThumbButtonInfo, TaskbarThumbButtonInfo> setPropertyAction = <>9__47_12;
                if (<>9__47_12 == null)
                {
                    Action<ThumbButtonInfo, TaskbarThumbButtonInfo> local1 = <>9__47_12;
                    setPropertyAction = <>9__47_12 = (x, y) => TaskbarThumbButtonInfoWrapper.CopyIfNeeded<ImageSource>(y.ImageSource, x.ImageSource, v => y.ImageSource = v);
                }
                TaskbarThumbButtonInfoWrapper.OnThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }

            internal void <.cctor>b__47_12(ThumbButtonInfo x, TaskbarThumbButtonInfo y)
            {
                TaskbarThumbButtonInfoWrapper.CopyIfNeeded<ImageSource>(y.ImageSource, x.ImageSource, v => y.ImageSource = v);
            }

            internal void <.cctor>b__47_14(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                Action<ThumbButtonInfo, TaskbarThumbButtonInfo> setPropertyAction = <>9__47_15;
                if (<>9__47_15 == null)
                {
                    Action<ThumbButtonInfo, TaskbarThumbButtonInfo> local1 = <>9__47_15;
                    setPropertyAction = <>9__47_15 = (x, y) => TaskbarThumbButtonInfoWrapper.CopyIfNeeded<bool>(y.IsBackgroundVisible, x.IsBackgroundVisible, v => y.IsBackgroundVisible = v);
                }
                TaskbarThumbButtonInfoWrapper.OnThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }

            internal void <.cctor>b__47_15(ThumbButtonInfo x, TaskbarThumbButtonInfo y)
            {
                TaskbarThumbButtonInfoWrapper.CopyIfNeeded<bool>(y.IsBackgroundVisible, x.IsBackgroundVisible, v => y.IsBackgroundVisible = v);
            }

            internal void <.cctor>b__47_17(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                Action<ThumbButtonInfo, TaskbarThumbButtonInfo> setPropertyAction = <>9__47_18;
                if (<>9__47_18 == null)
                {
                    Action<ThumbButtonInfo, TaskbarThumbButtonInfo> local1 = <>9__47_18;
                    setPropertyAction = <>9__47_18 = (x, y) => TaskbarThumbButtonInfoWrapper.CopyIfNeeded<bool>(y.IsEnabled, x.IsEnabled, v => y.IsEnabled = v);
                }
                TaskbarThumbButtonInfoWrapper.OnThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }

            internal void <.cctor>b__47_18(ThumbButtonInfo x, TaskbarThumbButtonInfo y)
            {
                TaskbarThumbButtonInfoWrapper.CopyIfNeeded<bool>(y.IsEnabled, x.IsEnabled, v => y.IsEnabled = v);
            }

            internal void <.cctor>b__47_20(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                Action<ThumbButtonInfo, TaskbarThumbButtonInfo> setPropertyAction = <>9__47_21;
                if (<>9__47_21 == null)
                {
                    Action<ThumbButtonInfo, TaskbarThumbButtonInfo> local1 = <>9__47_21;
                    setPropertyAction = <>9__47_21 = (x, y) => TaskbarThumbButtonInfoWrapper.CopyIfNeeded<bool>(y.IsInteractive, x.IsInteractive, v => y.IsInteractive = v);
                }
                TaskbarThumbButtonInfoWrapper.OnThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }

            internal void <.cctor>b__47_21(ThumbButtonInfo x, TaskbarThumbButtonInfo y)
            {
                TaskbarThumbButtonInfoWrapper.CopyIfNeeded<bool>(y.IsInteractive, x.IsInteractive, v => y.IsInteractive = v);
            }

            internal void <.cctor>b__47_23(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                Action<ThumbButtonInfo, TaskbarThumbButtonInfo> setPropertyAction = <>9__47_24;
                if (<>9__47_24 == null)
                {
                    Action<ThumbButtonInfo, TaskbarThumbButtonInfo> local1 = <>9__47_24;
                    setPropertyAction = <>9__47_24 = (x, y) => TaskbarThumbButtonInfoWrapper.CopyIfNeeded<Visibility>(y.Visibility, x.Visibility, v => y.Visibility = v);
                }
                TaskbarThumbButtonInfoWrapper.OnThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }

            internal void <.cctor>b__47_24(ThumbButtonInfo x, TaskbarThumbButtonInfo y)
            {
                TaskbarThumbButtonInfoWrapper.CopyIfNeeded<Visibility>(y.Visibility, x.Visibility, v => y.Visibility = v);
            }

            internal void <.cctor>b__47_26(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                Action<TaskbarThumbButtonInfo, ThumbButtonInfo> setPropertyAction = <>9__47_27;
                if (<>9__47_27 == null)
                {
                    Action<TaskbarThumbButtonInfo, ThumbButtonInfo> local1 = <>9__47_27;
                    setPropertyAction = <>9__47_27 = (x, y) => y.CommandParameter = x.CommandParameter;
                }
                TaskbarThumbButtonInfoWrapper.OnTaskbarThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }

            internal void <.cctor>b__47_27(TaskbarThumbButtonInfo x, ThumbButtonInfo y)
            {
                y.CommandParameter = x.CommandParameter;
            }

            internal void <.cctor>b__47_28(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                Action<TaskbarThumbButtonInfo, ThumbButtonInfo> setPropertyAction = <>9__47_29;
                if (<>9__47_29 == null)
                {
                    Action<TaskbarThumbButtonInfo, ThumbButtonInfo> local1 = <>9__47_29;
                    setPropertyAction = <>9__47_29 = (x, y) => y.Command = new TaskbarThumbButtonInfoWrapper.ThumbButtonInfoCommand(x);
                }
                TaskbarThumbButtonInfoWrapper.OnTaskbarThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }

            internal void <.cctor>b__47_29(TaskbarThumbButtonInfo x, ThumbButtonInfo y)
            {
                y.Command = new TaskbarThumbButtonInfoWrapper.ThumbButtonInfoCommand(x);
            }

            internal void <.cctor>b__47_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                Action<ThumbButtonInfo, TaskbarThumbButtonInfo> setPropertyAction = <>9__47_4;
                if (<>9__47_4 == null)
                {
                    Action<ThumbButtonInfo, TaskbarThumbButtonInfo> local1 = <>9__47_4;
                    setPropertyAction = <>9__47_4 = (x, y) => TaskbarThumbButtonInfoWrapper.CopyCommandIsNeeded(x, y);
                }
                TaskbarThumbButtonInfoWrapper.OnThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }

            internal void <.cctor>b__47_30(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                Action<TaskbarThumbButtonInfo, ThumbButtonInfo> setPropertyAction = <>9__47_31;
                if (<>9__47_31 == null)
                {
                    Action<TaskbarThumbButtonInfo, ThumbButtonInfo> local1 = <>9__47_31;
                    setPropertyAction = <>9__47_31 = (x, y) => y.Description = x.Description;
                }
                TaskbarThumbButtonInfoWrapper.OnTaskbarThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }

            internal void <.cctor>b__47_31(TaskbarThumbButtonInfo x, ThumbButtonInfo y)
            {
                y.Description = x.Description;
            }

            internal void <.cctor>b__47_32(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                Action<TaskbarThumbButtonInfo, ThumbButtonInfo> setPropertyAction = <>9__47_33;
                if (<>9__47_33 == null)
                {
                    Action<TaskbarThumbButtonInfo, ThumbButtonInfo> local1 = <>9__47_33;
                    setPropertyAction = <>9__47_33 = (x, y) => y.DismissWhenClicked = x.DismissWhenClicked;
                }
                TaskbarThumbButtonInfoWrapper.OnTaskbarThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }

            internal void <.cctor>b__47_33(TaskbarThumbButtonInfo x, ThumbButtonInfo y)
            {
                y.DismissWhenClicked = x.DismissWhenClicked;
            }

            internal void <.cctor>b__47_34(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                Action<TaskbarThumbButtonInfo, ThumbButtonInfo> setPropertyAction = <>9__47_35;
                if (<>9__47_35 == null)
                {
                    Action<TaskbarThumbButtonInfo, ThumbButtonInfo> local1 = <>9__47_35;
                    setPropertyAction = <>9__47_35 = (x, y) => y.ImageSource = x.ImageSource;
                }
                TaskbarThumbButtonInfoWrapper.OnTaskbarThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }

            internal void <.cctor>b__47_35(TaskbarThumbButtonInfo x, ThumbButtonInfo y)
            {
                y.ImageSource = x.ImageSource;
            }

            internal void <.cctor>b__47_36(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                Action<TaskbarThumbButtonInfo, ThumbButtonInfo> setPropertyAction = <>9__47_37;
                if (<>9__47_37 == null)
                {
                    Action<TaskbarThumbButtonInfo, ThumbButtonInfo> local1 = <>9__47_37;
                    setPropertyAction = <>9__47_37 = (x, y) => y.IsBackgroundVisible = x.IsBackgroundVisible;
                }
                TaskbarThumbButtonInfoWrapper.OnTaskbarThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }

            internal void <.cctor>b__47_37(TaskbarThumbButtonInfo x, ThumbButtonInfo y)
            {
                y.IsBackgroundVisible = x.IsBackgroundVisible;
            }

            internal void <.cctor>b__47_38(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                Action<TaskbarThumbButtonInfo, ThumbButtonInfo> setPropertyAction = <>9__47_39;
                if (<>9__47_39 == null)
                {
                    Action<TaskbarThumbButtonInfo, ThumbButtonInfo> local1 = <>9__47_39;
                    setPropertyAction = <>9__47_39 = (x, y) => y.IsEnabled = x.IsEnabled;
                }
                TaskbarThumbButtonInfoWrapper.OnTaskbarThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }

            internal void <.cctor>b__47_39(TaskbarThumbButtonInfo x, ThumbButtonInfo y)
            {
                y.IsEnabled = x.IsEnabled;
            }

            internal void <.cctor>b__47_4(ThumbButtonInfo x, TaskbarThumbButtonInfo y)
            {
                TaskbarThumbButtonInfoWrapper.CopyCommandIsNeeded(x, y);
            }

            internal void <.cctor>b__47_40(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                Action<TaskbarThumbButtonInfo, ThumbButtonInfo> setPropertyAction = <>9__47_41;
                if (<>9__47_41 == null)
                {
                    Action<TaskbarThumbButtonInfo, ThumbButtonInfo> local1 = <>9__47_41;
                    setPropertyAction = <>9__47_41 = (x, y) => y.IsInteractive = x.IsInteractive;
                }
                TaskbarThumbButtonInfoWrapper.OnTaskbarThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }

            internal void <.cctor>b__47_41(TaskbarThumbButtonInfo x, ThumbButtonInfo y)
            {
                y.IsInteractive = x.IsInteractive;
            }

            internal void <.cctor>b__47_42(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                Action<TaskbarThumbButtonInfo, ThumbButtonInfo> setPropertyAction = <>9__47_43;
                if (<>9__47_43 == null)
                {
                    Action<TaskbarThumbButtonInfo, ThumbButtonInfo> local1 = <>9__47_43;
                    setPropertyAction = <>9__47_43 = (x, y) => y.Visibility = x.Visibility;
                }
                TaskbarThumbButtonInfoWrapper.OnTaskbarThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }

            internal void <.cctor>b__47_43(TaskbarThumbButtonInfo x, ThumbButtonInfo y)
            {
                y.Visibility = x.Visibility;
            }

            internal void <.cctor>b__47_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                Action<ThumbButtonInfo, TaskbarThumbButtonInfo> setPropertyAction = <>9__47_6;
                if (<>9__47_6 == null)
                {
                    Action<ThumbButtonInfo, TaskbarThumbButtonInfo> local1 = <>9__47_6;
                    setPropertyAction = <>9__47_6 = (x, y) => TaskbarThumbButtonInfoWrapper.CopyIfNeeded<string>(y.Description, x.Description, v => y.Description = v);
                }
                TaskbarThumbButtonInfoWrapper.OnThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }

            internal void <.cctor>b__47_6(ThumbButtonInfo x, TaskbarThumbButtonInfo y)
            {
                TaskbarThumbButtonInfoWrapper.CopyIfNeeded<string>(y.Description, x.Description, v => y.Description = v);
            }

            internal void <.cctor>b__47_8(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                Action<ThumbButtonInfo, TaskbarThumbButtonInfo> setPropertyAction = <>9__47_9;
                if (<>9__47_9 == null)
                {
                    Action<ThumbButtonInfo, TaskbarThumbButtonInfo> local1 = <>9__47_9;
                    setPropertyAction = <>9__47_9 = (x, y) => TaskbarThumbButtonInfoWrapper.CopyIfNeeded<bool>(y.DismissWhenClicked, x.DismissWhenClicked, v => y.DismissWhenClicked = v);
                }
                TaskbarThumbButtonInfoWrapper.OnThumbButtonInfoPropertyChanged(d, e, setPropertyAction);
            }

            internal void <.cctor>b__47_9(ThumbButtonInfo x, TaskbarThumbButtonInfo y)
            {
                TaskbarThumbButtonInfoWrapper.CopyIfNeeded<bool>(y.DismissWhenClicked, x.DismissWhenClicked, v => y.DismissWhenClicked = v);
            }
        }

        private class ThumbButtonInfoCommand : ICommand
        {
            public event EventHandler CanExecuteChanged
            {
                add
                {
                    if (this.InternalCommand != null)
                    {
                        this.InternalCommand.CanExecuteChanged += value;
                    }
                }
                remove
                {
                    if (this.InternalCommand != null)
                    {
                        this.InternalCommand.CanExecuteChanged -= value;
                    }
                }
            }

            public ThumbButtonInfoCommand(TaskbarThumbButtonInfo taskbarThumbButtonInfo)
            {
                ITaskbarThumbButtonInfo info = taskbarThumbButtonInfo;
                this.Click = info.Click;
                this.Action = taskbarThumbButtonInfo.Action;
                this.InternalCommand = taskbarThumbButtonInfo.Command;
            }

            public bool CanExecute(object parameter) => 
                (this.InternalCommand == null) || this.InternalCommand.CanExecute(parameter);

            public void Execute(object parameter)
            {
                if (this.Action != null)
                {
                    this.Action();
                }
                if (this.Click != null)
                {
                    this.Click(this, EventArgs.Empty);
                }
                if ((this.InternalCommand != null) && this.InternalCommand.CanExecute(parameter))
                {
                    this.InternalCommand.Execute(parameter);
                }
            }

            public System.Action Action { get; private set; }

            public ICommand InternalCommand { get; private set; }

            public EventHandler Click { get; private set; }
        }
    }
}

