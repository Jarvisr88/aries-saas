namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ClipboardBandLayoutInfo
    {
        public ClipboardBandLayoutInfo(int bandPanelHeight, int columnPanelHeight)
        {
            this.BandPanelInfo = new List<ClipboardBandCellInfo>[bandPanelHeight];
            this.ColumnPanelInfo = new List<ClipboardBandCellInfo>[columnPanelHeight];
        }

        public void AddBandPanelCell(int row, ClipboardBandCellInfo cell)
        {
            this.BandPanelInfo[row] ??= new List<ClipboardBandCellInfo>();
            this.BandPanelInfo[row].Add(cell);
        }

        public void AddColumnPanelCell(int row, ClipboardBandCellInfo cell)
        {
            this.ColumnPanelInfo[row] ??= new List<ClipboardBandCellInfo>();
            this.ColumnPanelInfo[row].Add(cell);
        }

        public int GetBandPanelRowWidth(int row = 0, bool includeCurrentBand = false)
        {
            int num = 0;
            if (includeCurrentBand)
            {
                if (this.BandPanelInfo[0] != null)
                {
                    for (int j = 0; j < this.BandPanelInfo[0].Count; j++)
                    {
                        num += (this.BandPanelInfo[0][j].SpaceBefore + this.BandPanelInfo[0][j].Width) + this.BandPanelInfo[0][j].SpaceAfter;
                    }
                }
                while ((this.BandPanelInfo[row] == null) && (row > 1))
                {
                    row--;
                }
            }
            if ((this.BandPanelInfo.Length == 0) || (this.BandPanelInfo[row] == null))
            {
                return (includeCurrentBand ? num : 0);
            }
            int num2 = 0;
            for (int i = 0; i < this.BandPanelInfo[row].Count; i++)
            {
                num2 += (this.BandPanelInfo[row][i].Width + this.BandPanelInfo[row][i].SpaceAfter) + this.BandPanelInfo[row][i].SpaceBefore;
            }
            return ((num > num2) ? num : num2);
        }

        public int GetColumnPanelRowWidth(int row = 0, int bandIndex = -1)
        {
            if ((this.ColumnPanelInfo.Length == 0) || (this.ColumnPanelInfo[row] == null))
            {
                return 0;
            }
            int num = 0;
            if (bandIndex == -1)
            {
                for (int i = 0; i < this.ColumnPanelInfo[row].Count; i++)
                {
                    num += (this.ColumnPanelInfo[row][i].Width + this.ColumnPanelInfo[row][i].SpaceAfter) + this.ColumnPanelInfo[row][i].SpaceBefore;
                }
            }
            else
            {
                for (int i = 0; (i < this.ColumnPanelInfo[row].Count) && (this.ColumnPanelInfo[row][i].BandIndex <= bandIndex); i++)
                {
                    num += (this.ColumnPanelInfo[row][i].Width + this.ColumnPanelInfo[row][i].SpaceAfter) + this.ColumnPanelInfo[row][i].SpaceBefore;
                }
            }
            return num;
        }

        public void NormalizeSpaces()
        {
            this.NormalizeSpacesInternal(this.BandPanelInfo, false);
            this.NormalizeSpacesInternal(this.ColumnPanelInfo, true);
        }

        private void NormalizeSpacesInternal(List<ClipboardBandCellInfo>[] panel, bool isColumnPanel = false)
        {
            int num = 0;
            int num2 = 0;
            while (num < panel.Length)
            {
                num = 0;
                int index = 0;
                while (true)
                {
                    if (index >= panel.Length)
                    {
                        num2++;
                        break;
                    }
                    if ((panel[index] == null) || (num2 >= panel[index].Count))
                    {
                        num++;
                    }
                    else if (panel[index][num2].Height > 1)
                    {
                        if (num2 == 0)
                        {
                            for (int i = 1; i < panel[index][num2].Height; i++)
                            {
                                if (panel[index + i] == null)
                                {
                                    panel[index + i] = new List<ClipboardBandCellInfo>();
                                    ClipboardBandCellInfo item = new ClipboardBandCellInfo(0, 0, string.Empty, string.Empty, new XlCellFormatting(), false) {
                                        SpaceBefore = panel[index][num2].SpaceBefore,
                                        Width = panel[index][num2].Width
                                    };
                                    panel[index + i].Add(item);
                                }
                            }
                        }
                        if (num2 == (panel[index].Count - 1))
                        {
                            int num5 = !isColumnPanel ? this.GetBandPanelRowWidth(index, false) : this.GetColumnPanelRowWidth(index, panel[index][num2].BandIndex);
                            for (int i = 1; i < panel[index][num2].Height; i++)
                            {
                                if (panel[index + i] != null)
                                {
                                    int num7 = !isColumnPanel ? this.GetBandPanelRowWidth(index + i, false) : this.GetColumnPanelRowWidth(index + i, panel[index][num2].BandIndex);
                                    ClipboardBandCellInfo local1 = panel[index + i][panel[index + i].Count - 1];
                                    local1.SpaceAfter += num5 - num7;
                                }
                                else
                                {
                                    panel[index + i] = new List<ClipboardBandCellInfo>();
                                    ClipboardBandCellInfo item = new ClipboardBandCellInfo(0, 0, string.Empty, string.Empty, new XlCellFormatting(), false) {
                                        SpaceBefore = num5 - panel[index][num2].Width,
                                        Width = panel[index][num2].Width
                                    };
                                    panel[index + i].Add(item);
                                }
                            }
                        }
                    }
                    index++;
                }
            }
        }

        public ClipboardBandLayoutInfo Without(bool isWithoutBand, bool IsWithoutHeader)
        {
            this.BandPanelInfo = isWithoutBand ? new List<ClipboardBandCellInfo>[0] : this.BandPanelInfo;
            this.ColumnPanelInfo = IsWithoutHeader ? new List<ClipboardBandCellInfo>[0] : this.ColumnPanelInfo;
            return this;
        }

        public List<ClipboardBandCellInfo>[] BandPanelInfo { get; private set; }

        public List<ClipboardBandCellInfo>[] ColumnPanelInfo { get; private set; }

        public List<ClipboardBandCellInfo>[] HeaderPanelInfo
        {
            get
            {
                List<ClipboardBandCellInfo>[] listArray = new List<ClipboardBandCellInfo>[this.BandPanelInfo.Length + this.ColumnPanelInfo.Length];
                int num = 0;
                foreach (List<ClipboardBandCellInfo> list in this.BandPanelInfo)
                {
                    listArray[num++] = list;
                }
                foreach (List<ClipboardBandCellInfo> list2 in this.ColumnPanelInfo)
                {
                    listArray[num++] = list2;
                }
                return listArray;
            }
        }
    }
}

