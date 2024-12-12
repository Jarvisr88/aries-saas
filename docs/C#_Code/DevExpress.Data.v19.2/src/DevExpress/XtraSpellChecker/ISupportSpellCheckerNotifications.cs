namespace DevExpress.XtraSpellChecker
{
    using System;

    public interface ISupportSpellCheckerNotifications
    {
        void DoAfterCheck();
        void DoBeforeCheck();
    }
}

