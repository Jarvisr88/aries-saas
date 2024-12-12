namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Grid.Native;

    public class TabViewDetailDescriptor : MultiDetailDescriptor
    {
        internal override DetailInfoWithContent CreateRowDetailInfo(RowDetailContainer container) => 
            new TabsDetailInfo(this, container);
    }
}

