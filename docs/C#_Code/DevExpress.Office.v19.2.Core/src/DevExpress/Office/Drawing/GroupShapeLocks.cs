namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class GroupShapeLocks : IGroupLocks, ICommonDrawingLocks, ISupportsNoChangeAspect, ISupportsCopyFrom<GroupShapeLocks>, ICloneable<GroupShapeLocks>
    {
        private readonly CommonDrawingLocks locks;

        public GroupShapeLocks(CommonDrawingLocks innerLocks)
        {
            this.locks = innerLocks;
        }

        public GroupShapeLocks Clone()
        {
            GroupShapeLocks locks = new GroupShapeLocks(new CommonDrawingLocks(this.InnerLocks.DocumentModelPart));
            locks.CopyFrom(this);
            return locks;
        }

        public void CopyFrom(GroupShapeLocks value)
        {
            this.InnerLocks.CopyFrom(value.InnerLocks);
        }

        public override bool Equals(object obj)
        {
            GroupShapeLocks locks = obj as GroupShapeLocks;
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

        public bool NoUngroup
        {
            get => 
                this.locks.NoUngroup;
            set => 
                this.locks.NoUngroup = value;
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

