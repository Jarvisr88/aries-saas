namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;

    public class ThemedWindowWrapper : WindowWrapper, IWindowWrapper<ThemedWindow>, ITargetWrapper<ThemedWindow>
    {
        public override void Activate()
        {
            this.Target.Activate();
        }

        public override void Close()
        {
            this.Target.Close();
        }

        public override void Show()
        {
            this.Target.Show();
        }

        public override MessageBoxResult ShowDialog()
        {
            this.Target.ShowDialog();
            return this.Target.DialogButtonResult;
        }

        public ThemedWindow Target
        {
            get => 
                (ThemedWindow) base.Target;
            set => 
                base.Target = value;
        }
    }
}

