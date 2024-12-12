namespace DevExpress.Emf
{
    using System;

    public abstract class DXTilingBrush : DXBrush
    {
        private DXWrapMode wrapMode;
        private DXTransformationMatrix transform;

        protected DXTilingBrush(DXWrapMode wrapMode) : this(wrapMode, new DXTransformationMatrix())
        {
        }

        protected DXTilingBrush(DXWrapMode wrapMode, DXTransformationMatrix transform)
        {
            this.wrapMode = wrapMode;
            this.transform = transform;
        }

        public DXTransformationMatrix Transform
        {
            get => 
                this.transform;
            set => 
                this.transform = value;
        }

        public DXWrapMode WrapMode
        {
            get => 
                this.wrapMode;
            set => 
                this.wrapMode = value;
        }
    }
}

