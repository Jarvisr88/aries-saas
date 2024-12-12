namespace DevExpress.Xpf.Core.HandleDecorator
{
    using System;

    public class PaddingEdges
    {
        private int left;
        private int right;
        private int top;
        private int bottom;

        public int Left
        {
            get => 
                this.left;
            set => 
                this.left = value;
        }

        public int Right
        {
            get => 
                this.right;
            set => 
                this.right = value;
        }

        public int Top
        {
            get => 
                this.top;
            set => 
                this.top = value;
        }

        public int Bottom
        {
            get => 
                this.bottom;
            set => 
                this.bottom = value;
        }

        public bool IsEmpty =>
            (this.left == 0) && ((this.top == 0) && ((this.right == 0) && (this.bottom == 0)));
    }
}

