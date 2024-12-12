namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export;
    using DevExpress.Printing.ExportHelpers;
    using System;
    using System.Collections.Generic;

    public class TreeListExcelExporter<TCol, TRow> : GridViewExcelExporter<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public TreeListExcelExporter(IGridView<TCol, TRow> viewToExport) : base(viewToExport)
        {
        }

        public TreeListExcelExporter(IGridView<TCol, TRow> viewToExport, IDataAwareExportOptions options) : base(viewToExport, options)
        {
        }

        protected virtual void AddGroupNodeDataRange(XlGroup peekGroup, int startGroup, int endGroup, int groupLevel)
        {
            Group item = new Group {
                Start = startGroup - 1,
                End = endGroup,
                GroupLevel = groupLevel
            };
            peekGroup.DataRanges.Add(item);
        }

        private int CalcMaxNodeLevel(IGroupRow<TRow> parent)
        {
            int maxRowLevel = 0;
            base.ForAllRowsCore(parent, base.View, delegate (TRow x) {
                maxRowLevel = Math.Max(maxRowLevel, x.GetRowLevel());
            });
            return maxRowLevel;
        }

        protected override bool CanExportRow(IRowBase row) => 
            base.ExportInfo.ComplyWithFormatLimits();

        protected override void CloseGroup(int endExcelGroupIndex)
        {
            XlGroup peekGroup = base.ExportInfo.GroupsStack.Peek();
            int startGroup = peekGroup.StartGroup;
            int outlineLevel = peekGroup.Group.OutlineLevel;
            List<Group> dataRanges = peekGroup.DataRanges;
            if (peekGroup.GroupId == -1)
            {
                this.AddGroupNodeDataRange(peekGroup, base.ExportInfo.ExportRowIndex, base.ExportInfo.ExportRowIndex, outlineLevel - 1);
            }
            else
            {
                if (peekGroup.DataRanges.Count == 0)
                {
                    Group item = new Group {
                        Start = startGroup,
                        End = endExcelGroupIndex,
                        GroupLevel = outlineLevel
                    };
                    peekGroup.DataRanges.Add(item);
                }
                this.AddGroupNodeDataRange(peekGroup, startGroup, startGroup, outlineLevel - 1);
                this.AddGroupToList(startGroup, endExcelGroupIndex, outlineLevel);
            }
            base.Context.SetGroupSummary(peekGroup);
            base.ExportInfo.GroupsStack.Pop();
            base.ExportInfo.Exporter.EndGroup();
            if (base.ExportInfo.GroupsStack.Count > 0)
            {
                base.ExportInfo.GroupsStack.Peek().DataRanges.AddRange(dataRanges);
            }
        }

        internal override ExportInfo<TCol, TRow> CreateExportInfo() => 
            new TreeListExportInfo<TCol, TRow>(this);

        protected override void ExportData()
        {
            TRow lastExportedNode = default(TRow);
            int groupId = 1;
            int endExcelGroupIndex = 0;
            int startExcelGroupIndex = base.ExportInfo.ExportRowIndex;
            bool wasDataNode = false;
            int maxNodeLevel = 0;
            this.ForAllRows(base.View, delegate (TRow node) {
                lastExportedNode = node;
                int rowLevel = node.GetRowLevel();
                bool flag = false;
                while (true)
                {
                    if (((TreeListExcelExporter<TCol, TRow>) this).ExportInfo.GroupsStack.Count > 0)
                    {
                        XlGroup group = ((TreeListExcelExporter<TCol, TRow>) this).ExportInfo.GroupsStack.Peek();
                        if (group.Group.OutlineLevel >= (rowLevel + 1))
                        {
                            if (!flag)
                            {
                                endExcelGroupIndex = ((TreeListExcelExporter<TCol, TRow>) this).ExportInfo.Exporter.CurrentRowIndex;
                            }
                            ((TreeListExcelExporter<TCol, TRow>) this).CompleteMergingInGroup(lastExportedNode, endExcelGroupIndex);
                            ((TreeListExcelExporter<TCol, TRow>) this).CloseGroup(endExcelGroupIndex);
                            flag = true;
                            continue;
                        }
                    }
                    if (((TreeListExcelExporter<TCol, TRow>) this).CanExportRow(node))
                    {
                        if (!wasDataNode)
                        {
                            startExcelGroupIndex = ((TreeListExcelExporter<TCol, TRow>) this).ExportInfo.ExportRowIndex;
                        }
                        if (node.IsGroupRow)
                        {
                            ((TreeListExcelExporter<TCol, TRow>) this).SetGroupNodeRange(rowLevel);
                            maxNodeLevel = ((TreeListExcelExporter<TCol, TRow>) this).CalcMaxNodeLevel((IGroupRow<TRow>) node);
                        }
                        ((TreeListExcelExporter<TCol, TRow>) this).Context.AddRow(node);
                    }
                    if (node.IsGroupRow && ((TreeListExcelExporter<TCol, TRow>) this).ExportInfo.AllowGroupingRows)
                    {
                        IGroupRow<TRow> groupRow = node as IGroupRow<TRow>;
                        int num2 = groupId;
                        groupId = num2 + 1;
                        ((TreeListExcelExporter<TCol, TRow>) this).Context.CreateExportDataGroup(rowLevel, node.LogicalPosition, ((TreeListExcelExporter<TCol, TRow>) this).ExportInfo.ExportRowIndex, num2, ((TreeListExcelExporter<TCol, TRow>) this).IsCollapsed(groupRow), ((TreeListExcelExporter<TCol, TRow>) this).GetShowNodeFooter(groupRow));
                        wasDataNode = false;
                    }
                    else
                    {
                        if (rowLevel < maxNodeLevel)
                        {
                            ((TreeListExcelExporter<TCol, TRow>) this).Context.CreateExportDataGroup(rowLevel, node.LogicalPosition, startExcelGroupIndex, -1, false, false);
                        }
                        wasDataNode = true;
                    }
                    ((TreeListExcelExporter<TCol, TRow>) this).ExportInfo.ReportProgress(((TreeListExcelExporter<TCol, TRow>) this).ExportInfo.ExportRowIndex);
                    return;
                }
            });
            if (base.ExportInfo.GroupsStack.Count == 0)
            {
                this.AddGroupToList(startExcelGroupIndex, base.ExportInfo.ExportRowIndex, -1);
            }
            else
            {
                this.CloseAllOpenGroups();
            }
            if (base.ExportInfo.OptionsAllowAddAutoFilter)
            {
                base.Context.AddAutoFilter();
            }
            base.CompleteMerging(lastExportedNode, base.GetLastRowIndex(endExcelGroupIndex));
            base.Context.CreateFooter();
            this.RunExporters();
            base.ExportInfo.CompleteReportProgress();
        }

        private bool GetShowNodeFooter(IGroupRow<TRow> groupNode)
        {
            ITreeListGroupNode<TRow> node = groupNode as ITreeListGroupNode<TRow>;
            return ((node == null) || node.ExportNodeSummary);
        }

        protected virtual void SetGroupNodeRange(int currentNodeLevel)
        {
            this.AddGroupToList(base.ExportInfo.ExportRowIndex, base.ExportInfo.ExportRowIndex, currentNodeLevel);
        }
    }
}

