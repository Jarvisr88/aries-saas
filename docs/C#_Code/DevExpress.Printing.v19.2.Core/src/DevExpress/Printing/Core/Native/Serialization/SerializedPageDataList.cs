namespace DevExpress.Printing.Core.Native.Serialization
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;

    public class SerializedPageDataList
    {
        public const string SerializedPageDataListKey = "SerializedPageDataList";
        private List<SerializedPageData> list;
        private Dictionary<long, int> pageIdToIndex;
        private List<long> pageIds;
        private bool pageIdsModified;
        private int originalPageCount;
        private SizeF maxPageSize;
        private long maxPageId;
        private int maxPageStreamIndex;

        public SerializedPageDataList()
        {
            this.list = new List<SerializedPageData>();
            this.pageIdToIndex = new Dictionary<long, int>();
            this.pageIds = new List<long>();
            this.originalPageCount = -1;
        }

        public SerializedPageDataList(string serializedList)
        {
            this.list = new List<SerializedPageData>();
            this.pageIdToIndex = new Dictionary<long, int>();
            this.pageIds = new List<long>();
            this.originalPageCount = -1;
            this.Deserialize(serializedList);
        }

        public void Add(Page page, int streamIndex)
        {
            this.maxPageSize = SizeF.Empty;
            this.pageIds.Insert(page.Index, page.ID);
            this.pageIdsModified = true;
            if ((page.Document != null) && (page.Document.State == DocumentState.Created))
            {
                this.list.Insert(page.Index, new SerializedPageData(page.PageData, streamIndex, page.ID, page.OriginalIndex, page.OriginalPageCount));
            }
            else
            {
                this.list.Insert(page.Index, new SerializedPageData(page.PageData, streamIndex, page.ID, -1, -1));
            }
            this.maxPageStreamIndex = Math.Max(this.maxPageStreamIndex, streamIndex);
        }

        private void Deserialize(string serializedList)
        {
            SerializedPageData? nullable = null;
            if (!string.IsNullOrEmpty(serializedList))
            {
                char[] separator = new char[] { '|' };
                string[] strArray = serializedList.Split(separator);
                if (strArray.Length >= 3)
                {
                    SerializedPageData data2;
                    int num = int.Parse(strArray[0]);
                    this.list.Clear();
                    this.pageIds.Clear();
                    this.SetOriginalPageCount(int.Parse(strArray[1]));
                    char[] chArray2 = new char[] { ';' };
                    foreach (string str in strArray[2].Split(chArray2))
                    {
                        char[] chArray3 = new char[] { ',' };
                        string[] strArray3 = str.Split(chArray3);
                        SerializedPageData data = new SerializedPageData(strArray3[1]);
                        int num3 = int.Parse(strArray3[0]);
                        if (nullable != null)
                        {
                            for (int j = this.list.Count; j < num3; j++)
                            {
                                this.list.Add(nullable.Value);
                                this.maxPageStreamIndex = Math.Max(this.maxPageStreamIndex, nullable.Value.StreamIndex);
                                this.pageIds.Add(nullable.Value.PageID);
                                data2 = nullable.Value;
                                this.pageIdToIndex[data2.PageID] = j;
                                nullable = new SerializedPageData?(SerializedPageData.Create(nullable.Value));
                            }
                        }
                        nullable = new SerializedPageData?(data);
                    }
                    for (int i = this.list.Count; i < num; i++)
                    {
                        this.list.Add(nullable.Value);
                        this.maxPageStreamIndex = Math.Max(this.maxPageStreamIndex, nullable.Value.StreamIndex);
                        this.pageIds.Add(nullable.Value.PageID);
                        data2 = nullable.Value;
                        this.pageIdToIndex[data2.PageID] = i;
                        nullable = new SerializedPageData?(SerializedPageData.Create(nullable.Value));
                    }
                    this.maxPageSize = SizeF.Empty;
                }
            }
        }

        private SizeF GetMaxPageSize()
        {
            float width = 0f;
            float height = 0f;
            foreach (SerializedPageData data in this.list)
            {
                SizeF pageSize = data.PageData.PageSize;
                if (pageSize.Width > width)
                {
                    width = pageSize.Width;
                }
                if (pageSize.Height > height)
                {
                    height = pageSize.Height;
                }
            }
            return new SizeF(width, height);
        }

        public int GetPageIndex(long pageId)
        {
            int num;
            this.InvalidatePageIds();
            return (!this.pageIdToIndex.TryGetValue(pageId, out num) ? -1 : num);
        }

        private void InvalidatePageIds()
        {
            if (this.pageIdsModified)
            {
                this.pageIdToIndex.Clear();
                this.maxPageId = 0L;
                int num = 0;
                while (true)
                {
                    if (num >= this.pageIds.Count)
                    {
                        this.pageIdsModified = false;
                        break;
                    }
                    this.maxPageId = Math.Max(this.maxPageId, this.pageIds[num]);
                    this.pageIdToIndex[this.pageIds[num]] = num;
                    num++;
                }
            }
        }

        public void RemoveAt(int pageIndex)
        {
            this.maxPageSize = SizeF.Empty;
            this.pageIds.RemoveAt(pageIndex);
            this.list.RemoveAt(pageIndex);
            this.pageIdsModified = true;
        }

        public static SerializedPageDataList Restore(string serializedList) => 
            new SerializedPageDataList(serializedList);

        public string Serialize()
        {
            if (this.Count == 0)
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Count);
            sb.Append('|');
            sb.Append(this.OriginalPageCount);
            sb.Append('|');
            SerializedPageData? nullable = null;
            for (int i = 0; i < this.list.Count; i++)
            {
                SerializedPageData next = this.list[i];
                if ((nullable == null) || !nullable.Value.PriorTo(next))
                {
                    next.Serialize(i, sb);
                    sb.Append(';');
                }
                nullable = new SerializedPageData?(next);
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }

        public void SetOriginalPageCount(int pageCount)
        {
            this.originalPageCount = pageCount;
            for (int i = 0; i < this.list.Count; i++)
            {
                SerializedPageData data = this.list[i];
                this.list[i] = new SerializedPageData(this.list[i].PageData, this.list[i].StreamIndex, data.PageID, i, pageCount);
            }
        }

        public int OriginalPageCount =>
            (this.originalPageCount < 0) ? this.Count : this.originalPageCount;

        public List<long> PageIds =>
            this.pageIds;

        public int MaxPageStreamIndex =>
            this.maxPageStreamIndex;

        public long MaxPageID
        {
            get
            {
                this.InvalidatePageIds();
                return this.maxPageId;
            }
        }

        public SizeF MaxPageSize
        {
            get
            {
                if (this.maxPageSize.IsEmpty)
                {
                    this.maxPageSize = this.GetMaxPageSize();
                }
                return this.maxPageSize;
            }
        }

        public int Count =>
            this.list.Count;

        public SerializedPageData this[int index]
        {
            get => 
                this.list[index];
            set => 
                this.list[index] = value;
        }

        [XtraSerializableProperty]
        public string Serialized
        {
            get => 
                this.Serialize();
            set => 
                this.Deserialize(value);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SerializedPageData : IEquatable<SerializedPageDataList.SerializedPageData>
        {
            private static CultureInfo cultureInfo;
            private MarginsF margins;
            private MarginsF minMargins;
            private SizeF pageSize;
            private PaperKind paperKind;
            private bool landscape;
            private int streamIndex;
            private long pageID;
            private int originalPageIndex;
            private int originalPageCount;
            public static SerializedPageDataList.SerializedPageData Create(SerializedPageDataList.SerializedPageData prev) => 
                new SerializedPageDataList.SerializedPageData(prev);

            public int StreamIndex =>
                this.streamIndex;
            public long PageID =>
                this.pageID;
            public int OriginalPageIndex =>
                this.originalPageIndex;
            public int OriginalPageCount =>
                this.originalPageCount;
            public ReadonlyPageData PageData =>
                new ReadonlyPageData(this.margins, this.minMargins, this.paperKind, this.pageSize, this.landscape);
            private static float parseFloat(string v) => 
                float.Parse(v, cultureInfo);

            private static string serializeFloat(float v) => 
                v.ToString(cultureInfo);

            public SerializedPageData(string data)
            {
                char[] separator = new char[] { ' ' };
                string[] strArray = data.Split(separator);
                this.streamIndex = int.Parse(strArray[0]);
                this.pageID = long.Parse(strArray[1]);
                this.pageSize = new SizeF(parseFloat(strArray[2]), parseFloat(strArray[3]));
                this.landscape = int.Parse(strArray[4]) != 0;
                this.margins = new MarginsF(parseFloat(strArray[5]), parseFloat(strArray[6]), parseFloat(strArray[7]), parseFloat(strArray[8]));
                this.minMargins = new MarginsF(parseFloat(strArray[9]), parseFloat(strArray[10]), parseFloat(strArray[11]), parseFloat(strArray[12]));
                this.paperKind = (PaperKind) int.Parse(strArray[13]);
                this.originalPageIndex = int.Parse(strArray[14]);
                this.originalPageCount = int.Parse(strArray[15]);
            }

            public SerializedPageData(ReadonlyPageData pageData, int streamIndex, long pageID, int originalPageIndex, int originalPageCount)
            {
                this.pageSize = pageData.SizeF;
                this.landscape = pageData.Landscape;
                this.margins = pageData.MarginsF;
                this.minMargins = pageData.MinMarginsF;
                this.streamIndex = streamIndex;
                this.pageID = pageID;
                this.paperKind = pageData.PaperKind;
                this.originalPageIndex = originalPageIndex;
                this.originalPageCount = originalPageCount;
            }

            public SerializedPageData(SerializedPageDataList.SerializedPageData pageData)
            {
                this.pageSize = pageData.pageSize;
                this.landscape = pageData.landscape;
                this.margins = pageData.margins;
                this.minMargins = pageData.minMargins;
                this.paperKind = pageData.paperKind;
                this.streamIndex = pageData.streamIndex + 1;
                this.pageID = pageData.pageID + 1L;
                this.originalPageIndex = (pageData.originalPageIndex >= 0) ? (pageData.originalPageIndex + 1) : -1;
                this.originalPageCount = (pageData.originalPageCount >= 0) ? pageData.originalPageCount : -1;
            }

            public bool PriorTo(SerializedPageDataList.SerializedPageData next) => 
                (this.pageSize == next.pageSize) && (((this.streamIndex + 1) == next.streamIndex) && (((this.pageID + 1L) == next.pageID) && ((this.paperKind == next.paperKind) && ((this.landscape == next.landscape) && (this.margins.Equals(next.margins) && (this.minMargins.Equals(next.minMargins) && ((((this.originalPageIndex + 1) == next.originalPageIndex) || ((this.originalPageIndex == -1) && (next.originalPageIndex == -1))) && ((this.originalPageCount == next.originalPageCount) || ((this.originalPageCount == -1) && (next.originalPageCount == -1))))))))));

            public void Serialize(int index, StringBuilder sb)
            {
                sb.Append(index);
                sb.Append(',');
                sb.Append(this.streamIndex);
                sb.Append(' ');
                sb.Append(this.pageID);
                sb.Append(' ');
                sb.Append(serializeFloat(this.pageSize.Width));
                sb.Append(' ');
                sb.Append(serializeFloat(this.pageSize.Height));
                sb.Append(' ');
                sb.Append(this.landscape ? 1 : 0);
                sb.Append(' ');
                sb.Append(serializeFloat(this.margins.Left));
                sb.Append(' ');
                sb.Append(serializeFloat(this.margins.Right));
                sb.Append(' ');
                sb.Append(serializeFloat(this.margins.Top));
                sb.Append(' ');
                sb.Append(serializeFloat(this.margins.Bottom));
                sb.Append(' ');
                sb.Append(serializeFloat(this.minMargins.Left));
                sb.Append(' ');
                sb.Append(serializeFloat(this.minMargins.Right));
                sb.Append(' ');
                sb.Append(serializeFloat(this.minMargins.Top));
                sb.Append(' ');
                sb.Append(serializeFloat(this.minMargins.Bottom));
                sb.Append(' ');
                sb.Append((int) this.paperKind);
                sb.Append(' ');
                sb.Append(this.originalPageIndex);
                sb.Append(' ');
                sb.Append(this.originalPageCount);
            }

            public bool Equals(SerializedPageDataList.SerializedPageData other) => 
                (this.pageID == other.pageID) && ((this.pageSize == other.pageSize) && ((this.streamIndex == other.streamIndex) && (this.landscape == other.landscape)));

            static SerializedPageData()
            {
                cultureInfo = CultureInfo.InvariantCulture;
            }
        }
    }
}

