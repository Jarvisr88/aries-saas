namespace DevExpress.XtraPrinting.Export
{
    using System;

    public class CoordInfo : IComparable<CoordInfo>
    {
        public float coord;
        public int index;
        public bool isStart;

        public int CompareTo(CoordInfo other) => 
            this.coord.CompareTo(other.coord);
    }
}

