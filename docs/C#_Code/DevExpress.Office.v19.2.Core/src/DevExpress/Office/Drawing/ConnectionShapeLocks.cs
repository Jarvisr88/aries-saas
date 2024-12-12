namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class ConnectionShapeLocks : IConnectionLocks, IDrawingLocks, ICommonDrawingLocks, ISupportsNoChangeAspect, ISupportsCopyFrom<ConnectionShapeLocks>, ICloneable<ConnectionShapeLocks>
    {
        private readonly CommonDrawingLocks locks;

        public ConnectionShapeLocks(CommonDrawingLocks innerLocks)
        {
            this.locks = innerLocks;
        }

        public ConnectionShapeLocks Clone()
        {
            ConnectionShapeLocks locks = new ConnectionShapeLocks(new CommonDrawingLocks(this.InnerLocks.DocumentModelPart));
            locks.CopyFrom(this);
            return locks;
        }

        public void CopyFrom(ConnectionShapeLocks value)
        {
            this.InnerLocks.CopyFrom(value.InnerLocks);
        }

        public override bool Equals(object obj)
        {
            ConnectionShapeLocks locks = obj as ConnectionShapeLocks;
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

        public CommonDrawingLocks InnerLocks =>
            this.locks;
    }
}

