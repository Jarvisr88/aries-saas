namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;

    public class SearchControlPropertyProvider : FrameworkElement
    {
        internal static readonly DependencyPropertyKey ActualShowClearButtonPropertyKey;
        public static readonly DependencyProperty ActualShowClearButtonProperty;
        public static readonly DependencyProperty DisplayTextProperty;
        public static readonly DependencyProperty IsNullTextVisibleProperty;
        private static readonly DependencyPropertyKey ActualShowFindButtonPropertyKey;
        public static readonly DependencyProperty ActualShowFindButtonProperty;
        public static readonly DependencyProperty FindCommandProperty;
        private static readonly DependencyPropertyKey ActualImmediateMRUPopupPropertyKey;
        public static readonly DependencyProperty ActualImmediateMRUPopupProperty;
        public static readonly DependencyProperty CloseCommandInternalProperty;
        private static readonly DependencyPropertyKey ActualPostModePropertyKey;
        public static readonly DependencyProperty ActualPostModeProperty;
        public static readonly DependencyProperty ClearSearchTextCommandProperty;
        public static readonly DependencyProperty SearchTextProperty;
        internal static readonly DependencyPropertyKey ActualShowResultInfoPropertyKey;
        public static readonly DependencyProperty ActualShowResultInfoProperty;
        private readonly DevExpress.Xpf.Editors.SearchControl SearchControl;

        public event EventHandler DisplayTextChanged;

        static SearchControlPropertyProvider()
        {
            Type ownerType = typeof(SearchControlPropertyProvider);
            ActualShowClearButtonPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualShowClearButton", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None));
            ActualShowClearButtonProperty = ActualShowClearButtonPropertyKey.DependencyProperty;
            DisplayTextProperty = DependencyPropertyManager.Register("DisplayText", typeof(string), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (d, e) => ((SearchControlPropertyProvider) d).OnDisplayTextChanged()));
            IsNullTextVisibleProperty = DependencyPropertyManager.Register("IsNullTextVisible", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None, (d, e) => ((SearchControlPropertyProvider) d).OnDisplayTextChanged()));
            ActualShowFindButtonPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualShowFindButton", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            ActualShowFindButtonProperty = ActualShowFindButtonPropertyKey.DependencyProperty;
            FindCommandProperty = DependencyPropertyManager.Register("FindCommand", typeof(ICommand), ownerType, new FrameworkPropertyMetadata(null));
            ActualImmediateMRUPopupPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualImmediateMRUPopup", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.None));
            ActualImmediateMRUPopupProperty = ActualImmediateMRUPopupPropertyKey.DependencyProperty;
            ActualPostModePropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualPostMode", typeof(PostMode), ownerType, new FrameworkPropertyMetadata(PostMode.Delayed, FrameworkPropertyMetadataOptions.None));
            ActualPostModeProperty = ActualPostModePropertyKey.DependencyProperty;
            CloseCommandInternalProperty = DependencyPropertyManager.Register("CloseCommandInternal", typeof(ICommand), ownerType, new FrameworkPropertyMetadata(null));
            ClearSearchTextCommandProperty = DependencyPropertyManager.Register("ClearSearchTextCommand", typeof(ICommand), ownerType, new FrameworkPropertyMetadata(null));
            SearchTextProperty = DependencyProperty.Register("SearchText", typeof(string), ownerType, new PropertyMetadata(null, (d, e) => ((SearchControlPropertyProvider) d).OnSearchTextChanged()));
            ActualShowResultInfoPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualShowResultInfo", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None));
            ActualShowResultInfoProperty = ActualShowResultInfoPropertyKey.DependencyProperty;
        }

        public SearchControlPropertyProvider(DevExpress.Xpf.Editors.SearchControl SearchControl)
        {
            this.SearchControl = SearchControl;
            this.FindCommand = DelegateCommandFactory.Create<object>(command => SearchControl.OnFindCommandExecuted(), false);
            this.ClearSearchTextCommand = DelegateCommandFactory.Create<object>(command => SearchControl.OnClearSearchTextCommandExecuted(), false);
        }

        public void BindEditorProperties(DevExpress.Xpf.Editors.SearchControl searchControl, ButtonEdit editor)
        {
            Binding binding3 = new Binding {
                Source = editor,
                Path = new PropertyPath(BaseEdit.DisplayTextProperty.GetName(), new object[0]),
                Mode = BindingMode.OneWay
            };
            Binding binding = binding3;
            base.SetBinding(DisplayTextProperty, binding);
            binding3 = new Binding {
                Source = editor,
                Path = new PropertyPath(BaseEdit.IsNullTextVisibleProperty.GetName(), new object[0]),
                Mode = BindingMode.OneWay
            };
            Binding binding2 = binding3;
            base.SetBinding(IsNullTextVisibleProperty, binding2);
            this.CloseCommandInternal = DelegateCommandFactory.Create<object>(delegate (object o) {
                editor.DoValidate();
                if (searchControl.CloseCommand != null)
                {
                    searchControl.CloseCommand.Execute(null);
                }
            }, false);
            editor.KeyDown += delegate (object o, KeyEventArgs e) {
                if ((e.Key == Key.Return) && ((ModifierKeysHelper.GetKeyboardModifiers(e) == ModifierKeys.None) && searchControl.SaveMRUOnStringChanged))
                {
                    searchControl.UpdateMRU();
                }
            };
            editor.EditValueChanged += delegate (object o, EditValueChangedEventArgs e) {
                if (searchControl.SaveMRUOnStringChanged)
                {
                    searchControl.UpdateMRU();
                }
            };
        }

        private void OnDisplayTextChanged()
        {
            if (this.DisplayTextChanged != null)
            {
                this.DisplayTextChanged(this, EventArgs.Empty);
            }
        }

        private void OnSearchTextChanged()
        {
            if (this.SearchText != this.SearchControl.SearchText)
            {
                this.IsSearchTextPosting = true;
                this.SearchControl.SearchText = this.SearchText;
                this.IsSearchTextPosting = false;
            }
        }

        public void UpdateActualImmediateMRUPopup(DevExpress.Xpf.Editors.SearchControl SearchControl)
        {
            this.ActualImmediateMRUPopup = (SearchControl.ImmediateMRUPopup != null) ? SearchControl.ImmediateMRUPopup.Value : false;
        }

        internal void UpdatePostMode(DevExpress.Xpf.Editors.SearchControl searchControl)
        {
            if (searchControl.PostMode == null)
            {
                this.ActualPostMode = (searchControl.FindMode == FindMode.FindClick) ? PostMode.Immediate : PostMode.Delayed;
            }
            else
            {
                this.ActualPostMode = searchControl.PostMode.Value;
            }
        }

        internal bool IsSearchTextPosting { get; private set; }

        public bool ActualShowClearButton
        {
            get => 
                (bool) base.GetValue(ActualShowClearButtonProperty);
            internal set => 
                base.SetValue(ActualShowClearButtonPropertyKey, value);
        }

        public bool ActualShowResultInfo
        {
            get => 
                (bool) base.GetValue(ActualShowResultInfoProperty);
            internal set => 
                base.SetValue(ActualShowResultInfoPropertyKey, value);
        }

        public string DisplayText
        {
            get => 
                (string) base.GetValue(DisplayTextProperty);
            set => 
                base.SetValue(DisplayTextProperty, value);
        }

        public bool IsNullTextVisible
        {
            get => 
                (bool) base.GetValue(IsNullTextVisibleProperty);
            set => 
                base.SetValue(IsNullTextVisibleProperty, value);
        }

        public bool ActualShowFindButton
        {
            get => 
                (bool) base.GetValue(ActualShowFindButtonProperty);
            internal set => 
                base.SetValue(ActualShowFindButtonPropertyKey, value);
        }

        public ICommand FindCommand
        {
            get => 
                (ICommand) base.GetValue(FindCommandProperty);
            set => 
                base.SetValue(FindCommandProperty, value);
        }

        public ICommand ClearSearchTextCommand
        {
            get => 
                (ICommand) base.GetValue(ClearSearchTextCommandProperty);
            set => 
                base.SetValue(ClearSearchTextCommandProperty, value);
        }

        public bool ActualImmediateMRUPopup
        {
            get => 
                (bool) base.GetValue(ActualImmediateMRUPopupProperty);
            internal set => 
                base.SetValue(ActualImmediateMRUPopupPropertyKey, value);
        }

        public PostMode ActualPostMode
        {
            get => 
                (PostMode) base.GetValue(ActualPostModeProperty);
            internal set => 
                base.SetValue(ActualPostModePropertyKey, value);
        }

        public ICommand CloseCommandInternal
        {
            get => 
                (ICommand) base.GetValue(CloseCommandInternalProperty);
            set => 
                base.SetValue(CloseCommandInternalProperty, value);
        }

        public string SearchText
        {
            get => 
                (string) base.GetValue(SearchTextProperty);
            set => 
                base.SetValue(SearchTextProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SearchControlPropertyProvider.<>c <>9 = new SearchControlPropertyProvider.<>c();

            internal void <.cctor>b__16_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SearchControlPropertyProvider) d).OnDisplayTextChanged();
            }

            internal void <.cctor>b__16_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SearchControlPropertyProvider) d).OnDisplayTextChanged();
            }

            internal void <.cctor>b__16_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SearchControlPropertyProvider) d).OnSearchTextChanged();
            }
        }
    }
}

