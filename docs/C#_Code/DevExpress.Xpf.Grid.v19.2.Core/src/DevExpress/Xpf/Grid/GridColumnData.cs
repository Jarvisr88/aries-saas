namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class GridColumnData : GridDataBase, ISupportVisibleIndex
    {
        private ColumnBase column;
        private bool updateDataOnEditorContentUpdated;

        public GridColumnData(ColumnsRowDataBase rowData)
        {
            this.RowDataBase = rowData;
        }

        protected virtual void OnColumnChanged(ColumnBase newValue)
        {
            this.StopAnimation();
            this.ColumnCore = newValue;
            if ((base.Editor != null) && (!ReferenceEquals(this.ColumnCore, base.Editor.Column) && this.View.AssignEditorSettings))
            {
                this.updateDataOnEditorContentUpdated = true;
            }
            else
            {
                this.UpdateValue(false);
            }
        }

        protected override void OnContentChanged()
        {
            base.OnContentChanged();
            if (base.editor != null)
            {
                base.editor.UpdateEditableValue();
            }
        }

        protected internal override void OnDataChanged()
        {
            if (((base.Editor == null) || (this.View.ShouldUpdateCellData && ReferenceEquals(base.Editor.ColumnCore, this.Column))) || !this.View.AssignEditorSettings)
            {
                base.OnDataChanged();
            }
            else
            {
                this.updateDataOnEditorContentUpdated = true;
            }
        }

        internal void OnEditorContentUpdated()
        {
            if (this.IsValueDirty)
            {
                this.updateDataOnEditorContentUpdated = false;
                this.OnDataChanged();
            }
        }

        internal virtual void StartAnimation(IList<IList<AnimationTimeline>> animations)
        {
        }

        internal virtual void StopAnimation()
        {
        }

        internal void UpdateCellBackgroundAppearance()
        {
            if (base.editor != null)
            {
                base.editor.GridCellEditorOwner.UpdateCellBackgroundAppearance();
            }
        }

        internal void UpdateCellForegroundAppearance()
        {
            if (base.editor != null)
            {
                base.editor.GridCellEditorOwner.UpdateCellForegroundAppearance();
            }
        }

        protected internal override void UpdateValue(bool forceUpdate = false)
        {
            base.UpdateValue(false);
            this.RaiseContentChanged();
        }

        protected ColumnsRowDataBase RowDataBase { get; private set; }

        [Description("Gets the owner view.")]
        public DataViewBase View =>
            this.RowDataBase.View;

        internal ColumnBase ColumnCore { get; private set; }

        public ColumnBase Column
        {
            get => 
                this.column;
            internal set
            {
                if (!ReferenceEquals(this.column, value))
                {
                    this.column = value;
                    this.OnColumnChanged(this.column);
                    base.RaisePropertyChanged("Column");
                }
            }
        }

        protected bool IsValueDirty =>
            this.updateDataOnEditorContentUpdated;

        [Description("Gets the column's position among visible columns.")]
        public int VisibleIndex { get; internal set; }

        internal int ActualVisibleIndex { get; set; }
    }
}

