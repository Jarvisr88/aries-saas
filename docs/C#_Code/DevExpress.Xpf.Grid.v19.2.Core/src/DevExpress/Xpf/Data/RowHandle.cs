namespace DevExpress.Xpf.Data
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class RowHandle : INotifyPropertyChanged
    {
        private int value;

        public event PropertyChangedEventHandler PropertyChanged;

        public RowHandle(int value)
        {
            this.value = value;
        }

        public override bool Equals(object obj)
        {
            RowHandle handle = obj as RowHandle;
            return ((handle != null) ? (this.Value == handle.Value) : false);
        }

        public override int GetHashCode() => 
            this.Value;

        [Description("Gets the row's handle.")]
        public int Value =>
            this.value;
    }
}

