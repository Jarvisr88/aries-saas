namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class JPXArea
    {
        private int x0;
        private int y0;
        private int x1;
        private int y1;

        protected JPXArea()
        {
        }

        public int X0
        {
            get => 
                this.x0;
            protected set => 
                this.x0 = value;
        }

        public int Y0
        {
            get => 
                this.y0;
            protected set => 
                this.y0 = value;
        }

        public int X1
        {
            get => 
                this.x1;
            protected set => 
                this.x1 = value;
        }

        public int Y1
        {
            get => 
                this.y1;
            protected set => 
                this.y1 = value;
        }

        public int Width =>
            this.x1 - this.x0;

        public int Height =>
            this.y1 - this.y0;
    }
}

