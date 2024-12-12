namespace DevExpress.Xpf.Editors
{
    using DevExpress.Data.Mask;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class WpfMaskManager
    {
        private readonly IMaskManagerProvider maskManagerProvider;

        public WpfMaskManager(IMaskManagerProvider maskManagerProvider)
        {
            this.maskManagerProvider = maskManagerProvider;
            this.SetInitialEditValueLocker = new Locker();
            this.Initialize();
        }

        public bool Backspace() => 
            this.MaskManager.Backspace();

        public bool CheckCursorLeft() => 
            this.MaskManager.CursorLeft(false, true);

        public bool CheckCursorRight() => 
            this.MaskManager.CursorRight(false, true);

        public bool CursorEnd(bool forceSelection) => 
            this.MaskManager.CursorEnd(forceSelection);

        public bool CursorHome(bool forceSelection) => 
            this.MaskManager.CursorHome(forceSelection);

        public bool CursorLeft(bool forceSelection) => 
            this.MaskManager.CursorLeft(forceSelection, false);

        public bool CursorRight(bool forceSelection) => 
            this.MaskManager.CursorRight(forceSelection, false);

        public bool CursorToDisplayPosition(int newPosition, bool forceSelection) => 
            (this.MaskManager != null) ? this.MaskManager.CursorToDisplayPosition(newPosition, forceSelection) : false;

        public bool Delete() => 
            this.MaskManager.Delete();

        public bool FlushPendingEditActions() => 
            this.MaskManager.FlushPendingEditActions();

        public object GetCurrentEditValue() => 
            this.MaskManager?.GetCurrentEditValue();

        public void GotFocus()
        {
            Action<TimeSpanMaskManager> action = <>c.<>9__46_0;
            if (<>c.<>9__46_0 == null)
            {
                Action<TimeSpanMaskManager> local1 = <>c.<>9__46_0;
                action = <>c.<>9__46_0 = x => x.GotFocus();
            }
            (this.MaskManager as TimeSpanMaskManager).Do<TimeSpanMaskManager>(action);
        }

        public void Initialize()
        {
            this.UnsubscribeEvents();
            this.MaskManager = this.maskManagerProvider.CreateNew();
            this.SubscribeEvents();
        }

        public bool Insert(string insertion) => 
            this.MaskManager.Insert(insertion);

        public void LostFocus()
        {
            Action<TimeSpanMaskManager> action = <>c.<>9__47_0;
            if (<>c.<>9__47_0 == null)
            {
                Action<TimeSpanMaskManager> local1 = <>c.<>9__47_0;
                action = <>c.<>9__47_0 = x => x.LostFocus();
            }
            (this.MaskManager as TimeSpanMaskManager).Do<TimeSpanMaskManager>(action);
        }

        private void MaskManagerEditTextChanged(object sender, EventArgs e)
        {
            this.SetInitialEditValueLocker.DoIfNotLocked(() => this.maskManagerProvider.UpdateRequired());
        }

        private void MaskManagerLocalEditAction(object sender, CancelEventArgs e)
        {
            this.maskManagerProvider.LocalEditActionPerformed();
        }

        public void SelectAll()
        {
            this.MaskManager.SelectAll();
        }

        public void SetInitialEditValue(object initialEditValue)
        {
            if (this.MaskManager != null)
            {
                this.SetInitialEditValueLocker.DoLockedAction(() => this.MaskManager.SetInitialEditValue(initialEditValue));
            }
        }

        public bool SpinDown() => 
            this.MaskManager.SpinDown();

        public bool SpinUp() => 
            this.MaskManager.SpinUp();

        private void SubscribeEvents()
        {
            if (this.MaskManager != null)
            {
                this.MaskManager.LocalEditAction += new CancelEventHandler(this.MaskManagerLocalEditAction);
                this.MaskManager.EditTextChanged += new EventHandler(this.MaskManagerEditTextChanged);
            }
        }

        public bool Undo() => 
            this.MaskManager.Undo();

        private void UnsubscribeEvents()
        {
            if (this.MaskManager != null)
            {
                this.MaskManager.LocalEditAction -= new CancelEventHandler(this.MaskManagerLocalEditAction);
                this.MaskManager.EditTextChanged -= new EventHandler(this.MaskManagerEditTextChanged);
            }
        }

        private Locker SetInitialEditValueLocker { get; set; }

        private DevExpress.Data.Mask.MaskManager MaskManager { get; set; }

        public int DisplayCursorPosition =>
            (this.MaskManager != null) ? this.MaskManager.DisplayCursorPosition : 0;

        public int DisplaySelectionLength =>
            (this.MaskManager != null) ? this.MaskManager.DisplaySelectionLength : 0;

        public int DisplaySelectionStart =>
            (this.MaskManager != null) ? this.MaskManager.DisplaySelectionStart : 0;

        public bool CanUndo =>
            this.MaskManager.CanUndo;

        public bool IsFinal =>
            this.MaskManager.IsFinal;

        public bool IsMatch =>
            this.MaskManager.IsMatch;

        public string DisplayText =>
            (this.MaskManager == null) ? string.Empty : this.MaskManager.DisplayText;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly WpfMaskManager.<>c <>9 = new WpfMaskManager.<>c();
            public static Action<TimeSpanMaskManager> <>9__46_0;
            public static Action<TimeSpanMaskManager> <>9__47_0;

            internal void <GotFocus>b__46_0(TimeSpanMaskManager x)
            {
                x.GotFocus();
            }

            internal void <LostFocus>b__47_0(TimeSpanMaskManager x)
            {
                x.LostFocus();
            }
        }
    }
}

