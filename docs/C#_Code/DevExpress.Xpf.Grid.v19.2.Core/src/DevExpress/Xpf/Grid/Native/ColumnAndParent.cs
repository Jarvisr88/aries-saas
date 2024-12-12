namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using System;

    public class ColumnAndParent
    {
        public readonly IModelItem ColumnModel;
        public readonly IModelItem OwnerModel;

        public ColumnAndParent(IModelItem columnModel, IModelItem onwerModel)
        {
            this.ColumnModel = columnModel;
            this.OwnerModel = onwerModel;
        }
    }
}

