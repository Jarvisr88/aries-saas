namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Collections.Generic;

    public class SparklinePointArgumentComparer : Comparer<SparklinePoint>
    {
        private bool isAsceding;

        public SparklinePointArgumentComparer() : this(true)
        {
        }

        public SparklinePointArgumentComparer(bool isAsceding)
        {
            this.isAsceding = isAsceding;
        }

        public override int Compare(SparklinePoint pointInArray, SparklinePoint newРoint)
        {
            if (newРoint == null)
            {
                throw new ArgumentException();
            }
            return ((pointInArray != null) ? (!this.isAsceding ? newРoint.Argument.CompareTo(pointInArray.Argument) : pointInArray.Argument.CompareTo(newРoint.Argument)) : 1);
        }
    }
}

