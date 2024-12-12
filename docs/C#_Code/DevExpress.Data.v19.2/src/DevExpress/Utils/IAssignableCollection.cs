namespace DevExpress.Utils
{
    using System;

    public interface IAssignableCollection
    {
        void Assign(IAssignableCollection source);
        void Clear();
    }
}

