namespace DevExpress.Data.Helpers
{
    using System;

    public interface IIndexRenumber
    {
        int GetCount();
        int GetValue(int pos);
        void SetValue(int pos, int val);
    }
}

