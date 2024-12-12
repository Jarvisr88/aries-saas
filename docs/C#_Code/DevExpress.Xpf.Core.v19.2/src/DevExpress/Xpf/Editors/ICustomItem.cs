namespace DevExpress.Xpf.Editors
{
    using System;

    public interface ICustomItem
    {
        object EditValue { get; set; }

        object DisplayValue { get; set; }
    }
}

