namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class ButtonEditSettings : TextEditSettings
    {
        public static readonly DependencyProperty IsTextEditableProperty;
        public static readonly DependencyProperty AllowDefaultButtonProperty;
        public static readonly DependencyProperty NullValueButtonPlacementProperty;
        public static readonly DependencyProperty ButtonsSourceProperty;
        public static readonly DependencyProperty ButtonTemplateProperty;
        public static readonly DependencyProperty ButtonTemplateSelectorProperty;
        public static readonly DependencyProperty ShowTextProperty;
        private ButtonInfoCollection buttons;
        private IEnumerable<ButtonInfoBase> buttonsSourceButtons;
        private static readonly object DefaultButtonClickKey = new object();

        public event RoutedEventHandler DefaultButtonClick
        {
            add
            {
                base.AddHandler(DefaultButtonClickKey, value);
            }
            remove
            {
                base.RemoveHandler(DefaultButtonClickKey, value);
            }
        }

        static ButtonEditSettings()
        {
            Type ownerType = typeof(ButtonEditSettings);
            IsTextEditableProperty = DependencyPropertyManager.Register("IsTextEditable", typeof(bool?), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            AllowDefaultButtonProperty = DependencyPropertyManager.Register("AllowDefaultButton", typeof(bool?), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            NullValueButtonPlacementProperty = DependencyPropertyManager.Register("NullValueButtonPlacement", typeof(EditorPlacement?), ownerType, new PropertyMetadata(new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            ButtonsSourceProperty = DependencyPropertyManager.Register("ButtonsSource", typeof(IEnumerable), ownerType, new PropertyMetadata(new PropertyChangedCallback(ButtonEditSettings.OnButtonsChanged)));
            ButtonTemplateProperty = DependencyPropertyManager.Register("ButtonTemplate", typeof(DataTemplate), ownerType, new PropertyMetadata(new PropertyChangedCallback(ButtonEditSettings.OnButtonsChanged)));
            ButtonTemplateSelectorProperty = DependencyPropertyManager.Register("ButtonTemplateSelector", typeof(DataTemplateSelector), ownerType, new PropertyMetadata(new PropertyChangedCallback(ButtonEditSettings.OnButtonsChanged)));
            ShowTextProperty = DependencyPropertyManager.Register("ShowText", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
        }

        protected override void AssignToEditCore(IBaseEdit edit)
        {
            base.AssignToEditCore(edit);
            IInplaceBaseEdit edit2 = edit as IInplaceBaseEdit;
            if (edit2 != null)
            {
                bool? allowDefaultButton = this.AllowDefaultButton;
                edit2.AllowDefaultButton = (allowDefaultButton != null) ? allowDefaultButton.GetValueOrDefault() : true;
                edit2.ShowText = this.ShowText;
            }
            else
            {
                ButtonEdit be = edit as ButtonEdit;
                if (be != null)
                {
                    base.SetValueFromSettings(ShowTextProperty, () => be.ShowText = this.ShowText);
                    base.SetValueFromSettings(IsTextEditableProperty, () => be.IsTextEditable = this.IsTextEditable);
                    base.SetValueFromSettings(AllowDefaultButtonProperty, () => be.AllowDefaultButton = this.AllowDefaultButton);
                    base.SetValueFromSettings(NullValueButtonPlacementProperty, () => be.NullValueButtonPlacement = this.NullValueButtonPlacement);
                    base.SetValueFromSettings(ButtonsSourceProperty, () => be.ButtonsSource = this.ButtonsSource, () => this.ClearEditorPropertyIfNeeded(be, ButtonEdit.ButtonsSourceProperty, ButtonsSourceProperty));
                    base.SetValueFromSettings(ButtonTemplateProperty, () => be.ButtonTemplate = this.ButtonTemplate, () => this.ClearEditorPropertyIfNeeded(be, ButtonEdit.ButtonTemplateProperty, ButtonTemplateProperty));
                    base.SetValueFromSettings(ButtonTemplateSelectorProperty, () => be.ButtonTemplateSelector = this.ButtonTemplateSelector, () => this.ClearEditorPropertyIfNeeded(be, ButtonEdit.ButtonTemplateSelectorProperty, ButtonTemplateSelectorProperty));
                    if (!Equals(be.Buttons, this.Buttons))
                    {
                        be.Buttons.Clear();
                        foreach (ButtonInfoBase base2 in this.Buttons)
                        {
                            be.Buttons.Add((ButtonInfoBase) ((ICloneable) base2).Clone());
                        }
                    }
                }
            }
        }

        private void ButtonsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.RaiseChangedEventIfNotLoading();
            if (e.NewItems != null)
            {
                foreach (ButtonInfoBase base2 in e.NewItems)
                {
                    if (base2 != null)
                    {
                        base.AddLogicalChild(base2);
                    }
                }
            }
            if (e.OldItems != null)
            {
                foreach (ButtonInfoBase base3 in e.OldItems)
                {
                    if (base3 != null)
                    {
                        base.RemoveLogicalChild(base3);
                    }
                }
            }
        }

        private void ClearButtonsSourceButtons()
        {
            this.buttonsSourceButtons = null;
        }

        protected internal List<ButtonInfoBase> CreateButtonsSourceButtons(IEnumerable buttonsSource, DataTemplate template, DataTemplateSelector templateSelector)
        {
            List<ButtonInfoBase> list = new List<ButtonInfoBase>();
            if (buttonsSource != null)
            {
                foreach (object obj2 in buttonsSource)
                {
                    DataTemplate template2 = this.GetActualteButtonTemplate(obj2, template, templateSelector);
                    Func<ButtonInfoBase, ButtonInfoBase> evaluator = <>c.<>9__49_0;
                    if (<>c.<>9__49_0 == null)
                    {
                        Func<ButtonInfoBase, ButtonInfoBase> local1 = <>c.<>9__49_0;
                        evaluator = <>c.<>9__49_0 = x => x;
                    }
                    ButtonInfoBase item = this.GetButtonInfoFromTemplate(template2).Return<ButtonInfoBase, ButtonInfoBase>(evaluator, <>c.<>9__49_1 ??= () => new ButtonInfo());
                    item.DataContext = obj2;
                    list.Add(item);
                }
            }
            return list;
        }

        protected internal virtual ButtonInfoBase CreateDefaultButtonInfo()
        {
            ButtonInfo info1 = new ButtonInfo();
            info1.GlyphKind = GlyphKind.Regular;
            info1.IsDefaultButton = true;
            return info1;
        }

        protected internal virtual ButtonInfoBase CreateNullValueButtonInfo()
        {
            if (!CompatibilitySettings.UseLegacyDeleteButtonInButtonEdit)
            {
                DeleteButtonInfo info2 = new DeleteButtonInfo();
                info2.IsDefaultButton = true;
                return info2;
            }
            LegacyDeleteButtonInfo info1 = new LegacyDeleteButtonInfo();
            info1.GlyphKind = GlyphKind.Cancel;
            info1.Content = EditorLocalizer.Active.GetLocalizedString(EditorStringId.SetNullValue);
            info1.IsDefaultButton = true;
            return info1;
        }

        protected internal virtual IEnumerable<ButtonInfoBase> GetActualButtons()
        {
            List<ButtonInfoBase> list = new List<ButtonInfoBase>(this.Buttons);
            list.AddRange(this.ButtonsSourceButtons);
            return list;
        }

        private DataTemplate GetActualteButtonTemplate(object item, DataTemplate template, DataTemplateSelector templateSelector) => 
            (templateSelector == null) ? template : templateSelector.SelectTemplate(item, this);

        private ButtonInfoBase GetButtonInfoFromTemplate(DataTemplate template) => 
            (template != null) ? TemplateHelper.LoadFromTemplate<ButtonInfoBase>(template) : null;

        protected internal override bool IsPasteGesture(Key key, ModifierKeys modifiers) => 
            this.IsTextEditable.GetValueOrDefault(true) && base.IsPasteGesture(key, modifiers);

        private static void OnButtonsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OnSettingsPropertyChanged(d, e);
            ((ButtonEditSettings) d).ClearButtonsSourceButtons();
        }

        protected internal virtual void RaiseDefaultButtonClick(object sender, RoutedEventArgs e)
        {
            Delegate delegate2;
            if (base.Events.TryGetValue(DefaultButtonClickKey, out delegate2))
            {
                ((RoutedEventHandler) delegate2)(sender, e);
            }
        }

        [Category("Appearance ")]
        public EditorPlacement? NullValueButtonPlacement
        {
            get => 
                (EditorPlacement?) base.GetValue(NullValueButtonPlacementProperty);
            set => 
                base.SetValue(NullValueButtonPlacementProperty, value);
        }

        [Description("Gets or sets whether end-users are allowed to edit the text displayed in the edit box. This is a dependency property."), Category("Behavior")]
        public bool? IsTextEditable
        {
            get => 
                (bool?) base.GetValue(IsTextEditableProperty);
            set => 
                base.SetValue(IsTextEditableProperty, value);
        }

        [Category("Behavior"), Description("Gets or sets whether the editor's default button is displayed. This is a dependency property.")]
        public bool? AllowDefaultButton
        {
            get => 
                (bool?) base.GetValue(AllowDefaultButtonProperty);
            set => 
                base.SetValue(AllowDefaultButtonProperty, value);
        }

        public IEnumerable ButtonsSource
        {
            get => 
                (IEnumerable) base.GetValue(ButtonsSourceProperty);
            set => 
                base.SetValue(ButtonsSourceProperty, value);
        }

        public DataTemplate ButtonTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ButtonTemplateProperty);
            set => 
                base.SetValue(ButtonTemplateProperty, value);
        }

        public DataTemplateSelector ButtonTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ButtonTemplateSelectorProperty);
            set => 
                base.SetValue(ButtonTemplateSelectorProperty, value);
        }

        public bool ShowText
        {
            get => 
                (bool) base.GetValue(ShowTextProperty);
            set => 
                base.SetValue(ShowTextProperty, value);
        }

        [Category("Behavior"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), SkipPropertyAssertion, Description("Returns the collection of buttons.")]
        public ButtonInfoCollection Buttons
        {
            get
            {
                if (this.buttons == null)
                {
                    this.buttons = new ButtonInfoCollection();
                    this.buttons.CollectionChanged += new NotifyCollectionChangedEventHandler(this.ButtonsCollectionChanged);
                }
                return this.buttons;
            }
        }

        private IEnumerable<ButtonInfoBase> ButtonsSourceButtons
        {
            get
            {
                this.buttonsSourceButtons ??= this.CreateButtonsSourceButtons(this.ButtonsSource, this.ButtonTemplate, this.ButtonTemplateSelector);
                return this.buttonsSourceButtons;
            }
        }

        protected override IEnumerator LogicalChildren
        {
            get
            {
                List<object> list = new List<object>();
                if (base.LogicalChildren != null)
                {
                    IEnumerator logicalChildren = base.LogicalChildren;
                    while (logicalChildren.MoveNext())
                    {
                        list.Add(logicalChildren.Current);
                    }
                }
                if (this.Buttons != null)
                {
                    foreach (ButtonInfoBase base2 in this.Buttons)
                    {
                        if (base2 != null)
                        {
                            list.Add(base2);
                        }
                    }
                }
                return list.GetEnumerator();
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ButtonEditSettings.<>c <>9 = new ButtonEditSettings.<>c();
            public static Func<ButtonInfoBase, ButtonInfoBase> <>9__49_0;
            public static Func<ButtonInfoBase> <>9__49_1;

            internal ButtonInfoBase <CreateButtonsSourceButtons>b__49_0(ButtonInfoBase x) => 
                x;

            internal ButtonInfoBase <CreateButtonsSourceButtons>b__49_1() => 
                new ButtonInfo();
        }
    }
}

