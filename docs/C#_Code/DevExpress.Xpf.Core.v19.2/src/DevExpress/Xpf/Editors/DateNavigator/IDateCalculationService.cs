namespace DevExpress.Xpf.Editors.DateNavigator
{
    using System;

    public interface IDateCalculationService
    {
        bool IsDisabled(DateTime date);
        bool IsSpecialDate(DateTime date);
        bool IsWorkday(DateTime date);
    }
}

