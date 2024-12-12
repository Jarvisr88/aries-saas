namespace DevExpress.Xpf.Grid.Automation
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;

    public abstract class DataControlAutomationPeer : DataControlAutomationPeerBase, IGridProvider, IRawElementProviderSimple
    {
        public DataControlAutomationPeer(DataControlBase dataControl) : base(dataControl, dataControl)
        {
        }

        protected internal void ClearLogicalPeerCache()
        {
            LogicalPeerCache logicalPeerCache = base.DataControl.logicalPeerCache;
            if (logicalPeerCache != null)
            {
                logicalPeerCache.Clear();
            }
        }

        protected internal abstract AutomationPeer CreateRowPeer(int rowHandle);
        protected internal abstract AutomationPeer GetCellPeer(int rowHandle, ColumnBase column, bool force = false);
        protected internal abstract AutomationPeer GetGroupFooterAutomationPeer(int rowHandle);
        protected internal AutomationPeer GetRowPeer(int rowHandle)
        {
            AutomationPeer peer = null;
            if (!base.DataControl.LogicalPeerCache.DataRows.TryGetValue(rowHandle, out peer))
            {
                peer = this.CreateRowPeer(rowHandle);
                base.DataControl.LogicalPeerCache.DataRows[rowHandle] = peer;
            }
            return peer;
        }

        protected internal List<AutomationPeer> GetRowPeers()
        {
            List<AutomationPeer> list = new List<AutomationPeer>();
            for (int i = base.DataControl.DataView.PageVisibleTopRowIndex; i < (base.DataControl.DataView.PageVisibleTopRowIndex + base.DataControl.DataView.PageVisibleDataRowCount); i++)
            {
                object visibleIndexByScrollIndex = base.DataControl.DataProviderBase.GetVisibleIndexByScrollIndex(i);
                if (visibleIndexByScrollIndex is GroupSummaryRowKey)
                {
                    list.Add(this.GetGroupFooterAutomationPeer(((GroupSummaryRowKey) visibleIndexByScrollIndex).RowHandle.Value));
                }
                else
                {
                    list.Add(this.GetRowPeer(base.DataControl.GetRowHandleByVisibleIndexCore((int) visibleIndexByScrollIndex)));
                }
            }
            return list;
        }

        protected internal virtual void ResetDataPanelChildrenForce()
        {
        }

        protected internal abstract void ResetDataPanelPeer();
        protected internal virtual void ResetDataPanelPeerCache()
        {
        }

        protected internal abstract void ResetHeadersChildrenCache();
        protected internal abstract void ResetPeers();
        IRawElementProviderSimple IGridProvider.GetItem(int row, int column)
        {
            if (column >= base.DataControl.viewCore.VisibleColumnsCore.Count)
            {
                return null;
            }
            int rowHandleByVisibleIndexCore = base.DataControl.GetRowHandleByVisibleIndexCore(row);
            base.DataControl.DataView.ScrollIntoView(rowHandleByVisibleIndexCore);
            base.DataControl.UpdateLayout();
            this.ResetDataPanelChildrenForce();
            AutomationPeer peer = this.GetCellPeer(rowHandleByVisibleIndexCore, base.DataControl.viewCore.VisibleColumnsCore[column], true);
            return base.ProviderFromPeer(peer);
        }

        object IRawElementProviderSimple.GetPatternProvider(int patternId) => 
            (patternId != GridPatternIdentifiers.ColumnCountProperty.Id) ? ((patternId != GridPatternIdentifiers.RowCountProperty.Id) ? null : ((object) ((IGridProvider) this).RowCount)) : ((IGridProvider) this).ColumnCount;

        object IRawElementProviderSimple.GetPropertyValue(int propertyId) => 
            (propertyId != AutomationElementIdentifiers.NameProperty.Id) ? ((propertyId != AutomationElementIdentifiers.ClassNameProperty.Id) ? null : this.ControlName) : "";

        int IGridProvider.ColumnCount =>
            base.DataControl.ColumnsCore.Count;

        int IGridProvider.RowCount =>
            base.DataControl.VisibleRowCount;

        IRawElementProviderSimple IRawElementProviderSimple.HostRawElementProvider =>
            null;

        ProviderOptions IRawElementProviderSimple.ProviderOptions =>
            ProviderOptions.ClientSideProvider;

        protected abstract string ControlName { get; }
    }
}

