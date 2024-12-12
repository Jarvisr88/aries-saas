namespace DevExpress.Xpf.Grid.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class MasterRowScrollInfo
    {
        public MasterRowScrollInfo(int startScrollIndex, int detailStartScrollIndex, bool wholeDetailScrolledOut)
        {
            this.StartScrollIndex = startScrollIndex;
            this.DetailStartScrollIndex = detailStartScrollIndex;
            this.WholeDetailScrolledOut = wholeDetailScrolledOut;
        }

        public override bool Equals(object obj)
        {
            MasterRowScrollInfo info = obj as MasterRowScrollInfo;
            return ((info != null) && ((info.StartScrollIndex == this.StartScrollIndex) && ((info.DetailStartScrollIndex == this.DetailStartScrollIndex) && (info.WholeDetailScrolledOut == this.WholeDetailScrolledOut))));
        }

        public override int GetHashCode() => 
            base.GetHashCode();

        public int StartScrollIndex { get; private set; }

        public int DetailStartScrollIndex { get; private set; }

        public bool WholeDetailScrolledOut { get; private set; }
    }
}

