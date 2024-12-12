namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class PictureLocks : IPictureLocks, IDrawingLocks, ICommonDrawingLocks, ISupportsNoChangeAspect, ISupportsCopyFrom<PictureLocks>, ICloneable<PictureLocks>
    {
        private readonly CommonDrawingLocks locks;

        public PictureLocks(CommonDrawingLocks innerLocks)
        {
            this.locks = innerLocks;
        }

        public PictureLocks Clone()
        {
            PictureLocks locks = new PictureLocks(new CommonDrawingLocks(this.InnerLocks.DocumentModelPart));
            locks.CopyFrom(this);
            return locks;
        }

        public void CopyFrom(PictureLocks value)
        {
            this.InnerLocks.CopyFrom(value.InnerLocks);
        }

        public override bool Equals(object obj)
        {
            PictureLocks locks = obj as PictureLocks;
            return ((locks != null) ? this.InnerLocks.Equals(locks.InnerLocks) : false);
        }

        public override int GetHashCode() => 
            this.InnerLocks.GetHashCode();

        public bool NoGroup
        {
            get => 
                this.locks.NoGroup;
            set => 
                this.locks.NoGroup = value;
        }

        public bool NoSelect
        {
            get => 
                this.locks.NoSelect;
            set => 
                this.locks.NoSelect = value;
        }

        public bool NoRotate
        {
            get => 
                this.locks.NoRotate;
            set => 
                this.locks.NoRotate = value;
        }

        public bool NoChangeAspect
        {
            get => 
                this.locks.NoChangeAspect;
            set => 
                this.locks.NoChangeAspect = value;
        }

        public bool NoMove
        {
            get => 
                this.locks.NoMove;
            set => 
                this.locks.NoMove = value;
        }

        public bool NoResize
        {
            get => 
                this.locks.NoResize;
            set => 
                this.locks.NoResize = value;
        }

        public bool NoEditPoints
        {
            get => 
                this.locks.NoEditPoints;
            set => 
                this.locks.NoEditPoints = value;
        }

        public bool NoAdjustHandles
        {
            get => 
                this.locks.NoAdjustHandles;
            set => 
                this.locks.NoAdjustHandles = value;
        }

        public bool NoChangeArrowheads
        {
            get => 
                this.locks.NoChangeArrowheads;
            set => 
                this.locks.NoChangeArrowheads = value;
        }

        public bool NoChangeShapeType
        {
            get => 
                this.locks.NoChangeShapeType;
            set => 
                this.locks.NoChangeShapeType = value;
        }

        public bool IsEmpty =>
            this.locks.IsEmpty();

        public bool NoCrop
        {
            get => 
                this.locks.NoCrop;
            set => 
                this.locks.NoCrop = value;
        }

        public CommonDrawingLocks InnerLocks =>
            this.locks;
    }
}

