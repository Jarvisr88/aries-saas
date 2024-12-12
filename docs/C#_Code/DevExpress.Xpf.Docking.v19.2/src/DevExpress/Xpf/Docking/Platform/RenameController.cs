namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class RenameController : IDisposable
    {
        private DevExpress.Xpf.Docking.Platform.RenameHint renameHint;
        private bool isDisposing;

        public RenameController(DockingHintAdornerBase adorner)
        {
            this.Adorner = adorner;
            this.Container = DockLayoutManager.GetDockLayoutManager(this.Adorner.AdornedElement);
        }

        internal void CancelRenaming()
        {
            if (this.EnsureRenameHint() && this.renameHint.IsRenamingStarted)
            {
                this.renameHint.CancelRenaming();
            }
            this.Adorner.Update();
        }

        protected DevExpress.Xpf.Docking.Platform.RenameHint CreateRenameHint()
        {
            Size size = new Size();
            return (DevExpress.Xpf.Docking.Platform.RenameHint) this.Adorner.CreateDockHintElement(DockVisualizerElement.RenameHint, size, Alignment.Fill);
        }

        public void Dispose()
        {
            if (!this.isDisposing)
            {
                this.isDisposing = true;
                this.Container = null;
            }
        }

        internal void EndRenaming()
        {
            if (this.EnsureRenameHint() && this.renameHint.IsRenamingStarted)
            {
                this.renameHint.EndRenaming();
            }
            this.Adorner.Update();
        }

        protected bool EnsureRenameHint()
        {
            this.renameHint ??= this.CreateRenameHint();
            return (this.renameHint != null);
        }

        internal void StartRenaming(IDockLayoutElement layoutElement)
        {
            if (this.Adorner.ContainsSelection(layoutElement.Item))
            {
                if (this.EnsureRenameHint())
                {
                    if (this.renameHint.IsRenamingStarted)
                    {
                        this.CancelRenaming();
                    }
                    this.renameHint.StartRenaming(layoutElement);
                }
                this.Adorner.Update();
            }
        }

        public bool IsRenamingStarted =>
            (this.RenameHint != null) && this.RenameHint.IsRenamingStarted;

        protected DockLayoutManager Container { get; private set; }

        protected DockingHintAdornerBase Adorner { get; private set; }

        protected internal DevExpress.Xpf.Docking.Platform.RenameHint RenameHint =>
            this.renameHint;
    }
}

