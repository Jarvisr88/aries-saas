namespace DevExpress.XtraPrinting.Preview
{
    using System;

    public interface IWaitIndicator
    {
        bool Hide(object result);
        object Show(string description);
    }
}

