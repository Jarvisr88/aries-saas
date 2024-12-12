namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;

    public class DXDialogWindowWrapper : WindowWrapper, IWindowWrapper<DXDialogWindow>, ITargetWrapper<DXDialogWindow>
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
            this.Target.ShowDialogWindow();
            return this.Target.dialogWindowResult;
        }

        public DXDialogWindow Target
        {
            get => 
                (DXDialogWindow) base.Target;
            set => 
                base.Target = value;
        }
    }
}

