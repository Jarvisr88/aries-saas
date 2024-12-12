namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RowPointer
    {
        private readonly int[] path;
        private readonly int handle;

        public RowPointer(int handle) : this(handle, new int[0])
        {
        }

        public RowPointer(int handle, int[] path)
        {
            this.handle = handle;
            this.path = path;
        }

        public override bool Equals(object obj)
        {
            RowPointer pointer = obj as RowPointer;
            return ((pointer != null) ? ((pointer.Handle == this.Handle) && pointer.Path.SequenceEqual<int>(this.path)) : false);
        }

        public override int GetHashCode() => 
            (((0x11 * 0x17) + this.path.GetHashCode(EqualityComparer<int>.Default)) * 0x17) + this.Handle.GetHashCode();

        public int[] Path =>
            this.path;

        public int Handle =>
            this.handle;
    }
}

