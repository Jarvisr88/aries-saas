namespace DevExpress.Office.Drawing
{
    using System;
    using System.Collections.Generic;

    public class HitTestInfoCollection : List<HitTestInfo>, IDisposable
    {
        public void Dispose()
        {
            foreach (HitTestInfo info in this)
            {
                info.Dispose();
            }
            base.Clear();
        }
    }
}

