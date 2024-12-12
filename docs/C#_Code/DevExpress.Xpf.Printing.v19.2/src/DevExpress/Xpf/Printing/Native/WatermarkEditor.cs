namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Printing;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    public class WatermarkEditor : DXWindow, IComponentConnector
    {
        public static readonly DependencyProperty ModelProperty = DependencyPropertyManager.Register("Model", typeof(LegacyWatermarkEditorViewModel), typeof(WatermarkEditor), new PropertyMetadata(null, new PropertyChangedCallback(WatermarkEditor.OnModelChanged)));
        internal WatermarkControl watermarkControl;
        internal Button buttonClearAll;
        internal Button buttonOk;
        internal Button buttonCancel;
        private bool _contentLoaded;

        public WatermarkEditor()
        {
            this.InitializeComponent();
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        internal Delegate _CreateDelegate(System.Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

        private void ButtonClearAllClick(object sender, RoutedEventArgs e)
        {
            this.Model.ClearAllCommand.Execute(null);
        }

        private void ButtonOkClick(object sender, RoutedEventArgs e)
        {
            base.DialogResult = true;
            base.Close();
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/DevExpress.Xpf.Printing.v19.2;component/native/watermarkeditor.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        private static void OnModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LegacyWatermarkEditorViewModel newValue = e.NewValue as LegacyWatermarkEditorViewModel;
            if (newValue != null)
            {
                ((WatermarkEditor) d).DataContext = newValue;
                newValue.DialogService = new DevExpress.Xpf.Printing.DialogService((FrameworkElement) d);
            }
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.watermarkControl = (WatermarkControl) target;
                    return;

                case 2:
                    this.buttonClearAll = (Button) target;
                    this.buttonClearAll.Click += new RoutedEventHandler(this.ButtonClearAllClick);
                    return;

                case 3:
                    this.buttonOk = (Button) target;
                    this.buttonOk.Click += new RoutedEventHandler(this.ButtonOkClick);
                    return;

                case 4:
                    this.buttonCancel = (Button) target;
                    return;
            }
            this._contentLoaded = true;
        }

        public LegacyWatermarkEditorViewModel Model
        {
            get => 
                (LegacyWatermarkEditorViewModel) base.GetValue(ModelProperty);
            set => 
                base.SetValue(ModelProperty, value);
        }
    }
}

