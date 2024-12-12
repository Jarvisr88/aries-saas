namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    [DXToolboxBrowsable(false)]
    public class PopupCalcEditCalculator : Calculator
    {
        public static readonly DependencyProperty IsMemoryIndicatorProperty = DependencyPropertyManager.RegisterAttached("IsMemoryIndicator", typeof(bool), typeof(PopupCalcEditCalculator), new PropertyMetadata());
        private readonly List<Control> memoryIndicators = new List<Control>();

        public PopupCalcEditCalculator()
        {
            this.SetDefaultStyleKey(typeof(PopupCalcEditCalculator));
            base.LayoutUpdated += new EventHandler(this.OnLayoutUpdated);
        }

        private void CheckMemoryIndicator(FrameworkElement element)
        {
            if (GetIsMemoryIndicator(element) && (element is Control))
            {
                this.memoryIndicators.Add(element as Control);
            }
        }

        public static bool GetIsMemoryIndicator(DependencyObject obj) => 
            (bool) obj.GetValue(IsMemoryIndicatorProperty);

        internal void InitMemory(decimal value)
        {
            base.View.Memory = value;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.OwnerEdit != null)
            {
                this.OwnerEdit.Calculator = this;
                this.UpdateDisplayText();
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            this.UpdateDisplayText();
        }

        private void OnLayoutUpdated(object sender, EventArgs e)
        {
            base.LayoutUpdated -= new EventHandler(this.OnLayoutUpdated);
            this.memoryIndicators.Clear();
            LayoutHelper.ForEachElement(this, new LayoutHelper.ElementHandler(this.CheckMemoryIndicator));
            this.UpdateMemoryIndicators(false);
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);
            this.UpdateDisplayText();
        }

        protected override void PropertyChangedDisplayText(string oldValue)
        {
            base.PropertyChangedDisplayText(oldValue);
            this.UpdateDisplayText();
        }

        protected override void PropertyChangedMemory(decimal oldValue)
        {
            base.PropertyChangedMemory(oldValue);
            this.UpdateMemoryIndicators(true);
        }

        public static void SetIsMemoryIndicator(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMemoryIndicatorProperty, value);
        }

        private void UpdateDisplayText()
        {
            if (this.OwnerEdit != null)
            {
                this.OwnerEdit.ForceChangeDisplayText();
            }
        }

        private void UpdateMemoryIndicators(bool useTransitions)
        {
            foreach (Control control in this.memoryIndicators)
            {
                VisualStateManager.GoToState(control, (base.Memory == 0M) ? "EmptyMemory" : "MemoryAssigned", useTransitions);
            }
        }

        public PopupCalcEdit OwnerEdit =>
            (PopupCalcEdit) BaseEdit.GetOwnerEdit(this);
    }
}

