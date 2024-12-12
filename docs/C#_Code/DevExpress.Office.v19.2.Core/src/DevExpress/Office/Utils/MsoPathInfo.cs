namespace DevExpress.Office.Utils
{
    using System;
    using System.Runtime.CompilerServices;

    public class MsoPathInfo
    {
        public MsoPathInfo(MsoPathEscape escapeType, int segments)
        {
            this.PathType = MsoPathType.Escape;
            this.PathEscape = escapeType;
            this.Segments = segments;
        }

        public MsoPathInfo(MsoPathType pathType, int segments)
        {
            this.PathType = pathType;
            this.Segments = segments;
        }

        public MsoPathType PathType { get; set; }

        public MsoPathEscape PathEscape { get; set; }

        public int Segments { get; set; }
    }
}

