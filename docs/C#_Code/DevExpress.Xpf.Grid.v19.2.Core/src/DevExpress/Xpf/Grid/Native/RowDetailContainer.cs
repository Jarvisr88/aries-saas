namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Runtime.CompilerServices;

    public sealed class RowDetailContainer
    {
        public RowDetailContainer(DataControlBase masterDataControl, object row)
        {
            this.MasterDataControl = masterDataControl;
            this.Row = row;
        }

        public void Clear()
        {
            Action<DetailInfoWithContent> action = <>c.<>9__17_0;
            if (<>c.<>9__17_0 == null)
            {
                Action<DetailInfoWithContent> local1 = <>c.<>9__17_0;
                action = <>c.<>9__17_0 = x => x.Clear();
            }
            this.EnumerateInfoWithContent(action);
        }

        public void Detach()
        {
            Action<DetailInfoWithContent> action = <>c.<>9__18_0;
            if (<>c.<>9__18_0 == null)
            {
                Action<DetailInfoWithContent> local1 = <>c.<>9__18_0;
                action = <>c.<>9__18_0 = x => x.Detach();
            }
            this.EnumerateInfoWithContent(action);
        }

        private void EnumerateInfoWithContent(Action<DetailInfoWithContent> action)
        {
            foreach (RowDetailInfoBase base2 in this.RootDetailInfo.GetRowDetailInfoEnumerator())
            {
                DetailInfoWithContent content = base2 as DetailInfoWithContent;
                if (content != null)
                {
                    action(content);
                }
            }
        }

        public void RemoveFromDetailClones()
        {
            Action<DetailInfoWithContent> action = <>c.<>9__19_0;
            if (<>c.<>9__19_0 == null)
            {
                Action<DetailInfoWithContent> local1 = <>c.<>9__19_0;
                action = <>c.<>9__19_0 = x => x.RemoveFromDetailClones();
            }
            this.EnumerateInfoWithContent(action);
        }

        public DetailInfoWithContent RootDetailInfo { get; set; }

        public DataControlBase MasterDataControl { get; private set; }

        public int MasterListIndex { get; internal set; }

        public object Row { get; internal set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RowDetailContainer.<>c <>9 = new RowDetailContainer.<>c();
            public static Action<DetailInfoWithContent> <>9__17_0;
            public static Action<DetailInfoWithContent> <>9__18_0;
            public static Action<DetailInfoWithContent> <>9__19_0;

            internal void <Clear>b__17_0(DetailInfoWithContent x)
            {
                x.Clear();
            }

            internal void <Detach>b__18_0(DetailInfoWithContent x)
            {
                x.Detach();
            }

            internal void <RemoveFromDetailClones>b__19_0(DetailInfoWithContent x)
            {
                x.RemoveFromDetailClones();
            }
        }
    }
}

