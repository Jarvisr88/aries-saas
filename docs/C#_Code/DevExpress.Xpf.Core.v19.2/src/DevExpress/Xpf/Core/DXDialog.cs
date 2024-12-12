namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [TemplatePart(Name="YesButton", Type=typeof(Button)), TemplatePart(Name="Footer", Type=typeof(Panel)), TemplatePart(Name="OkButton", Type=typeof(Button)), TemplatePart(Name="CancelButton", Type=typeof(Button)), TemplatePart(Name="NoButton", Type=typeof(Button))]
    public class DXDialog : DXWindow
    {
        private const string FooterName = "Footer";
        private const string OkButtonName = "OkButton";
        internal const string CancelButtonName = "CancelButton";
        private const string YesButtonName = "YesButton";
        private const string NoButtonName = "NoButton";
        private bool setButtonHandlers;
        private MessageBoxResult messageBoxResult;
        private CancelEventHandler onOkButtonClick;

        public event CancelEventHandler OkButtonClick
        {
            add
            {
                this.onOkButtonClick += value;
            }
            remove
            {
                this.onOkButtonClick -= value;
            }
        }

        static DXDialog()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(DXDialog), new FrameworkPropertyMetadata(typeof(DXDialog)));
        }

        public DXDialog() : this(string.Empty)
        {
        }

        public DXDialog(string title) : this(title, DialogButtons.OkCancel)
        {
        }

        public DXDialog(string title, DialogButtons dialogButtons) : this(title, dialogButtons, true)
        {
        }

        public DXDialog(string title, DialogButtons dialogButtons, bool setButtonHandlers)
        {
            base.Title = title;
            this.Buttons = dialogButtons;
            this.setButtonHandlers = setButtonHandlers;
        }

        protected virtual void ApplyDialogButtonProperty()
        {
            switch (this.Buttons)
            {
                case DialogButtons.Ok:
                    this.SetButtonVisibilities(true, false, false, false, false);
                    return;

                case DialogButtons.OkCancel:
                    this.SetButtonVisibilities(true, true, false, false, false);
                    return;

                case DialogButtons.YesNoCancel:
                    this.SetButtonVisibilities(false, true, true, true, false);
                    return;

                case DialogButtons.YesNo:
                    this.SetButtonVisibilities(false, false, true, true, false);
                    return;
            }
        }

        protected virtual bool BeforeOnButtonClick(Button button, MessageBoxResult messageBoxResult)
        {
            this.OkButton.Focus();
            if (this.onOkButtonClick == null)
            {
                return true;
            }
            CancelEventArgs e = new CancelEventArgs();
            this.onOkButtonClick(this, e);
            return !e.Cancel;
        }

        private static DialogButtons GetDialogButtons(MessageBoxButton buttons)
        {
            switch (buttons)
            {
                case MessageBoxButton.OK:
                    return DialogButtons.Ok;

                case MessageBoxButton.OKCancel:
                    return DialogButtons.OkCancel;

                case MessageBoxButton.YesNoCancel:
                    return DialogButtons.YesNoCancel;

                case MessageBoxButton.YesNo:
                    return DialogButtons.YesNo;
            }
            return DialogButtons.Ok;
        }

        protected virtual void GetTemplateChildren()
        {
            this.Footer = base.GetTemplateChild("Footer") as Panel;
            this.OkButton = base.GetTemplateChild("OkButton") as Button;
            this.CancelButton = base.GetTemplateChild("CancelButton") as Button;
            this.YesButton = base.GetTemplateChild("YesButton") as Button;
            this.NoButton = base.GetTemplateChild("NoButton") as Button;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.GetTemplateChildren();
            if (this.setButtonHandlers)
            {
                this.SetButtonHandlers();
            }
            this.ApplyDialogButtonProperty();
        }

        protected virtual void OnButtonClick(bool? result, MessageBoxResult messageBoxResult)
        {
            this.messageBoxResult = messageBoxResult;
            base.DialogResult = result;
        }

        protected void SetButtonHandler(Button button, bool? result, MessageBoxResult messageBoxResult)
        {
            if (button != null)
            {
                button.Click += (d, e) => this.OnButtonClick(result, messageBoxResult);
            }
        }

        protected virtual void SetButtonHandlers()
        {
            this.SetOkButtonHandler(this.OkButton, true, MessageBoxResult.OK);
            this.SetButtonHandler(this.CancelButton, false, MessageBoxResult.Cancel);
            this.SetButtonHandler(this.YesButton, true, MessageBoxResult.Yes);
            this.SetButtonHandler(this.NoButton, false, MessageBoxResult.No);
        }

        protected virtual void SetButtonVisibilities(bool ok, bool cancel, bool yes, bool no, bool apply)
        {
            this.SetButtonVisibility(this.OkButton, ok);
            this.SetButtonVisibility(this.CancelButton, cancel);
            this.SetButtonVisibility(this.YesButton, yes);
            this.SetButtonVisibility(this.NoButton, no);
        }

        protected void SetButtonVisibility(Button button, bool visible)
        {
            if (button != null)
            {
                button.SetVisible(visible);
            }
        }

        protected void SetOkButtonHandler(Button button, bool? result, MessageBoxResult messageBoxResult)
        {
            if (button != null)
            {
                button.Click += delegate (object d, RoutedEventArgs e) {
                    if (this.BeforeOnButtonClick(button, messageBoxResult))
                    {
                        this.OnButtonClick(result, messageBoxResult);
                    }
                };
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void Show()
        {
            throw new NotSupportedException();
        }

        public MessageBoxResult ShowDialog(MessageBoxButton button)
        {
            this.Buttons = GetDialogButtons(button);
            this.messageBoxResult = MessageBoxResult.None;
            base.ShowDialog();
            return this.messageBoxResult;
        }

        public DialogButtons Buttons { get; set; }

        public Button OkButton { get; private set; }

        public Button CancelButton { get; private set; }

        public Button YesButton { get; private set; }

        public Button NoButton { get; private set; }

        protected Panel Footer { get; private set; }
    }
}

