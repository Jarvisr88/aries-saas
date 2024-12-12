namespace DevExpress.Text.Fonts
{
    using System;
    using System.Collections.Generic;

    public abstract class DXShaper : IDisposable
    {
        protected DXShaper()
        {
        }

        public virtual void Dispose()
        {
        }

        public virtual IList<DXCluster> GetEllipsisRun(float fontSizeInPoints) => 
            this.GetTextRuns("...", false, fontSizeInPoints, false);

        public virtual IList<DXCluster> GetHyphenRun(float fontSizeInPoints) => 
            this.GetTextRuns("-", false, fontSizeInPoints, false);

        public abstract IList<DXCluster> GetTextRuns(string text, bool directionRightToLeft, float fontSizeInPoints, bool useKerning);
    }
}

