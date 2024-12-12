namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class GridContainer : ManagerContainerBase
    {
        public GridContainer()
        {
            this.SetDefaultStyleKey(typeof(GridContainer));
        }

        protected override object CreateContent()
        {
            this.Grid = GridAssemblyHelper.Instance.CreateGrid();
            return this.Grid;
        }

        public UIElement Grid { get; private set; }
    }
}

