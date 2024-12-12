namespace DevExpress.Data
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;

    [ToolboxItem(false), ToolboxBitmap(typeof(ResFinder), "Bitmaps256.EntityBindingSource.bmp")]
    public class EntityBindingSource : Component, IListSource
    {
        private IList list;
        private object dataSource;

        protected virtual IList GetDesigntimeList();
        protected virtual IList GetRuntimeList();
        IList IListSource.GetList();

        public virtual object DataSource { get; set; }

        protected virtual bool CacheList { get; }

        bool IListSource.ContainsListCollection { get; }
    }
}

