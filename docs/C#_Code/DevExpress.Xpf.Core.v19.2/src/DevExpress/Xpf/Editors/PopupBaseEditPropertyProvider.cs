namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class PopupBaseEditPropertyProvider : ButtonEditPropertyProvider
    {
        public static readonly DependencyProperty PopupTopAreaTemplateProperty;
        public static readonly DependencyProperty PopupBottomAreaTemplateProperty;
        public static readonly DependencyProperty PopupViewModelProperty;
        public static readonly DependencyProperty AddNewCommandProperty;

        static PopupBaseEditPropertyProvider()
        {
            Type ownerType = typeof(PopupBaseEditPropertyProvider);
            PopupTopAreaTemplateProperty = DependencyPropertyManager.Register("PopupTopAreaTemplate", typeof(ControlTemplate), ownerType, new PropertyMetadata(null));
            PopupBottomAreaTemplateProperty = DependencyPropertyManager.Register("PopupBottomAreaTemplate", typeof(ControlTemplate), ownerType, new PropertyMetadata(null));
            PopupViewModelProperty = DependencyPropertyManager.Register("PopupViewModel", typeof(DevExpress.Xpf.Editors.PopupViewModel), ownerType, new PropertyMetadata(null));
            AddNewCommandProperty = DependencyPropertyManager.Register("AddNewCommand", typeof(ICommand), ownerType, new PropertyMetadata(null));
        }

        public PopupBaseEditPropertyProvider(TextEdit editor) : base(editor)
        {
            this.PopupViewModel = this.CreatePopupViewModel();
            this.ResizeGripViewModel = this.CreateResizeGripViewModel();
        }

        protected virtual DevExpress.Xpf.Editors.PopupViewModel CreatePopupViewModel() => 
            new DevExpress.Xpf.Editors.PopupViewModel(this.Editor);

        protected virtual DevExpress.Xpf.Editors.ResizeGripViewModel CreateResizeGripViewModel() => 
            new DevExpress.Xpf.Editors.ResizeGripViewModel(this.Editor);

        protected internal virtual PopupCloseMode GetClosePopupOnClickMode()
        {
            PopupCloseMode? closePopupOnClickMode = this.Editor.ClosePopupOnClickMode;
            return ((closePopupOnClickMode != null) ? closePopupOnClickMode.GetValueOrDefault() : this.StyleSettings.GetClosePopupOnClickMode(this.Editor));
        }

        protected virtual bool GetFocusPopupOnOpenInternal() => 
            this.StyleSettings.ShouldFocusPopup;

        protected virtual bool GetIgnorePopupSizeConstraints() => 
            false;

        protected override ControlTemplate GetPopupBottomAreaTemplate() => 
            this.Editor.PopupBottomAreaTemplate ?? this.StyleSettings.GetPopupBottomAreaTemplate(this.Editor);

        public override PopupFooterButtons GetPopupFooterButtons()
        {
            PopupFooterButtons? popupFooterButtons = this.Editor.PopupFooterButtons;
            return ((popupFooterButtons != null) ? popupFooterButtons.GetValueOrDefault() : this.StyleSettings.GetPopupFooterButtons(this.Editor));
        }

        protected override ControlTemplate GetPopupTopAreaTemplate() => 
            this.Editor.PopupTopAreaTemplate ?? this.StyleSettings.GetPopupTopAreaTemplate(this.Editor);

        protected internal virtual bool GetShowSizeGrip()
        {
            bool? showSizeGrip = this.Editor.ShowSizeGrip;
            return ((showSizeGrip != null) ? showSizeGrip.GetValueOrDefault() : this.StyleSettings.GetShowSizeGrip(this.Editor));
        }

        public virtual void UpdatePopupTemplates()
        {
            this.PopupTopAreaTemplate = this.GetPopupTopAreaTemplate();
            this.PopupBottomAreaTemplate = this.GetPopupBottomAreaTemplate();
        }

        private PopupBaseEdit Editor =>
            (PopupBaseEdit) base.Editor;

        private PopupBaseEditStyleSettings StyleSettings =>
            (PopupBaseEditStyleSettings) base.StyleSettings;

        public virtual bool StaysPopupOpen
        {
            get
            {
                bool? staysPopupOpen = this.Editor.StaysPopupOpen;
                return ((staysPopupOpen != null) ? staysPopupOpen.GetValueOrDefault() : this.StyleSettings.StaysPopupOpen());
            }
        }

        public ControlTemplate PopupTopAreaTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(PopupTopAreaTemplateProperty);
            set => 
                base.SetValue(PopupTopAreaTemplateProperty, value);
        }

        public ControlTemplate PopupBottomAreaTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(PopupBottomAreaTemplateProperty);
            set => 
                base.SetValue(PopupBottomAreaTemplateProperty, value);
        }

        public DevExpress.Xpf.Editors.PopupViewModel PopupViewModel
        {
            get => 
                (DevExpress.Xpf.Editors.PopupViewModel) base.GetValue(PopupViewModelProperty);
            set => 
                base.SetValue(PopupViewModelProperty, value);
        }

        public ICommand AddNewCommand
        {
            get => 
                (ICommand) base.GetValue(AddNewCommandProperty);
            set => 
                base.SetValue(AddNewCommandProperty, value);
        }

        public bool FocusPopupOnOpen
        {
            get
            {
                bool? focusPopupOnOpen = this.Editor.FocusPopupOnOpen;
                return ((focusPopupOnOpen != null) ? focusPopupOnOpen.GetValueOrDefault() : this.GetFocusPopupOnOpenInternal());
            }
        }

        public bool IgnorePopupSizeConstraints
        {
            get
            {
                bool? ignorePopupSizeConstraints = this.Editor.IgnorePopupSizeConstraints;
                return ((ignorePopupSizeConstraints != null) ? ignorePopupSizeConstraints.GetValueOrDefault() : this.GetIgnorePopupSizeConstraints());
            }
        }

        public DevExpress.Xpf.Editors.ResizeGripViewModel ResizeGripViewModel { get; private set; }
    }
}

